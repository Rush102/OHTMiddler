﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OHTM;
using OHTM.StatusMachine;
using Veh_HandShakeData;
using TcpIpClientSample;
using MirleOHT.類別.DCPS;
using OHTM.ReadCsv;
using System.Threading;
using OHTM.NLog_USE;
using System.Diagnostics;

namespace OHTM
{
    public partial class Form1 : Form
    {
        bool ForLog = false; //Jason++ 190519 Add for not print too much same log;
        //++++++++++++++++++++++++++++++++++++++++++                    // Roy-+171204
        //MotionInfo_Client motionInfoClientData = new MotionInfo_Client();
        //MotionInfo_Server motionInfoServerData = new MotionInfo_Server();
        //++++++++++++++++++++++++++++++++++++++++++                 
        MotionInfo_Vehicle_Comm vehCommData = new MotionInfo_Vehicle_Comm();
        MotionInfo_Vehicle_Inter_Comm_ReportData vehInterCommReptData = new MotionInfo_Vehicle_Inter_Comm_ReportData();
        MotionInfo_Inter_Comm_SendData vehInterCommSendData = new MotionInfo_Inter_Comm_SendData();
        MotionInfo_HandShake_RecieveData vehHandShakeRxData = new MotionInfo_HandShake_RecieveData();
        MotionInfo_HandShake_SendData vehHandShakeSendData = new MotionInfo_HandShake_SendData();
        string veh_Current_Address, veh_Current_Section;
        double veh_Offset;
        enum Status : int
        {
            OK,
            NG,
            TimeOut
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CsvGlobal.NowFileDir = CsvGlobal.SectionPath;
            //new ReadCsv.ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.NowFileDir);

            if (!DDS_SetUp.Initial())
            {
                MessageBox.Show("DDS Initalization Fails.");
                return;
            }
            else
            {
                //MessageBox.Show("DDS Initialization Succeeds.");                 // Roy-190205
            }

            DDS_Global.vehCommDataInst = DDS_Global.motionInfo_VehCommWriter.RegisterInstance(vehCommData);
            ////DDS_Global.vehHandShakeRxDataInst = DDS_Global.motionInfo_HandShakeRecieveDataWriter.RegisterInstance(vehHandShakeRxData);
            ////DDS_Global.vehHandShakeSendDataInst = DDS_Global.motionInfo_HandShakeSendDataWriter.RegisterInstance(vehHandShakeSendData);

            DDS_Global.vehInterCommSendDataInst = DDS_Global.motionInfo_VehInterCommSendDataWriter.RegisterInstance(vehInterCommSendData);
            DDS_Global.vehInterCommReptDataInst = DDS_Global.motionInfo_VehInterCommReptDataWriter.RegisterInstance(vehInterCommReptData);
            DDS_Global.vehHandShakeRxDataInst = DDS_Global.motionInfo_HandShakeRecieveDataWriter.RegisterInstance(vehHandShakeRxData);
            DDS_Global.vehHandShakeSendDataInst = DDS_Global.motionInfo_HandShakeSendDataWriter.RegisterInstance(vehHandShakeSendData);

            //++++++++++++++++++++++++++++++++++++++++++                    // Roy+171204
            DDS_Global.vehClientDataInst = DDS_Global.motionInfo_ClientWriter.RegisterInstance(DDS_Global.motionInfoClientData);
            DDS_Global.vehServerDataInst = DDS_Global.motionInfo_ServerWriter.RegisterInstance(DDS_Global.motionInfoServerData);

            DDS_Global.dctVehServerDataInst = new Dictionary<string, DDS.InstanceHandle>();                 //+++++                 // Roy+180125
            //++++++++++++++++++++++++++++++++++++++++++                

            //+++++++++++++++++++++++++++++                    // Roy+180125
            List<int> lstExactMapZones = new List<int> { 1, 3, 8, 12, 15, 17, 19, 21, 24, 35, 41, 43, 301 };                    // hard-code here ...

            DDS_Global.dctVehServerDataInst.Clear();

            foreach (var item in lstExactMapZones)
            {
                string sCurrentZoneName = item.ToString().PadLeft(4, '0');

                DDS_Global.motionInfoServerData.vehID = item;                    // primary ~ key-value ... SIGNIFICANT ... !!!
                DDS_Global.dctVehServerDataInst[sCurrentZoneName] = DDS_Global.motionInfo_ServerWriter.RegisterInstance(DDS_Global.motionInfoServerData);
            }
            //+++++++++++++++++++++++++++++                

            //
            ckbOffLine.Checked = false;
            ckbBlockCtrl.Checked = true;
            ckbBlockCtlOn.Checked = true;
            FakeCstID.Checked = true;
            //
            timerVehStatusData.Interval = 500;
            timerVehStatusData.Enabled = true;
            //
            Veh_VehM_Global.timeout_Config = new ConfigClass();
            Veh_VehM_Global.vehVehM = new StatusMachine.Veh_VehM();
            //StatusMachine.Veh_VehM_Global.vehVehMomm = new Veh_VehM_Comm_Data();

