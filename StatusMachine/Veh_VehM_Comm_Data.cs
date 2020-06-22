using System;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Veh_HandShakeData;
using com.mirle.iibg3k0.ttc.Common;
using com.mirle.iibg3k0.ttc.Common.TCPIP;
using TcpIpClientSample;
using MirleOHT.類別.DCPS;
using OHTM.NLog_USE;
using System.Xml.Linq;
using OHTM.ReadCsv;
using System.Globalization;
using System.IO;
using System.Collections.Generic;

namespace OHTM.StatusMachine
{
    public class Veh_VehM_Comm_Data
    {
        MotionInfo_Vehicle_Inter_Comm_ReportData_134[] motionInfoInterCommReport_134 = null;
        MotionInfo_Vehicle_Inter_Comm_ReportData[] motionInfoInterCommReport = null;
        DDS.SampleInfo[] sampleInfo_RptData = null;
        DDS.SampleInfo[] sampleInfo_RptData_134 = null;
        //public event EventHandler eventCmd31_CycleRun;
        public event EventHandler<ReportMsgEventArg> eventMsgFromVehM;
        Stopwatch sw = new Stopwatch();
        long time = 0, timeOut = 10000;
        bool blTimeOut = false;

        public TcpIpAgent clientAgent { get; private set; }
        int[] needCheckSeqNoIfPacketID = new int[]
        {
            WrapperMessage.TransReqFieldNumber,
        };
        object objectCheckNewCmdIDProcess = new object();
        object sendRecv_LockObj = new object();
        public com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode snedRecv<TSource>(WrapperMessage wrapper, out TSource stRecv, out string rtnMsg)
        {
            lock (sendRecv_LockObj)
            {
                int resendtimes = 0;
                TrxTcpIp.ReturnCode returncode;
                bool retrySendRecv = true;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In send back :Ready for sendRecv_Google");
                stRecv = default(TSource);
                rtnMsg = "";
                returncode = TrxTcpIp.ReturnCode.SendDataFail;
                while (retrySendRecv == true)
                {
                    try
                    {
                        returncode = clientAgent.TrxTcpIp.sendRecv_Google(wrapper, out stRecv, out rtnMsg);
                        if (returncode != TrxTcpIp.ReturnCode.Normal)
                        {
                            resendtimes = resendtimes + 1;
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ~~~~~~com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode snedRecv~~~~~");
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : Times = " + resendtimes);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~END~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                            Thread.Sleep(100);
                        }
                        else if (returncode == TrxTcpIp.ReturnCode.Normal)
                        {
                            retrySendRecv = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionErrorLog(ex);
                        //retrySendRecv = false;
                        //Veh_VehM_Global.check_recieve_36 = true;
                        //Veh_VehM_Global.vehBlockPassReply = (int)Status.NG;
                        //Veh_VehM_Global.vehVehMomm.sned_Str194(9999, ErrorStatus.ErrSet);
                    }

                }
                return returncode;
            }
        }

        private static void ExceptionErrorLog(Exception ex)
        {
            string err = ex.Message + Environment.NewLine + ex.StackTrace;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
        }

        public bool TcpIpTimeOut { get { return blTimeOut; } }                           // Roy+171128

        public Veh_VehM_Comm_Data()
        {
            try
            {
                if (!Veh_VehM_Global.OffLineTest)
                {
                    CreatTcpIpClientAgent();                                                 //Create the tcp/ip client agent.
                    registeredEvent();                                                       //Add the connect/disconnect situation and message.
                    clientAgent.injectDecoder
                        (new com.mirle.iibg3k0.ttc.Common.TCPIP.DecodRawData.DecodeRawData_Google(unPackWrapperMsg));                    //用來注入解開封包的Function
                                                                                                                                         //clientAgent.setNeedToBeConfirmReceivePacketID(needCheckSeqNoIfPacketID); //設定哪些的"ID"，是需要檢查Seq No
                    Task.Run(() => clientAgent.start());
                }
                blTimeOut = false;
            }
            catch (Exception ex)
            {
                ExceptionErrorLog(ex);
            }
        }

        public Veh_VehM_Comm_Data(long time)
        {
            this.timeOut = time;
            this.blTimeOut = false;
        }

        public void str31_Receive(object sender, TcpIpEventArgs e)
        {
            try
            {
                checkProcessFlag();
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str31_Receive : ThreadID = {0}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", Thread.CurrentThread.ManagedThreadId);
                if (!clientAgent.IsConnection)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} Client not connect!!!!!!!!!!!!!!!!!!", Thread.CurrentThread.ManagedThreadId);

                    return;
                }
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} Client is OK ~~~~~~~~~~~~~~~~~~~", Thread.CurrentThread.ManagedThreadId);