            Veh_VehM_Global.vehVehM.eventMsgToVehM += new EventHandler<ReportMsgEventArg>(EventTxbMsgToVehMChanged);
            Veh_VehM_Global.vehVehM.eventBlockQuery += new EventHandler<BlockControlQueryArg>(EventBlockControlTriggered);
            Veh_VehM_Global.vehVehM.eventBlockQuery += new EventHandler<BlockControlQueryArg>(EventLabelShowBlockRequest); // here is cmd 32(trans_complete_response)

        }

        private void timerVehStatusData_Tick(object sender, EventArgs e)
        {

            DDS.ReturnCode status;
            MotionInfo_Vehicle_Comm[] vehData = null;
            MotionInfo_Vehicle_Inter_Comm_ReportData[] vehInterComm_Rept_Data = null;
            MotionInfo_HandShake_RecieveData[] vehHSRxData = null;
            MotionInfo_HandShake_SendData[] vehHSSendData = null;
            DDS.SampleInfo[] sampleInfo_ReptData = null;
            DDS.SampleInfo[] sampleInfo = null;
            DDS.SampleInfo[] sampleInfo_RxData = null;
            DDS.SampleInfo[] sampleInfo_SendData = null;



            status = DDS_Global.motionInfo_VehCommReader.Take(
                ref vehData,
                ref sampleInfo,
                DDS.SampleStateKind.Any,
                DDS.ViewStateKind.Any,
                DDS.InstanceStateKind.Any);

            ErrorHandler.checkStatus(status, "MotionInf_VehCommData Reader Error");


            #region Show Msg from OHT_Vehicle
            status = DDS_Global.motionInfo_VehInterCommReptDataReader.Read(
                        ref vehInterComm_Rept_Data,
                        ref sampleInfo_ReptData,
                        DDS.SampleStateKind.Any,
                        DDS.ViewStateKind.Any,
                        DDS.InstanceStateKind.Any);

            ErrorHandler.checkStatus(status, "MotionInf_VehCommData Reader Error");
            if (vehInterComm_Rept_Data.Count() > 0) //Make sure there is data in the for loop 
            {
                foreach (MotionInfo_Vehicle_Inter_Comm_ReportData reptData in vehInterComm_Rept_Data) //load unload action address ... etc
                {
                    veh_Current_Address = reptData.Address.ToString();
                    veh_Current_Section = reptData.Section.ToString();
                    veh_Offset = reptData.DistanceFromSectionStart;
                }
            }
            this.txbMsgFromVeh.Text = string.Format(
                @"EventTypes: {0},
                Section: {1} ,
                Address: {2},
                Load Status: {3},
                Obsticle Stop Status: {4},
                Block Stop Status: {5},
                Pause Status: {6},
                Left Guide Status: {7},
                Right Guide Status: {8},
                Distance From Section: {9},
                Obsticle Dist: {10} ",
                Veh_VehM_Global.eventTypes.ToString(),
                Veh_VehM_Global.Section.ToString(),
                Veh_VehM_Global.Address,
                Veh_VehM_Global.vehLoadStatus.ToString(),
                Veh_VehM_Global.vehObstStopStatus.ToString(),
                Veh_VehM_Global.vehBlockStopStatus.ToString(),
                Veh_VehM_Global.vehPauseStatus.ToString(),
                Veh_VehM_Global.vehLeftGuideLockStatus.ToString(),
                Veh_VehM_Global.vehRightGuideLockStatus.ToString(),
                Veh_VehM_Global.DistanceFromSectionStart.ToString(),
                Veh_VehM_Global.vehObstDist.ToString());
            #endregion

            DDS_Global.motionInfo_HandShakeRecieveDataReader.Read(ref vehHSRxData, ref sampleInfo_RxData,
                DDS.SampleStateKind.Any,
                DDS.ViewStateKind.Any,
                DDS.InstanceStateKind.Any);

            DDS_Global.motionInfo_HandShakeSendDataReader.Read(ref vehHSSendData, ref sampleInfo_SendData,
                DDS.SampleStateKind.NotRead,
                DDS.ViewStateKind.New,
                DDS.InstanceStateKind.Any);

            if (vehHSSendData != null)
            {
                foreach (MotionInfo_HandShake_SendData data in vehHSSendData)
                {
                    txbSendDataSenStatus.Text = data.cmdSend.ToString();
                    txbSendDataRxStatus.Text = data.cmdReceive.ToString();
                }
            }

            if (vehHSRxData != null)
            {
                foreach (MotionInfo_HandShake_RecieveData data in vehHSRxData)
                {
                    txbRxDataSendStatus.Text = data.cmdSend.ToString();
                    txbRxDataRxStatus.Text = data.cmdReceive.ToString();
                }
            }

            string guideSections = "", cycleSections = "";

            if (Veh_VehM_Global.GuideSections != null)
            {
                foreach (string s in Veh_VehM_Global.GuideSections)
                {
                    guideSections += s;
                }
            }

            if (Veh_VehM_Global.CycleSections != null)
            {
                foreach (string s in Veh_VehM_Global.CycleSections)
                {
                    cycleSections += s;
                }
            }

            txbMsgToVehicle.Text = string.Format(
                  @"CmdID: {0},
                  ActionType: {1},
                  ToAddress: {2},
                  Guiding Sections: {3}, 
                  CycleSections: {4}",
                 Veh_VehM_Global.enCmdID.ToString(), Veh_VehM_Global.enActionType.ToString(),
                DDS_Global.motionInfoInterCommSendData.udtMove.Address,
                guideSections, cycleSections);

            this.txbMsgFromVehM.Refresh();//jason++ 180823
            /*
             * this is use at the timng that should translate the battery capicity
             */
            if (Veh_VehM_Global.checkForNoMoveSend144 == true)
            {
                if (ForLog != false)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Form : Battery Capicity thread : Enter checkForNoMoveSend144");
                }
                ForLog = false;
                MotionInfo_Vehicle_Inter_Comm_ReportData[] motionInfoInterCommReport = null;
                DDS.SampleInfo[] sampleInfo_RptData = null;
                //DDS_Global.motionInfo_VehInterCommReptDataReader.Take(
                //                ref motionInfoInterCommReport,
                //                ref sampleInfo_RptData,
                //                 DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);

                if (motionInfoInterCommReport != null)
                {
                    if (motionInfoInterCommReport.Count() != 0)
                    {
                        //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Form : Battery Capicity thread : motionInfoInterCommReport.Count() =  " + motionInfoInterCommReport.Count());
                        var t = new Thread
                        (() =>
                            {
                                //190821 jason--
                                //foreach (MotionInfo_Vehicle_Inter_Comm_ReportData data in motionInfoInterCommReport)
                                //{
                                //    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Form : Battery Capicity thread : eventType = " + data.eventTypes);

                                //    if (data.eventTypes != (int)VehEventTypes.BatteryReport&& data.eventTypes != (int)VehEventTypes.Address_Pass)
                                //    {
                                //        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Fatal, null, "@Form : Battery Capicity thread : Take the other eventType, Add eventType {0} back to DDS topic.", data.eventTypes);
                                //    }
                                //    else
                                //    {
                                //        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Form : Battery Capicity thread : Start for write 144 to 300 start ");
                                //        Veh_VehM_Global.vehVehM.SendValuesForRept(data, "144");
                                //        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Form : Battery Capicity thread :  End  for write 144 to 300  End  ");
                                //    }
                                //}
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Form : Battery Capicity thread : Exit for the Battery Capicity Send. Now thread number = " + Process.GetCurrentProcess().Threads.Count);
                            }
                        );
                        t.Priority = ThreadPriority.Lowest;
                        t.Start();
                    }
                }

            }
            else
            {
                if (ForLog != true)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Form : Battery Capicity thread : No Enter checkForNoMoveSend144");
                }
                ForLog = true;
            }

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++              // Roy+171204                  // Roy-180302
            ////MotionInfo_Server[] serverData = null
            //////DDS.SampleInfo[] sampleInfo = null;                     // Roy-+180125
            ////sampleInfo = null;                     // Roy+180125
            ////int iIdx = -1;                     // Roy+180125