                // In this region, these should be copy & paste on every receive;
                ID_31_TRANS_REQUEST recive_str = (ID_31_TRANS_REQUEST)e.objPacket;
                ID_131_TRANS_RESPONSE send_str = null;
                ActiveType actionType = recive_str.ActType;
                Veh_VehM_Global.NowActiveType = actionType;
                WrapperMessage wrapper = new WrapperMessage();
                Boolean resp_cmp;
                ///<summary> This part is checking whether this vehicle has already got a command
                ///Yes => reply 1 for can't get a new command 
                ///</summary>
                ///
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} start check cmd status", Thread.CurrentThread.ManagedThreadId);
                /*
                 * Check Mode of the vehicle.
                 */
                string[] passSections = recive_str.GuideSections.ToArray();
                string[] cycRunSecs = recive_str.CycleSections.ToArray();
                string[] passSegment = recive_str.GuideSegments.ToArray();
                Veh_VehM_Global.GuideSections = passSections;
                if (Veh_VehM_Global.GuideSections != null && Veh_VehM_Global.GuideSections.Length != 0)
                {
                    // A20.06.22 take off for the 
                    //for (int i = 0; i < Veh_VehM_Global.GuideSections.Length; i++)
                    //{
                    //    if (Veh_VehM_Global.GuideSections[i].Length == 5)
                    //    {
                    //        if (!Veh_VehM_Global.GuideSections[i].StartsWith("+") && !Veh_VehM_Global.GuideSections[i].StartsWith("-"))
                    //        {
                    //            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : Old from 300 section {1} = {0}.", Veh_VehM_Global.GuideSections[i].ToString(), i);
                    //            Veh_VehM_Global.GuideSections[i] = Veh_VehM_Global.GuideSections[i].Substring(1);
                    //            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : New from 300 section {1} = {0}.", Veh_VehM_Global.GuideSections[i].ToString(), i);
                    //        }
                    //    }
                    //}
                }
                if (Veh_VehM_Global.vehModeStatus_fromVehC == VehControlMode.OnlineRemote)
                {
                    if (Veh_VehM_Global_Property.already_have_command_Check == true && recive_str.ActType != ActiveType.Override)
                    {
                        //Veh_VehM_Global_Property.already_have_command_Check = false;
                        send_str = new ID_131_TRANS_RESPONSE
                        {
                            CmdID = recive_str.CmdID,
                            ActType = Veh_VehM_Global.NowActiveType,
                            ReplyCode = 1,
                            NgReason = "Already have a command"
                        };

                        wrapper = new WrapperMessage
                        {
                            ID = WrapperMessage.TransRespFieldNumber,
                            SeqNum = e.iSeqNum,
                            TransResp = send_str
                        };

                        resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                        Veh_VehM_Global.CheckNewCmdIDProcess = false;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                        //vehicle.currentExcuteCMD_ID = cmd_id;
                        //Console.WriteLine("Don't Need New Command");
                        return;
                    }
                    else if (Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist && (recive_str.ActType == ActiveType.Load || recive_str.ActType == ActiveType.Loadunload))
                    {
                        Veh_VehM_Global_Property.already_have_command_Check = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str31_Receive :  LOAD??   XXX???!@#$%^&");
                        send_str = new ID_131_TRANS_RESPONSE
                        {
                            CmdID = recive_str.CmdID,
                            ActType = Veh_VehM_Global.NowActiveType,
                            ReplyCode = 1,
                            NgReason = "Already have a CST. Don't give a load cmd now."
                        };

                        wrapper = new WrapperMessage
                        {
                            ID = WrapperMessage.TransRespFieldNumber,
                            SeqNum = e.iSeqNum,
                            TransResp = send_str
                        };

                        resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                        Veh_VehM_Global.CheckNewCmdIDProcess = false;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                        //vehicle.currentExcuteCMD_ID = cmd_id;
                        //Console.WriteLine("Don't Need New Command");
                        return;
                    }
                    else if (Veh_VehM_Global.hasCst == VhLoadCSTStatus.NotExist && recive_str.ActType == ActiveType.Unload)
                    {
                        //Veh_VehM_Global_Property.already_have_command_Check = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str31_Receive :  UNLOAD?? XXX???!@#$%^&");
                        send_str = new ID_131_TRANS_RESPONSE
                        {
                            CmdID = recive_str.CmdID,
                            ActType = Veh_VehM_Global.NowActiveType,
                            ReplyCode = 1,
                            NgReason = "Don't have a CST. Don't give a unload cmd now."
                        };

                        wrapper = new WrapperMessage
                        {
                            ID = WrapperMessage.TransRespFieldNumber,
                            SeqNum = e.iSeqNum,
                            TransResp = send_str
                        };

                        resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                        Veh_VehM_Global.CheckNewCmdIDProcess = false;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                        //vehicle.currentExcuteCMD_ID = cmd_id;
                        //Console.WriteLine("Don't Need New Command");
                        return;
                    }
                }
                else
                {
                    //Veh_VehM_Global_Property.already_have_command_Check = false;
                    send_str = new ID_131_TRANS_RESPONSE
                    {
                        CmdID = recive_str.CmdID,
                        ActType = Veh_VehM_Global.NowActiveType,
                        ReplyCode = 1,
                        NgReason = "Vehicle mode is not Auto."
                    };

                    wrapper = new WrapperMessage
                    {
                        ID = WrapperMessage.TransRespFieldNumber,
                        SeqNum = e.iSeqNum,
                        TransResp = send_str
                    };

                    resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                    Veh_VehM_Global.CheckNewCmdIDProcess = false;

                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                    //vehicle.currentExcuteCMD_ID = cmd_id;
                    //Console.WriteLine("Don't Need New Command");
                    return;
                }
                /****
                 ****
                 ****/
                if (actionType == ActiveType.Home)
                {
                    //Veh_VehM_Global_Property.already_have_command_Check = false;
                    send_str = new ID_131_TRANS_RESPONSE
                    {
                        CmdID = recive_str.CmdID,
                        ActType = Veh_VehM_Global.NowActiveType,
                        ReplyCode = 0,
                        NgReason = ""
                    };

                    wrapper = new WrapperMessage
                    {
                        ID = WrapperMessage.TransRespFieldNumber,
                        SeqNum = e.iSeqNum,
                        TransResp = send_str
                    };

                    resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                    //vehicle.currentExcuteCMD_ID = cmd_id;
                    //Console.WriteLine("Don't Need New Command");
                    return;
                }
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} cmd status is ok", Thread.CurrentThread.ManagedThreadId);
                /*
                 * ForOHT Check the Cmd start address
                 */
                string cmdStartAddress = "";


                //
                string cmd_id = recive_str.CmdID;
                //ActiveType actionType = recive_str.ActType;

                string fromAdr = recive_str.LoadAdr;
                string toAdr = recive_str.ToAdr;
                string CST_ID = recive_str.CSTID;


                if ((passSections != null) && (passSections.Count() != 0))
                {
                    bool temp_check = CheckSections.check_the_start_Address_ForOHT(passSections[0]);
                    if (temp_check == false)
                    {
                        Veh_VehM_Global_Property.already_have_command_Check = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str31_Receive :  Move?? Check the start address.");
                        send_str = new ID_131_TRANS_RESPONSE
                        {
                            CmdID = recive_str.CmdID,
                            ActType = Veh_VehM_Global.NowActiveType,
                            ReplyCode = 1,
                            NgReason = "Wrong start address, please check again."
                        };

                        wrapper = new WrapperMessage
                        {
                            ID = WrapperMessage.TransRespFieldNumber,
                            SeqNum = e.iSeqNum,
                            TransResp = send_str
                        };

                        resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                        Veh_VehM_Global.CheckNewCmdIDProcess = false;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                        //vehicle.currentExcuteCMD_ID = cmd_id;
                        //Console.WriteLine("Don't Need New Command");
                        return;
                    }
                }
                else if ((passSections != null) && (passSections.Count() == 0))
                {
                    if (recive_str.ActType == ActiveType.Move)
                    {
                        Veh_VehM_Global_Property.already_have_command_Check = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str31_Receive :  Move?? Check the start address.");
                        send_str = new ID_131_TRANS_RESPONSE
                        {
                            CmdID = recive_str.CmdID,
                            ActType = Veh_VehM_Global.NowActiveType,
                            ReplyCode = 1,
                            NgReason = "Wrong Move section, please check again."
                        };

                        wrapper = new WrapperMessage
                        {
                            ID = WrapperMessage.TransRespFieldNumber,
                            SeqNum = e.iSeqNum,
                            TransResp = send_str
                        };

                        resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                        Veh_VehM_Global.CheckNewCmdIDProcess = false;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                        //vehicle.currentExcuteCMD_ID = cmd_id;
                        //Console.WriteLine("Don't Need New Command");
                        return;
                    }
                }
                else if ((passSections == null) && (passSections.Count() == 0))
                {
                    if (recive_str.ActType == ActiveType.Move)
                    {
                        Veh_VehM_Global_Property.already_have_command_Check = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str31_Receive :  Move?? Check the start address.");
                        send_str = new ID_131_TRANS_RESPONSE
                        {
                            CmdID = recive_str.CmdID,
                            ActType = Veh_VehM_Global.NowActiveType,
                            ReplyCode = 1,
                            NgReason = "Wrong move Sections, please check again."
                        };

                        wrapper = new WrapperMessage
                        {
                            ID = WrapperMessage.TransRespFieldNumber,
                            SeqNum = e.iSeqNum,
                            TransResp = send_str
                        };

                        resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                        Veh_VehM_Global.CheckNewCmdIDProcess = false;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                        //vehicle.currentExcuteCMD_ID = cmd_id;
                        //Console.WriteLine("Don't Need New Command");
                        return;
                    }
                }
                //string[] cycRunSecs = recive_str.CycleSections.ToArray();
                //string[] passSegment = recive_str.GuideSegments.ToArray();
                if (CST_ID == "")
                {
                    if (recive_str.ActType == ActiveType.Load || recive_str.ActType == ActiveType.Loadunload || recive_str.ActType == ActiveType.Unload)
                    {
                        Veh_VehM_Global_Property.already_have_command_Check = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str31_Receive :  Move?? Check the start address.");
                        send_str = new ID_131_TRANS_RESPONSE
                        {
                            CmdID = recive_str.CmdID,
                            ActType = Veh_VehM_Global.NowActiveType,
                            ReplyCode = 1,
                            NgReason = "Empty CST ID."
                        };

                        wrapper = new WrapperMessage
                        {
                            ID = WrapperMessage.TransRespFieldNumber,
                            SeqNum = e.iSeqNum,
                            TransResp = send_str
                        };

                        resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                        Veh_VehM_Global.CheckNewCmdIDProcess = false;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                        //vehicle.currentExcuteCMD_ID = cmd_id;
                        //Console.WriteLine("Don't Need New Command");
                        return;
                    }
                }
                cleanMonthLogFunc();
                Veh_VehM_Global_Property.already_have_command_Check = true;
                Veh_VehM_Global.BlockControlSection = "";
                Veh_VehM_Global.cmdID = cmd_id;
                Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
                Veh_VehM_Global.command_ID_from_VehM = recive_str.CmdID;
                string reason = string.Empty;
                bool canReceiveCmd = true;
                
                Veh_VehM_Global.CycleSections = cycRunSecs;
                Veh_VehM_Global.cmdRunning = true;
                
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : New Cmd from 300 section num = {0}.", Veh_VehM_Global.GuideSections.Length);
                

                Veh_VehM_Global.LoadAddress = fromAdr;              // Roy+180319
                Veh_VehM_Global.UnloadAddress = toAdr;              // Roy+180319
                //Veh_VehM_Global.Address = toAdr;
                /**/
                //Veh_VehM_Global.CycleSections = cycRunSecs;
                Veh_VehM_Global.NowActiveType = actionType;
                if (CST_ID != "")
                {
                    Veh_VehM_Global.cmd_CstIDfromOHTC = CST_ID;
                    Veh_VehM_Global.CSTID_Load = CST_ID;
                    Veh_VehM_Global.CSTID_UnLoad = CST_ID;
                }
                else
                {
                    Veh_VehM_Global.cmd_CstIDfromOHTC = "";
                }
                Veh_VehM_Global.cmdRunning = true;

                // Initialize the Packet Content
                //DDS_Global.motionInfoInterCommSendData.Move.Sections = passSections;
                //DDS_Global.motionInfoInterCommSendData.Load.CSTID = int.Parse(CST_ID);
                string msg = string.Empty;
                Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} Start translate the cmd 31 situation.", Thread.CurrentThread.ManagedThreadId);
                #region "解碼/翻譯給OHT"
                switch (actionType)
                {
                    case ActiveType.Move:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        //SpinWait.SpinUntil(() => false, 20);
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Move;
                        //DDS_Global.motionInfoInterCommSendData.Move.Address = toAdr;              // Roy-180319
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;              // Roy*180328

                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        GetMsgString(fromAdr, toAdr, CST_ID, ref msg);
                        OnEventMsgFromVehM(new ReportMsgEventArg(msg));
                        break;
                    case ActiveType.Home:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        //SpinWait.SpinUntil(() => false, 20);
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Home_Calibration;
                        //DDS_Global.motionInfoInterCommSendData.Move.Address = toAdr;              // Roy-180319
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;              // Roy*180328

                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        GetMsgString(fromAdr, toAdr, CST_ID, ref msg);
                        OnEventMsgFromVehM(new ReportMsgEventArg(msg));
                        break;
                    case ActiveType.Movetomtl:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        //SpinWait.SpinUntil(() => false, 20);
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Movetomtl;
                        //DDS_Global.motionInfoInterCommSendData.Move.Address = toAdr;              // Roy-180319
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;              // Roy*180328

                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        GetMsgString(fromAdr, toAdr, CST_ID, ref msg);
                        OnEventMsgFromVehM(new ReportMsgEventArg(msg));
                        break;
                    case ActiveType.Mtlhome:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        //SpinWait.SpinUntil(() => false, 20);
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Mtlhome;
                        //DDS_Global.motionInfoInterCommSendData.Move.Address = toAdr;              // Roy-180319
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;              // Roy*180328

                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        GetMsgString(fromAdr, toAdr, CST_ID, ref msg);
                        OnEventMsgFromVehM(new ReportMsgEventArg(msg));
                        break;
                    case ActiveType.Systemin:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Systemin;

                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;              // Roy*180328

                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        GetMsgString(fromAdr, toAdr, CST_ID, ref msg);
                        OnEventMsgFromVehM(new ReportMsgEventArg(msg));
                        break;
                    case ActiveType.Systemout:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        //SpinWait.SpinUntil(() => false, 20);
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Systemout;
                        //DDS_Global.motionInfoInterCommSendData.Move.Address = toAdr;              // Roy-180319
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;              // Roy*180328

                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        GetMsgString(fromAdr, toAdr, CST_ID, ref msg);
                        OnEventMsgFromVehM(new ReportMsgEventArg(msg));
                        break;
                    case ActiveType.Loadunload:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load_Unload;

                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;              // Roy*180328

                        Veh_VehM_Global.LoadAddress = fromAdr;              // Roy+180319
                        Veh_VehM_Global.UnloadAddress = toAdr;              // Roy+180319
                        //Veh_VehM_Global.GuideSectionsStartToLoad = passStartToLoadSections;
                        //Veh_VehM_Global.GuideSections = passToDestinationSections;
                        //Veh_VehM_Global.GuideAddressesStartToLoad = passStartToLoadAddress;
                        //Veh_VehM_Global.GuideAddressesToDestination = passToDestinationAddress;
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        GetMsgString(fromAdr, toAdr, CST_ID, ref msg);
                        OnEventMsgFromVehM(new ReportMsgEventArg(msg));
                        //SpinWait.SpinUntil(() => false, 50);
                        //
                        break;

                    case ActiveType.Load:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load;
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToLoad;

                        Veh_VehM_Global.LoadAddress = fromAdr;              // Roy+180319

                        DDS_Global.motionInfoInterCommSendData.udtLoad.Veh_CSTID = CST_ID;                 // Roy*180319
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        //// 
                        break;

                    case ActiveType.Unload:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.UnLoad;
                        //DDS_Global.motionInfoInterCommSendData.Move.Address = toAdr;              // Roy-180319
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToUnload;

                        Veh_VehM_Global.UnloadAddress = toAdr;              // Roy+180319

                        //Veh_VehM_Global.GuideSectionsStartToLoad = passStartToLoadSections;
                        //Veh_VehM_Global.GuideSections = passToDestinationSections;
                        //Veh_VehM_Global.GuideAddressesStartToLoad = passStartToLoadAddress;
                        //Veh_VehM_Global.GuideAddressesToDestination = passToDestinationAddress;

                        DDS_Global.motionInfoInterCommSendData.udtUnLoad.Veh_CSTID = CST_ID;                 // Roy*180319
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        //// 
                        break;

                    case ActiveType.Round:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Cycle;
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.cycle;

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;
                        
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                        GetMsgString(fromAdr, toAdr, CST_ID, ref msg);
                        OnEventMsgFromVehM(new ReportMsgEventArg(msg));
                        break;

                    case ActiveType.Override:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0} actionType = {1}", Thread.CurrentThread.ManagedThreadId, actionType.ToString());
                        //Veh_VehM.Veh_Abort_Procedure();       //This should done in the actiontype.
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Override;
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

                        switch (Veh_VehM_Global_Property.lastMoveType_4_Override_Check)
                        {
                            case Veh_VehM_Global.ActionType.Move:
                                //Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Move;
                                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;
                                break;
                            case Veh_VehM_Global.ActionType.Load:
                                //Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load;
                                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToLoad;
                                break;
                            case Veh_VehM_Global.ActionType.UnLoad:
                                //Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.UnLoad;
                                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToUnload;
                                break;
                            case Veh_VehM_Global.ActionType.Load_Unload:
                                if (Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist)
                                {
                                    //Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.UnLoad;
                                    DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToUnload;
                                }
                                else
                                {
                                    //Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load_Unload;
                                    DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;
                                }
                                break;
                        }
                        Veh_VehM_Global.UnloadAddress = toAdr;              // Roy+180319

                        //Veh_VehM_Global.GuideSectionsStartToLoad = passStartToLoadSections;
                        //Veh_VehM_Global.GuideSections = passToDestinationSections;
                        //Veh_VehM_Global.GuideAddressesStartToLoad = passStartToLoadAddress;
                        //Veh_VehM_Global.GuideAddressesToDestination = passToDestinationAddress;

                        DDS_Global.motionInfoInterCommSendData.udtUnLoad.Veh_CSTID = CST_ID;                 // Roy*180319
                        Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
                        break;
                }
                //Preparing the packet content 131 for sending to OHC
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : ThreadID = {0}  Translate done , start send back 131.", Thread.CurrentThread.ManagedThreadId);
                send_str = new ID_131_TRANS_RESPONSE
                {
                    CmdID = cmd_id,
                    ActType = Veh_VehM_Global.NowActiveType,
                    ReplyCode = canReceiveCmd ? 0 : 1,
                    NgReason = reason
                };
                wrapper = new WrapperMessage
                {
                    ID = WrapperMessage.TransRespFieldNumber,
                    SeqNum = e.iSeqNum,
                    TransResp = send_str
                };

                resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                //vehicle.currentExcuteCMD_ID = cmd_id;

                Veh_VehM_Global.can_abort_cancel = true;
                Console.WriteLine("Received");
                #endregion         // "解碼/翻譯給OHT"
            }

            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);

            }
        }

        private void checkProcessFlag()
        {
            lock (objectCheckNewCmdIDProcess)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : start  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                while (Veh_VehM_Global.CheckNewCmdIDProcess == true)
                {
                    SpinWait.SpinUntil(() => false, 50);
                }
                Veh_VehM_Global.CheckNewCmdIDProcess = true;

                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());
            }
        }

        public void str32_Receive(object sender, TcpIpEventArgs e) //**
        {
            //This part didn't need, due to the first send is ;
        }

        public void str33_Receive(object sender, TcpIpEventArgs e)
        {
            if (!clientAgent.IsConnection)
            {
                return;
            }
            // In this region, these should be copy & paste on every receive;
            ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST recive_str = (ID_33_CONTROL_ZONE_REPUEST_CANCEL_REQUEST)e.objPacket;
            ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE send_str = null;

            //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd33;
            //
            ControlType controlType = recive_str.ControlType;
            string cancelSecID = recive_str.CancelSecID;
            bool canReceiveCmd = true;

            send_str = new ID_133_CONTROL_ZONE_REPUEST_CANCEL_RESPONSE
            {
                ControlType = controlType,
                CancelSecID = cancelSecID
                //ReplyCode = canReceiveCmd ? 0 : 1
                //Shouldn't here have a reply code? jason++ 181009
            };

            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.ControlZoneRespFieldNumber,
                SeqNum = e.iSeqNum,
                ControlZoneResp = send_str
            };

            Boolean resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
            //vehicle.currentExcuteCMD_ID = cmd_id;

            Console.WriteLine("Received");

            string msg = string.Empty;
            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;

        }

        public void str36_Receive(object sender, TcpIpEventArgs e)
        {
            if (!clientAgent.IsConnection)
            {
                return;
            }
            ID_36_TRANS_EVENT_RESPONSE recive_str = (ID_36_TRANS_EVENT_RESPONSE)e.objPacket;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str36_Receive : ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);

            Veh_VehM_Global.check_recieve_36 = true;
            //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd36;
            //
            Veh_VehM_Global.vehBlockPassReply = (int)recive_str.IsBlockPass;
            //Veh_VehM_Global.vehReserveReply = (int)recive_str.IsReserveSuccess;
            Veh_VehM_Global.vehHIDPassReply = (int)recive_str.IsHIDPass;
            //if(Veh_VehM_Global.vehBlockPassReply == 0)
            //{
            //}
            if (Veh_VehM_Global.vehBlockPassReply == (int)Status.NG)
            {
                Veh_VehM_Global_Property.pause_Type_Check = PauseType.Block;
            }
            else if (Veh_VehM_Global.vehHIDPassReply == (int)Status.NG)
            {
                Veh_VehM_Global_Property.pause_Type_Check = PauseType.Hid;
            }
        }

        public void str37_Receive(object sender, TcpIpEventArgs e)
        {
            try
            {
                checkProcessFlag();
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str37_Receive : ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);

                if (!clientAgent.IsConnection)
                {
                    return;
                }

                ID_37_TRANS_CANCEL_REQUEST recive_str = (ID_37_TRANS_CANCEL_REQUEST)e.objPacket;
                ID_137_TRANS_CANCEL_RESPONSE send_str = null;
                Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd37;
                //
                string cmd_id = recive_str.CmdID;
                CMDCancelType act_Type = recive_str.ActType;
                WrapperMessage wrapper;
                if ((Veh_VehM_Global_Property.already_have_command_Check == false) || ((Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist) && (act_Type == CMDCancelType.CmdCancel))
                    || ((Veh_VehM_Global.hasCst == VhLoadCSTStatus.NotExist) && (act_Type == CMDCancelType.CmdAbout)) || (Veh_VehM_Global.cmdID != cmd_id) || (Veh_VehM_Global.can_abort_cancel == false))
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@ str37_Receive :already_have_command_Check = {0}", Veh_VehM_Global_Property.already_have_command_Check.ToString());
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@ str37_Receive :hasCst = {0}", Veh_VehM_Global.hasCst.ToString());
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@ str37_Receive :act_Type = {0}", act_Type.ToString());
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@ str37_Receive :cmdID = {0}", Veh_VehM_Global.cmdID.ToString());
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@ str37_Receive :can_abort_cancel = {0}", Veh_VehM_Global.can_abort_cancel.ToString());

                    send_str = new ID_137_TRANS_CANCEL_RESPONSE
                    {
                        CmdID = cmd_id,
                        ActType = act_Type,
                        ReplyCode = 1
                    };

                    wrapper = new WrapperMessage
                    {
                        ID = WrapperMessage.TransCancelRespFieldNumber,
                        SeqNum = e.iSeqNum,
                        TransCancelResp = send_str
                    };
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str37_Receive : Can't do the cancel_abort.", Thread.CurrentThread.ManagedThreadId);
                    Boolean resp_cmp_1 = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                    Veh_VehM_Global.CheckNewCmdIDProcess = false;

                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                    return;
                }
                //Veh_VehM_Global_Property.already_have_command_Check = false;
                if (act_Type == CMDCancelType.CmdAbout)
                {
                    Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Abort;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str37_Receive : {0}.", Veh_VehM_Global.enActionType.ToString());
                }
                else if (act_Type == CMDCancelType.CmdCancel)
                {
                    Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Cancel;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str37_Receive : {0}.", Veh_VehM_Global.enActionType.ToString());
                }
                else if (act_Type == CMDCancelType.CmdForceFinish)
                {
                    Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Forcefinish;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str37_Receive : {0}.", Veh_VehM_Global.enActionType.ToString());
                }
                //Preparing the packet content 131 for sending to OHC
                send_str = new ID_137_TRANS_CANCEL_RESPONSE
                {
                    CmdID = cmd_id,
                    ActType = act_Type,
                    ReplyCode = 0
                };

                wrapper = new WrapperMessage
                {
                    ID = WrapperMessage.TransCancelRespFieldNumber,
                    SeqNum = e.iSeqNum,
                    TransCancelResp = send_str
                };

                Boolean resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                //vehicle.currentExcuteCMD_ID = cmd_id;
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
                Console.WriteLine("Received");
                DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;
                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToCancel;

                // Initialize the Packet Content
                //DDS_Global.motionInfoInterCommSendData.Move.Sections = passSections;
                //DDS_Global.motionInfoInterCommSendData.Load.CSTID = int.Parse(CST_ID);
                string msg = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionErrorLog(ex);
            }
        }

        public void str39_Receive(object sender, TcpIpEventArgs e)
        {
            try
            {
                checkProcessFlag();
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str39_Receive : ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);

                /*
                 * 判定是否能進行此動作。
                 * 1. 目前沒有命令的狀況下不能接受暫停。            或者去認flag 來判定是否要往下丟(該如何做呢? while(global_flag!=true) spinWait)
                 * 2. 若知道正在使用夾爪當中也不能接受命令。        Veh_VehM_Global.can_Pause
                 */
                ID_39_PAUSE_REQUEST recive_str = (ID_39_PAUSE_REQUEST)e.objPacket;
                //if (recive_str.EventType == PauseEvent.Pause)
                //{
                //    while (Veh_VehM_Global.can_Pause == false)
                //    {
                //        Thread.Sleep(500);
                //    }
                //}
                /*
                 * 等柏言大大改完再問continue 濾掉的問題。
                 * 目前先不用，因為判定邏輯會有問題。
                 */
                //if (recive_str.EventType == PauseEvent.Continue)
                //{
                //    while (Veh_VehM_Global.reallyStop == false)
                //    {
                //        Thread.Sleep(500);
                //    }
                //}

                ID_139_PAUSE_RESPONSE send_str = null;

                Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd39;

                //PauseEvent eType = PauseEvent.Pause;                  // Roy-180302 ... 不可用(死)定值
                PauseEvent eType = recive_str.EventType;                // Roy+180302 ... 撈VehM命令內容
                PauseType pType = recive_str.PauseType;
                Veh_VehM_Global.now_Pause = recive_str.EventType;
                WrapperMessage wrapper;
                Boolean resp_cmp;
                if ((recive_str.PauseType != PauseType.OhxC) && (recive_str.PauseType != PauseType.Safety) && (recive_str.PauseType != PauseType.EarthQuake) && (recive_str.PauseType != PauseType.Hid))
                {
                    if (recive_str.EventType == PauseEvent.Pause)
                    {
                        send_str = new ID_139_PAUSE_RESPONSE
                        {
                            EventType = eType,
                            ReplyCode = 1
                        };

                        wrapper = new WrapperMessage
                        {
                            //ID = WrapperMessage.TransRespFieldNumber,                  // Roy-180302
                            ID = WrapperMessage.PauseRespFieldNumber,                  // Roy+180302
                            SeqNum = e.iSeqNum,
                            PauseResp = send_str                  // Roy+180302
                        };

                        resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                        Veh_VehM_Global.CheckNewCmdIDProcess = false;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                        return; //
                    }
                }
                send_str = new ID_139_PAUSE_RESPONSE
                {
                    EventType = eType,
                    ReplyCode = 0
                };

                wrapper = new WrapperMessage
                {
                    //ID = WrapperMessage.TransRespFieldNumber,                  // Roy-180302
                    ID = WrapperMessage.PauseRespFieldNumber,                  // Roy+180302
                    SeqNum = e.iSeqNum,
                    PauseResp = send_str                  // Roy+180302
                };

                resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                //clientAgent.StopWatch_FromTheLastCommTime.Elapsed.TotalMinutes
                //SpinWait.SpinUntil(() => false, 1000);
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
                Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
                switch (eType)
                {
                    case PauseEvent.Continue:
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;

                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToContinue;
                        Veh_VehM_Global_Property.same_pause_Command_Check = false;
                        if (recive_str.PauseType == PauseType.All)
                        {
                            Veh_VehM_Global.HID_pause = false;
                            Veh_VehM_Global.normal_pause = false;
                            Veh_VehM_Global.safety_pause = false;
                            Veh_VehM_Global.earthquake_pause = false;
                        }
                        else if (recive_str.PauseType == PauseType.Hid)
                        {
                            Veh_VehM_Global.HID_pause = false;
                        }
                        else if (recive_str.PauseType == PauseType.OhxC)
                        {
                            Veh_VehM_Global.normal_pause = false;
                        }
                        else if (recive_str.PauseType == PauseType.Safety)
                        {
                            Veh_VehM_Global.safety_pause = false;
                        }
                        else if (recive_str.PauseType == PauseType.EarthQuake)
                        {
                            Veh_VehM_Global.earthquake_pause = false;
                        }
                        else if (recive_str.PauseType == PauseType.Block)
                        {
                            DDS_Global.checkblock = true;
                        }

                        bool checkcontinue = check_Continue_1();
                        if (checkcontinue == true)
                        {

                        }
                        else
                        {

                            EventType eventTypes = new EventType();
                            CompleteStatus cmpStatus = new CompleteStatus();
                            ActiveType activeType = new ActiveType();
                            PauseEvent pauseContinue = new PauseEvent();
                            VHActionStatus actionStatus = new VHActionStatus();
                            VhGuideStatus lGuideStatus = new VhGuideStatus();
                            VhGuideStatus rGuideStatus = new VhGuideStatus();
                            VhLoadCSTStatus loadStatus = new VhLoadCSTStatus();
                            VHModeStatus modeStatus = new VHModeStatus();
                            VhPowerStatus powerStatus = new VhPowerStatus();

                            VhStopSingle obstStatus = new VhStopSingle();
                            VhStopSingle blockStatus = new VhStopSingle();
                            VhStopSingle pauseStatus = new VhStopSingle();

                            Veh_VehM_Global.vehVehM.EventTypeConv(Veh_VehM_Global.eventTypes, ref eventTypes);
                            Veh_VehM_Global.vehVehM.CompleteStatusConv(Veh_VehM_Global.cmpStatus, ref cmpStatus);
                            Veh_VehM_Global.vehVehM.GuideStatusConv(Veh_VehM_Global.vehLeftGuideLockStatus, ref lGuideStatus);
                            Veh_VehM_Global.vehVehM.GuideStatusConv(Veh_VehM_Global.vehRightGuideLockStatus, ref rGuideStatus);
                            Veh_VehM_Global.vehVehM.StopStatusConv(Veh_VehM_Global.vehBlockStopStatus, ref blockStatus);
                            Veh_VehM_Global.vehVehM.StopStatusConv(Veh_VehM_Global.vehObstStopStatus, ref obstStatus);
                            Veh_VehM_Global.vehVehM.StopStatusConv(Veh_VehM_Global.vehPauseStatus, ref pauseStatus);
                            Veh_VehM_Global.vehVehM.LoadStatusConv(Veh_VehM_Global.vehLoadStatus, ref loadStatus);
                            Veh_VehM_Global.vehVehM.ModeStatusConv(Veh_VehM_Global.vehModeStatus_fromVehC, ref modeStatus);
                            Veh_VehM_Global.vehVehM.ActionStatusConv(Veh_VehM_Global.vehActionStatus, ref actionStatus);
                            sned_Str144("ID_144",
                                Veh_VehM_Global.Address, Veh_VehM_Global.Section,
                                modeStatus, actionStatus, powerStatus, Veh_VehM_Global.hasCst, obstStatus, blockStatus,
                                pauseStatus, lGuideStatus, rGuideStatus, (int)Veh_VehM_Global.DistanceFromSectionStart,
                                Veh_VehM_Global.BatteryCapacity, Veh_VehM_Global.BatteryTemperature, Veh_VehM_Global.SteeringWheel, Veh_VehM_Global.ErrorStatus);
                        }

                        break;

                    case PauseEvent.Pause:
                        DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;
                        if (recive_str.PauseType == PauseType.Hid)
                        {
                            Veh_VehM_Global.HID_pause = true;
                        }
                        else if (recive_str.PauseType == PauseType.OhxC)
                        {
                            Veh_VehM_Global.normal_pause = true;
                        }
                        else if (recive_str.PauseType == PauseType.Safety)
                        {
                            Veh_VehM_Global.safety_pause = true;
                        }
                        else if (recive_str.PauseType == PauseType.EarthQuake)
                        {
                            Veh_VehM_Global.earthquake_pause = true;
                        }
                        if ((Veh_VehM_Global.HID_pause == true) || (Veh_VehM_Global.normal_pause == true) || (Veh_VehM_Global.safety_pause == true) || (Veh_VehM_Global.earthquake_pause == true))
                        {
                            EventType eventTypes = new EventType();
                            CompleteStatus cmpStatus = new CompleteStatus();
                            ActiveType activeType = new ActiveType();
                            PauseEvent pauseContinue = new PauseEvent();
                            VHActionStatus actionStatus = new VHActionStatus();
                            VhGuideStatus lGuideStatus = new VhGuideStatus();
                            VhGuideStatus rGuideStatus = new VhGuideStatus();
                            VhLoadCSTStatus loadStatus = new VhLoadCSTStatus();
                            VHModeStatus modeStatus = new VHModeStatus();
                            VhPowerStatus powerStatus = new VhPowerStatus();

                            VhStopSingle obstStatus = new VhStopSingle();
                            VhStopSingle blockStatus = new VhStopSingle();
                            VhStopSingle pauseStatus = new VhStopSingle();

                            Veh_VehM_Global.vehVehM.EventTypeConv(Veh_VehM_Global.eventTypes, ref eventTypes);
                            Veh_VehM_Global.vehVehM.CompleteStatusConv(Veh_VehM_Global.cmpStatus, ref cmpStatus);
                            Veh_VehM_Global.vehVehM.GuideStatusConv(Veh_VehM_Global.vehLeftGuideLockStatus, ref lGuideStatus);
                            Veh_VehM_Global.vehVehM.GuideStatusConv(Veh_VehM_Global.vehRightGuideLockStatus, ref rGuideStatus);
                            Veh_VehM_Global.vehVehM.StopStatusConv(Veh_VehM_Global.vehBlockStopStatus, ref blockStatus);
                            Veh_VehM_Global.vehVehM.StopStatusConv(Veh_VehM_Global.vehObstStopStatus, ref obstStatus);
                            Veh_VehM_Global.vehVehM.StopStatusConv(Veh_VehM_Global.vehPauseStatus, ref pauseStatus);
                            Veh_VehM_Global.vehVehM.LoadStatusConv(Veh_VehM_Global.vehLoadStatus, ref loadStatus);
                            Veh_VehM_Global.vehVehM.ModeStatusConv(Veh_VehM_Global.vehModeStatus_fromVehC, ref modeStatus);
                            Veh_VehM_Global.vehVehM.ActionStatusConv(Veh_VehM_Global.vehActionStatus, ref actionStatus);
                            sned_Str144("ID_144",
                                Veh_VehM_Global.Address, Veh_VehM_Global.Section,
                                modeStatus, actionStatus, powerStatus, Veh_VehM_Global.hasCst, obstStatus, blockStatus,
                                pauseStatus, lGuideStatus, rGuideStatus, (int)Veh_VehM_Global.DistanceFromSectionStart,
                                Veh_VehM_Global.BatteryCapacity, Veh_VehM_Global.BatteryTemperature, Veh_VehM_Global.SteeringWheel, Veh_VehM_Global.ErrorStatus);
                        }
                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToPause;
                        break;

                }
            }
            catch (Exception ex)
            {
                ExceptionErrorLog(ex);
            }
        }

        public bool check_Continue_1()
        {
            bool check_Continue = true;
            if (
                (Veh_VehM_Global.normal_pause == true)
                || (Veh_VehM_Global.safety_pause == true)
                || (Veh_VehM_Global.earthquake_pause == true)
                || (Veh_VehM_Global.HID_pause == true)
                )
            {
                check_Continue = false;
            }

            return check_Continue;
        }
        public void str41_Recieve(object sender, TcpIpEventArgs e)
        {
            try
            {
                checkProcessFlag();
                ID_41_MODE_CHANGE_REQ recive_str = (ID_41_MODE_CHANGE_REQ)e.objPacket;
                ID_141_MODE_CHANGE_RESPONSE send_str = null;
                VehControlMode tempMode = VehControlMode.OnlineLocal;
                Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd41;
                Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enChangeMode;
                //Veh_VehM_Global.vehModeStatus = recive_str.OperatingVHMode;
                OperatingVHMode operatingVHMode = recive_str.OperatingVHMode;
                if (operatingVHMode == (OperatingVHMode)0)
                {
                    Veh_VehM_Global.Modestatus_from300 = VehControlMode.OnlineRemote;
                    tempMode = VehControlMode.OnlineRemote;
                }
                else if (operatingVHMode == (OperatingVHMode)1)
                {
                    Veh_VehM_Global.Modestatus_from300 = VehControlMode.OnlineLocal;
                    tempMode = VehControlMode.OnlineLocal;
                }
                send_str = new ID_141_MODE_CHANGE_RESPONSE
                {
                    ReplyCode = 0
                };

                WrapperMessage wrapper = new WrapperMessage
                {
                    ID = WrapperMessage.ModeChangeRespFieldNumber,
                    SeqNum = e.iSeqNum,
                    ModeChangeResp = send_str
                };
                bool resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
                Console.WriteLine("Received");

                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdForChangeStatus;

                DDS_Global.motionInfoInterCommSendData.udtControlMode = tempMode;
                ////
                DDS.ReturnCode status;
                status = DDS_Global.motionInfo_VehInterCommSendDataWriter.Write(DDS_Global.motionInfoInterCommSendData);
                Veh_VehM_Global.CheckNewCmdIDProcess = false;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : 41  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

                ErrorHandler.checkStatus(status, "@Drv_VehC_PrismTech_VortexOpenSplice:  OnTimedEvent  \\   <DDS>  motionInfo_HandShakeRecieveDataWriter.Write - Error");                    // Roy*180612

                // Inform SendData Sent
                DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_Move_Procedure : Send the change mode command to the Veh");
                Veh_VehM_Global.checkForNoMoveSend144 = false;

            }
            catch (Exception ex)
            {
                ExceptionErrorLog(ex);
            }
        }
        public void str43_Receive(object sender, TcpIpEventArgs e)
        {
            try
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str43_Receive : ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);
                string local_CstID = "";
                bool rejectCmdCheck = false;
                bool sendCheck = false;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@TCPIP:" + " " + "43 recieve");
                ID_43_STATUS_REQUEST recive_str = (ID_43_STATUS_REQUEST)e.objPacket;

                //ID_143_STATUS_RESPONSE send_str = null;
                DDS_Global.motionInfo_VehInterCommReptData_134Reader.Take(
                                   ref motionInfoInterCommReport_134,
                                   ref sampleInfo_RptData_134,
                                    DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);
                foreach (MotionInfo_Vehicle_Inter_Comm_ReportData_134 data in motionInfoInterCommReport_134)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL str43 @@@@@@eventTypes = {1}.", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);

                    if (data.Section != null)
                    {
                        if (data.Section.Count() != 0)
                        {
                            rejectCmdCheck = true;
                            break;
                        }
                    }
                }
                VHModeStatus modeStatusforVehC = VHModeStatus.None;
                if (motionInfoInterCommReport_134 != null && rejectCmdCheck == true)
                {
                    string address;
                    string section;
                    
                    VhLoadCSTStatus hasCst = VhLoadCSTStatus.NotExist;
                    VehControlMode modeStatus;
                    VHActionStatus actionStatus;
                    VhPowerStatus powerStatus;
                    VhStopSingle obstacleStatus;
                    VhStopSingle blockingStatus;
                    VhStopSingle hIDStatus;
                    VhStopSingle pauseStatus;
                    VhStopSingle errorStatus;
                    UInt32 sec_Distance;
                    Int32 obst_Distance;
                    string obst_VehicleID;
                    string stopped_Block_ID;
                    //string stopped_HID_ID;

                    foreach (MotionInfo_Vehicle_Inter_Comm_ReportData_134 data in motionInfoInterCommReport_134)
                    {
                        sendCheck = true;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@TCPIP: Real 143 send info from Veh");
                        if (Veh_VehM_Global.fakeID == false)
                        {
                            if (data.loadStatus.With_CST == 1)
                            {
                                hasCst = VhLoadCSTStatus.Exist;
                                Veh_VehM_Global.hasCst = hasCst;
                            }
                            Veh_VehM_Global.CSTID_Load = data.loadStatus.Veh_CSTID;
                            if (Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist)
                            {
                                CheckBarcodeReaderID.BarcodeReaderID checkBCRreturncode = (CheckBarcodeReaderID.BarcodeReaderID)0;
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@TCPIP: HasCST CSTID_Load = {0}", data.loadStatus.Veh_CSTID.ToString());

                                if (Veh_VehM_Global.CSTID_Load == "(n/a)" || Veh_VehM_Global.CSTID_Load == "")
                                {
                                    checkBCRreturncode = (CheckBarcodeReaderID.BarcodeReaderID)2;
                                }
                                Veh_VehM_Global.vehVehM.SendValuesForRept_134(data, "136_BCR", checkBCRreturncode);
                                local_CstID = Veh_VehM_Global.CSTID_Load;
                            }
                            else
                            {
                                local_CstID = "";
                            }
                        }
                        else if (Veh_VehM_Global.fakeID == true)
                        {
                            if (data.loadStatus.With_CST == 1)
                            {
                                hasCst = VhLoadCSTStatus.Exist;
                                Veh_VehM_Global.hasCst = hasCst;
                            }
                            Veh_VehM_Global.CSTID_Load = data.loadStatus.Veh_CSTID;
                            if (Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist)
                            {
                                CheckBarcodeReaderID.BarcodeReaderID checkBCRreturncode = (CheckBarcodeReaderID.BarcodeReaderID)0;
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@TCPIP: HasCST CSTID_Load = {0}", data.loadStatus.Veh_CSTID.ToString());

                                if (Veh_VehM_Global.CSTID_Load == "(n/a)" || Veh_VehM_Global.CSTID_Load == "")
                                {
                                    checkBCRreturncode = (CheckBarcodeReaderID.BarcodeReaderID)2;
                                }
                                Veh_VehM_Global.vehVehM.SendValuesForRept_134(data, "136_BCR", checkBCRreturncode);
                                local_CstID = Veh_VehM_Global.CSTID_Load;
                            }
                            else
                            {
                                local_CstID = "";
                            }
                        }
                        address = data.Address;
                        if (data.Section.Substring(0, 1) == "+")
                        {
                            data.Section = data.Section.Substring(1);
                        }
                        // A20.06.22 take off for the 
                        //if (data.Section.Length == 4)
                        //{
                        //    data.Section = "0" + data.Section;
                        //}
                        section = data.Section;
                        //section = new ToolFunc().DelLastAndFirstCharString(section); //jason++ 190331 del the  "+ -" and the little section.
                        modeStatus = data.ConrtolMode;
                        actionStatus = (VHActionStatus)data.vehActionStatus;
                        powerStatus = (VhPowerStatus)data.vehPowerStatus;
                        obstacleStatus = (VhStopSingle)data.vehObstacleStopStatus;
                        blockingStatus = (VhStopSingle)data.vehBlockStopStatus;
                        hIDStatus = (VhStopSingle)data.vehHIDStopStatus;
                        pauseStatus = (VhStopSingle)data.vehPauseStatus;
                        //errorStatus = 0; // temp jason++ 181017
                        sec_Distance = (UInt32)data.DistanceFromSectionStart;
                        obst_Distance = data.vehObstDist;
                        /*****/
                        Veh_VehM_Global.Address = address;
                        Veh_VehM_Global.Section = section;

                        Veh_VehM_Global.vehModeStatus_fromVehC = modeStatus;
                        Veh_VehM_Global.vehActionStatus = (int)actionStatus;

                        Veh_VehM_Global.vehObstStopStatus = (int)obstacleStatus;
                        Veh_VehM_Global.vehBlockStopStatus = (int)blockingStatus;
                        Veh_VehM_Global.vehHIDStopStatus = (int)hIDStatus;
                        Veh_VehM_Global.vehPauseStatus = (int)pauseStatus;
                        errorStatus = 0; // temp jason++ 181017
                        Veh_VehM_Global.DistanceFromSectionStart = sec_Distance;
                        Veh_VehM_Global.vehObstDist = obst_Distance;
                        Veh_VehM_Global.BatteryCapacity = data.BatteryCapacity;
                        Veh_VehM_Global.veh_ChargeStatus = data.ChargeStatus;
                        /*****/
                        obst_VehicleID = "";   //???? temp jason++ 181017
                        stopped_Block_ID = ""; //???? temp jason++ 181017
                                               //stopped_HID_ID = "";   //???? temp jason++ 181017
                        switch (modeStatus)
                        {
                            case VehControlMode.OnlineLocal:
                                modeStatusforVehC = VHModeStatus.Manual;
                                break;
                            case VehControlMode.OnlineRemote:
                                modeStatusforVehC = VHModeStatus.AutoRemote;
                                break;
                        }
                        actionStatus = (VHActionStatus)0;
                        powerStatus = (VhPowerStatus)1;
                        ID_143_STATUS_RESPONSE strSends;
                        if (Veh_VehM_Global.cmdRunning == true)
                        {
                            actionStatus = (VHActionStatus)1;
                        }
                        if (Veh_VehM_Global.cmdID == null)
                        {
                            Veh_VehM_Global.cmdID = "";
                        }
                        if (Veh_VehM_Global.CSTID_Load == null)
                        {
                            Veh_VehM_Global.CSTID_Load = "";
                            local_CstID = Veh_VehM_Global.CSTID_Load;
                        }
                        if (data.ErrorCode != 0 && data.ErrorCode != 1)
                        {
                            //ErrorHandling(ref done);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : str43_Receive !!!!!! Error Code != 0 , Please check again.");

                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : str43_Receive !!!!!! Error Code = {0}", data.ErrorCode.ToString());
                            //bool checkError = false;
                            //bool dd = ErrorCodeRead(data, ref checkError);
                            errorStatus = VhStopSingle.StopSingleOn;
                        }
                        strSends = new ID_143_STATUS_RESPONSE
                        {
                            CurrentAdrID = address,
                            CurrentSecID = section,
                            ModeStatus = modeStatusforVehC,
                            ActionStatus = actionStatus,
                            HasCST = hasCst,
                            PowerStatus = powerStatus,
                            ObstacleStatus = obstacleStatus,
                            //ReserveStatus = 0,
                            BlockingStatus = blockingStatus,
                            //HIDStatus = hIDStatus,
                            PauseStatus = pauseStatus,
                            ErrorStatus = errorStatus,
                            SecDistance = (uint)sec_Distance,
                            ObstDistance = obst_Distance,
                            ObstVehicleID = obst_VehicleID,
                            //ReserveInfos
                            StoppedBlockID = stopped_Block_ID,
                            //StoppedHIDID = stopped_HID_ID,
                            CmdID = Veh_VehM_Global.cmdID,
                            CSTID = local_CstID,
                            //DrivingDirection = 0,
                            //BatteryCapacity = (uint)Veh_VehM_Global.BatteryCapacity,
                            //ChargeStatus = (VhChargeStatus)Veh_VehM_Global.veh_ChargeStatus,
                            //BatteryTemperature = 40
                        };
                        WrapperMessage wrappers = new WrapperMessage
                        {
                            ID = WrapperMessage.StatusReqRespFieldNumber,
                            SeqNum = e.iSeqNum,
                            StatusReqResp = strSends
                        };
                        sw.Start();
                        Boolean resp_cmps = clientAgent.TrxTcpIp.SendGoogleMsg(wrappers, true);
                        if (modeStatusforVehC == VHModeStatus.Manual)
                        {
                            Veh_VehM_Global.Modestatus_from300 = VehControlMode.OnlineLocal;
                            Veh_VehM_Global.startCheck = true;
                            Task.Run(() => Veh_VehM_Global.vehVehM.Modechange());
                        }
                        sw.Stop();
                        if (Veh_VehM_Global.enCmdID == 0) // jason++ 190519 Avoid the cmd43 from VehC influence the thread controll of the form.cs which is used for dealing with the Battery Capicity.
                        {
                            Veh_VehM_Global.checkForNoMoveSend144 = true;
                        }
                    }

                }
                else
                {
                    string address;
                    string section;
                    VhLoadCSTStatus hasCst = VhLoadCSTStatus.NotExist;
                    VehControlMode modeStatus;
                    VHActionStatus actionStatus;
                    VhPowerStatus powerStatus;
                    VhStopSingle obstacleStatus;
                    VhStopSingle blockingStatus;
                    VhStopSingle hIDStatus;
                    VhStopSingle pauseStatus;
                    VhStopSingle errorStatus;
                    UInt32 sec_Distance;
                    Int32 obst_Distance;
                    string obst_VehicleID;
                    string stopped_Block_ID;
                    //string stopped_HID_ID;


                    sendCheck = true;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@TCPIP: Real 143 send info from Veh");
                    if ((int)(Veh_VehM_Global.hasCst) == 1)
                    {
                        hasCst = VhLoadCSTStatus.Exist;
                    }
                    address = Veh_VehM_Global.Address;

                    section = Veh_VehM_Global.Section;
                    //section = new ToolFunc().DelLastAndFirstCharString(section); //jason++ 190331 del the  "+ -" and the little section.
                    modeStatus = Veh_VehM_Global.vehModeStatus_fromVehC;
                    actionStatus = (VHActionStatus)Veh_VehM_Global.vehActionStatus;

                    obstacleStatus = (VhStopSingle)Veh_VehM_Global.vehObstStopStatus;
                    blockingStatus = (VhStopSingle)Veh_VehM_Global.vehBlockStopStatus;
                    hIDStatus = (VhStopSingle)Veh_VehM_Global.vehHIDStopStatus;
                    pauseStatus = (VhStopSingle)Veh_VehM_Global.vehPauseStatus;
                    errorStatus = Veh_VehM_Global.ErrorStatus;
                    sec_Distance = (UInt32)Veh_VehM_Global.DistanceFromSectionStart;
                    obst_Distance = Veh_VehM_Global.vehObstDist;
                    obst_VehicleID = "";   //???? temp jason++ 181017
                    stopped_Block_ID = ""; //???? temp jason++ 181017
                                           //stopped_HID_ID = "";   //???? temp jason++ 181017
                    switch (modeStatus)
                    {
                        case VehControlMode.OnlineLocal:
                            modeStatusforVehC = VHModeStatus.Manual;
                            break;
                        case VehControlMode.OnlineRemote:
                            modeStatusforVehC = VHModeStatus.AutoRemote;
                            break;
                    }
                    actionStatus = (VHActionStatus)0;
                    powerStatus = (VhPowerStatus)1;
                    ID_143_STATUS_RESPONSE strSends;
                    if (Veh_VehM_Global.cmdRunning == true)
                    {
                        actionStatus = (VHActionStatus)1;
                    }
                    if (Veh_VehM_Global.cmdID == null)
                    {
                        Veh_VehM_Global.cmdID = "";
                    }
                    if (Veh_VehM_Global.CSTID_Load == null)
                    {
                        Veh_VehM_Global.CSTID_Load = "";
                    }
                    if (hasCst == VhLoadCSTStatus.Exist)
                    {
                        local_CstID = Veh_VehM_Global.CSTID_Load;
                    }
                    else if (hasCst == VhLoadCSTStatus.NotExist)
                    {
                        local_CstID = "";
                    }
                    strSends = new ID_143_STATUS_RESPONSE
                    {
                        CurrentAdrID = address,
                        CurrentSecID = section,
                        ModeStatus = modeStatusforVehC,
                        ActionStatus = actionStatus,
                        HasCST = hasCst,
                        PowerStatus = powerStatus,
                        ObstacleStatus = obstacleStatus,
                        //ReserveStatus = 0,
                        BlockingStatus = blockingStatus,
                        //HIDStatus = hIDStatus,
                        PauseStatus = pauseStatus,
                        ErrorStatus = errorStatus,
                        SecDistance = (uint)sec_Distance,
                        ObstDistance = obst_Distance,
                        ObstVehicleID = obst_VehicleID,
                        //ReserveInfos
                        StoppedBlockID = stopped_Block_ID,
                        //StoppedHIDID = stopped_HID_ID,
                        CmdID = Veh_VehM_Global.cmdID,
                        CSTID = local_CstID,
                    };
                    WrapperMessage wrappers = new WrapperMessage
                    {
                        ID = WrapperMessage.StatusReqRespFieldNumber,
                        SeqNum = e.iSeqNum,
                        StatusReqResp = strSends
                    };
                    sw.Start();
                    Boolean resp_cmps = clientAgent.TrxTcpIp.SendGoogleMsg(wrappers, true);

                    sw.Stop();
                    if (Veh_VehM_Global.enCmdID == 0) // jason++ 190519 Avoid the cmd43 from VehC influence the thread controll of the form.cs which is used for dealing with the Battery Capicity.
                    {
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                    }
                }
                if (sendCheck == false)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@TCPIP: Fake 143 send no info from Veh");
                    ID_143_STATUS_RESPONSE strSends;

                    strSends = new ID_143_STATUS_RESPONSE
                    {
                        CurrentAdrID = "0",
                        CurrentSecID = "0",
                        ModeStatus = 0,
                        ActionStatus = 0,
                        HasCST = 0,
                        PowerStatus = 0,
                        ObstacleStatus = 0,
                        //ReserveStatus = 0,
                        BlockingStatus = 0,
                        //HIDStatus = hIDStatus,
                        PauseStatus = 0,
                        ErrorStatus = 0,
                        SecDistance = 0,
                        ObstDistance = 0,
                        ObstVehicleID = "0",
                        //ReserveInfos
                        StoppedBlockID = "0",
                        //StoppedHIDID = stopped_HID_ID,
                        CmdID = "",
                        CSTID = "",
                        //DrivingDirection = 0,
                        //BatteryCapacity = 0,
                        //ChargeStatus = 0,
                        //BatteryTemperature = 0
                    };
                    WrapperMessage wrappers = new WrapperMessage
                    {
                        ID = WrapperMessage.StatusReqRespFieldNumber,
                        SeqNum = e.iSeqNum,
                        StatusReqResp = strSends
                    };
                    sw.Start();
                    Boolean resp_cmps = clientAgent.TrxTcpIp.SendGoogleMsg(wrappers, true);

                    sw.Stop();
                    if (Veh_VehM_Global.enCmdID == 0) // jason++ 190519 Avoid the cmd43 from VehC influence the thread controll of the form.cs which is used for dealing with the Battery Capicity.
                    {
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                    }
                }
                //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd43;
                /*
                 * Time Check
                 */
                string nowTime = recive_str.SystemTime;

                DateTime mesDateTime = DateTime.ParseExact(nowTime.Trim(), "yyyyMMddHHmmssff", CultureInfo.CurrentCulture);
                updateSystemTime(mesDateTime);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@TCPIP:" + " " + "143 send");
                Veh_VehM_Global.check143sendBeforeTimer = true;
            }
            catch (Exception ex)
            {
                ExceptionErrorLog(ex);
            }
        }
        public void updateSystemTime(DateTime hostTime)
        {
            SystemTime st = new SystemTime();
            st.FromDateTime(hostTime);
            SystemTime.SetSystemTime(ref st);
            SystemTime.GetSystemTime(ref st);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Set System Time:{0}", st.ToDateTime().ToString("yyyyMMddHHmmssff"));
        }
        public void str44_Receive(object sender, TcpIpEventArgs e)
        {
            // Here get the 144 first 
            ID_44_STATUS_CHANGE_RESPONSE recive_str = (ID_44_STATUS_CHANGE_RESPONSE)e.objPacket;
            ID_144_STATUS_CHANGE_REP send_str = null;

            //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd44;
        }
        public void str45_Receive(object sender, TcpIpEventArgs e)
        {
            ID_45_POWER_OPE_REQ recive_str = (ID_45_POWER_OPE_REQ)e.objPacket;
            ID_145_POWER_OPE_RESPONSE send_str = null;

            //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd45;

            OperatingPowerMode operatingPowerMode = recive_str.OperatingPowerMode;

            send_str = new ID_145_POWER_OPE_RESPONSE
            {
                ReplyCode = 0
            };

            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.PowerOpeRespFieldNumber,
                SeqNum = e.iSeqNum,
                PowerOpeResp = send_str
            };

            Boolean resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
            //vehicle.currentExcuteCMD_ID = cmd_id;


            Console.WriteLine("Received");
        }
        //public void str51_Receive(object sender, TcpIpEventArgs e)
        //{
        //    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str51_Receive : ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);

        //    ID_51_AVOID_REQUEST recive_str = (ID_51_AVOID_REQUEST)e.objPacket;
        //    ID_151_AVOID_RESPONSE send_str = null;

        //    Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd51;

        //    send_str = new ID_151_AVOID_RESPONSE
        //    {
        //        ReplyCode = 0
        //    };

        //    WrapperMessage wrapper = new WrapperMessage
        //    {
        //        ID = WrapperMessage.AvoidRespFieldNumber,
        //        SeqNum = e.iSeqNum,
        //        AvoidResp = send_str
        //    };

        //    Boolean resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
        //    //vehicle.currentExcuteCMD_ID = cmd_id;
        //    string[] passToDestinationAddress = recive_str.GuideAddresses.ToArray();
        //    string[] passToDestinationSections = recive_str.GuideSections.ToArray();
        //    Veh_VehM_Global.GuideSections = passToDestinationSections;
        //    Veh_VehM_Global.GuideAddressesToDestination = passToDestinationAddress;
        //    Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.AvoidRequest;

        //    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;


        //    //Console.WriteLine("Received");
        //}
        //public void str52_Receive(object sender, TcpIpEventArgs e)
        //{
        //    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : str52_Receive : ThreadID = {0}", Thread.CurrentThread.ManagedThreadId);

        //    ID_52_AVOID_COMPLETE_RESPONSE recive_str = (ID_52_AVOID_COMPLETE_RESPONSE)e.objPacket;
        //    ID_152_AVOID_COMPLETE_REPORT send_str = null;

        //    Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd52;

        //    send_str = new ID_152_AVOID_COMPLETE_REPORT
        //    {
        //        CmpStatus = 0
        //    };

        //    WrapperMessage wrapper = new WrapperMessage
        //    {
        //        ID = WrapperMessage.AvoidCompleteRepFieldNumber,
        //        SeqNum = e.iSeqNum,
        //        AvoidCompleteRep = send_str
        //    };

        //    //Boolean resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
        //    //vehicle.currentExcuteCMD_ID = cmd_id;


        //    Console.WriteLine("Received");
        //}
        public void str71_Receive(object sender, TcpIpEventArgs e)
        {
            ID_71_RANGE_TEACHING_REQUEST recive_str = (ID_71_RANGE_TEACHING_REQUEST)e.objPacket;
            ID_171_RANGE_TEACHING_RESPONSE send_str = new ID_171_RANGE_TEACHING_RESPONSE
            {
                ReplyCode = 0
            };
            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.RangeTeachingRespFieldNumber,
                SeqNum = e.iSeqNum,
                RangeTeachingResp = send_str
            };
            Boolean resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
            string[] section = { ReadCsv.Map.GetSection(recive_str.FromAdr, recive_str.ToAdr) };
            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
            //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd71;
            Veh_VehM_Global.FromAdr = recive_str.FromAdr;
            Veh_VehM_Global.ToAdr = recive_str.ToAdr;
            Veh_VehM_Global.Address = recive_str.ToAdr;
            Veh_VehM_Global.GuideSections = section;
            Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }
        public void str72_Receive(object sender, TcpIpEventArgs e)
        {
            ID_72_RANGE_TEACHING_COMPLETE_RESPONSE recive_str = (ID_72_RANGE_TEACHING_COMPLETE_RESPONSE)e.objPacket;
            ID_172_RANGE_TEACHING_COMPLETE_REPORT send_str = null;

            //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd72;
            //172 first;
        }
        public void str74_Receive(object sender, TcpIpEventArgs e)
        {
            ID_74_ADDRESS_TEACH_RESPONSE recive_str = (ID_74_ADDRESS_TEACH_RESPONSE)e.objPacket;
            ID_174_ADDRESS_TEACH_REPORT send_str = null;

            //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd74;
            //174 first
        }
        public void str91_Receive(object sender, TcpIpEventArgs e)
        {
            try
            {
                ID_91_ALARM_RESET_REQUEST recive_str = (ID_91_ALARM_RESET_REQUEST)e.objPacket;
                ID_191_ALARM_RESET_RESPONSE send_str = null;

                send_str = new ID_191_ALARM_RESET_RESPONSE
                {
                    ReplyCode = 0
                };

                WrapperMessage wrapper = new WrapperMessage
                {
                    ID = WrapperMessage.AlarmResetRespFieldNumber,
                    SeqNum = e.iSeqNum,
                    AlarmResetResp = send_str
                };

                Boolean resp_cmp = clientAgent.TrxTcpIp.SendGoogleMsg(wrapper, true);
                //vehicle.currentExcuteCMD_ID = cmd_id;
                string rtnMsg = string.Empty;
                //string errCodeS = "" + errCode;
                ID_194_ALARM_REPORT strSend;
                ID_94_ALARM_RESPONSE stRecv;
                strSend = new ID_194_ALARM_REPORT
                {
                    ErrCode = "0",
                    ErrStatus = 0
                };

                WrapperMessage wrapper1 = new WrapperMessage
                {
                    ID = WrapperMessage.AlarmRepFieldNumber,
                    AlarmRep = strSend
                };
                sw.Start();
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 191 Send .");

                com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper1, out stRecv, out rtnMsg);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 191 End .");
                sw.Stop();

                time = sw.ElapsedMilliseconds;

                sw.Reset();

                //if (time < timeOut)
                //{
                //    blTimeOut = false;
                //    return result == TrxTcpIp.ReturnCode.Normal;
                //}
                //else
                //{
                //    blTimeOut = true;
                //    Console.WriteLine("#{0}# TCP/IP Comm: TimeOut", DateTime.Now.ToString("HH:mm:ss.fff"));
                //    return result == TrxTcpIp.ReturnCode.Timeout;
                //}
                Console.WriteLine("Received");
            }
            catch (Exception ex)
            {
                ExceptionErrorLog(ex);
            }
        }
        public void str94_Receive(object sender, TcpIpEventArgs e)
        {
            ID_94_ALARM_RESPONSE recive_str = (ID_94_ALARM_RESPONSE)e.objPacket;
            ID_194_ALARM_REPORT send_str = null;
            //194 first
        }

        public bool sned_Str132(string cmdID, ActiveType actType,
            string cstID, int cmpCode, CompleteStatus cmpStatus, int cmddistance)
        {
            string rtnMsg = string.Empty;
            Veh_VehM_Global_Property.already_have_command_Check = false;
            ID_132_TRANS_COMPLETE_REPORT stSend;
            ID_32_TRANS_COMPLETE_RESPONSE stRecv;
            Veh_VehM_Global.cmdRunning = false;
            CompleteStatus Complete_Status;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 132 send back : cmd_Distance = {0}", cmddistance);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 132 send back : Veh_VehM_Global.hasCst = {0}", Veh_VehM_Global.hasCst.ToString());
            string cstIDfromlocal = "";
            if (Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist)
            {
                cstIDfromlocal = Veh_VehM_Global.CSTID_Load;
            }
            else
            {
                cstIDfromlocal = "";
            }
            ////VhLoadCSTStatus temp_hascst = VhLoadCSTStatus.NotExist;
            ////if (cstID != null)
            ////{
            ////    if(cstID != "")
            ////    {
            ////        temp_hascst = VhLoadCSTStatus.Exist;
            ////    }
            ////}

            Complete_Status = cmpStatus;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 132 send back : cmpStatus = {0}", cmpStatus.ToString());

            stSend = new ID_132_TRANS_COMPLETE_REPORT()
            {
                CmdID = cmdID,
                //ActType = actType,    //jason-- 180829
                CSTID = Veh_VehM_Global.cmd_CstIDfromOHTC,
                //CmpCode = cmpCode,
                CmpStatus = Complete_Status,
                CurrentSecID = Veh_VehM_Global.Section,
                CurrentAdrID = Veh_VehM_Global.Address,
                SecDistance = (uint)Veh_VehM_Global.DistanceFromSectionStart,
                //CmdPowerConsume = (uint)Veh_VehM_Global_Property.Cmd_Power_Consume_Check,
                CmdDistance = cmddistance,
                HasCST = Veh_VehM_Global.hasCst,
                CarCSTID = cstIDfromlocal
            };
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 132 send back : stsend is set. ");

            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.TransEventRepFieldNumber,
                TranCmpRep = stSend
            };

            sw.Start();
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 132 send back :Ready for sendRecv");

            com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper, out stRecv, out rtnMsg);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 132 send back :End for sendRecv");

            sw.Stop();

            time = sw.ElapsedMilliseconds;

            sw.Reset();

            if (time < timeOut)
            {
                blTimeOut = false;
                return result == TrxTcpIp.ReturnCode.Normal;
            }
            else
            {
                blTimeOut = true;
                Console.WriteLine("#{0}# TCP/IP Comm: TimeOut", DateTime.Now.ToString("HH:mm:ss.fff"));
                return result == TrxTcpIp.ReturnCode.Timeout;
            }


        }

        public bool sned_Str134(string cmdID, EventType eventType, string section, string address, string blockSec,
            VhGuideStatus leftGuide, VhGuideStatus rightGuide, VhStopSingle blockStatus, VhStopSingle pauseStatus,
            VhStopSingle obstStatus, VhLoadCSTStatus loadStatus, double sec_dist = 0)
        {
            string rtnMsg = string.Empty;

            ID_134_TRANS_EVENT_REP stSend;
            //ID_34_TRANS_EVENT_RESPONSE stRecv;        //jason-- 180829

            stSend = new ID_134_TRANS_EVENT_REP()
            {
                EventType = EventType.AdrPass, //jason++ 181019  
                CurrentAdrID = address,
                CurrentSecID = section,
                //HasCST = loadStatus,
                //ObstacleStatus = obstStatus,    //?
                //PauseStatus = pauseStatus,  //?               //jason-- 180829
                LeftGuideLockStatus = leftGuide,
                RightGuideLockStatus = rightGuide,
                SecDistance = (uint)sec_dist,
                //DrivingDirection = DriveDirction,
                //CtrDistance = 0,    //?
                //ObstDistance = 0,   //?
                //RequestBlockID = blockSec   //?
            };

            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.TransEventRepFieldNumber,
                TransEventRep = stSend
            };

            //sw.Start();
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : 134 send google start", Veh_VehM_Global.DriveDirection);

            //com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = clientAgent.TrxTcpIp.sendRecv_Google(wrapper, out stRecv, out rtnMsg); //jason-- 180830
            clientAgent.TrxTcpIp.SendGoogleMsg(wrapper);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : 134 send google end", Veh_VehM_Global.DriveDirection);

            // Waiting for reply
            //sw.Stop();

            time = sw.ElapsedMilliseconds;

            if (time > timeOut)
            {
                blTimeOut = true;
                sw.Reset();
                Console.WriteLine("#{0}#TCP/IP Comm: Time Out", DateTime.Now.ToString("HH:mm:ss.fff"));                   // Roy+180308
                //return result == TrxTcpIp.ReturnCode.Timeout;     //jason-- 180830
                return true;    //jason++ 180830
            }
            else
            {
                blTimeOut = false;
                sw.Reset();
            }

            //return result == TrxTcpIp.ReturnCode.Normal;  //jason-- 180830
            return true; //jason++ 180830
        }

        public bool sned_Str136(string cmdID, EventType eventType, string currentSecID, string currentAdrID, string[] reserveSections, string requestBlockID,
            string requestHIDID, VhLoadCSTStatus hasCST, string cstID, string releaseBlockAdrID, string releaseHIDAdrID,
            double sec_dist = 0, BCRReadResult bCRRead = BCRReadResult.BcrNormal, int PositionCheckTimes = 0)
        {
            try
            {
                string rtnMsg = string.Empty;
                //if(Veh_VehM_Global.check_recieve_36 == true)
                //{
                string cstID_136_local = "";
                if (eventType == EventType.Bcrread || eventType == EventType.LoadComplete)
                {
                    cstID_136_local = cstID;
                }
                ID_136_TRANS_EVENT_REP strSend;
                ID_36_TRANS_EVENT_RESPONSE stRecv;
                if (eventType == EventType.BlockRelease)
                {
                    requestBlockID = "";
                }
                strSend = new ID_136_TRANS_EVENT_REP
                {
                    EventType = eventType,
                    RequestBlockID = requestBlockID,
                    RequestHIDID = requestHIDID,
                    //HasCST = hasCST,
                    CSTID = cstID_136_local,
                    ReleaseBlockAdrID = releaseBlockAdrID,
                    ReleaseHIDAdrID = releaseHIDAdrID,
                    CurrentAdrID = currentAdrID,
                    CurrentSecID = currentSecID,
                    SecDistance = (uint)sec_dist,
                    BCRReadResult = bCRRead,
                    PositionChcekTimes = (uint)PositionCheckTimes
                };
                Veh_VehM_Global.block_continue_section = requestBlockID;
                WrapperMessage wrapper = new WrapperMessage
                {
                    ID = WrapperMessage.ImpTransEventRepFieldNumber,
                    ImpTransEventRep = strSend
                };
                //sw.Start();
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 136 Send .");
                com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper, out stRecv, out rtnMsg);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 136 End 1.");
                if (eventType == EventType.Bcrread)
                {
                    switch (stRecv.ReplyActiveType)
                    {
                        case CMDCancelType.CmdCancelIdMismatch:
                            Veh_VehM_Global.check_4_BCRread = false;
                            Veh_VehM_Global.start_loading_or_unloading = false;
                            Veh_VehM_Global.CancelType4Report = CMDCancelType.CmdCancelIdMismatch;
                            break;
                        case CMDCancelType.CmdCancelIdReadFailed:
                            Veh_VehM_Global.check_4_BCRread = false;
                            Veh_VehM_Global.start_loading_or_unloading = false;
                            Veh_VehM_Global.CancelType4Report = CMDCancelType.CmdCancelIdReadFailed;
                            break;
                        case CMDCancelType.CmdCancelIdReadDuplicate:
                            Veh_VehM_Global.check_4_BCRread = false;
                            Veh_VehM_Global.start_loading_or_unloading = false;
                            Veh_VehM_Global.CancelType4Report = CMDCancelType.CmdCancelIdReadDuplicate;
                            break;
                        case CMDCancelType.CmdNone:
                            Veh_VehM_Global.check_4_BCRread = true;
                            Veh_VehM_Global.CancelType4Report = CMDCancelType.CmdNone;
                            break;
                    }
                }
                else if(eventType == EventType.BlockReq)
                {
                    Veh_VehM_Global.vehBlockPassReply_BlockReq = (int)stRecv.IsBlockPass;
                }
                if (stRecv.RenameCarrierID != "")
                {
                    Veh_VehM_Global.CSTID_Load = stRecv.RenameCarrierID;
                }

                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 136 End 2 .");


                //sw.Stop();
                return true; //jason++ 180830
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }

        public bool sned_Str143(string currentAdrID, string currentSecID, VHModeStatus modeStatus, VHActionStatus actionStatus,
            VhPowerStatus powerStatus, VhStopSingle obstacleStatus, VhStopSingle blockingStatus, VhStopSingle hidStatus,
            VhStopSingle pauseStatus, VhStopSingle errorStatus, int secDistance, int obstDistance, string obstVehicleID,
            string stoppedBlockID, string stoppedHIDID, int batteryCapicity)
        {
            string rtnMsg = string.Empty;

            ID_143_STATUS_RESPONSE strSend;
            ID_43_STATUS_REQUEST stRecv;
            actionStatus = 0;
            if (Veh_VehM_Global.cmdRunning == true)
            {
                actionStatus = (VHActionStatus)1;
            }
            strSend = new ID_143_STATUS_RESPONSE
            {
                CurrentAdrID = currentAdrID,
                CurrentSecID = currentSecID,
                ModeStatus = modeStatus,
                ActionStatus = actionStatus,
                PowerStatus = powerStatus,
                ObstacleStatus = obstacleStatus,
                BlockingStatus = blockingStatus,
                //HIDStatus = hidStatus,
                PauseStatus = pauseStatus,
                ErrorStatus = errorStatus,
                SecDistance = (uint)secDistance,
                ObstDistance = obstDistance,
                ObstVehicleID = obstVehicleID,
                StoppedBlockID = stoppedBlockID,
                //StoppedHIDID = stoppedHIDID,
                CmdID = Veh_VehM_Global.cmdID,
                CSTID = Veh_VehM_Global.CSTID_Load,
                //BatteryCapacity = (uint)batteryCapicity,
            };

            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.StatusReqRespFieldNumber,
                StatusReqResp = strSend
            };
            sw.Start();
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 143 Send .");

            com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper, out stRecv, out rtnMsg);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 143 End .");

            sw.Stop();

            time = sw.ElapsedMilliseconds;

            sw.Reset();

            if (time < timeOut)
            {
                blTimeOut = false;
                return result == TrxTcpIp.ReturnCode.Normal;
            }
            else
            {
                blTimeOut = true;
                Console.WriteLine("#{0}# TCP/IP Comm: TimeOut", DateTime.Now.ToString("HH:mm:ss.fff"));
                return result == TrxTcpIp.ReturnCode.Timeout;
            }
        }

        public bool sned_Str144(string cmdID, string currAdr, string currSec, VHModeStatus mStatus, VHActionStatus aStatus,
                        VhPowerStatus powerStatus, VhLoadCSTStatus lStatus, VhStopSingle oStatus,
                        VhStopSingle bStatus, VhStopSingle pauseStatus, VhGuideStatus leftGuide,
                        VhGuideStatus rightGuide, int sec_Dist, int BatteryCapacity, int BatteryTemperature, int SteeringWheel, VhStopSingle errorStatus = VhStopSingle.StopSingleOff)
        {
            string rtnMsg = string.Empty;
            VHActionStatus actionStatus;
            ID_144_STATUS_CHANGE_REP stSend;
            ID_44_STATUS_CHANGE_RESPONSE stRecv;
            string local_144_CstId = "";
            if (lStatus == VhLoadCSTStatus.Exist)
            {
                local_144_CstId = Veh_VehM_Global.CSTID_Load;
            }

            VhStopSingle earthquake_signal = VhStopSingle.StopSingleOff;
            VhStopSingle safety_signal = VhStopSingle.StopSingleOff;
            VhStopSingle HID_signal = VhStopSingle.StopSingleOff;
            VhStopSingle temp_normal_pause = new VhStopSingle();
            if (pauseStatus == VhStopSingle.StopSingleOn)
            {
                if (Veh_VehM_Global.earthquake_pause == true)
                {
                    earthquake_signal = VhStopSingle.StopSingleOn;
                }
                if (Veh_VehM_Global.safety_pause == true)
                {
                    safety_signal = VhStopSingle.StopSingleOn;
                }
                if (Veh_VehM_Global.HID_pause == true)
                {
                    HID_signal = VhStopSingle.StopSingleOn;
                }
                if (Veh_VehM_Global.normal_pause == true)
                {
                    temp_normal_pause = VhStopSingle.StopSingleOn;
                }
            }

            try
            {
                actionStatus = 0;
                if (Veh_VehM_Global.cmdRunning == true)
                {
                    actionStatus = (VHActionStatus)1;
                }
                stSend = new ID_144_STATUS_CHANGE_REP
                {
                    CurrentAdrID = currAdr,
                    CurrentSecID = currSec,
                    ModeStatus = mStatus,
                    ActionStatus = actionStatus,
                    PowerStatus = powerStatus,
                    HasCST = lStatus, //jason-- 180830
                    ObstacleStatus = oStatus,
                    //ReserveStatus = 0,

                    PauseStatus = temp_normal_pause,
                    ErrorStatus = errorStatus,
                    CmdID = Veh_VehM_Global.cmdID,
                    CSTID = local_144_CstId,
                    SecDistance = (uint)sec_Dist,

                    StoppedBlockID = "",
                    StoppedHIDID = "",
                    BlockingStatus = bStatus,
                    HIDStatus = HID_signal,
                    EarthquakePauseTatus = earthquake_signal,
                    SafetyPauseStatus = safety_signal
                };

                WrapperMessage wrapper = new WrapperMessage
                {
                    ID = WrapperMessage.StatueChangeRepFieldNumber,
                    StatueChangeRep = stSend
                };

                //sw.Start();
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : 144 send google start", Veh_VehM_Global.DriveDirection);
                clientAgent.TrxTcpIp.SendGoogleMsg(wrapper);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : 144 send google end", Veh_VehM_Global.DriveDirection);
                //sw.Stop();

                time = sw.ElapsedMilliseconds;

                if (time > timeOut)
                {
                    blTimeOut = true;
                    sw.Reset();
                    Console.WriteLine("#{0}#TCP/IP Comm: Time Out", DateTime.Now.ToString("HH:mm:ss.fff"));                   // Roy+180308
                                                                                                                              //return result == TrxTcpIp.ReturnCode.Timeout;     //jason-- 180830
                    return true;    //jason++ 180830
                }
                else
                {
                    blTimeOut = false;
                    sw.Reset();
                }

                //return result == TrxTcpIp.ReturnCode.Normal;  //jason-- 180830
                return true; //jason++ 180830
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }
        //public bool sned_Str152(int Cmp_Code)
        //{
        //    string rtnMsg = string.Empty;
        //    ID_52_AVOID_COMPLETE_RESPONSE stRecv;
        //    ID_152_AVOID_COMPLETE_REPORT strSend;

        //    strSend = new ID_152_AVOID_COMPLETE_REPORT
        //    {
        //        CmpStatus = Cmp_Code
        //    };

        //    WrapperMessage wrapper = new WrapperMessage
        //    {
        //        ID = WrapperMessage.AvoidCompleteRepFieldNumber,
        //        AvoidCompleteRep = strSend
        //    };
        //    sw.Start();

        //    com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper, out stRecv, out rtnMsg);

        //    sw.Stop();

        //    time = sw.ElapsedMilliseconds;

        //    sw.Reset();

        //    if (time < timeOut)
        //    {
        //        blTimeOut = false;
        //        return result == TrxTcpIp.ReturnCode.Normal;
        //    }
        //    else
        //    {
        //        blTimeOut = true;
        //        Console.WriteLine("#{0}# TCP/IP Comm: TimeOut", DateTime.Now.ToString("HH:mm:ss.fff"));
        //        return result == TrxTcpIp.ReturnCode.Timeout;
        //    }
        //}
        public bool sned_Str172(int completeCode)
        {
            string rtnMsg = string.Empty;
            ID_72_RANGE_TEACHING_COMPLETE_RESPONSE stRecv;
            ID_172_RANGE_TEACHING_COMPLETE_REPORT send_str = new ID_172_RANGE_TEACHING_COMPLETE_REPORT
            {
                FromAdr = Veh_VehM_Global.FromAdr,
                ToAdr = Veh_VehM_Global.ToAdr,
                SecDistance = Convert.ToUInt32(Veh_VehM_Global_Property.cmd_Length_Check),
                CompleteCode = completeCode
            };
            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.RangeTeachingCmpRepFieldNumber,
                //SeqNum = Veh_VehM_Global.iSeqNum,
                RangeTeachingCmpRep = send_str
            };

            sw.Start();
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 172 Send .");

            com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper, out stRecv, out rtnMsg);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 172 End .");

            sw.Stop();

            time = sw.ElapsedMilliseconds;

            sw.Reset();

            if (time < timeOut)
            {
                blTimeOut = false;
                return result == TrxTcpIp.ReturnCode.Normal;
            }
            else
            {
                blTimeOut = true;
                Console.WriteLine("#{0}# TCP/IP Comm: TimeOut", DateTime.Now.ToString("HH:mm:ss.fff"));
                return result == TrxTcpIp.ReturnCode.Timeout;
            }
        }

        public bool sned_Str174(string addr, int position)
        {
            string rtnMsg = string.Empty;
            ID_74_ADDRESS_TEACH_RESPONSE stRecv;
            ID_174_ADDRESS_TEACH_REPORT strSend;

            strSend = new ID_174_ADDRESS_TEACH_REPORT
            {
                Addr = addr,
                Position = position
            };

            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.AddressTeachRepFieldNumber,
                AddressTeachRep = strSend
            };
            sw.Start();
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 174 Send .");

            com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper, out stRecv, out rtnMsg);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 174 End .");

            sw.Stop();

            time = sw.ElapsedMilliseconds;

            sw.Reset();

            if (time < timeOut)
            {
                blTimeOut = false;
                return result == TrxTcpIp.ReturnCode.Normal;
            }
            else
            {
                blTimeOut = true;
                Console.WriteLine("#{0}# TCP/IP Comm: TimeOut", DateTime.Now.ToString("HH:mm:ss.fff"));
                return result == TrxTcpIp.ReturnCode.Timeout;
            }
        }

        public bool sned_Str194(int errCode, ErrorStatus errorStatus)
        {
            string rtnMsg = string.Empty;
            string errCodeS = "" + errCode;
            if (errCodeS.Substring(0, 1) == "-")
            {
                errCodeS = errCodeS.Substring(1);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : Str194 no minus errCodeS = {0}.", errCodeS);
            }
            else
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data : Str194 keep minus errCodeS = {0}.", errCodeS);
            }
            ID_194_ALARM_REPORT strSend;
            ID_94_ALARM_RESPONSE stRecv;
            strSend = new ID_194_ALARM_REPORT
            {
                ErrCode = errCodeS,
                ErrStatus = errorStatus
            };

            WrapperMessage wrapper = new WrapperMessage
            {
                ID = WrapperMessage.AlarmRepFieldNumber,
                AlarmRep = strSend
            };
            sw.Start();
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 194 Send .");

            com.mirle.iibg3k0.ttc.Common.TrxTcpIp.ReturnCode result = snedRecv(wrapper, out stRecv, out rtnMsg);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In 194 End .");

            sw.Stop();

            time = sw.ElapsedMilliseconds;

            sw.Reset();

            if (time < timeOut)
            {
                blTimeOut = false;
                return result == TrxTcpIp.ReturnCode.Normal;
            }
            else
            {
                blTimeOut = true;
                Console.WriteLine("#{0}# TCP/IP Comm: TimeOut", DateTime.Now.ToString("HH:mm:ss.fff"));
                return result == TrxTcpIp.ReturnCode.Timeout;
            }
        }

        private void CreatTcpIpClientAgent()
        {
            #region config xml
            string configPath = "D:\\UserData\\Veh" + "\\config\\";
            XDocument ipsetting = XDocument.Load(configPath + "ipsetting.xml");
            int clientNum = int.Parse(ipsetting.Element("Domain").Element("clientNum").Value);
            string clientName = ipsetting.Element("Domain").Element("clientName").Value;
            string sRemoteIP = ipsetting.Element("Domain").Element("sRemoteIP").Value;
            string sRemotePort = ipsetting.Element("Domain").Element("sRemotePort").Value;
            string sLocalIP = ipsetting.Element("Domain").Element("sLocalIP").Value;
            string sLocalPort = ipsetting.Element("Domain").Element("sLocalPort").Value;
            int reconnection_interval_ms = int.Parse(ipsetting.Element("Domain").Element("reconnection_interval_ms").Value);  //斷線多久之後再進行一次嘗試恢復連線的動作
            //int max_reconnection_count = int.Parse(ipsetting.Element("Domain").Element("max_reconnection_count").Value);  //斷線後最多嘗試幾次重新恢復連線 (若設定為0則不進行自動重新連線)
            int max_reconnection_count = 9999;
            //int retry_count = int.Parse(ipsetting.Element("Domain").Element("retry_count").Value);  //SendRecv Time out後要再重複發送的次數
            int retry_count = 2;
            #endregion
            int iRemotePort = int.Parse(sRemotePort);
            int iLocalPort = int.Parse(sLocalPort);

            int recv_timeout_ms = (int)timeOut;           //等待sendRecv Reply的Time out時間(milliseconds)
            int send_timeout_ms = 0;               //暫時無用
            int max_readSize = 0;                  //暫時無用


            clientAgent = new TcpIpAgent(clientNum, clientName,
                sLocalIP, iLocalPort, sRemoteIP, iRemotePort,
                TcpIpAgent.TCPIP_AGENT_COMM_MODE.CLINET_MODE
                  , recv_timeout_ms, send_timeout_ms, max_readSize, reconnection_interval_ms,
                  max_reconnection_count, retry_count, AppConstants.FrameBuilderType.PC_TYPE_MIRLE);
        }
        private void cleanMonthLogFunc()
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "~~~~~~~~Start delete Log~~~~~~~~");
            int DayBefore = Veh_VehM_Global.timeout_Config.deleteDueDate;//刪除X天前資料
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : cleanMonthLogFunc : DayBefore = {0} .", DayBefore.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : cleanMonthLogFunc : deleteDueDate = {0} .", Veh_VehM_Global.timeout_Config.deleteDueDate.ToString());
            DeleteOldFiles(@"D:\Log\Process", DayBefore);
            DeleteOldFiles(@"D:\Log\Monitor", DayBefore);
            DeleteOldFiles(@"D:\Log\PROC_300VehM\logs", DayBefore);
            DeleteOldFiles(@"D:\Log\VehM_Sim_Log", DayBefore);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "~~~~~~~~~End delete Log~~~~~~~~~");
        }
        void DeleteOldFiles(string dir, int days)
        {
            string[] Files;
            List<string> lsFlies = new List<string>();
            try
            {
                Files = Directory.GetDirectories(dir);
                if (!Directory.Exists(dir)) return;
                var now = DateTime.Now;

                foreach (var file in Files)//砍子資料夾用
                {
                    DirectoryInfo di = new DirectoryInfo(file);
                    var t = File.GetCreationTime(file);
                    var elapsedTicks = now.Ticks - t.Ticks;
                    var elapsedSpan = new TimeSpan(elapsedTicks);
                    if (elapsedSpan.TotalDays > days)
                    {
                        di.Delete(true);
                    }
                }
                foreach (var f in Directory.GetFileSystemEntries(dir).Where(f => File.Exists(f)))//各個資料夾砍資料
                {
                    var t = File.GetCreationTime(f);

                    var elapsedTicks = now.Ticks - t.Ticks;
                    var elapsedSpan = new TimeSpan(elapsedTicks);

                    if (elapsedSpan.TotalDays > days) File.Delete(f);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
            }
        }
        /// <summary>
        /// 註冊要監聽的事件
        /// </summary>
        void registeredEvent()
        {
            // Add Event Handlers for all the recieved messages
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.TransReqFieldNumber, str31_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.TranCmpRespFieldNumber, str32_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.ControlZoneReqFieldNumber, str33_Receive);
            //clientAgent.addTcpIpReceivedHandler(WrapperMessage.CSTIDRenameReqFieldNumber, str35_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.ImpTransEventRespFieldNumber, str36_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.TransCancelReqFieldNumber, str37_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.PauseReqFieldNumber, str39_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.ModeChangeReqFieldNumber, str41_Recieve);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.StatusReqFieldNumber, str43_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.StatusChangeRespFieldNumber, str44_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.PowerOpeReqFieldNumber, str45_Receive);
            //clientAgent.addTcpIpReceivedHandler(WrapperMessage.AvoidReqFieldNumber, str51_Receive);
            //clientAgent.addTcpIpReceivedHandler(WrapperMessage.AvoidCompleteRespFieldNumber, str52_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.RangeTeachingReqFieldNumber, str71_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.RangeTeachingCmpRespFieldNumber, str72_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.AddressTeachRespFieldNumber, str74_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.AlarmResetReqFieldNumber, str91_Receive);
            clientAgent.addTcpIpReceivedHandler(WrapperMessage.AlarmRespFieldNumber, str94_Receive);
            //
            //Here need to be careful for the TCPIP
            //

            clientAgent.addTcpIpConnectedHandler(Connection);       //連線時的通知
            clientAgent.addTcpIpDisconnectedHandler(Disconnection); //斷線時的通知
        }

        protected void Connection(object sender, TcpIpEventArgs e)
        {
            TcpIpAgent agent = sender as TcpIpAgent;
            Console.WriteLine("Vh ID:{0}, connection.", agent.Name);
        }

        protected void Disconnection(object sender, TcpIpEventArgs e)
        {
            TcpIpAgent agent = sender as TcpIpAgent;
            Console.WriteLine("Vh ID:{0}, disconnection.", agent.Name);
        }
        #region 需要注入的解封包使用的Function
        public static Google.Protobuf.IMessage unPackWrapperMsg(byte[] raw_data)
        {
            WrapperMessage WarpperMsg = ToObject<WrapperMessage>(raw_data);
            return WarpperMsg;
        }

        public static T ToObject<T>(byte[] buf) where T : Google.Protobuf.IMessage<T>, new()
        {
            if (buf == null)
                return default(T);

            Google.Protobuf.MessageParser<T> parser = new Google.Protobuf.MessageParser<T>(() => new T());
            return parser.ParseFrom(buf);
        }
        #endregion 需要注入的解封包使用的Function
        protected void OnEventMsgFromVehM(ReportMsgEventArg e)
        {
            if (eventMsgFromVehM != null)
            {
                eventMsgFromVehM(this, e);
            }
        }

        protected void GetMsgString(string fromAdr, string toAdr, string CST_ID, ref string msg)
        {
            string sGuide = string.Empty, sCycle = string.Empty;
            if (Veh_VehM_Global.GuideSections != null)
            {
                foreach (string str in Veh_VehM_Global.GuideSections)
                {
                    sGuide = sGuide + str + "_";
                }
            }
            if (Veh_VehM_Global.CycleSections != null)
            {
                foreach (string str in Veh_VehM_Global.CycleSections)
                {
                    sCycle = sCycle + str + "_";
                }
            }
            msg = string.Format(@"Cmd_ID: {0},
                            Active Type: {1},
                            From Address: {2},
                            To Address: {3},
                            Guide Sections:{4},
                            Cycle Sections:{5},
                            CST_ID: {6}",
                            Veh_VehM_Global.enCmdID, Veh_VehM_Global.NowActiveType.ToString(),
                            fromAdr, toAdr, sGuide, sCycle, CST_ID.ToString());
        }

    }


    public class Cmd31EventArg : EventArgs
    {
        public Cmd31EventArg()
        {

        }
    }


    public class VHMSGIF
    {
        public const int LEN_MESSAGE_SIZE = 100;
        public const int LEN_ITEM_CSTID = 16;
        public const int LEN_ITEM_DATETIME = 14;
        public const int LEN_ITEM_TESTDATA = 8;
        //public const int LEN_ITEM_PASSSEGMENT = 4;
        public const int LEN_ITEM_PASSSEGMENT = 6;

        public const ushort ITEM_RESPCODE_OK = 0;
        public const ushort ITEM_RESPCODE_NG = 1;

        public const ushort ITEM_MULTIFLAG_LAST = 0;
        public const ushort ITEM_MULTIFLAG_CONT = 1;

        // Controller -> Vehicle Packet ID = [1 - 99]
        // Vehicle -> Controller Packet ID = [101 - 199]
        public const int ID_NONE = 0;
        public const int ID_HOST_KISO_VERSION_REPORT = 1;
        public const int ID_HOST_KISO_VERSION_RESPONSE = 101;

        public const int ID_VHCL_KISO_VERSION_REPORT = 102;
        public const int ID_VHCL_KISO_VERSION_RESPONSE = 2;

        public const int ID_KISO_LIST_COUNT_REPORT = 11;
        public const int ID_KISO_LIST_COUNT_RESPONSE = 111;

        public const int ID_KISO_TRAVEL_REPORT = 13;
        public const int ID_KISO_TRAVEL_RESPONSE = 113;

        public const int ID_KISO_SECTION_REPORT = 15;
        public const int ID_KISO_SECTION_RESPONSE = 115;

        public const int ID_KISO_ADDRESS_REPORT = 17;
        public const int ID_KISO_ADDRESS_RESPONSE = 117;

        public const int ID_KISO_SCALE_REPORT = 19;
        public const int ID_KISO_SCALE_RESPONSE = 119;

        public const int ID_KISO_CONTROL_REPORT = 21;
        public const int ID_KISO_CONTROL_RESPONSE = 121;

        public const int ID_KISO_GUIDE_REPORT = 23;
        public const int ID_KISO_GUIDE_RESPONSE = 123;

        public const int ID_KISO_GRIPPER_REPORT = 25;
        public const int ID_KISO_GRIPPER_RESPONSE = 125;

        public const int ID_TRANS_REQUEST = 31;
        public const int ID_TRANS_REQUEST_RESPONSE = 131;

        public const int ID_TRANS_COMPLETE_REPORT = 132;
        public const int ID_TRANS_COMPLETE_RESPONSE = 32;

        public const int ID_TRANS_EVENT_REPORT = 134;
        public const int ID_TRANS_EVENT_RESPONSE = 34;

        public const int ID_TRANS_CHANGE_REQUEST = 35;
        public const int ID_TRANS_CHANGE_RESPONSE = 135;

        public const int ID_TRANS_CANCEL_REQUEST = 37;
        public const int ID_TRANS_CANCEL_RESPONSE = 137;

        public const int ID_PAUSE_REQUEST = 39;
        public const int ID_PAUSE_RESPONSE = 139;

        public const int ID_MODE_CHANGE_REQUEST = 41;
        public const int ID_MODE_CHANGE_RESPONSE = 141;

        public const int ID_STATUS_REQUEST = 43;
        public const int ID_STATUS_RESPONSE = 143;

        public const int ID_STATUS_CHANGE_REPORT = 144;
        public const int ID_STATUS_CHANGE_RESPONSE = 44;

        public const int ID_POWER_OPE_REQUEST = 45;
        public const int ID_POWER_OPE_RESPONSE = 145;

        public const int ID_INDIVIDUAL_DATA_UPLOAD_REQUEST = 61;
        public const int ID_INDIVIDUAL_DATA_UPLOAD_REPORT = 161;

        public const int ID_INDIVIDUAL_DATA_DOWNLOAD_REQUEST = 162;
        public const int ID_INDIVIDUAL_DATA_DOWNLOAD_REPORT = 62;

        public const int ID_INDIVIDUAL_DATA_CHANGE_REQUEST = 63;
        public const int ID_INDIVIDUAL_DATA_CHANGE_RESPONSE = 163;

        public const int ID_SECTION_TEACH_REQUEST = 71;
        public const int ID_SECTION_TEACH_RESPONSE = 171;

        public const int ID_SECTION_TEACH_COMPLETE_REPORT = 172;
        public const int ID_SECTION_TEACH_COMPLETE_RESPONSE = 72;

        public const int ID_ADDRESS_TEACH_REPORT = 174;
        public const int ID_ADDRESS_TEACH_RESPONSE = 74;

        public const int ID_ALARM_RESET_REQUEST = 91;
        public const int ID_ALARM_RESET_RESPONSE = 191;

        public const int ID_ALARM_REPORT = 194;
        public const int ID_ALARM_RESPONSE = 94;

        public const int ID_LOG_UPLOAD_REQUEST = 95;
        public const int ID_LOG_UPLOAD_RESPONSE = 195;

        public const int ID_LOG_DATA_REPORT = 196;
        public const int ID_LOG_DATA_RESPONSE = 96;

        public const int ID_VHCL_COMM_TEST_REQUEST = 198;
        public const int ID_VHCL_COMM_TEST_REPORT = 98;

        public const int ID_HOST_COMM_TEST_REQUEST = 99;
        public const int ID_HOST_COMM_TEST_REPORT = 199;

        public static string[] ID_NAMES;

        public static void PrcInitializeIDNames()
        {
            string sWk = "";

            ID_NAMES = new string[256];

            for (int ii = 0; ii < ID_NAMES.Length; ii++)
            {
                switch (ii)
                {
                    case ID_HOST_KISO_VERSION_REPORT: sWk = "[  1]Host Version Rep."; break;
                    case ID_HOST_KISO_VERSION_RESPONSE: sWk = "[101]Host Version Resp."; break;
                    case ID_VHCL_KISO_VERSION_REPORT: sWk = "[102]Vehicle Version Rep."; break;
                    case ID_VHCL_KISO_VERSION_RESPONSE: sWk = "[  2]Vehicle Version Resp."; break;
                    case ID_KISO_LIST_COUNT_REPORT: sWk = "[ 11]Kiso ListCount Rep."; break;
                    case ID_KISO_LIST_COUNT_RESPONSE: sWk = "[111]Kiso ListCount Resp."; break;
                    case ID_KISO_TRAVEL_REPORT: sWk = "[ 13]Kiso Travel Rep."; break;
                    case ID_KISO_TRAVEL_RESPONSE: sWk = "[113]Kiso Travel Resp."; break;
                    case ID_KISO_SECTION_REPORT: sWk = "[ 15]Kiso Section Rep."; break;
                    case ID_KISO_SECTION_RESPONSE: sWk = "[115]Kiso Section Resp."; break;
                    case ID_KISO_ADDRESS_REPORT: sWk = "[ 17]Kiso Address Rep."; break;
                    case ID_KISO_ADDRESS_RESPONSE: sWk = "[117]Kiso Address Resp."; break;
                    case ID_KISO_SCALE_REPORT: sWk = "[ 19]Kiso Scale Rep."; break;
                    case ID_KISO_SCALE_RESPONSE: sWk = "[119]Kiso Scale Resp."; break;
                    case ID_KISO_CONTROL_REPORT: sWk = "[ 21]Kiso Control Rep."; break;
                    case ID_KISO_CONTROL_RESPONSE: sWk = "[121]Kiso Control Resp."; break;
                    case ID_KISO_GUIDE_REPORT: sWk = "[ 23]Kiso Guide Rep."; break;
                    case ID_KISO_GUIDE_RESPONSE: sWk = "[123]Kiso Guide Resp."; break;
                    case ID_TRANS_REQUEST: sWk = "[ 31]Trans Req."; break;
                    case ID_TRANS_REQUEST_RESPONSE: sWk = "[131]Trans Resp."; break;
                    case ID_TRANS_COMPLETE_REPORT: sWk = "[132]TransComp Rep."; break;
                    case ID_TRANS_COMPLETE_RESPONSE: sWk = "[ 32]TransComp Resp."; break;
                    case ID_TRANS_EVENT_REPORT: sWk = "[134]TransEvent Rep."; break;
                    case ID_TRANS_EVENT_RESPONSE: sWk = "[ 34]TransEvent Resp."; break;
                    case ID_TRANS_CHANGE_REQUEST: sWk = "[ 35]TransChange Req."; break;
                    case ID_TRANS_CHANGE_RESPONSE: sWk = "[135]TransChange Resp."; break;
                    case ID_TRANS_CANCEL_REQUEST: sWk = "[ 37]TransCancel Req."; break;
                    case ID_TRANS_CANCEL_RESPONSE: sWk = "[137]TransCancel Resp."; break;
                    case ID_PAUSE_REQUEST: sWk = "[ 39]Pause Req."; break;
                    case ID_PAUSE_RESPONSE: sWk = "[139]Pause Resp."; break;
                    case ID_MODE_CHANGE_REQUEST: sWk = "[ 41]ModeChange Req."; break;
                    case ID_MODE_CHANGE_RESPONSE: sWk = "[141]ModeChange Resp."; break;
                    case ID_STATUS_REQUEST: sWk = "[ 43]Status Req."; break;
                    case ID_STATUS_RESPONSE: sWk = "[143]Status Resp."; break;
                    case ID_STATUS_CHANGE_REPORT: sWk = "[144]StatusChange Rep."; break;
                    case ID_STATUS_CHANGE_RESPONSE: sWk = "[ 44]StatusChange Resp."; break;
                    case ID_POWER_OPE_REQUEST: sWk = "[ 45]PowerOpe Req."; break;
                    case ID_POWER_OPE_RESPONSE: sWk = "[145]PowerOpe Resp."; break;
                    case ID_INDIVIDUAL_DATA_UPLOAD_REQUEST: sWk = "[ 61]IndividualUp Req."; break;
                    case ID_INDIVIDUAL_DATA_UPLOAD_REPORT: sWk = "[161]IndividualUp Rep."; break;
                    case ID_INDIVIDUAL_DATA_DOWNLOAD_REQUEST: sWk = "[162]IndividualDown Req."; break;
                    case ID_INDIVIDUAL_DATA_DOWNLOAD_REPORT: sWk = "[ 62]IndividualDown Rep."; break;
                    case ID_INDIVIDUAL_DATA_CHANGE_REQUEST: sWk = "[ 63]IndividualChange Req."; break;
                    case ID_INDIVIDUAL_DATA_CHANGE_RESPONSE: sWk = "[163]IndividualChange Resp."; break;
                    case ID_SECTION_TEACH_REQUEST: sWk = "[ 71]SectionTeach Req."; break;
                    case ID_SECTION_TEACH_RESPONSE: sWk = "[171]SectionTeach Resp."; break;
                    case ID_SECTION_TEACH_COMPLETE_REPORT: sWk = "[172]SectionTeachComp Rep."; break;
                    case ID_SECTION_TEACH_COMPLETE_RESPONSE: sWk = "[ 72]SectionTeachComp Resp."; break;
                    case ID_ADDRESS_TEACH_REPORT: sWk = "[174]AddressTeach Rep."; break;
                    case ID_ADDRESS_TEACH_RESPONSE: sWk = "[ 74]AddressTeach Resp."; break;
                    case ID_ALARM_RESET_REQUEST: sWk = "[ 91]AlarmReset Req."; break;
                    case ID_ALARM_RESET_RESPONSE: sWk = "[191]AlarmReset Resp."; break;
                    case ID_ALARM_REPORT: sWk = "[194]Alarm Rep."; break;
                    case ID_ALARM_RESPONSE: sWk = "[ 94]Alarm Resp."; break;
                    case ID_LOG_UPLOAD_REQUEST: sWk = "[ 95]LogUpload Req."; break;
                    case ID_LOG_UPLOAD_RESPONSE: sWk = "[195]LogUpload Resp."; break;
                    case ID_LOG_DATA_REPORT: sWk = "[196]LogData Rep."; break;
                    case ID_LOG_DATA_RESPONSE: sWk = "[ 96]LogData Resp."; break;
                    case ID_VHCL_COMM_TEST_REQUEST: sWk = "[198]Vehicle CommTest Req."; break;
                    case ID_VHCL_COMM_TEST_REPORT: sWk = "[ 98]Vehicle CommTest Resp."; break;
                    case ID_HOST_COMM_TEST_REQUEST: sWk = "[ 99]Host CommTest Req."; break;
                    case ID_HOST_COMM_TEST_REPORT: sWk = "[199]Host CommTest Resp."; break;
                    default: sWk = "[   ]Not Defined"; break;
                }
                ID_NAMES[ii] = sWk.PadRight(32);
            }
        }

    }

}