            ////status = DDS_Global.motionInfo_ServerReader.Read(
            ////    ref serverData,
            ////    ref sampleInfo,
            ////    DDS.SampleStateKind.Any,
            ////    DDS.ViewStateKind.Any,
            ////    DDS.InstanceStateKind.Any);

            ////ErrorHandler.checkStatus(status, "MotionInfo_ServerReader Reader Error");

            ////foreach (MotionInfo_Server mdata in serverData)
            ////{
            ////    iIdx++;                // Roy+180125

            ////    if (!chkDdsEdit.Checked)
            ////    {
            ////        //if ((sampleInfo[iIdx].ViewState == DDS.ViewStateKind.New) && (sampleInfo[iIdx].InstanceState == DDS.InstanceStateKind.Alive))                // Roy-+180125
            ////        if ((sampleInfo[iIdx].ViewState == DDS.ViewStateKind.New) || (sampleInfo[iIdx].SampleState == DDS.SampleStateKind.NotRead))                // Roy+180125
            ////        {
            ////            //txtDdsBcZone.Text = mdata.CAST_ID.ToString().PadLeft(4, '0');              // Roy-*180125
            ////            txtDdsBcZone.Text = mdata.vehID.ToString().PadLeft(4, '0');                // Roy+180125
            ////            txtDdsLockBy.Text = mdata.FROM_ADR_ID.ToString();
            ////            txtDdsAcqBy.Text = mdata.TO_ADR_ID.ToString();

            ////            break;                // Roy+180125
            ////        }
            ////    }
            ////}
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        }

        private void btnConnect_Click(object sender, EventArgs e) // OHTM to VehM online
        {
            if (!this.ckbOffLine.Checked)
            {
                StatusMachine.Veh_VehM_Global.vehVehMomm = new StatusMachine.Veh_VehM_Comm_Data();
                Veh_VehM_Global.vehVehMomm.eventMsgFromVehM +=
                    new EventHandler<ReportMsgEventArg>(EventTxbMsgFromVehMChanged);
            }
        }



        private void btnFromVehMCmd31_6Cycle_Click(object sender, EventArgs e)  //VehM to OHTM to OHT cycle command
        {
            TransCommand_StopCommand.transCommand_StopCommand(8192);
            Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Cycle;

            //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = (int)CmdType.Move;                 // Roy-180319

            //DDS_Global.motionInfoInterCommSendData.Move.Address = txbEntranceAddress.Text;                // "20316";                 //***                       // Roy*180308                 // Roy-180319 ... will be processed internally ...
            Veh_VehM_Global.Address = txbEntryAddress.Text;                // "20316";                 //***                       // Roy*180308

            List<string> sections = new List<string>();                      // assume now@ [10321] 

            /*                  // Roy-180308
            sections.Add("0006");
            sections.Add("0002");
            sections.Add("0004");
            sections.Add("0005");
            sections.Add("0201");          
            */

            //+++++++++++++++++++++++++++++++++                  // Roy+180308
            for (int i = 0; i < txbGuideSections4PureMove.Lines.Length; i++)
            {
                sections.Add(txbGuideSections4PureMove.Lines[i]);
            }
            //+++++++++++++++++++++++++++++++++

            Veh_VehM_Global.GuideSections = sections.ToArray();

            string guideSections = "_";

            foreach (var str in sections)
            {
                guideSections += str;
            }

            //DDS_Global.motionInfoInterCommSendData.Move.Sections = Veh_VehM_Global.GuideSections;                 // Roy-180319


            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            sections.Clear();

            /*                  // Roy-180308
            sections.Add("0202");
            sections.Add("0006");
            sections.Add("0002");
            sections.Add("0004");
            sections.Add("0005");
            sections.Add("0201");                    
            */

            //+++++++++++++++++++++++++++++++++                  // Roy+180308
            for (int i = 0; i < txbCycleSections.Lines.Length; i++)
            {
                sections.Add(txbCycleSections.Lines[i]);
            }
            //+++++++++++++++++++++++++++++++++

            Veh_VehM_Global.CycleSections = sections.ToArray();

            string cycleSections = "_";

            foreach (var str in sections)
            {
                cycleSections += str;
            }

            //DDS_Global.motionInfoInterCommSendData.Move.Sections = Veh_VehM_Global.GuideSections;                    

            txbMsgToVehicle.Text = string.Format(
                @"CmdID: {0},
                  ActionType: {1},
                  ToAddress: {2},
                  Guiding Sections: {3}, 
                  CycleSections: {4}",
                Veh_VehM_Global.enCmdID.ToString(), Veh_VehM_Global.enActionType.ToString(),
                Veh_VehM_Global.Address,
                guideSections, cycleSections);                     // Roy*180319

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }


        private void ckbOffLine_CheckedChanged(object sender, EventArgs e)  //OHTM to VehM offline
        {
            if (ckbOffLine.Checked)
            {
                Veh_VehM_Global.OffLineTest = true;
                this.btnConnect.Enabled = false;

                this.ckbBlockCtlOn.Enabled = false;             // Roy+180319
            }
            else
            {
                Veh_VehM_Global.OffLineTest = false;
                this.btnConnect.Enabled = true;

                this.ckbBlockCtlOn.Enabled = true;             // Roy+180319
            }
        }

        public void EventTxbMsgFromVehMChanged(object sender, ReportMsgEventArg e) //event msg from VehM
        {
            object obj = new object();
            if (this.InvokeRequired)  //判斷是否需要執行委派
            {
                lock (obj)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        EventTxbMsgFromVehMChanged(sender, e);      //進行畫面更新
                    });
                }
                return;
            }
            this.txbMsgFromVehM.Text = e.Msg;

            this.txbMsgFromVehM.Refresh();
        }

        protected void EventTxbMsgToVehMChanged(object sender, ReportMsgEventArg e)  //event msg to VehM
        {
            object obj = new object();
            if (this.InvokeRequired)
            {
                lock (obj)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        EventTxbMsgToVehMChanged(sender, e);
                    });
                }
                return;
            }
            this.txbMsgToVehM.Text = e.Msg;
            this.txbMsgToVehM.Refresh();
        }

        protected void EventLabelShowBlockRequest(object sender, BlockControlQueryArg e) //show block issue
        {
            object obj = new object();
            if (this.InvokeRequired)
            {
                lock (obj)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        EventLabelShowBlockRequest(sender, e);
                    });
                }
                return;
            }
            this.lbBlockCtrlReqst.Visible = true;
            this.lbBlockCtrlReqst.ForeColor = Color.Red;
            this.lbBlockCtrlReqst.Text = "Block Control Requested:" + "Section ID: " +
                e.Query.Section + "Block Control Request:" + e.Query.BlockSectionPassReqst.ToString();                     // Roy*180319


        }

        protected void EventBlockControlTriggered(object sender, BlockControlQueryArg e)
        {
            // Block locked or unlocked
            bool blBlockesd = true;
            if (blBlockesd)
            {
                Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
                Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd32;
                Veh_VehM_Global.eventTypes = (int)VehEventTypes.BlockSection_Query;                     // Roy*180319
                Veh_VehM_Global.vehBlockPassReqst = (int)Status.NG;
            }
            else
            {
                Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
                Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd32;
                Veh_VehM_Global.eventTypes = (int)VehEventTypes.BlockSection_Query;                     // Roy*180319
                Veh_VehM_Global.vehBlockPassReqst = (int)Status.OK;
            }
            //
        }



        private void btnBlockLocked_Click(object sender, EventArgs e)
        {
            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
            Veh_VehM_Global.eventTypes = (int)VehEventTypes.BlockSection_Query;                     // Roy*180319
            Veh_VehM_Global.vehBlockPassReqst = (int)Status.NG;
        }

        private void btnBlockReleased_Click(object sender, EventArgs e)
        {
            ////Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
            ////Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
            ////Veh_VehM_Global.eventTypes = (int)VehEventTypes.Block_Query;
            ////Veh_VehM_Global.vehBlockPassReqst = (int)Status.OK;

            //Veh_VehM_Global.blCycleRun = false;                                 // useless so far ... !!!                     // Roy-180302
        }



        private void btnToVehMCmd134_Click(object sender, EventArgs e)
        {

            //Veh_VehM_Global.Address = "10401";
            //Veh_VehM_Global.Section = "007";
            //Veh_VehM_Global.BlockControlSection = "";
            //Veh_VehM_Global.cmpCode = (int)CmdType.Stop;
            //Veh_VehM_Global.cmpStatus = (int)VehCompleteStatus.Normal;
            //Veh_VehM_Global.DistanceFromSectionStart = 100;
            //Veh_VehM_Global.vehLeftGuideLockStatus = (int)VehLeftGuideLockStatus.UnLocked;
            //Veh_VehM_Global.vehRightGuideLockStatus = (int)VehLeftGuideLockStatus.Locked;
            //Veh_VehM_Global.vehModeStatus = (int)VehModeStatus.Stop;
            //Veh_VehM_Global.vehLoadStatus = (int)VehLoadedStatus.NotExisted;
            //Veh_VehM_Global.vehPauseStatus = (int)VehPauseStatus.Stop;
            //Veh_VehM_Global.eventTypes = (int)VehEventTypes.Moving_Pause;
            //Veh_VehM_Global.vehBlockStopStatus = (int)VehBlockStopStatus.Stop;
            //Veh_VehM_Global.vehObsticleStopStatus = (int)VehObstacleStopStatus.Stop;
            //Veh_VehM_Global.vehObstDist = 0;
            //Veh_VehM_Global.vehModeStatus = (int)VehModeStatus.Stop;
            //Veh_VehM_Global.CSTID_Load = (int)VehLoadedStatus.NotExisted;
            //Veh_VehM_Global.CSTID_UnLoad = (int)VehLoadedStatus.NotExisted;
            //Veh_VehM_Global.vehActionStatus = (int)VehActionStatus.Stopping;

            EventType eventTypes = EventType.AdrOrMoveArrivals;   //jason++ 180829  addrarival-- AdrOrMoveArrivals++

            VhGuideStatus lGuideStatus = VhGuideStatus.Lock;
            VhGuideStatus rGuideStatus = VhGuideStatus.Unlock;
            VhLoadCSTStatus loadStatus = VhLoadCSTStatus.NotExist;

            VhStopSingle obstStatus = VhStopSingle.StopSingleOff;
            VhStopSingle blockStatus = VhStopSingle.StopSingleOff;
            VhStopSingle pauseStatus = VhStopSingle.StopSingleOn;

            Task.Run(() => Veh_VehM_Global.vehVehMomm.sned_Str134("ID_134", eventTypes, veh_Current_Section,
                veh_Current_Address, Veh_VehM_Global.BlockControlSection,
                lGuideStatus, rGuideStatus, blockStatus, pauseStatus, obstStatus, loadStatus,
                veh_Offset));

        }

        private void ckbBlockCtrl_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbBlockCtrl.Checked)
            {
                Veh_VehM_Global.vehBlockPassReply = (int)Status.OK;
            }
            else
            {
                Veh_VehM_Global.vehBlockPassReply = (int)Status.NG;
            }

        }


        private void ckbBlockCtlOn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckbBlockCtlOn.Checked)
                Veh_VehM_Global.blBlockCtrl = true;
            else
                Veh_VehM_Global.blBlockCtrl = false;
        }


        private void chkDdsEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDdsEdit.Checked)
            {
                //txtDdsBcZone.Enabled = true;                  // Roy-180125
                //txtDdsLockBy.Enabled = true;                  // Roy-180125
                //txtDdsAcqBy.Enabled = true;                  // Roy-180125
                txtDdsBcZone.ReadOnly = false;                  // Roy+180125
                txtDdsLockBy.ReadOnly = false;                  // Roy+180125
                txtDdsAcqBy.ReadOnly = false;                  // Roy+180125

                txtDdsBcZone.ForeColor = Color.Red;                       // Roy+180125
                txtDdsLockBy.ForeColor = Color.Red;                       // Roy+180125
                txtDdsAcqBy.ForeColor = Color.Red;                       // Roy+180125

                txtDdsBcZone.BackColor = Color.Yellow;
                txtDdsLockBy.BackColor = Color.Yellow;
                txtDdsAcqBy.BackColor = Color.Yellow;
            }
            else
            {
                //txtDdsBcZone.Enabled = false;                  // Roy-180125
                //txtDdsLockBy.Enabled = false;                  // Roy-180125
                //txtDdsAcqBy.Enabled = false;                  // Roy-180125
                txtDdsBcZone.ReadOnly = true;                  // Roy+180125
                txtDdsLockBy.ReadOnly = true;                  // Roy+180125
                txtDdsAcqBy.ReadOnly = true;                  // Roy+180125

                txtDdsBcZone.ForeColor = Color.Black;                       // Roy+180125
                txtDdsLockBy.ForeColor = Color.Black;                       // Roy+180125
                txtDdsAcqBy.ForeColor = Color.Black;                       // Roy+180125

                txtDdsBcZone.BackColor = Color.LimeGreen;
                txtDdsLockBy.BackColor = Color.LimeGreen;
                txtDdsAcqBy.BackColor = Color.LimeGreen;

                DDS_Global.motionInfoServerData.CAST_ID = DateTime.Now.ToString("HH:mm:ss.fff");             // txtDdsBcSection.Text;               // Roy*180125
                DDS_Global.motionInfoServerData.vehID = int.Parse(txtDdsBcZone.Text.Trim());               // Roy+180125
                DDS_Global.motionInfoServerData.FROM_ADR_ID = txtDdsLockBy.Text;
                DDS_Global.motionInfoServerData.TO_ADR_ID = txtDdsAcqBy.Text;

                //DDS_Global.motionInfo_ServerWriter.Write(DDS_Global.motionInfoServerData);              // Roy-180125

                //                  // Roy-180302
                ////DDS.ReturnCode status = DDS_Global.motionInfo_ServerWriter.Write(DDS_Global.motionInfoServerData, DDS_Global.dctVehServerDataInst[DDS_Global.motionInfoServerData.vehID.ToString().PadLeft(4, '0')]);              // Roy+180125
                ///
                ////System.Diagnostics.Debug.Assert(status == DDS.ReturnCode.Ok);              // Roy+180125
                ////ErrorHandler.checkStatus(status, "motionInfo_ServerWriter Error");              // Roy+180125
                //
            }
        }


        private void btnFromVehMCmd39_1PauseMove_Click(object sender, EventArgs e)
        {
            //Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enEventsOnDriving;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd39;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Pause;

            //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = CmdType.Pause;             //Stop;                      // ... ?                       // Roy*180302                // Roy-180319

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }


        private void btnFromVehMCmd39_0RestartPausingMove_Click(object sender, EventArgs e)                // Roy+180302 .... NG !
        {
            //Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enEventsOnDriving;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd39;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Restart;             // 'Restart' will be better ...                  // Roy*180319

            //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = CmdType.Restart;                      // ... ?             // 'Restart' will be better ...                 // Roy-*180319            

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }


        private void btnOhtmStopMovingCycle_Click(object sender, EventArgs e)                // Roy+180302
        {
            if (Veh_VehM_Global.blCycleRun)
            {
                //Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

                Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
                Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.dark31;                     //+++ 
                Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Stop;                                //will be processed in 'Veh_VehM \ ActionTypeStatusMachine' ...

                //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = CmdType.Stop;                      // ... ?                 // Roy-180319

                if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                    Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;

                Veh_VehM_Global.blCycleRun = false;
            }
        }


        private void btnFromVehMCmd31_3LoadUnload_Click(object sender, EventArgs e)                      // WOW ...
        {
            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load_Unload;

            //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = CmdType.Move;                // Roy-180319
            //DDS_Global.motionInfoInterCommSendData.Move.Type = (int)MoveType.single;                //+++                // Roy+180302                       // Roy-180319

            List<string> sections = new List<string>();

            for (int i = 0; i < txbGuideSections4Load.Lines.Length; i++)
            {
                sections.Add(txbGuideSections4Load.Lines[i]);
            }

            Veh_VehM_Global.GuideSections = sections.ToArray();
            Veh_VehM_Global.LoadAddress = txbLoadAddress.Text;

            //++++++++++++++++++++++++++++++++++++++++              // Roy+180319
            sections.Clear();

            for (int i = 0; i < txbGuideSections4Unload.Lines.Length; i++)
            {
                sections.Add(txbGuideSections4Unload.Lines[i]);
            }

            Veh_VehM_Global.GuideSections2nd = sections.ToArray();
            Veh_VehM_Global.UnloadAddress = txbUnloadAddress.Text;
            //++++++++++++++++++++++++++++++++++++++++

            //Veh_VehM_Global.ToAddress = txbToAddress.Text;                // Roy-180319 ... useless so far ...
            //Veh_VehM_Global.Address = txbToAddress.Text;                // "20316";                 //+++                       // Roy+180308                 // Roy-180319 ... useless so far ...

            Veh_VehM_Global.CSTID_Load = txbCstID4Load.Text;                // Roy*180308                     // Roy*180319
            Veh_VehM_Global.CSTID_UnLoad = txbCstID4Load.Text;                // Roy+180319

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }


        private void btnFromVehMCmd31_1Load_Click(object sender, EventArgs e)                // Roy+180302
        {
            //--------------------------------------                // Roy-190205
            //Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
            //Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
            //Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load;
            //--------------------------------------

            //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = CmdType.Move;                // Roy-180319
            //DDS_Global.motionInfoInterCommSendData.Move.Type = (int)MoveType.single;                //+++                 // Roy-180319

            List<string> sections = new List<string>();

            for (int i = 0; i < txbGuideSections4Load.Lines.Length; i++)
            {
                sections.Add(txbGuideSections4Load.Lines[i]);
            }

            Veh_VehM_Global.GuideSections = sections.ToArray();

            Veh_VehM_Global.LoadAddress = txbLoadAddress.Text;

            //Veh_VehM_Global.ToAddress = txbToAddress.Text;                // Roy-180319 ... useless so far ...
            //Veh_VehM_Global.Address = txbToAddress.Text;                // "20316";                 //+++                       // Roy+180308                 // Roy-180319 ... useless so far ...

            Veh_VehM_Global.CSTID_Load = txbCstID4Load.Text;                // Roy*180308                     // Roy*180319

            //+++++++++++++++++++++++++++++++++++++++++++++++               // Roy+190205
            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load;

            if (sections.Count() == 0)
            {
                Veh_VehM_Global.NowActiveType = ActiveType.Load;
            }
            else
            {
                //Veh_VehM_Global.NowActiveType = ActiveType.Move;
            }
            //+++++++++++++++++++++++++++++++++++++++++++++++

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }


        private void btnFromVehMCmd31_5Continue_Click(object sender, EventArgs e)                // Roy+180302
        {
            //Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;                   //+++ 
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Continue;

            //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = CmdType.Continue;                      // ... ?                    // Roy-180319       

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }

        private void btnFromVehMCmd31_0Single_Click(object sender, EventArgs e)                // Roy+180302
        {
            Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enTransferRequest;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd31;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Move;

            //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = (int)CmdType.Move;                 // Roy-180319
            //DDS_Global.motionInfoInterCommSendData.Move.Address = txbToAddress.Text;                // "20316";                 //***                 // Roy-180319 ... will be processed internally ...

            Veh_VehM_Global.Address = txbToAddress.Text;                // "20316";                 //***      

            List<string> sections = new List<string>();                      // assume now@ [10321] 

            /*         
            sections.Add("0006");
            sections.Add("0002");
            sections.Add("0004");
            sections.Add("0005");
            sections.Add("0201");          
            */

            for (int i = 0; i < txbGuideSections4PureMove.Lines.Length; i++)
            {
                sections.Add(txbGuideSections4PureMove.Lines[i]);
            }

            Veh_VehM_Global.GuideSections = sections.ToArray();

            string guideSections = "_";

            foreach (var str in sections)
            {
                guideSections += str;
            }

            //DDS_Global.motionInfoInterCommSendData.Move.Sections = Veh_VehM_Global.GuideSections;                  // Roy-180319     

            txbMsgToVehicle.Text = string.Format(
                @"CmdID: {0},
                  ActionType: {1},
                  ToAddress: {2},
                  Guiding Sections: {3} ",
                Veh_VehM_Global.enCmdID.ToString(), Veh_VehM_Global.enActionType.ToString(),
                Veh_VehM_Global.Address,
                guideSections);                     // Roy*180319

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }

        private void btnFromVehMCmd37_0Cancel_Click(object sender, EventArgs e)                // Roy+180302                // Roy*180319
        {
            //Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enEventsOnDriving;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd37;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Cancel;

            //DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = CmdType.Cancel;                // Roy-180319

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e) //134send
        {
            Veh_VehM_Global.vehVehMomm.sned_Str134("ID_134", (EventType)4, "0410",
                            "30101", "", 0, 0, 0, 0, 0, 0, 0
                            );
        }

        private void FakeCstID_CheckedChanged(object sender, EventArgs e)
        {
            if (FakeCstID.Checked)
            {
                Veh_VehM_Global.fakeID = true;

            }
            else
            {
                Veh_VehM_Global.fakeID = false;

            }
        }

        private void FakeMapCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FakeMapCheckBox.Checked)
            {
                Veh_VehM_Global.fakeMap = true;

            }
            else
            {
                Veh_VehM_Global.fakeMap = false;

            }
        }

        private void testLoadUnloadCmd_Click(object sender, EventArgs e)
        {
            string[] UpSections = { "0041", "0040", "0042" };
            string[] UpAddress = { " " };
            //string sections = new TransSectioncs().TransSections(UpSections , UpAddress);
        }

        private void test_Veh_readMap_Click(object sender, EventArgs e)
        {
            //string[] FromVehCSections = { "008","007","006" };
            //string[] FromVehCAddresses = { "18084","48037","48030","18028" };

            string[] FromVehCSections = { "008", "028", "016", "015" };
            string[] FromVehCAddresses = { "18084", "48037", "48064", "48053", "48054" };

            //string[] test = new TransSectioncs().TransMap4Veh(FromVehCSections, FromVehCAddresses , out int temp);

            //for (int i = 0; i < test.Count(); i++)
            //{
            //    Console.WriteLine(test[i].ToString());
            //}
            //Console.WriteLine("XXX = "+temp);
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
            Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load;
            //DDS_Global.motionInfoInterCommSendData.Move.Address = fromAdr;              // Roy-180319
            DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToLoad;

            //Veh_VehM_Global.fromAddress_LoadPort = fromAdr;              // Roy-180319
            //Veh_VehM_Global.toAddress_UnloadPort = toAdr;              // Roy-180319
            Veh_VehM_Global.LoadAddress = "28074";              // Roy+180319
            string[] temp = new string[0];
            Veh_VehM_Global.GuideSectionsStartToLoad = temp;
            Veh_VehM_Global.GuideSectionsToDestination = temp;
            Veh_VehM_Global.GuideAddressesStartToLoad = temp;
            Veh_VehM_Global.GuideAddressesToDestination = temp;

            DDS_Global.motionInfoInterCommSendData.udtLoad.Veh_CSTID = "ASDFG";                 // Roy*180319
            //Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
            Veh_VehM_Global.CSTID_Load = "ASDFG";
            Task.Run(() => new Veh_VehM().ActionTypeStatusMachine());
        }

        private void UnLoadBtn_Click(object sender, EventArgs e)
        {
            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
            Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.UnLoad;
            //DDS_Global.motionInfoInterCommSendData.Move.Address = toAdr;              // Roy-180319
            DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = MoveType.single;              // Roy*180328

            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToUnload;

            Veh_VehM_Global.UnloadAddress = "28074";              // Roy+180319
            string[] temp = new string[0];
            Veh_VehM_Global.GuideSectionsStartToLoad = temp;
            Veh_VehM_Global.GuideSectionsToDestination = temp;
            Veh_VehM_Global.GuideAddressesStartToLoad = temp;
            Veh_VehM_Global.GuideAddressesToDestination = temp;

            DDS_Global.motionInfoInterCommSendData.udtUnLoad.Veh_CSTID = "ASDFG";
            Veh_VehM_Global.CSTID_UnLoad = "ASDFG";
            //Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
            Task.Run(() => new Veh_VehM().ActionTypeStatusMachine());
        }

        private void fake144charge_check_CheckedChanged(object sender, EventArgs e)
        {
            if (fake144charge_check.Checked)
            {
                Veh_VehM_Global.fake144chargecheck = true;
            }
            else
            {
                Veh_VehM_Global.fake144chargecheck = false;
            }
        }

        private void timerForNotMove_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnFromVehMCmd37_1Abort_Click(object sender, EventArgs e)                // Roy+180302                // Roy*180319
        {
            //Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;

            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enEventsOnDriving;
            Veh_VehM_Global.enCmdID = Veh_VehM_Global.CmdID.cmd37;
            Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Abort;

            // DDS_Global.motionInfoInterCommSendData.CmdType.cmdType = CmdType.Abort;                // Roy-180319

            if (!Veh_VehM_Global.vehVehM.timerEventSquence.Enabled)
                Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = true;
        }

    }
}
