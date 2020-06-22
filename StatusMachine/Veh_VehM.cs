﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using TcpIpClientSample;
using MirleOHT.類別.DCPS;
using OHTM.NLog_USE;
using OHTM.ReadCsv;
using Veh_HandShakeData;
using OHTM.ErrorCode;
using System.Diagnostics;
using System.Data;

namespace OHTM.StatusMachine
{
    public class Veh_VehM
    {
        private bool bIsOk2GoOnTrigger = false; // use for checking arrive the ending address of the cmd;
        private VehEventTypes curVehEventType = VehEventTypes.NotDefinedYet;
        static bool blRxDataSent = false;
        static bool blSendDataReceived = false;
        Task task;
        object sendDataDDS_obj = new object();
        //public event EventHandler<ReportMsgEventArg> eventMsgFromVehM;
        public event EventHandler<BlockControlQueryArg> eventBlockQuery;
        public event EventHandler<ReportMsgEventArg> eventMsgToVehM;
        //
        EventType eventTypes;
        CompleteStatus cmpStatus;
        ActiveType activeType;
        PauseEvent pauseContinue;
        VHActionStatus actionStatus;
        VhGuideStatus lGuideStatus, rGuideStatus;
        VhLoadCSTStatus loadStatus;
        VHModeStatus modeStatus;
        VhPowerStatus powerStatus;

        VhStopSingle obstStatus, blockStatus, pauseStatus;
        bool check_UseorNot;
        bool checkSendCmd = false;
        //
        long time = 10000;
        Veh_VehM_Comm_Data vehTcpComm;
        enum RxStatus { N0 = 0, Yes = 1 }
        enum TxStatus { No = 0, Yes = 1 }
        enum CmdID { cmd31 = 31, cmd131 = 131, cmd32 = 32, cmd132 = 132, cmd39 = 39, cmd139 = 139 }
        enum VehJobDone { No = 0, Yes = 1 }
        protected enum LoadCommand { UnLoad = 0, Load = 1, JustMove = 2 }
        LoadCommand loadunloadcheck;
        //LoadCommand enLoad
        //{
        //    get { return loadunloadcheck; }
        //    set { loadunloadcheck = value; }
        //}

        VehJobDone enJobDone;

        System.Timers.Timer timerActionType = new System.Timers.Timer();
        public System.Timers.Timer timerEventSquence = new System.Timers.Timer();                   // EventSquenceStatusMachine_Elapsed ...
        public System.Timers.Timer AlarmFor10Min = new System.Timers.Timer();
        public System.Timers.Timer AlarmForHalfSec = new System.Timers.Timer();
        static string prevAddress = string.Empty;
        Veh_VehM_Global.SequenceEvents seqEvents;

        //MotionInfo_Vehicle_Comm[] motionInfoComm = null;
        //DDS.SampleInfo[] sampleInfo = null;
        MotionInfo_Vehicle_Inter_Comm_ReportData_134[] motionInfoInterCommReport134 = null;
        MotionInfo_Vehicle_Inter_Comm_ReportData[] motionInfoInterCommReport = null;
        DDS.SampleInfo[] sampleInfo_RptData = null;
        DDS.SampleInfo[] sampleInfo_RptData_134 = null;
        MotionInfo_HandShake_RecieveData[] handShakeRxData = null;
        DDS.SampleInfo[] sampleInfo_RxData = null;
        MotionInfo_HandShake_SendData[] handShakeSendData = null;
        DDS.SampleInfo[] sampleInfo_SendData = null;

        public Veh_VehM()
        {
            vehTcpComm = new Veh_VehM_Comm_Data(time);
            Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
            Veh_VehM_Global.mResetEvent = new ManualResetEvent(false);
            AlarmFor10Min.Interval = 5000;
            AlarmFor10Min.Elapsed += EventSquence5SecAlarm_Elapsed;
            AlarmFor10Min.AutoReset = true;
            AlarmFor10Min.Enabled = true;
            /**/
            AlarmForHalfSec.Interval = 100;
            AlarmForHalfSec.Elapsed += EventSquencehalfSecAlarm_Elapsed;
            AlarmForHalfSec.AutoReset = true;
            //AlarmForHalfSec.Enabled = true;
            /**/
            timerEventSquence.Interval = 30;
            timerEventSquence.Elapsed += EventSquenceStatusMachine_Elapsed;
            timerEventSquence.AutoReset = true;
            var t = new Thread
                    (() =>
                    {
                        AlarmForHalfSec.Enabled = startNewCommData();
                    }
                    );
            t.Priority = ThreadPriority.Lowest;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :In new Veh_VehM object , Now thread number = " + Process.GetCurrentProcess().Threads.Count);
            t.Start();


            timerEventSquence.Enabled = true;
        }

        private bool startNewCommData()
        {
            bool done = false;
            while (done != true)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Enter Veh_VehM134");
                DDS_Global.motionInfo_VehInterCommReptData_134Reader.Read(
                    ref motionInfoInterCommReport134,
                    ref sampleInfo_RptData_134,
                     DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);
                foreach (MotionInfo_Vehicle_Inter_Comm_ReportData_134 data in motionInfoInterCommReport134)
                {
                    if (data.Section.StartsWith("+0000"))
                    {
                        data.Section = data.Section.Replace("+0000", "-");
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "DDS Crazy +0000 -0000");
                    }
                    else if (data.Section.StartsWith("-0000"))
                    {
                        data.Section = data.Section.Replace("-0000", "+");
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "DDS Crazy +0000 -0000");
                    }
                    StatusMachine.Veh_VehM_Global.vehVehMomm = new StatusMachine.Veh_VehM_Comm_Data();
                    Veh_VehM_Global.vehVehMomm.eventMsgFromVehM += new EventHandler<ReportMsgEventArg>(new Form1().EventTxbMsgFromVehMChanged);
                    done = true;
                    break;
                }
                Thread.Sleep(100);
            }
            while (Veh_VehM_Global.check143sendBeforeTimer == false)
            {
                SpinWait.SpinUntil(() => false, 100);
            }
            return true;
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void EventSquence5SecAlarm_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                #region 20 minute no complete
                twentyMinutesNotComplete();
                #endregion
                #region Send 134 from OHTM to OHTC for checking TCPIP
                oneMinute134SendCheckTCPIP();
                #endregion
                #region Two Minute no move check
                TwoMinutesNoMoveCheck();
                #endregion
                #region Load Unload Timeout Check
                //Jason-- 191128 No use for now.
                //loadUnloadTimeoutCheck();
                #endregion
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
            }
        }

        private void TwoMinutesNoMoveCheck()
        {
            if (Veh_VehM_Global_Property.already_have_command_Check == true)
            {
                if (Veh_VehM_Global.command_ID_from_VehM != "")
                {
                    if (Veh_VehM_Global.command_ID_from_VehM == Veh_VehM_Global.lastCmdID && Veh_VehM_Global.keep_AddressID == Veh_VehM_Global.Address)
                    {
                        Veh_VehM_Global.time_end_One = DateTime.Now;//計時結束 取得目前時間

                        double timecount = ((TimeSpan)(Veh_VehM_Global.time_end_One - Veh_VehM_Global.time_start_One)).TotalMinutes;
                        if (timecount > Veh_VehM_Global.timeout_Config.sameAddressTimeout && Veh_VehM_Global.oneminuteHasSent_One == false)
                        {
                            Veh_VehM_Global.oneminuteHasSent_One = true;
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquence10MinAlarm_Elapsed -999", Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                            //Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                            //SendValuesForRept(data, "144");
                            /*********************************/
                            Veh_VehM_Global.vehVehMomm.sned_Str144("ID_144",
                            Veh_VehM_Global.Address, Veh_VehM_Global.Section,
                            modeStatus, actionStatus, powerStatus, Veh_VehM_Global.hasCst, obstStatus, blockStatus,
                            pauseStatus, lGuideStatus, rGuideStatus,
                            (int)Veh_VehM_Global.DistanceFromSectionStart,
                            Veh_VehM_Global.BatteryCapacity, Veh_VehM_Global.BatteryTemperature, Veh_VehM_Global.SteeringWheel, Veh_VehM_Global.ErrorStatus);
                            Veh_VehM_Global.vehVehMomm.sned_Str194(998, ErrorStatus.ErrSet);
                            Veh_VehM_Global.time_start_One = DateTime.Now;//Re計時 取得目前時間
                        }
                    }
                    else
                    {
                        if (Veh_VehM_Global.command_ID_from_VehM != Veh_VehM_Global.lastCmdID)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquence10MinAlarm_Elapsed set cmd id = {0}", Veh_VehM_Global.command_ID_from_VehM.ToString(), Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308

                            Veh_VehM_Global.lastCmdID = Veh_VehM_Global.command_ID_from_VehM;
                            Veh_VehM_Global.time_start_One = DateTime.Now;//計時開始 取得目前時間
                        }
                        if (Veh_VehM_Global.keep_AddressID != Veh_VehM_Global.Address)
                        {
                            if (Veh_VehM_Global.oneminuteHasSent_One == true)
                            {
                                Veh_VehM_Global.oneminuteHasSent_One = false;
                                Veh_VehM_Global.vehVehMomm.sned_Str194(998, ErrorStatus.ErrReset);
                            }
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquence10MinAlarm_Elapsed set Address id = {0}", Veh_VehM_Global.command_ID_from_VehM.ToString(), Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                            Veh_VehM_Global.keep_AddressID = Veh_VehM_Global.Address;
                            Veh_VehM_Global.time_start_One = DateTime.Now;//計時開始 取得目前時間
                        }
                    }
                }
            }
            else
            {
                if (Veh_VehM_Global.oneminuteHasSent_One != false || Veh_VehM_Global.lastCmdID != "" || Veh_VehM_Global.command_ID_from_VehM != "")
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquence10MinAlarm_Elapsed reset Address id", Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                    Veh_VehM_Global.oneminuteHasSent_One = false;
                    Veh_VehM_Global.lastCmdID = "";
                    Veh_VehM_Global.keep_AddressID = Veh_VehM_Global.Address;
                    //Veh_VehM_Global.command_ID_from_VehM = "";
                    Veh_VehM_Global.time_start_One = DateTime.Now;//計時開始 取得目前時間
                }
            }
        }

        private void oneMinute134SendCheckTCPIP()
        {
            if (Veh_VehM_Global.vehVehMomm.clientAgent != null)
            {
                if (Veh_VehM_Global.vehVehMomm.clientAgent.StopWatch_FromTheLastCommTime.Elapsed.TotalMinutes >= 1.5)
                {
                    MotionInfo_Vehicle_Inter_Comm_ReportData_134 tempfor134 = new MotionInfo_Vehicle_Inter_Comm_ReportData_134();
                    tempfor134.Address = Veh_VehM_Global.Address;
                    tempfor134.Section = Veh_VehM_Global.Section;
                    tempfor134.BlockControlSection = Veh_VehM_Global.BlockControlSection;
                    tempfor134.cmpCode = Veh_VehM_Global.cmpCode;
                    tempfor134.cmpStatus = Veh_VehM_Global.cmpStatus;
                    tempfor134.DistanceFromSectionStart = Veh_VehM_Global.DistanceFromSectionStart;
                    tempfor134.WalkLength = Veh_VehM_Global.vehWalkLength;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Veh_CommandComplete tempfor134.DistanceFromSectionStart = {0}", tempfor134.DistanceFromSectionStart.ToString());                // Roy+180308
                    tempfor134.loadStatus.With_CST = (int)Veh_VehM_Global.hasCst;
                    tempfor134.vehLeftGuideLockStatus = Veh_VehM_Global.vehLeftGuideLockStatus;
                    tempfor134.vehRightGuideLockStatus = Veh_VehM_Global.vehRightGuideLockStatus;
                    tempfor134.ConrtolMode = Veh_VehM_Global.vehModeStatus_fromVehC;
                    tempfor134.vehLoadStatus = Veh_VehM_Global.vehLoadStatus;
                    tempfor134.vehPauseStatus = Veh_VehM_Global.vehPauseStatus;
                    tempfor134.eventTypes = Veh_VehM_Global.eventTypes;
                    tempfor134.vehBlockStopStatus = Veh_VehM_Global.vehBlockStopStatus;
                    tempfor134.vehObstacleStopStatus = Veh_VehM_Global.vehObstStopStatus;
                    tempfor134.vehObstDist = Veh_VehM_Global.vehObstDist;
                    tempfor134.loadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_Load;
                    tempfor134.unLoadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_UnLoad;
                    tempfor134.vehActionStatus = Veh_VehM_Global.vehActionStatus;
                    tempfor134.HIDControlSection = Veh_VehM_Global.HIDControlSection;
                    tempfor134.BatteryCapacity = Veh_VehM_Global.BatteryCapacity;
                    tempfor134.BatteryTemperature = Veh_VehM_Global.BatteryTemperature;
                    SendValuesForRept_134(tempfor134, "134");
                }
            }
        }

        private void twentyMinutesNotComplete()
        {
            if (Veh_VehM_Global_Property.already_have_command_Check == true)
            {
                if (Veh_VehM_Global.command_ID_from_VehM != "")
                {
                    if (Veh_VehM_Global.command_ID_from_VehM == Veh_VehM_Global.lastCmdID)
                    {
                        Veh_VehM_Global.time_end_Ten = DateTime.Now;//計時結束 取得目前時間

                        double timecount = ((TimeSpan)(Veh_VehM_Global.time_end_Ten - Veh_VehM_Global.time_start_Ten)).TotalMinutes;
                        if (timecount > Veh_VehM_Global.timeout_Config.sameCmdTimeout && Veh_VehM_Global.tenminuteHasSent_Ten == false)
                        {
                            Veh_VehM_Global.tenminuteHasSent_Ten = true;
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : twentyMinutesNotComplete !!!");

                            /****************144******************/
                            Veh_VehM_Global.vehVehMomm.sned_Str144(
                                "ID_144", Veh_VehM_Global.Address, Veh_VehM_Global.Section, modeStatus, actionStatus, powerStatus, Veh_VehM_Global.hasCst, obstStatus, blockStatus,
                                pauseStatus, lGuideStatus, rGuideStatus, (int)Veh_VehM_Global.DistanceFromSectionStart,
                                Veh_VehM_Global.BatteryCapacity, Veh_VehM_Global.BatteryTemperature, Veh_VehM_Global.SteeringWheel, Veh_VehM_Global.ErrorStatus
                            );
                            /*************************************/

                            /******** Cancel the Cmd**************/
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : EventSquence10MinAlarm_Elapsed => Begin 'Abort' Procedure");
                            Veh_Abort_Procedure();
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : EventSquence10MinAlarm_Elapsed => 'Abort' Procedure Complete");
                            /*************************************/

                            /****************132******************/
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquence10MinAlarm_Elapsed -999", Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                            Veh_VehM_Global.vehVehMomm.sned_Str194(-999, ErrorStatus.ErrSet);

                            cmpStatus = CompleteStatus.CmpStatusVehicleAbort;
                            Veh_VehM_Global.vehVehMomm.sned_Str132(
                            Veh_VehM_Global.command_ID_from_VehM, activeType, Veh_VehM_Global.CSTID_Load.ToString(),
                            Veh_VehM_Global.cmpCode, cmpStatus, Veh_VehM_Global.vehWalkLength
                            );
                            /*************************************/
                        }
                    }
                    else if (Veh_VehM_Global.command_ID_from_VehM != Veh_VehM_Global.lastCmdID)
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquence10MinAlarm_Elapsed set cmd id = {0}", Veh_VehM_Global.command_ID_from_VehM.ToString(), Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308

                        Veh_VehM_Global.lastCmdID = Veh_VehM_Global.command_ID_from_VehM;
                        Veh_VehM_Global.time_start_Ten = DateTime.Now;//計時開始 取得目前時間

                    }
                }
                else
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquence10MinAlarm_Elapsed no cmd", Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                }
            }
            else
            {
                if (Veh_VehM_Global.tenminuteHasSent_Ten != false || Veh_VehM_Global.lastCmdID != "" || Veh_VehM_Global.command_ID_from_VehM != "")
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquence10MinAlarm_Elapsed reset cmd id", Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                    Veh_VehM_Global.tenminuteHasSent_Ten = false;
                    Veh_VehM_Global.lastCmdID = "";
                    //Veh_VehM_Global.command_ID_from_VehM = "";
                    Veh_VehM_Global.time_start_Ten = DateTime.Now;//計時開始 取得目前時間
                }
            }
        }

        private void loadUnloadTimeoutCheck()
        {
            if (Veh_VehM_Global.start_loading_or_unloading == true)
            {
                Veh_VehM_Global.lul_time_end = DateTime.Now;//計時結束 取得目前時間

                double timecount = ((TimeSpan)(Veh_VehM_Global.lul_time_end - Veh_VehM_Global.lul_time_start)).TotalMinutes;
                if (timecount > 99999.0 && Veh_VehM_Global.lul_HasSent == false)
                {
                    Veh_VehM_Global.lul_HasSent = true;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquenceLoadUnloadAlarm_Elapsed -999", Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                    //Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;

                    /***************144******************/
                    Veh_VehM_Global.vehVehMomm.sned_Str144("ID_144", Veh_VehM_Global.Address, Veh_VehM_Global.Section, modeStatus, actionStatus, powerStatus,
                        Veh_VehM_Global.hasCst, obstStatus, blockStatus, pauseStatus, lGuideStatus, rGuideStatus, (int)Veh_VehM_Global.DistanceFromSectionStart,
                        Veh_VehM_Global.BatteryCapacity, Veh_VehM_Global.BatteryTemperature, Veh_VehM_Global.SteeringWheel, Veh_VehM_Global.ErrorStatus);
                    /*********************************/

                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : EventSquenceLoadUnloadAlarm_Elapsed => Begin 'Abort' Procedure");
                    Veh_Abort_Procedure();
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : EventSquenceLoadUnloadAlarm_Elapsed => 'Abort' Procedure Complete");

                    Veh_VehM_Global.vehVehMomm.sned_Str194(-999, ErrorStatus.ErrSet);

                    /****************132******************/

                    cmpStatus = CompleteStatus.CmpStatusVehicleAbort;

                    Veh_VehM_Global.vehVehMomm.sned_Str132(Veh_VehM_Global.command_ID_from_VehM, activeType, Veh_VehM_Global.CSTID_Load.ToString(), Veh_VehM_Global.cmpCode, cmpStatus,
                        Veh_VehM_Global.vehWalkLength);

                    /*************************************/

                }
            }
            else if (Veh_VehM_Global.lul_HasSent != false)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : EventSquenceLoadUnloadAlarm_Elapsed LoadUnload reset", Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                Veh_VehM_Global.lul_HasSent = false;
                Veh_VehM_Global.lul_time_start = DateTime.Now;//計時開始 取得目前時間
            }
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private void EventSquencehalfSecAlarm_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                #region Use for resend block request for the stop

                #endregion
                #region Use for send the 134 to the OHTC
                send134ToOHTC();
                #endregion
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
            }
        }

        private void send134ToOHTC()
        {
            //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Enter send134ToOHTC");
            DDS_Global.motionInfo_VehInterCommReptData_134Reader.Take(
                                        ref motionInfoInterCommReport134,
                                        ref sampleInfo_RptData_134,
                                         DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);

            if (motionInfoInterCommReport != null)
            {
                //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_Move_Procedure : enter AnalyzeDDSFeedback MOVE");
                AnalyzeDDSFeedbackJustFor134(LoadCommand.Load, motionInfoInterCommReport134);
            }           // # 
        }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void EventSquenceStatusMachine_Elapsed(object sender, ElapsedEventArgs e)
        {

            //DDS_Global.motionInfo_VehCommReader.Take(
            //    ref motionInfoComm, ref sampleInfo,
            //    DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);

            seqEvents = Veh_VehM_Global.seqEvents;
            //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  EventSquenceStatusMachine_Elapsed seqEvents = " + seqEvents);
            //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  EventSquenceStatusMachine_Elapsed CmdID = " + Veh_VehM_Global.enCmdID);

            switch (seqEvents)
            {
                case Veh_VehM_Global.SequenceEvents.enIdle:
                    Veh_VehM_Global.vehVehM.timerEventSquence.Enabled = false;                  //added wschen 20171124
                    break;

                case Veh_VehM_Global.SequenceEvents.enVeh_PowerOn:
                    // Work to do
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enVeh_Data:
                    // Work to do
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enControl_Start_Stop:
                    // Work to do
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enTransferRequest:
                    if (Veh_VehM_Global.vehModeStatus_fromVehC != VehControlMode.OnlineLocal)
                    {
                        Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                        Veh_TransferRequest_Procedure(Veh_VehM_Global.enCmdID);
                    }
                    else
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : the vehModeStatus_fromVehC type = {0}", Veh_VehM_Global.vehModeStatus_fromVehC.ToString());                // Roy+180308
                    }
                    break;

                case Veh_VehM_Global.SequenceEvents.enEventsOnDriving:
                    //Veh_EventsOnDriving_Procedure(Veh_VehM_Global.enCmdID);
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enTransferCancel:   // this part is not using, just let it go. 
                    // Work to do
                    //Veh_EventsOnDriving_Procedure(Veh_VehM_Global.enCmdID);
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enChangeMode:
                    // Work to do
                    //CmdForChangeStatus

                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    Veh_ChangeMode_Procedure(Veh_VehM_Global.enCmdID);

                    break;

                case Veh_VehM_Global.SequenceEvents.enChangePower:
                    // Work to do
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enErrorOccur_Clear:
                    // Work to do
                    break;

                case Veh_VehM_Global.SequenceEvents.enAutoDrivingTeaching:
                    // Work to do
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enGripperTeaching:
                    // Work to do
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enVehLogUpLoad:
                    // Work to do
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;

                case Veh_VehM_Global.SequenceEvents.enRecoverFromUnExpPowerFailure:
                    // Work to do
                    Veh_VehM_Global.seqEvents = Veh_VehM_Global.SequenceEvents.enIdle;
                    break;
            }
        }
        protected bool Veh_ChangeMode_Procedure(Veh_VehM_Global.CmdID enCmdID)
        {
            try
            {

                #region "cmd41"
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : get into the Veh_ChangeMode_Procedure : cmd41 type = {0}", activeType.ToString());                // Roy+180308

                Task.Run(() => Modechange());

                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_OHTC : Veh_ChangeMode_Procedure : " + Veh_VehM_Global.NowActiveType.ToString() + " Thread Started.");
                #endregion          // "cmd31"

                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected bool Veh_TransferRequest_Procedure(Veh_VehM_Global.CmdID enCmdID)
        {
            try
            {
                switch (enCmdID)
                {
                    //++++++++++++++++++++++++++++                 // Roy+180302
                    case Veh_VehM_Global.CmdID.dark31:
                        #region "dark31"
                        //if (task == null && !task.IsCompleted)                  // ... ??? 
                        //{
                        task = Task.Run(() => ActionTypeStatusMachine());
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_TransferRequest_Procedure : Stop Thread Started.");
                        //}
                        #endregion          // "dark31"

                        break;
                    //++++++++++++++++++++++++++++

                    case Veh_VehM_Global.CmdID.cmd31:
                        #region "cmd31"
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_OHTC : get into the Veh_TransferRequest_Procedure : cmd31 type = {0}", activeType.ToString());                // Roy+180308

                        check_UseorNot = false;

                        Task.Run(() => ActionTypeStatusMachine());

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_OHTC : Veh_TransferRequest_Procedure : " + Veh_VehM_Global.NowActiveType.ToString() + " Thread Started.");
                        #endregion          // "cmd31"
                        break;

                    case Veh_VehM_Global.CmdID.cmd32:
                        #region"cmd32"
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : get into the Veh_TransferRequest_Procedure : cmd32 ");                // Roy+180308
                        if (Veh_VehM_Global.vehBlockPassReqst == (int)Status.NG)
                        {
                            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToPause;
                            DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.BlockSectionPassReply = Status.NG;                                              // Roy*180319
                            DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = DDS_Global.motionInfoInterCommReptData.BlockSectionPassReqst.Section; // Roy*180319

                            DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.NG;
                            DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.OK;
                            DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.OK;
                        }
                        else
                        {
                            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToContinue;
                            DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.BlockSectionPassReply = Status.OK;                     // Roy*180319

                            DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = DDS_Global.motionInfoInterCommReptData.BlockSectionPassReqst.Section;                     // Roy*180319

                            DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                            DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.NG;
                            DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.NG;
                        }

                        VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);

                        // Inform SendData Sent
                        DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                        DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                        DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
                        //
                        #endregion          // "cmd32"

                        break;

                    case Veh_VehM_Global.CmdID.cmd37:
                        #region "cmd37"
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : Veh_TransferRequest_Procedure : get into the Veh_TransferRequest_Procedure : cmd37");                // Roy+180308
                        check_UseorNot = false;
                        Task.Run(() => ActionTypeStatusMachine());                    // Roy+180319
                        break;
                    #endregion

                    case Veh_VehM_Global.CmdID.cmd39:
                        #region "cmd39"
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : get into the Veh_TransferRequest_Procedure : cmd39");                // Roy+180308
                        check_UseorNot = true;
                        Task.Run(() => ActionTypeStatusMachine());                    // Roy-171128 .... [Cmd39: 暫停(純)走行] Pause/Stop 或 [Cmd37: 中止搬送/上下貨]  Cancel/Abort 應該都是發生在 其它 狀態機正在執行中 才有作用 ...                   // Roy+180302
                        break;
                    #endregion
                    case Veh_VehM_Global.CmdID.cmd51:
                        #region "cmd51"
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : get into the Veh_TransferRequest_Procedure : cmd51");                // Roy+180308
                        check_UseorNot = true;
                        Task.Run(() => ActionTypeStatusMachine());                    // Roy-171128 .... [Cmd39: 暫停(純)走行] Pause/Stop 或 [Cmd37: 中止搬送/上下貨]  Cancel/Abort 應該都是發生在 其它 狀態機正在執行中 才有作用 ...                   // Roy+180302
                        break;
                    #endregion
                    case Veh_VehM_Global.CmdID.cmd71:
                        #region "cmd71"
                        Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Teaching;
                        check_UseorNot = false;
                        Task.Run(() => ActionTypeStatusMachine());
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Teaching Thread Started.");               // Roy+180302
                        #endregion
                        break;

                }           // # switch (enCmdID)

                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void ActionTypeStatusMachine()
        {
            Veh_VehM_Global.CheckNewCmdIDProcess = false;

            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : end  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@checkProcessFlag : ActionTypeStatusMachine  CheckNewCmdIDProcess = {0}", Veh_VehM_Global.CheckNewCmdIDProcess.ToString());

            Veh_VehM_Global_Property.arrive_Complete_Check = false;
            //Veh_VehM_Global_Property.abort_On_Check = false;  // here may cause some problem
            Veh_VehM_Global.send144check = true;
            Veh_VehM_Global_Property.IsCmdAbort = false;
            if (check_UseorNot == false)
            {
                Veh_VehM_Global_Property.cmd_Length_Check = 0;  //reset the length
                Veh_VehM_Global_Property.abort_On_Check = false; // reset the cmd type;
                #region "enActionType"
                switch (Veh_VehM_Global.enActionType)
                {
                    case Veh_VehM_Global.ActionType.Movetomtl:
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        Veh_VehM_Global_Property.lastMoveType_4_Override_Check = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Idle;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Single-Move-toMTL' To address.");               // Roy*171030
                        Veh_Move_Procedure(false, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination);
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Single-Move-toMTL' To address Complete.");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusMoveToMtl);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;
                    case Veh_VehM_Global.ActionType.Mtlhome:
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        Veh_VehM_Global_Property.lastMoveType_4_Override_Check = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Idle;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Single-Move-MTLhome' To address.");               // Roy*171030
                        Veh_Move_Procedure(false, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination, true);
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Single-Move-MTLhome' To address Complete.");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusMtlhome);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;
                    case Veh_VehM_Global.ActionType.Systemin:
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        Veh_VehM_Global_Property.lastMoveType_4_Override_Check = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Idle;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Single-Move-SystemIn' To address.");               // Roy*171030

                        Veh_Move_Procedure(false, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination);
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Single-Move-SystemIn' To address Complete.");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusSystemIn);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;
                    case Veh_VehM_Global.ActionType.Systemout:
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        Veh_VehM_Global_Property.lastMoveType_4_Override_Check = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Idle;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Single-Move-SystemOut' To address.");               // Roy*171030

                        Veh_Move_Procedure(false, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination);
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Single-Move-SystemOut' To address Complete.");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusSystemOut);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;
                    case Veh_VehM_Global.ActionType.Move:
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        Veh_VehM_Global_Property.lastMoveType_4_Override_Check = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Move;
                        Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Idle;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Single-Move' To address.");               // Roy*171030

                        Veh_Move_Procedure(false, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination);

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusMove);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;

                    case Veh_VehM_Global.ActionType.Load_Unload:
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        int walkLengthTotal = 0;
                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Load_Unload;
                        Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Idle;

                        string[] loadSection = new string[40];
                        string[] unloadSection = new string[40];
                        CheckSections.ClassifySections(ref loadSection, ref unloadSection);
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Loading' Procedure");               // Roy*171030

                        bool tempcheck = Veh_LoadUnLoad_Procedure(LoadCommand.Load, MoveType.single, loadSection, Veh_VehM_Global.GuideAddressesStartToLoad, Veh_VehM_Global.CSTID_Load.ToString());              // Roy*180308 ... temp. 
                        if (tempcheck == false)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : ActionTypeStatusMachine => LoadUnload cmd Error GGGGGGGGGGGGG");
                            break;
                        }
                        if (Veh_VehM_Global_Property.IsCmdAbort != true) //override check abort check 
                        {
                            walkLengthTotal = Veh_VehM_Global_Property.cmd_Length_Check; //load walk length
                            if (bIsOk2GoOnTrigger == false)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : ActionTypeStatusMachine => LoadUnload cmd didn't arrive the last address");

                                if (Veh_VehM_Global_Property.abort_On_Check != true)
                                {
                                    Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusAbort);
                                }
                                Veh_VehM_Global.checkForNoMoveSend144 = true;
                                break;
                            }
                            if (Veh_VehM_Global.check_4_BCRread == false)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'ReadError ===>>> Cancel' Procedure");               // Roy*171030
                                if (Veh_VehM_Global_Property.abort_On_Check != true)
                                {
                                    Veh_VehM_Global.completeStatus completeStatus = Veh_VehM_Global.completeStatus.CmpStatusLoadunload;
                                    if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdMismatch)
                                    {
                                        completeStatus = Veh_VehM_Global.completeStatus.CmpStatusIdmisMatch;
                                    }
                                    else if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdReadFailed)
                                    {
                                        completeStatus = Veh_VehM_Global.completeStatus.CmpStatusIdreadFailed;
                                    }
                                    else if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdReadDuplicate)
                                    {
                                        completeStatus = Veh_VehM_Global.completeStatus.CmdStatusIdReadDuplicate;
                                    }
                                    Veh_CommandComplete(completeStatus);
                                    Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                                }
                            }
                            else if (Veh_VehM_Global.check_4_BCRread == true)
                            {
                                if (Veh_VehM_Global_Property.abort_On_Check != true)
                                {
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'UnLoading' Procedure");               // Roy*171030

                                    Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, unloadSection, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp.              // Roy*180319
                                    Veh_VehM_Global_Property.cmd_Length_Check = walkLengthTotal + Veh_VehM_Global_Property.cmd_Length_Check; //unload + load walk length
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'UnLoading' Procedure Complete");               // Roy*171030
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Load-n-UnLoad' Procedure Complete");
                                    // Roy*171030
                                    if (Veh_VehM_Global_Property.abort_On_Check != true)
                                    {
                                        Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusLoadunload);
                                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                                    }
                                }
                            }
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Loadunload is abort.");               // Roy*171030
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;

                    case Veh_VehM_Global.ActionType.Load:
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Load;
                        Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Idle;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Loading' Procedure");               // Roy*171030

                        bool tempcheck1 = Veh_LoadUnLoad_Procedure(LoadCommand.Load, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesStartToLoad, Veh_VehM_Global.CSTID_Load.ToString());              // Roy*180308 ... temp. 
                        if (tempcheck1 == false)
                        {

                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : ActionTypeStatusMachine => LoadUnload cmd Error GGGGGGGGGGGGG");

                            break;
                        }
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Loading' Procedure Complete");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_VehM_Global.completeStatus completeStatus = Veh_VehM_Global.completeStatus.CmpStatusLoad;
                            if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdMismatch)
                            {
                                completeStatus = Veh_VehM_Global.completeStatus.CmpStatusIdmisMatch;
                            }
                            else if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdReadFailed)
                            {
                                completeStatus = Veh_VehM_Global.completeStatus.CmpStatusIdreadFailed;
                            }
                            else if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdReadDuplicate)
                            {
                                completeStatus = Veh_VehM_Global.completeStatus.CmdStatusIdReadDuplicate;
                            }
                            Veh_CommandComplete(completeStatus);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;

                    case Veh_VehM_Global.ActionType.UnLoad:
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.UnLoad;
                        Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Idle;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Begin 'UnLoading' Procedure");               // Roy*171030

                        bool tempcheck2 = Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp. 
                        if (tempcheck2 == false)
                        {

                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : ActionTypeStatusMachine => LoadUnload cmd Error GGGGGGGGGGGGG");

                            break;
                        }
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  'UnLoading' Procedure Complete");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusUnload);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;
                    case Veh_VehM_Global.ActionType.Override:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Override' Procedure");      // Roy*171030
                        Veh_Override_issue();
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Override' Procedure Complete");

                        break;
                    //+++++++++++++++++++++++++++++++++++++++                   // Roy+180319
                    case Veh_VehM_Global.ActionType.Cancel:
                        while (!VehCanStop())
                        {
                            Thread.Sleep(10);
                        }
                        if (Veh_VehM_Global_Property.Pre31CmdType == Veh_VehM_Global.ActionType.Unknow)
                        {
                            return;
                        }
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Abort' Procedure");
                        Veh_Abort_Procedure();
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Abort' Procedure Complete");

                        Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusCancel);
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;

                    case Veh_VehM_Global.ActionType.Abort:      //This is the cancel really used.

                        while (!VehCanStop())
                        {
                            Thread.Sleep(10);
                        }
                        if (Veh_VehM_Global_Property.Pre31CmdType == Veh_VehM_Global.ActionType.Unknow)
                        {
                            return;
                        }
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Abort' Procedure");
                        Veh_Abort_Procedure();
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Abort' Procedure Complete");

                        Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusAbort);
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;

                    case Veh_VehM_Global.ActionType.Forcefinish:

                        while (!VehCanStop())
                        {
                            Thread.Sleep(10);
                        }
                        if (Veh_VehM_Global_Property.Pre31CmdType == Veh_VehM_Global.ActionType.Unknow)
                        {
                            return;
                        }
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Forcefinish' Procedure");
                        Veh_Forcefinish_Procedure();
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Forcefinish' Procedure Complete");

                        Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort);
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                        break;
                    //+++++++++++++++++++++++++++++++++++++++

                    case Veh_VehM_Global.ActionType.Cycle:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Cycle-Run' Procedure");               // Roy*171030
                        enJobDone = VehJobDone.No;

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Cycle-Run' Procedure Complete");

                        Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusMove);
                        break;

                    //+++++++++++++++++++++++++++++++++++++++                   // Roy+180319
                    case Veh_VehM_Global.ActionType.Restart:
                        //check_UseorNot = true;
                        if (Veh_VehM_Global_Property.same_pause_Command_Check == true)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Restart' Procedure");

                            Veh_Restart_Procedure();

                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Restart' Procedure Complete");

                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusMove);
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Not match 'Restart' Procedure");
                        }
                        break;
                    //+++++++++++++++++++++++++++++++++++++++
                    case Veh_VehM_Global.ActionType.Stop:
                        //check_UseorNot = true;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Stop' Procedure");               // Roy+180302

                        //Veh_Stop_Procedure();               // Roy+180302

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Stop' Procedure Complete");               // Roy+180302

                        Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusMove);
                        //enJobDone = VehJobDone.Yes;                 // Roy-+180308 ...打斷CycleRun內部迴圈用 ... 
                        break;
                    case Veh_VehM_Global.ActionType.Teaching:
                        //check_UseorNot = true;
                        int completeCode = 1;
                        Veh_Move_Procedure(false, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination);
                        if (Veh_VehM_Global_Property.cmd_Length_Check != 0)
                        {
                            completeCode = 0;
                        }
                        Veh_VehM_Global.vehVehMomm.sned_Str172(completeCode);

                        break;
                    case Veh_VehM_Global.ActionType.AvoidRequest:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'AvoidRequest' Procedure");               // Roy+180302

                        break;
                }           // # switch (Veh_VehM_Global.enActionType)
                #endregion          // "enActionType"
            }
            else if (check_UseorNot == true)
            {
                #region "pauseContinue"
                pauseContinue = Veh_VehM_Global.now_Pause;

                switch (pauseContinue)
                {
                    case PauseEvent.Continue:
                        bool checkcontinue = check_Continue_0();
                        if (DDS_Global.checkblock == true)
                        {
                            Veh_Continue_Procedure_Restart_Block();
                            DDS_Global.checkblock = false;
                        }
                        else if (checkcontinue == true)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Continue' Procedure");
                            Veh_Continue_Procedure_Restart();
                            //Veh_VehM_Global.checkForNoMoveSend144 = false;
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Continue' Procedure Complete");
                            //Veh_CommandComplete();
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine =>There is still pause.");
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => normal_pause     = {0}.", Veh_VehM_Global.normal_pause);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => safety_pause     = {0}.", Veh_VehM_Global.safety_pause);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => earthquake_pause = {0}.", Veh_VehM_Global.earthquake_pause);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => HID_pause        = {0}.", Veh_VehM_Global.HID_pause);

                        }
                        break;

                    case PauseEvent.Pause:
                        //190913
                        //if (Veh_VehM_Global.enActionType == Veh_VehM_Global.ActionType.Override)
                        //{
                        //    Veh_Override_issue();
                        //    break;
                        //}
                        //else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Pause' Procedure");               // Roy*171030

                            Veh_Pause_Procedure();
                            //Veh_VehM_Global.checkForNoMoveSend144 = true;
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Pause' Procedure Complete");
                            //Veh_CommandComplete();
                        }
                        break;
                }           // # switch (pauseContinue)
                #endregion          // "pauseContinue"
            }
        }

        public bool check_Continue_0()
        {
            bool check_Continue = false;
            if (
                (Veh_VehM_Global.normal_pause == true)
                || (Veh_VehM_Global.safety_pause == true)
                || (Veh_VehM_Global.earthquake_pause == true)
                || (Veh_VehM_Global.HID_pause == true)
                )
            {

            }
            else
            {
                check_Continue = true;
            }
            return check_Continue;
        }
        /*
         * This is use for control the status mode trpye.
         */
        public void Modechange()
        {
            try
            {
                if (Veh_VehM_Global.chekcForModeChangeThread == false)
                {
                    Veh_VehM_Global.checkForNoMoveSend144 = false;
                    Veh_VehM_Global.chekcForModeChangeThread = true;

                    if (Veh_VehM_Global.Modestatus_from300 == VehControlMode.OnlineLocal)
                    {
                        bool errordone = false;
                        bool tempuse = false;
                        while (tempuse == false && Veh_VehM_Global.startCheck == false)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Modechange done Local.");
                            DDS_Global.motionInfo_VehInterCommReptDataReader.Take(
                                            ref motionInfoInterCommReport,
                                            ref sampleInfo_RptData,
                                             DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);
                            if (motionInfoInterCommReport != null)
                            {
                                if (motionInfoInterCommReport.Count() != 0)
                                {
                                    foreach (MotionInfo_Vehicle_Inter_Comm_ReportData data in motionInfoInterCommReport)
                                    {
                                        tempuse = true;
                                        SendValuesForRept(data, "144");
                                    }
                                }
                            }
                            Thread.Sleep(100);
                        }
                        Veh_VehM_Global.startCheck = false;
                        while (Veh_VehM_Global.vehModeStatus_fromVehC == VehControlMode.OnlineLocal)
                        {
                            DDS_Global.motionInfo_VehInterCommReptDataReader.Take(
                                        ref motionInfoInterCommReport,
                                        ref sampleInfo_RptData,
                                         DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);
                            Veh_OnlineLocal_func(motionInfoInterCommReport, ref errordone);
                            if (errordone == true)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Modechange errordone !!!!!");

                                break;
                            }
                            Thread.Sleep(100);
                        }
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Modechange back to Remote by OHT-L.");

                    }
                    else if (Veh_VehM_Global.Modestatus_from300 == VehControlMode.OnlineRemote)
                    {
                        bool tempchecksend144 = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Modechange done Manual.");
                        while (tempchecksend144 == false && Veh_VehM_Global.startCheck == false)
                        {
                            DDS_Global.motionInfo_VehInterCommReptDataReader.Take(
                                           ref motionInfoInterCommReport,
                                           ref sampleInfo_RptData,
                                            DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);
                            if (motionInfoInterCommReport != null)
                            {
                                if (motionInfoInterCommReport.Count() != 0)
                                {
                                    foreach (MotionInfo_Vehicle_Inter_Comm_ReportData data in motionInfoInterCommReport)
                                    {
                                        SendValuesForRept(data, "144");
                                        tempchecksend144 = true;
                                    }
                                }
                            }
                            Thread.Sleep(100);
                        }
                        Veh_VehM_Global.startCheck = false;
                    }
                    Veh_VehM_Global.chekcForModeChangeThread = false;
                    Veh_VehM_Global.checkForNoMoveSend144 = true;
                }
                else
                {
                    Veh_VehM_Global.checkForNoMoveSend144 = true;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Modechange already have a command.");

                }
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);

            }
        }

        protected void Veh_OnlineLocal_func(MotionInfo_Vehicle_Inter_Comm_ReportData[] motionInfoInterCommReport, ref bool errordone)
        {
            foreach (MotionInfo_Vehicle_Inter_Comm_ReportData data in motionInfoInterCommReport)
            {
                //Transport report and Block Query
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL online local @@@@@@eventTypes = {1 }.", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);
                /*
                 * Check the error code for the Veh
                 */
                if (data.ErrorCode != 0)
                {
                    //ErrorHandling(ref done);
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : !!!!!! Error Code != 0 , Please check again.");
                    errordone = true;
                    return;
                }
                #region Check Vehicle Motion Status for reporting to VehM
                switch (data.eventTypes)
                {
                    case (int)VehEventTypes.Address_Pass:

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : online local Address {1} Passed.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            // With Connection to VehM
                            SendValuesForRept(data, "134");
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Ending 134", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);

                            //SendValuesForRept(data, "144");
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Ending 144.", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);

                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Passed Sent.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.cycle);
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Stop Moving");
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Passed.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        }
                        break;
                    case (int)VehEventTypes.Address_Arrival:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : online local Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            // With Connection to VehM
                            SendValuesForRept(data, "134");
                            SendValuesForRept(data, "144");
                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Stop Moving");
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        }
                        break;


                }
                #endregion
            }
        }
        /// <summary>  jason++ 190223
        /// Veh_Override_issue()
        /// This function will do an cancel/abort at first , then send a new message.
        /// </summary>
        protected void Veh_Override_issue()
        {
            //string[] loadsection = null;
            //string[] unloadsection = null;
            while (!VehCanStop())
            {
                Thread.Sleep(10);
            }
            if (Veh_VehM_Global_Property.Pre31CmdType == Veh_VehM_Global.ActionType.Unknow)
            {
                return;
            }
            Veh_Abort_Procedure();
            if (Veh_VehM_Global_Property.arrive_Complete_Check != true)
            {
                Thread.Sleep(500);
            }

            Veh_VehM_Global_Property.arrive_Complete_Check = false;
            Veh_VehM_Global_Property.abort_On_Check = false;
            Veh_VehM_Global.send144check = true;
            Veh_VehM_Global_Property.IsCmdAbort = false;
            switch (Veh_VehM_Global_Property.Pre31CmdType)
            {
                case Veh_VehM_Global.ActionType.Move:
                    if (VertifyOverrideMoveSections())
                    {
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Begin 'Single-Move' To address.");               // Roy*171030

                        Veh_Move_Procedure(false, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination);

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Single-Move' To address Complete.");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusMove);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                    }
                    break;
                case Veh_VehM_Global.ActionType.Load:
                    if (VertifyOverrideMoveSections())
                    {
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Begin 'Loading' Procedure");               // Roy*171030

                        Veh_LoadUnLoad_Procedure(LoadCommand.Load, MoveType.single, Veh_VehM_Global.GuideSectionsStartToLoad, Veh_VehM_Global.GuideAddressesStartToLoad, Veh_VehM_Global.CSTID_Load.ToString());              // Roy*180308 ... temp. 

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => 'Loading' Procedure Complete");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusLoad);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                    }
                    break;

                case Veh_VehM_Global.ActionType.UnLoad:
                    if (VertifyOverrideMoveSections())
                    {
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Begin 'UnLoading' Procedure");               // Roy*171030

                        Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp. 

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => 'UnLoading' Procedure Complete");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusUnload);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                    }
                    break;
                case Veh_VehM_Global.ActionType.Load_Unload:
                    if (VertifyBeforeLoading())
                    {
                        if (VertifyOverrideMoveSections())
                        {
                            Veh_VehM_Global.checkForNoMoveSend144 = false;
                            int walkLengthTotal = 0;

                            string[] loadSection = new string[40];
                            string[] unloadSection = new string[40];
                            CheckSections.ClassifySections(ref loadSection, ref unloadSection);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'Loading' Procedure");               // Roy*171030

                            bool tempcheck = Veh_LoadUnLoad_Procedure(LoadCommand.Load, MoveType.single, loadSection, Veh_VehM_Global.GuideAddressesStartToLoad, Veh_VehM_Global.CSTID_Load.ToString());              // Roy*180308 ... temp. 
                            if (tempcheck == false)
                            {

                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : ActionTypeStatusMachine => LoadUnload cmd Error GGGGGGGGGGGGG");
                                break;
                            }
                            if (Veh_VehM_Global_Property.IsCmdAbort != true) //override check abort check 
                            {
                                walkLengthTotal = Veh_VehM_Global_Property.cmd_Length_Check; //load walk length
                                if (bIsOk2GoOnTrigger == false)
                                {
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : ActionTypeStatusMachine => LoadUnload cmd didn't arrive the last address");

                                    if (Veh_VehM_Global_Property.abort_On_Check != true)
                                    {
                                        Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusAbort);
                                    }
                                    Veh_VehM_Global.checkForNoMoveSend144 = true;
                                    break;
                                }
                                if (Veh_VehM_Global.check_4_BCRread == false)
                                {
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'ReadError ===>>> Cancel' Procedure");               // Roy*171030
                                    if (Veh_VehM_Global_Property.abort_On_Check != true)
                                    {
                                        Veh_VehM_Global.completeStatus completeStatus = Veh_VehM_Global.completeStatus.CmpStatusLoadunload;
                                        if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdMismatch)
                                        {
                                            completeStatus = Veh_VehM_Global.completeStatus.CmpStatusIdmisMatch;
                                        }
                                        else if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdReadFailed)
                                        {
                                            completeStatus = Veh_VehM_Global.completeStatus.CmpStatusIdreadFailed;
                                        }
                                        else if (Veh_VehM_Global.CancelType4Report == CMDCancelType.CmdCancelIdReadDuplicate)
                                        {
                                            completeStatus = Veh_VehM_Global.completeStatus.CmdStatusIdReadDuplicate;
                                        }
                                        Veh_CommandComplete(completeStatus);
                                        Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                                    }
                                }
                                else if (Veh_VehM_Global.check_4_BCRread == true)
                                {
                                    if (Veh_VehM_Global_Property.abort_On_Check != true)
                                    {
                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Begin 'UnLoading' Procedure");               // Roy*171030

                                        Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, unloadSection, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp.              // Roy*180319
                                        Veh_VehM_Global_Property.cmd_Length_Check = walkLengthTotal + Veh_VehM_Global_Property.cmd_Length_Check; //unload + load walk length

                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'UnLoading' Procedure Complete");               // Roy*171030
                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => 'Load-n-UnLoad' Procedure Complete");
                                        // Roy*171030
                                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                                        {
                                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusLoadunload);
                                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine Override => Loadunload is abort.");               // Roy*171030
                            }
                            Veh_VehM_Global_Property.arrive_Complete_Check = true;
                            Veh_VehM_Global.checkForNoMoveSend144 = true;
                        }
                    }
                    else
                    {
                        if (VertifyOverrideMoveSections())
                        {
                            Veh_VehM_Global.checkForNoMoveSend144 = false;
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => L_UL Begin 'UnLoading' Procedure");               // Roy*171030

                            Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp. 

                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => L_UL 'UnLoading' Procedure Complete");
                            if (Veh_VehM_Global_Property.abort_On_Check != true)
                            {
                                Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusLoadunload);
                                Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                            }
                            Veh_VehM_Global_Property.arrive_Complete_Check = true;
                            Veh_VehM_Global.checkForNoMoveSend144 = true;
                        }
                    }
                    break;
                default:
                    break;

            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        /// <summary>  jason++ 190509
        /// Veh_Avoid_Request_issue()
        /// This function will do an cancel/abort at first , then give a new guiding addresses and sections for a new moving. 
        /// After ending the new moving, send an ID_152 to VehC.
        /// When recieve the VehC ID_52 , VehC will give an override cmd by ID_31 just after it. 
        /// </summary>
        protected void Veh_Avoid_Request_issue()
        {
            //string[] loadsection = null;
            //string[] unloadsection = null;
            while (!VehCanStop())
            {
                Thread.Sleep(10);
            }
            if (Veh_VehM_Global_Property.Pre31CmdType == Veh_VehM_Global.ActionType.Unknow)
            {
                return;
            }
            Veh_Abort_Procedure();
            Veh_VehM_Global_Property.IsCmdAbort = false;

            switch (Veh_VehM_Global_Property.Pre31CmdType)
            {
                case Veh_VehM_Global.ActionType.Move:
                    if (VertifyOverrideMoveSections())
                    {
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Begin 'Single-Move' To address.");               // Roy*171030

                        Veh_Move_Procedure(false, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination);

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Single-Move' To address Complete.");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusMove);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                    }
                    break;
                case Veh_VehM_Global.ActionType.Load:
                    if (VertifyOverrideMoveSections())
                    {
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Begin 'Loading' Procedure");               // Roy*171030

                        Veh_LoadUnLoad_Procedure(LoadCommand.Load, MoveType.single, Veh_VehM_Global.GuideSectionsStartToLoad, Veh_VehM_Global.GuideAddressesStartToLoad, Veh_VehM_Global.CSTID_Load.ToString());              // Roy*180308 ... temp. 

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => 'Loading' Procedure Complete");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusLoad);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                    }
                    break;

                case Veh_VehM_Global.ActionType.UnLoad:
                    if (VertifyOverrideMoveSections())
                    {
                        Veh_VehM_Global.checkForNoMoveSend144 = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Begin 'UnLoading' Procedure");               // Roy*171030

                        Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp. 

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => 'UnLoading' Procedure Complete");

                        if (Veh_VehM_Global_Property.abort_On_Check != true)
                        {
                            Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusUnload);
                            Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                    }
                    break;
                case Veh_VehM_Global.ActionType.Load_Unload:
                    if (VertifyBeforeLoading())
                    {
                        if (VertifyOverrideMoveSections())
                        {
                            Veh_VehM_Global.checkForNoMoveSend144 = false;
                            int walkLengthTotal = 0;

                            string[] loadSection = new string[40];
                            string[] unloadSection = new string[40];
                            //CheckSections.ClassifySections(ref loadSection, ref unloadSection);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Begin 'Loading' Procedure");               // Roy*171030

                            Veh_LoadUnLoad_Procedure(LoadCommand.Load, MoveType.single, Veh_VehM_Global.GuideSectionsStartToLoad, Veh_VehM_Global.GuideAddressesStartToLoad, Veh_VehM_Global.CSTID_Load.ToString());              // Roy*180308 ... temp. 

                            if (bIsOk2GoOnTrigger == false)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : Veh_Override_issue => Abort the load unload Procedure");

                                walkLengthTotal = Veh_VehM_Global_Property.cmd_Length_Check; //load walk length

                                if (Veh_VehM_Global_Property.abort_On_Check != true)
                                {
                                    Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusAbort);
                                }
                                break;
                            }

                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => Begin 'UnLoading' Procedure");               // Roy*171030

                            Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp.              // Roy*180319
                            Veh_VehM_Global_Property.cmd_Length_Check = walkLengthTotal + Veh_VehM_Global_Property.cmd_Length_Check; //unload + load walk length

                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => 'UnLoading' Procedure Complete");               // Roy*171030
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => 'Load-n-UnLoad' Procedure Complete");               // Roy*171030

                            if (Veh_VehM_Global_Property.abort_On_Check != true)
                            {
                                Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusLoadunload);
                                Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                            }
                        }
                        Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        Veh_VehM_Global.checkForNoMoveSend144 = true;
                    }
                    else
                    {
                        if (VertifyOverrideMoveSections())
                        {
                            Veh_VehM_Global.checkForNoMoveSend144 = false;
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => L_UL Begin 'UnLoading' Procedure");               // Roy*171030

                            Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp. 

                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_Override_issue => L_UL 'UnLoading' Procedure Complete");
                            if (Veh_VehM_Global_Property.IsCmdAbort != true)
                            {
                                Veh_LoadUnLoad_Procedure(LoadCommand.UnLoad, MoveType.single, Veh_VehM_Global.GuideSections, Veh_VehM_Global.GuideAddressesToDestination, Veh_VehM_Global.CSTID_UnLoad.ToString());              // Roy*180308 ... temp. 
                                Veh_VehM_Global_Property.Pre31CmdType = Veh_VehM_Global.ActionType.Unknow;
                            }
                            Veh_VehM_Global_Property.arrive_Complete_Check = true;
                            Veh_VehM_Global.checkForNoMoveSend144 = true;
                        }
                    }
                    break;
                default:
                    break;

            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private static bool VehCanStop()
        {
            bool temp = false;
            if (Veh_VehM_Global_Property.Pre31CmdStep == Pre31CmdSteps.Moving)
            {
                temp = true;
            }
            return temp;
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private bool VertifyBeforeLoading()
        {
            string loadSection = CheckSections.FindSectionOfAddress(Veh_VehM_Global.LoadAddress);

            return Veh_VehM_Global.GuideSections.Contains(loadSection);
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private bool VertifyOverrideMoveSections()
        {
            int index = 0;
            int overrideGuideSectionLength = Veh_VehM_Global.GuideSections.Length;
            Veh_VehM_Global_Property.curSection = Veh_VehM_Global.Section;
            while (index < overrideGuideSectionLength)
            {
                if ("0" + Veh_VehM_Global.GuideSections[index] == Veh_VehM_Global_Property.curSection)
                {
                    if (index > 0)
                    {
                        Veh_VehM_Global.GuideSections = Veh_VehM_Global.GuideSections.Skip(index).ToArray();
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : XX VertifyOverrideMoveSections new section start = {0} XX", Veh_VehM_Global.GuideSections[0].ToString());
                    }
                    break;
                }
                index++;
            }

            if (index == overrideGuideSectionLength)
            {
                //FindSectionFailReport();
                string something_wrong = "something wired.";
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM :  VertifyOverrideMoveSections >>> {0}", something_wrong);                // Roy+180308

                return false;
            }

            return true;
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected void Veh_CommandComplete(Veh_VehM_Global.completeStatus comstatus)
        {
            if (bIsOk2GoOnTrigger == true || Veh_VehM_Global_Property.abort_On_Check == true)
            {
                Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.MoveComplete;
                MotionInfo_Vehicle_Inter_Comm_ReportData tempfor144 = new MotionInfo_Vehicle_Inter_Comm_ReportData();

                tempfor144.Address = Veh_VehM_Global.Address;
                tempfor144.Section = Veh_VehM_Global.Section;
                tempfor144.BlockControlSection = Veh_VehM_Global.BlockControlSection;
                tempfor144.cmpCode = Veh_VehM_Global.cmpCode;
                tempfor144.cmpStatus = Veh_VehM_Global.cmpStatus;
                tempfor144.DistanceFromSectionStart = Veh_VehM_Global.DistanceFromSectionStart;
                tempfor144.WalkLength = Veh_VehM_Global.vehWalkLength;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Veh_CommandComplete tempfor144.DistanceFromSectionStart = {0}", tempfor144.DistanceFromSectionStart.ToString());                // Roy+180308
                tempfor144.loadStatus.With_CST = (int)Veh_VehM_Global.hasCst;
                tempfor144.vehLeftGuideLockStatus = Veh_VehM_Global.vehLeftGuideLockStatus;
                tempfor144.vehRightGuideLockStatus = Veh_VehM_Global.vehRightGuideLockStatus;
                tempfor144.ConrtolMode = Veh_VehM_Global.vehModeStatus_fromVehC;
                tempfor144.vehLoadStatus = Veh_VehM_Global.vehLoadStatus;
                tempfor144.vehPauseStatus = Veh_VehM_Global.vehPauseStatus;
                tempfor144.eventTypes = Veh_VehM_Global.eventTypes;
                tempfor144.vehBlockStopStatus = Veh_VehM_Global.vehBlockStopStatus;
                tempfor144.vehObstacleStopStatus = Veh_VehM_Global.vehObstStopStatus;
                tempfor144.vehObstDist = Veh_VehM_Global.vehObstDist;
                tempfor144.loadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_Load;
                tempfor144.unLoadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_UnLoad;
                tempfor144.vehActionStatus = Veh_VehM_Global.vehActionStatus;
                tempfor144.HIDControlSection = Veh_VehM_Global.HIDControlSection;
                tempfor144.BatteryCapacity = Veh_VehM_Global.BatteryCapacity;
                tempfor144.BatteryTemperature = Veh_VehM_Global.BatteryTemperature;
                MotionInfo_Vehicle_Inter_Comm_ReportData nothing = new MotionInfo_Vehicle_Inter_Comm_ReportData();
                nothing.cmpStatus = (int)comstatus;
                nothing.WalkLength = Veh_VehM_Global_Property.cmd_Length_Check;
                nothing.loadStatus.With_CST = (int)Veh_VehM_Global.hasCst;
                nothing.loadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_Load;
                nothing.unLoadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_UnLoad;
                nothing.DistanceFromSectionStart = Veh_VehM_Global.DistanceFromSectionStart;
                SpinWait.SpinUntil(() => false, 1000);
                SendValuesForRept(nothing, "132");
                ///
                /// Write the situation to the host.
                ///
                SendValuesForRept(tempfor144, "144");
                Veh_VehM_Global_Property.already_have_command_Check = false;   //
            }
            else
            {

            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected bool Veh_Move_Procedure(bool done, MoveType type, string[] sectionsArray, string[] addressesArray, bool isMtlhome = false)
        {
            try
            {
                int countnum_ofsections = 0;
                bIsOk2GoOnTrigger = false;
                int Store_Start_length = 0;
                int has_already_Count = 0;
                //int reserve_direction_list_count = 0;
                string[] sections = null;
                int[] reserve_direction_list = null;
                string[] willPassSections = null;
                if (sectionsArray != null && sectionsArray.Count() != 0)
                {
                    if (sectionsArray[0] != null)
                    {
                        if (sectionsArray.Count() != 0)
                        {
                            for (int i = 0; i < sectionsArray.Count(); i++)
                            {
                                sectionsArray[i] = "+" + sectionsArray[i];
                            }
                            //DDS_Global.motionInfoInterCommSendData.udtMove.GuidingSections = new TransSectioncs().TransMap4Veh(sectionsArray, addressesArray, out Store_Start_length, ref reserve_direction_list);
                            //reserve_direction_list_count = reserve_direction_list.Count();
                        }
                        sections = sectionsArray;
                        willPassSections = sections;
                    }
                }
                Veh_VehM_Global.Store_Start_Length = Store_Start_length;
                // Here need to put addresses.
                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;
                DDS_Global.motionInfoInterCommSendData.udtMove.GuidingSections = sections;
                DDS_Global.motionInfoInterCommSendData.udtMove.Address = Veh_VehM_Global.UnloadAddress;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL : udtMove.Address = {0}.", DDS_Global.motionInfoInterCommSendData.udtMove.Address);

                DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = (int)MoveType.single;                //+++               // Roy+180319

                DDS_Global.motionInfoInterCommSendData.udtMove.ForLoading = 0;                  // Roy+190205
                DDS_Global.motionInfoInterCommSendData.udtMove.ForUnLoading = 0;                  // Roy+190205
                //DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 0;                  // Roy+190205

                if (isMtlhome == false)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_Move_Procedure : Send the CmdToMove command to the Veh");
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 0;
                }
                else if (isMtlhome == true)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_Move_Procedure : Send the CmdToMaintenance command to the Veh");
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 1;
                }

                DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = type;
                DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.NG;
                DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.NG;
                //
                VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
                // Inform SendData Sent
                DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_Move_Procedure : Send the command to the Veh local");
                //
                motionInfoInterCommReport = null;
                sampleInfo_RptData = null;
                //
                done = false;
                string cstID = "NoCstID";
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_Move_Procedure :cstID = NoCstID ");

                Veh_VehM_Global.chargeport = false;
                Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Moving;
                while (!done)
                {
                    //if (!Veh_VehM_Global.blCycleRun) enJobDone = VehJobDone.Yes;
                    #region DDShandshake
                    DDS_Global.motionInfo_HandShakeRecieveDataReader.Take(
                        ref handShakeRxData,
                        ref sampleInfo_RxData,
                        DDS.SampleStateKind.Any,
                        DDS.ViewStateKind.Any,
                        DDS.InstanceStateKind.Any);

                    DDS_Global.motionInfo_HandShakeSendDataReader.Take(
                        ref handShakeSendData,
                        ref sampleInfo_SendData,
                        DDS.SampleStateKind.Any,
                        DDS.ViewStateKind.Any,
                        DDS.InstanceStateKind.Any);

                    if (handShakeSendData != null)
                    {
                        foreach (MotionInfo_HandShake_SendData data in handShakeSendData)
                        {
                            if (data.cmdReceive == 1)
                            {
                                Veh_VehM_Global.blSendDataReceived = true;
                                DDS_Global.motionInfoHandShakeTxData.cmdSend = 0;
                                DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                                DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
                            }
                            else
                            {
                                Veh_VehM_Global.blSendDataReceived = false;
                            }
                        }
                    }

                    if (handShakeRxData != null)
                    {
                        foreach (MotionInfo_HandShake_RecieveData data in handShakeRxData)
                        {
                            if (data.cmdSend == 1)
                            {
                                Veh_VehM_Global.blRxDataSent = true;
                                DDS_Global.motionInfoHandShakeRxData.cmdReceive = 1;
                                DDS_Global.motionInfo_HandShakeRecieveDataWriter.Write(DDS_Global.motionInfoHandShakeRxData);
                            }
                            else
                            {
                                Veh_VehM_Global.blRxDataSent = false;
                            }
                        }
                    }
                    #endregion
                    Veh_VehM_Global.blRxDataSent = true;

                    if (Veh_VehM_Global.blRxDataSent)
                    {
                        DDS_Global.motionInfo_VehInterCommReptDataReader.Take(
                                ref motionInfoInterCommReport,
                                ref sampleInfo_RptData,
                                 DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);

                        if (motionInfoInterCommReport != null)
                        {
                            AnalyzeDDSFeedback(LoadCommand.JustMove, ref done, motionInfoInterCommReport, cstID, reserve_direction_list, ref has_already_Count, willPassSections, ref countnum_ofsections);
                        }           // # if (motionInfoInterCommReport != null)
                    }           // # if (Veh_VehM_Global.blRxDataSent)
                    //new Fake144ForChargeMode().Fake144forChargeMode(done);
                    Thread.Sleep(10);
                }           // # while (enJobDone == VehJobDone.No)

                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected bool Veh_LoadUnLoad_Procedure(LoadCommand enLoad, MoveType type, string[] sectionsArray, string[] addresses, string cstID)                // Roy+180308
        {
            try
            {
                int countnum_ofsections = 0;
                bool done = false;
                string loadStatus = string.Empty;
                string[] sections = null;
                bIsOk2GoOnTrigger = false;                         // Roy+180308
                //int Store_Start_length = 0;
                int[] reserve_direction_list = null;
                int has_already_Count = 0;
                //int reserve_direction_list_count = 0;
                string[] willPassSections = null;
                if (sectionsArray != null && sectionsArray.Count() != 0)
                {
                    if (sectionsArray[0] != null)
                    {
                        if (sectionsArray.Count() != 0)
                        {
                            for (int i = 0; i < sectionsArray.Count(); i++)
                            {
                                sectionsArray[i] = "+" + sectionsArray[i];
                            }
                            //DDS_Global.motionInfoInterCommSendData.udtMove.GuidingSections = new TransSectioncs().TransMap4Veh(sectionsArray, addressesArray, out Store_Start_length, ref reserve_direction_list);
                            //reserve_direction_list_count = reserve_direction_list.Count();
                        }
                        //Veh_VehM_Global.Store_Start_Length = Store_Start_length;
                        sections = sectionsArray;
                        willPassSections = sections;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Veh_LoadUnLoad_Procedure sections = {0}", sections[0].ToString());

                    }
                }
                #region Inform OHT Vehicle move to Load Port or Unload Port
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Veh_LoadUnLoad_Procedure Enter the {0}", enLoad.ToString());
                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToMove;
                DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = (int)MoveType.single;                //+++               // Roy+180319

                DDS_Global.motionInfoInterCommSendData.udtMove.GuidingSections = sectionsArray;


                //System.Diagnostics.Debug.Assert(sections.Count() > 0);                        // Roy+180308               // Roy-190205

                if (enLoad == LoadCommand.Load)
                {
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForLoading = 1;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForUnLoading = 0;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 0;                  // Roy+190205

                    DDS_Global.motionInfoInterCommSendData.udtMove.Address = Veh_VehM_Global.LoadAddress;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL : udtMove.Address = {0}.", DDS_Global.motionInfoInterCommSendData.udtMove.Address);

                    loadStatus = "Load";

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++               // Roy+190205
                    if (sections == null)
                    {
                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToLoad;
                        //sendloading();
                    }
                    else if (sections.Count() == 0)
                    {
                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToLoad;
                        //sendloading();
                    }
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                }
                else
                {
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForLoading = 0;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForUnLoading = 1;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 0;                  // Roy+190205

                    DDS_Global.motionInfoInterCommSendData.udtMove.Address = Veh_VehM_Global.UnloadAddress;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL : udtMove.Address = {0}.", DDS_Global.motionInfoInterCommSendData.udtMove.Address);
                    // Veh_VehM_Global.ToAddress;               // Roy*180319
                    loadStatus = "UnLoad";

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++               // Roy+190205
                    if (sections == null)
                    {
                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToUnload;
                        //sendunloading();
                    }
                    else if (sections.Count() == 0)
                    {
                        DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToUnload;
                        //sendunloading();
                    }
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                }

                DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = type;
                DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.NG;
                DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.NG;

                //
                //VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
                if (sections == null || sections.Length == 0)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Veh_LoadUnLoad_Procedure sections = NULL!!!!");
                }
                else if (sections.Length != 0)
                {

                    VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Veh_LoadUnLoad_Procedure DDS SEND", sections[0].ToString());

                }
                DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
                #endregion



                motionInfoInterCommReport = null;
                sampleInfo_RptData = null;

                done = false;                           // Roy+180308 

                //
                Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Moving;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Veh_LoadUnLoad_Procedure : Enter load unload waiting loop !!!!");

                while (!done)
                {
                    #region DDS Share Memory Exchange
                    DDS_Global.motionInfo_HandShakeRecieveDataReader.Take(
                        ref handShakeRxData,
                        ref sampleInfo_RxData,
                        DDS.SampleStateKind.Any,
                        DDS.ViewStateKind.Any,
                        DDS.InstanceStateKind.Any);

                    DDS_Global.motionInfo_HandShakeSendDataReader.Take(
                        ref handShakeSendData,
                        ref sampleInfo_SendData,
                        DDS.SampleStateKind.Any,
                        DDS.ViewStateKind.Any,
                        DDS.InstanceStateKind.Any);

                    // Handshaking with OHT 
                    if (handShakeSendData != null)
                    {
                        foreach (MotionInfo_HandShake_SendData data in handShakeSendData)
                        {
                            if (data.cmdReceive == 1)
                            {
                                Veh_VehM_Global.blSendDataReceived = true;
                                DDS_Global.motionInfoHandShakeTxData.cmdSend = 0;
                                DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                                DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
                            }
                            else
                            {
                                Veh_VehM_Global.blSendDataReceived = false;
                            }
                        }
                        //blRxDataSent = true;
                    }

                    if (handShakeRxData != null)
                    {
                        foreach (MotionInfo_HandShake_RecieveData data in handShakeRxData)
                        {
                            if (data.cmdSend == 1)
                            {
                                Veh_VehM_Global.blRxDataSent = true;
                                DDS_Global.motionInfoHandShakeRxData.cmdReceive = 1;
                                DDS_Global.motionInfo_HandShakeRecieveDataWriter.Write(DDS_Global.motionInfoHandShakeRxData);
                            }
                            else
                            {
                                Veh_VehM_Global.blRxDataSent = false;
                            }
                        }
                        //blRxDataSent = true;
                    }
                    #endregion
                    //
                    Veh_VehM_Global.blRxDataSent = true;

                    #region Check OHT Vehicle Motion Status via DDS Exchanged Data
                    if (Veh_VehM_Global.blRxDataSent)
                    {
                        DDS_Global.motionInfo_VehInterCommReptDataReader.Take(
                                ref motionInfoInterCommReport,
                                ref sampleInfo_RptData,
                                 DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);
                        if (sections != null)
                        {
                            if (sections.Length != 0 && sections[0] != null)
                            {
                                if (motionInfoInterCommReport != null)
                                {
                                    //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_Move_Procedure : enter AnalyzeDDSFeedback MOVE");
                                    AnalyzeDDSFeedback(enLoad, ref done, motionInfoInterCommReport, cstID, reserve_direction_list, ref has_already_Count, willPassSections, ref countnum_ofsections);
                                }           // #  if (motionInfoInterCommReport != null)
                            }
                            else
                            {
                                if (enLoad != LoadCommand.JustMove)
                                {
                                    Veh_VehM_Global.can_abort_cancel = false;
                                    done = TrigEventLoadUnLoadingArrival_No_Section_Load_Unload(enLoad, cstID);
                                }
                            }
                        }
                        else
                        {
                            if (enLoad != LoadCommand.JustMove)
                            {
                                Veh_VehM_Global.can_abort_cancel = false;
                                done = TrigEventLoadUnLoadingArrival_No_Section_Load_Unload(enLoad, cstID);
                            }
                        }
                    }           // #  if (Veh_VehM_Global.blRxDataSent)
                    #endregion

                    Thread.Sleep(10);
                }               //# while (!done) 
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Veh_LoadUnLoad_Procedure : Abort_On_Check = {0} ", Veh_VehM_Global_Property.abort_On_Check.ToString());
                if (Veh_VehM_Global_Property.abort_On_Check != true)
                {
                    if (Veh_VehM_Global.checkError == true)
                    {
                        //bool temp_check = false;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "Veh_LoadUnLoad_Procedure : checkError = true  !!!!");
                        //return temp_check;
                    }
                    WaitForLoadUnLoading(enLoad);                         // Roy*180308 ... from WaitForLoading to WaitForLoadUnLoading
                }


                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++                    // Roy+180308
        protected bool TrigEventLoadUnLoadingArrival(LoadCommand enLoad, MotionInfo_Vehicle_Inter_Comm_ReportData data, string cstID)
        {
            string loadStatus = string.Empty;
            bIsOk2GoOnTrigger = true;
            Veh_VehM_Global.check_recieve_36 = false;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :~~ send 136 arrivals ~~");
            SendValuesForRept(data, "136");

            Thread.Sleep(1500);

            while (Veh_VehM_Global.check_recieve_36 != true)
            {
                Thread.Sleep(1000);
                SendValuesForRept(data, "136");
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :~~ Resend 136 arrivals.~~");
            }

            if (Veh_VehM_Global.check_recieve_36 == true)
            {
                Thread.Sleep(999);                  // Roy+191013

                if (enLoad == LoadCommand.Load)
                {
                    Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Loading;

                    #region "load-arrival event"
                    loadStatus = "Load";
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Address {1} Load Port Arrival.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address, loadStatus);

                    Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load;
                    DDS_Global.motionInfoInterCommSendData.udtLoad.Veh_CSTID = cstID;                     // Roy*180319
                    DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToLoad;

                    DDS_Global.motionInfoInterCommSendData.udtMove.ForLoading = 1;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForUnLoading = 0;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 0;                  // Roy+190205

                    DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.udtMove.GuidingSections = new string[0]; // Use for clear out the place of OHT.
                                                                                                    //
                    VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData); // This part really send the load cmd to the vehicle.
                    //send vhloading.   191021++ jason
                    Veh_VehM_Global.load_Process = "starting";
                    SendValuesForRept(data, "136");
                    Veh_VehM_Global.load_Process = null;
                    ////////
                    Veh_VehM_Global.lul_time_start = DateTime.Now;//計時開始 取得目前時間
                    Veh_VehM_Global.start_loading_or_unloading = true;
                    // Inform SendData Sent
                    DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                    DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                    DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);

                    // When motion stopped, report Load Arrival
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        // With Connection to VehM

                        //SendValuesForRept(data, "136");

                        if (!vehTcpComm.TcpIpTimeOut)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2}: Address {1} Load Cmd-Sent Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address, loadStatus);
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address, loadStatus);
                            Veh_TcpIpComm_TimeOutStop(MoveType.single);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  : Stop Loading");
                        }
                    }
                    else
                    {
                        // Without Connection to VehM
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Address {1} Load Cmd-Sent Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address, loadStatus);
                    }

                    //
                    ////done = true;
                    #endregion          // "load-arrival event"

                }
                else
                {
                    Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Unloading;

                    #region "unload-arrival event"
                    loadStatus = "UnLoad";

                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Address {1} UnLoad Port Arrival.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address, loadStatus);

                    //Wait for the Vehicle to arrive at the Load Port 

                    Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.UnLoad;
                    DDS_Global.motionInfoInterCommSendData.udtUnLoad.Veh_CSTID = cstID;                     // Roy*180319
                    DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToUnload;

                    DDS_Global.motionInfoInterCommSendData.udtMove.ForLoading = 0;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForUnLoading = 1;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 0;                  // Roy+190205

                    DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.udtMove.GuidingSections = new string[0]; // Use for clear out the place of OHT.
                                                                                                    //
                    VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
                    //send vhunloading
                    Veh_VehM_Global.unload_Process = "starting";
                    SendValuesForRept(data, "136");
                    Veh_VehM_Global.unload_Process = null;
                    //////
                    Veh_VehM_Global.lul_time_start = DateTime.Now;//計時開始 取得目前時間
                    Veh_VehM_Global.start_loading_or_unloading = true;
                    // Inform SendData Sent
                    DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                    DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                    DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);

                    // When motion stopped, report UnLoad Arrival
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        if (!vehTcpComm.TcpIpTimeOut)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2}: Address {1} UnLoad Cmd-Sent Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address, loadStatus);
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address, loadStatus);
                            Veh_TcpIpComm_TimeOutStop(MoveType.single);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  : Stop UnLoading");
                        }
                    }
                    else
                    {
                        // Without Connection to VehM
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Address {1} UnLoad Cmd-Sent Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address, loadStatus);
                    }

                    //
                    ////done = true;
                    #endregion          // "unload-arrival event"

                }
            }
            else
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :This shouldn't be happened, something very wierd");
            }
            return true;
        }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected bool TrigEventLoadUnLoadingArrival_No_Section_Load_Unload(LoadCommand enLoad, string cstID)
        {
            string loadStatus = string.Empty;
            bIsOk2GoOnTrigger = true;
            Veh_VehM_Global.check_recieve_36 = false;

            #region Create a 136 info for OHTC
            MotionInfo_Vehicle_Inter_Comm_ReportData tempforLoadUnload = new MotionInfo_Vehicle_Inter_Comm_ReportData();
            tempforLoadUnload.Address = Veh_VehM_Global.Address;
            tempforLoadUnload.Section = Veh_VehM_Global.Section;
            tempforLoadUnload.BlockControlSection = Veh_VehM_Global.BlockControlSection;
            tempforLoadUnload.cmpCode = Veh_VehM_Global.cmpCode;
            tempforLoadUnload.cmpStatus = Veh_VehM_Global.cmpStatus;
            tempforLoadUnload.DistanceFromSectionStart = Veh_VehM_Global.DistanceFromSectionStart;
            tempforLoadUnload.vehLeftGuideLockStatus = Veh_VehM_Global.vehLeftGuideLockStatus;
            tempforLoadUnload.vehRightGuideLockStatus = Veh_VehM_Global.vehRightGuideLockStatus;
            tempforLoadUnload.ConrtolMode = Veh_VehM_Global.vehModeStatus_fromVehC;
            tempforLoadUnload.vehLoadStatus = Veh_VehM_Global.vehLoadStatus;
            tempforLoadUnload.vehPauseStatus = Veh_VehM_Global.vehPauseStatus;
            tempforLoadUnload.eventTypes = Veh_VehM_Global.eventTypes;
            tempforLoadUnload.vehBlockStopStatus = Veh_VehM_Global.vehBlockStopStatus;
            tempforLoadUnload.vehObstacleStopStatus = Veh_VehM_Global.vehObstStopStatus;
            tempforLoadUnload.vehObstDist = Veh_VehM_Global.vehObstDist;
            tempforLoadUnload.loadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_Load;
            tempforLoadUnload.unLoadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_UnLoad;
            tempforLoadUnload.vehActionStatus = Veh_VehM_Global.vehActionStatus;
            tempforLoadUnload.HIDControlSection = Veh_VehM_Global.HIDControlSection;
            tempforLoadUnload.loadStatus.With_CST = (int)Veh_VehM_Global.hasCst;
            SendValuesForRept(tempforLoadUnload, "144");
            if (enLoad == LoadCommand.Load)
            {
                sendloading();
            }
            else if (enLoad == LoadCommand.UnLoad)
            {
                sendunloading();
            }
            #endregion

            Thread.Sleep(1000);

            while (Veh_VehM_Global.check_recieve_36 != true)
            {
                Thread.Sleep(1000);
                if (enLoad == LoadCommand.Load)
                {
                    sendloading();
                }
                else if (enLoad == LoadCommand.UnLoad)
                {
                    sendunloading();
                }
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM N :Resend 136 arrivals.");
            }

            if (Veh_VehM_Global.check_recieve_36 == true)
            {
                Thread.Sleep(999);                  // Roy+191013

                if (enLoad == LoadCommand.Load)
                {
                    Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Loading;

                    #region "load-arrival event"
                    loadStatus = "Load";
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Address {1} Load Port Arrival.", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.Address, loadStatus);

                    Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.Load;
                    DDS_Global.motionInfoInterCommSendData.udtLoad.Veh_CSTID = cstID;                     // Roy*180319
                    DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToLoad;

                    DDS_Global.motionInfoInterCommSendData.udtMove.ForLoading = 1;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForUnLoading = 0;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 0;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.Address = Veh_VehM_Global.LoadAddress; // Jason++ 200121
                    DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.udtMove.GuidingSections = new string[0]; // Use for clear out the place of OHT.
                                                                                                    //
                    VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
                    //send vhloading.   191021++ jason
                    Veh_VehM_Global.load_Process = "starting";
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Send start loading to OHTC  ", Veh_VehM_Global.Address, loadStatus);
                    SendValuesForRept(tempforLoadUnload, "136");
                    Veh_VehM_Global.load_Process = null;
                    ////////
                    Veh_VehM_Global.lul_time_start = DateTime.Now;//計時開始 取得目前時間
                    Veh_VehM_Global.start_loading_or_unloading = true;
                    // Inform SendData Sent
                    DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                    DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                    DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);

                    // When motion stopped, report Load Arrival
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        // With Connection to VehM


                        if (!vehTcpComm.TcpIpTimeOut)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2}: Address {1} Load Cmd-Sent Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.Address, loadStatus);
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.Address, loadStatus);
                            Veh_TcpIpComm_TimeOutStop(MoveType.single);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  : Stop Loading");
                        }
                    }
                    else
                    {
                        // Without Connection to VehM
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Address {1} Load Cmd-Sent Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.Address, loadStatus);
                    }

                    //
                    ////done = true;
                    #endregion          // "load-arrival event"

                }
                else if (enLoad == LoadCommand.UnLoad)
                {
                    Veh_VehM_Global_Property.Pre31CmdStep = Pre31CmdSteps.Unloading;

                    #region "unload-arrival event"
                    loadStatus = "UnLoad";

                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM N :  {2} : Address {1} UnLoad Port Arrival.", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.Address, loadStatus);

                    //Wait for the Vehicle to arrive at the Load Port 

                    Veh_VehM_Global.enActionType = Veh_VehM_Global.ActionType.UnLoad;
                    DDS_Global.motionInfoInterCommSendData.udtUnLoad.Veh_CSTID = cstID;                     // Roy*180319
                    DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToUnload;

                    DDS_Global.motionInfoInterCommSendData.udtMove.ForLoading = 0;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForUnLoading = 1;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.ForMaintain = 0;                  // Roy+190205
                    DDS_Global.motionInfoInterCommSendData.udtMove.Address = Veh_VehM_Global.UnloadAddress; // Jason++ 200121
                    DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.OK;
                    DDS_Global.motionInfoInterCommSendData.udtMove.GuidingSections = new string[0]; // Use for clear out the place of OHT.
                                                                                                    //
                    VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
                    //send vhunloading
                    Veh_VehM_Global.unload_Process = "starting";
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Send start unloading to OHTC  ", Veh_VehM_Global.Address, loadStatus);
                    SendValuesForRept(tempforLoadUnload, "136");
                    Veh_VehM_Global.unload_Process = null;
                    //////
                    Veh_VehM_Global.lul_time_start = DateTime.Now;//計時開始 取得目前時間
                    Veh_VehM_Global.start_loading_or_unloading = true;
                    // Inform SendData Sent
                    DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                    DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                    DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);

                    // When motion stopped, report UnLoad Arrival
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        // With Connection to VehM
                        if (!vehTcpComm.TcpIpTimeOut)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2}: Address {1} UnLoad Cmd-Sent Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.Address, loadStatus);
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.Address, loadStatus);
                            Veh_TcpIpComm_TimeOutStop(MoveType.single);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  : Stop UnLoading");
                        }
                    }
                    else
                    {
                        // Without Connection to VehM
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Address {1} UnLoad Cmd-Sent Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.Address, loadStatus);
                    }

                    //
                    ////done = true;
                    #endregion          // "unload-arrival event"

                }
            }
            else
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM N :This shouldn't be happened, something very wierd");
            }
            return true;
        }


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++           
        protected void WaitForLoadUnLoading(LoadCommand enLoad)                         // Roy*180308 ... from WaitForLoading to WaitForLoadUnLoading
        {
            bool Wdone = false;
            bool _911point = false;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Enter '{1}' Procedure ... ", DateTime.Now.ToString("HH:mm:ss.fff"), enLoad.ToString());                    // Roy+180308

            while (!Wdone)
            {
                //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Wait for Loading : done = {0} ... ", done);                    // Roy+180308
                #region handshake no use
                DDS_Global.motionInfo_HandShakeRecieveDataReader.Take(
                    ref handShakeRxData,
                    ref sampleInfo_RxData,
                    DDS.SampleStateKind.Any,
                    DDS.ViewStateKind.Any,
                    DDS.InstanceStateKind.Any);

                DDS_Global.motionInfo_HandShakeSendDataReader.Take(
                    ref handShakeSendData,
                    ref sampleInfo_SendData,
                    DDS.SampleStateKind.Any,
                    DDS.ViewStateKind.Any,
                    DDS.InstanceStateKind.Any);

                // Handshaking with OHT 
                if (handShakeSendData != null)
                {
                    foreach (MotionInfo_HandShake_SendData data in handShakeSendData)
                    {
                        if (data.cmdReceive == 1)
                        {
                            Veh_VehM_Global.blSendDataReceived = true;
                            DDS_Global.motionInfoHandShakeTxData.cmdSend = 0;
                            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
                        }
                        else
                        {
                            Veh_VehM_Global.blSendDataReceived = false;
                        }
                    }           // #  foreach (MotionInfo_HandShake_SendData data in handShakeSendData)
                }

                //
                if (handShakeRxData != null)
                {
                    foreach (MotionInfo_HandShake_RecieveData data in handShakeRxData)
                    {
                        if (data.cmdSend == 1)
                        {
                            Veh_VehM_Global.blRxDataSent = true;
                            DDS_Global.motionInfoHandShakeRxData.cmdReceive = 1;
                            DDS_Global.motionInfo_HandShakeRecieveDataWriter.Write(DDS_Global.motionInfoHandShakeRxData);
                        }
                        else
                        {
                            Veh_VehM_Global.blRxDataSent = false;
                        }
                    }           // # foreach (MotionInfo_HandShake_RecieveData data in handShakeRxData)
                }
                #endregion
                Veh_VehM_Global.blRxDataSent = true; // jason++ for something weird

                //
                if (Veh_VehM_Global.blRxDataSent)
                {
                    DDS_Global.motionInfo_VehInterCommReptDataReader.Take(
                            ref motionInfoInterCommReport,
                            ref sampleInfo_RptData,
                             DDS.SampleStateKind.Any, DDS.ViewStateKind.Any, DDS.InstanceStateKind.Any);

                    if (motionInfoInterCommReport != null)
                    {
                        foreach (MotionInfo_Vehicle_Inter_Comm_ReportData data in motionInfoInterCommReport)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Waiting for load unload @@@@@@eventTypes = {1 }.", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : LOAD error code  = {0}", data.ErrorCode.ToString());
                            bool Errordone = false;
                            Errordone = ErrorCodeRead(data, ref _911point);

                            if (Errordone == true)
                            {
                                Wdone = true;
                                break;
                            }
                            //Loading Status report 
                            switch (data.eventTypes)
                            {
                                //jason++ 181031
                                case (int)VehEventTypes.Load_Pick:
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Load pick.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                                    if (!Veh_VehM_Global.OffLineTest)
                                    {
                                        // With Connection to VehM
                                        Veh_VehM_Global.load_Process = "complete";
                                        SendValuesForRept(data, "136");
                                        Veh_VehM_Global.load_Process = null;

                                    }
                                    break;

                                case (int)VehEventTypes.Unload_Place:
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  {2} : Unload pick.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);           // Roy*190205

                                    if (!Veh_VehM_Global.OffLineTest)
                                    {
                                        // With Connection to VehM
                                        Veh_VehM_Global.unload_Process = "complete";
                                        SendValuesForRept(data, "136");
                                        Veh_VehM_Global.unload_Process = null;
                                    }
                                    break;

                                //jason++ 181031
                                case (int)VehEventTypes.Load_Complete:
                                    //Send Request to VehM
                                    Veh_VehM_Global.start_loading_or_unloading = false;
                                    Veh_VehM_Global.load_Process = "complete";
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : Load Complete 1 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                    Veh_VehM_Global.hasCst = (VhLoadCSTStatus)data.loadStatus.With_CST;              //For report to VehM

                                    if (data.loadStatus.Veh_CSTID != "ERROR1")
                                    {
                                        //++++++++++++++++++++++++++                    // Roy+180308
                                        if (enLoad != LoadCommand.Load)
                                        {
                                            //System.Diagnostics.Debug.Assert(false);
                                        }
                                        //++++++++++++++++++++++++++

                                        if ((data.cmpCode == (int)VehCompleteFlag.Finished)) // && (data.cmpStatus == (int)VehCompleteStatus.CmpAsNormal)
                                        {
                                            //
                                            if (!Veh_VehM_Global.OffLineTest)
                                            {
                                                if (Veh_VehM_Global.fakeID == true)
                                                {
                                                    data.loadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_Load;
                                                    data.unLoadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_UnLoad;
                                                    //Veh_VehM_Global.cstID = Veh_VehM_Global.CSTID_Load;
                                                }
                                                int checkBCRreturncode_temp = 0;
                                                if (data.loadStatus.Veh_CSTID != "")
                                                {
                                                    bool cstERROR = cstERROR_func(data.loadStatus.Veh_CSTID);
                                                    if (cstERROR != true)
                                                    {
                                                        if (data.loadStatus.Veh_CSTID == "ERROR1")
                                                        {
                                                            data.loadStatus.Veh_CSTID = "Busy Error";

                                                            /*
                                                             * This one is used for when interlock error happened.
                                                             */
                                                        }
                                                        else if (data.loadStatus.Veh_CSTID == "ERROR2")
                                                        {
                                                            data.loadStatus.Veh_CSTID = "";
                                                            //Veh_VehM_Global.BarcodeReadResult = 1;
                                                            /*
                                                             * This one is used for when ending load unload but doesn't have packet on it. 
                                                             */
                                                        }
                                                        else if (data.loadStatus.Veh_CSTID == "ERROR3")
                                                        {
                                                            //Veh_VehM_Global.hasCst = VhLoadCSTStatus.Exist;                  //For report to VehM

                                                            data.loadStatus.Veh_CSTID = "";
                                                            checkBCRreturncode_temp = 2;
                                                            //Veh_VehM_Global.BarcodeReadResult = 1;
                                                            /*
                                                             * This one is used for when ending load unload but doesn't have BCRreadResult. 
                                                             */
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : CSTID == ''.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                                }
                                                CheckBarcodeReaderID.BarcodeReaderID checkBCRreturncode = new CheckBarcodeReaderID().Check_BarcodeReaderID(Veh_VehM_Global.CSTID_Load, data.loadStatus.Veh_CSTID);
                                                Veh_VehM_Global.CSTID_Load = data.loadStatus.Veh_CSTID;
                                                Veh_VehM_Global.CSTID_UnLoad = data.unLoadStatus.Veh_CSTID;


                                                if (checkBCRreturncode_temp == 2)
                                                {
                                                    checkBCRreturncode = (CheckBarcodeReaderID.BarcodeReaderID)2;
                                                }
                                                if (Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist)
                                                {
                                                    switch (checkBCRreturncode)
                                                    {
                                                        case (CheckBarcodeReaderID.BarcodeReaderID)0:
                                                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : checkBCRreturncode 0 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                                            break;
                                                        case (CheckBarcodeReaderID.BarcodeReaderID)1:
                                                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : checkBCRreturncode 1 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308

                                                            break;
                                                        case (CheckBarcodeReaderID.BarcodeReaderID)2:
                                                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : checkBCRreturncode 2 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308

                                                            break;
                                                        case (CheckBarcodeReaderID.BarcodeReaderID)999:
                                                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : checkBCRreturncode 999 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308

                                                            break;
                                                    }
                                                }
                                                SendValuesForRept(data, "136_BCR", checkBCRreturncode);    /*this one for BCRResult*/
                                                //SendValuesForRept(data, "136", checkBCRreturncode);                             /*this one for complete*/
                                                Veh_VehM_Global.load_Process = null;

                                                if (!vehTcpComm.TcpIpTimeOut)
                                                {
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : Load Complete 2 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                                }
                                                else
                                                {
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading/Unloading : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                                    Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : TimeOut Stop Moving");                         // Roy*180308
                                                }
                                            }
                                            else
                                            {
                                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : Load Complete 3 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                            }

                                            //
                                            Wdone = true;
                                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Loading : Wdone = {1}.", DateTime.Now.ToString("HH:mm:ss.fff"), Wdone);                         // Roy*180308

                                            SendValuesForRept(data, "144");
                                        }
                                        /*
                                         * The abnormal case of loading report that the arm or the barcode is something error, // 190529 jason++
                                         */
                                        else if (data.cmpCode == (int)VehCompleteFlag.Unfinished)
                                        {
                                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM :  Wait for Loading : Wdone = ERROR happened.", DateTime.Now.ToString("HH:mm:ss.fff"));                         // Roy*180308
                                            if (data.loadStatus.With_CST == 1) //0? 1? which one will be no cst?
                                            {
                                                //This didn't have CST on the Veh.
                                                SendValuesForRept(data, "132");
                                            }
                                            if (data.loadStatus.With_CST == 0)
                                            {
                                                //This did have CST on the Veh.
                                                SendValuesForRept(data, "144");
                                                /*
                                                 * Need to define ErrorCode.
                                                 */
                                                SendValuesForRept(data, "194");
                                                /***********************************/

                                                SendValuesForRept(data, "132");
                                            }
                                        }
                                        /****************************************************************************************************/
                                    }
                                    if (_911point == true)
                                    {
                                        SendValuesForRept(data, "194", 0);
                                    }
                                    break;

                                case (int)VehEventTypes.Unload_Complete:
                                    //Send Request to VehM
                                    Veh_VehM_Global.start_loading_or_unloading = false;
                                    Veh_VehM_Global.unload_Process = "complete";
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Unloading : UnLoad Complete 0 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                    Veh_VehM_Global.hasCst = (VhLoadCSTStatus)data.loadStatus.With_CST;              //For report to VehM

                                    if (data.loadStatus.Veh_CSTID != "ERROR1")
                                    {
                                        //++++++++++++++++++++++++++                    // Roy+180308
                                        if (enLoad == LoadCommand.Load)
                                        {
                                            //System.Diagnostics.Debug.Assert(false);
                                        }
                                        //++++++++++++++++++++++++++

                                        if ((data.cmpCode == (int)VehCompleteFlag.Finished) && (data.cmpStatus == (int)VehCompleteStatus.CmpAsNormal))
                                        {
                                            //
                                            if (!Veh_VehM_Global.OffLineTest)
                                            {
                                                //SendValuesForRept(data, "136");
                                                Veh_VehM_Global.unload_Process = null;

                                                if (!vehTcpComm.TcpIpTimeOut)
                                                {
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Unloading : UnLoad Complete 1 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                                }
                                                else
                                                {
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Unloading : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                                    Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Unloading : TimeOut Stop Moving");                         // Roy*180308
                                                }
                                            }
                                            else
                                            {
                                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for UnLoading : UnLoad Complete 2 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                            }

                                            Wdone = true;
                                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for UnLoading : Wdone = {1}.", DateTime.Now.ToString("HH:mm:ss.fff"), Wdone);                         // Roy*180308

                                            SendValuesForRept(data, "144");

                                        }
                                        else if (data.cmpCode == (int)VehCompleteFlag.Unfinished)
                                        {
                                            if (!Veh_VehM_Global.OffLineTest)
                                            {
                                                //SendValuesForRept(data, "136");
                                                Veh_VehM_Global.unload_Process = null;

                                                if (!vehTcpComm.TcpIpTimeOut)
                                                {
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Unloading : UnLoad Complete 1 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                                }
                                                else
                                                {
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Unloading : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                                    Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for Unloading : TimeOut Stop Moving");                         // Roy*180308
                                                }
                                            }
                                            else
                                            {
                                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for UnLoading : UnLoad Complete 2 Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString());                         // Roy*180308
                                            }

                                            Wdone = true;
                                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Wait for UnLoading : Wdone = {1}.", DateTime.Now.ToString("HH:mm:ss.fff"), Wdone);                         // Roy*180308

                                            SendValuesForRept(data, "144");
                                        }
                                    }
                                    else if (data.loadStatus.Veh_CSTID == "ERROR1")
                                    {
                                        Veh_VehM_Global_Property.abort_On_Check = true;
                                        Veh_CommandComplete(Veh_VehM_Global.completeStatus.CmpStatusInterlockError);
                                    }
                                    //if (_911point == true)
                                    //{
                                    //    SendValuesForRept(data, "194", 0, "-911");
                                    //}
                                    break;

                            }           // # switch (data.eventTypes)

                            //prevAddress = data.Address;                           // Roy-180308
                            //Wait for the Vehicle to arrive at the Load Port 
                        }           // # foreach (MotionInfo_Vehicle_Inter_Comm_ReportData data in motionInfoInterCommReport)

                    }           // # if (motionInfoInterCommReport != null)
                }           // # if (Veh_VehM_Global.blRxDataSent)

                Thread.Sleep(10);
            }           // # while (!done)

            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  : Exit '{1}' Procedure ... ", DateTime.Now.ToString("HH:mm:ss.fff"), enLoad.ToString());                    // Roy+180308
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        protected void AnalyzeDDSFeedback(LoadCommand enload, ref bool done, MotionInfo_Vehicle_Inter_Comm_ReportData[] motionInfoInterCommReport, string cstID, int[] reserve_direction_list, ref int has_already_Count, string[] willPassSections, ref int _countnum_ofsections)
        {
            foreach (MotionInfo_Vehicle_Inter_Comm_ReportData data_ref in motionInfoInterCommReport)
            {
                ///                                                TEST Deep Clone
                ///
                MotionInfo_Vehicle_Inter_Comm_ReportData data = Veh_VehM_Global.Deep_Clone<MotionInfo_Vehicle_Inter_Comm_ReportData>(data_ref);
                ///
                ///
                int direct = 0;
                if (data.Section.StartsWith("+"))
                {
                    direct = 1;
                    data.Section = data.Section.Substring(1);
                }
                else if (data.Section.StartsWith("-"))
                {
                    direct = 2;
                    data.Section = data.Section.Substring(1);
                }
                if ("0" + data.Section != Veh_VehM_Global.Section)
                {
                    bool check_temp = false;
                    if (direct == 1)
                    {
                        data.Section = "+" + data.Section;
                    }
                    else if (direct == 2)
                    {
                        data.Section = "-" + data.Section;
                    }
                    for (int i = _countnum_ofsections; i < willPassSections.Length; i++)
                    {
                        if (willPassSections[i] == data.Section)
                        {
                            check_temp = true;
                            _countnum_ofsections = i;
                        }
                    }
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : AnalyzeDDSFeedback _countnum_ofsections = {0}", _countnum_ofsections.ToString());
                    if (check_temp == false)
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : AnalyzeDDSFeedback check_temp = fasle , something wrong.");
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : AnalyzeDDSFeedback data.section            = {0}", data.Section.ToString());
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : AnalyzeDDSFeedback Veh_VehM_Global.Section = {0}", Veh_VehM_Global.Section.ToString());
                        data.Section = Veh_VehM_Global.Section;
                        if (direct == 1)
                        {
                            data.Section = "+" + data.Section;
                        }
                        else if (direct == 2)
                        {
                            data.Section = "-" + data.Section;
                        }
                    }
                }
                //Transport report and Block Query
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL @@@@@@eventTypes = {1 }.", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);
                /*
                 * Check the error code for the Veh
                 */
                if (data.ErrorCode != 0 && data.ErrorCode != 1)
                {
                    //ErrorHandling(ref done);
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : AnalyzeDDSFeedback !!!!!! Error Code != 0 , Please check again.");
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : AnalyzeDDSFeedback !!!!!! Error Code = {0}", data.ErrorCode.ToString());
                    bool checkError = false;
                    bool dd = ErrorCodeRead(data, ref checkError);
                    done = true;

                    Veh_VehM_Global.checkError = true;

                    return;
                }
                #region Check Vehicle Motion Status for reporting to VehM
                switch (data.eventTypes)
                {
                    case (int)VehEventTypes.Address_Pass:

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Passed.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            // With Connection to VehM
                            SendValuesForRept(data, "134");
                            CheckAlreadySend(data);

                            //SendValuesForRept(data, "144");
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Ending 144.", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);

                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Passed Sent.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.cycle);
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Stop Moving");
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Passed.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        }
                        break;

                    case (int)VehEventTypes.BlockSection_Query:                     // Roy*180319
                                                                                    //Send Request to VehM
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL {2} : ####Send Block Query Address {1}", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);               // Roy*171030
                                                                                                                                                                                                                                                          // Send BlockQuery to VehM if OffLineTest is false
                        if (data.BlockControlSection.Substring(0, 1) == "+")
                        {
                            data.BlockControlSection = data.BlockControlSection.Substring(1);
                        }
                        // A20.06.22 take off for the 
                        //if (data.BlockControlSection.Length == 4)
                        //{
                        //    data.BlockControlSection = "0" + data.BlockControlSection;
                        //}
                        if (Veh_VehM_Global.BlockControlSection == data.BlockControlSection)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL {2} : ####Same Block Query section {1} , no new moving", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.BlockControlSection.ToString(), loadStatus);               // Roy*171030

                            break;
                        }
                        Veh_VehM_Global.BlockControlSection = data.BlockControlSection;                    // Roy+171128
                        Veh_VehM_Global.queryBlockSection = data.Section;
                        Veh_VehM_Global.queryVehAddres = data.Address;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL {2} : ####Send Block Query section {1}", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.BlockControlSection.ToString(), loadStatus);               // Roy*171030
                        if (!Veh_VehM_Global.OffLineTest)           // w/wo Connection to VehM
                        {
                            // With Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: ####Block Query Wait for Reply", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                            SendValuesForRept(data, "136");

                            // waiting for VehM reply 
                            do
                            {
                                Thread.Sleep(10);
                            } while (Veh_VehM_Global.check_recieve_36 == false);

                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                if (Veh_VehM_Global.blBlockCtrl)            // w/wo BlockOuery Option  // This flag is control by the check box.
                                {
                                    requestBlockRecieve(data);
                                }
                                else
                                {
                                    Veh_BlockControl_Move_Continue(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: ####No Block Query Move Grant-Pass ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                }
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Time Out", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : TimeOut Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                            }
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Veh_VehM_Global.OffLineTest = offline");
                            requestBlockRecieve(data);
                        }
                        break;

                    case (int)VehEventTypes.PostBlockSectionExit:
                        //Send Request to VehM
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL {2} : Send Block release Address {1}", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);               // Roy*171030
                        Veh_VehM_Global.queryBlockSection = data.Section;
                        Veh_VehM_Global.queryVehAddres = data.Address;      //Use this address to tell the VehM the exit address.//181129
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL {2} : Send Block release Address {1}", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.BlockControlSection.ToString(), loadStatus);               // Roy*171030
                        if (!Veh_VehM_Global.OffLineTest)           // w/wo Connection to VehM
                        {
                            // With Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: Block release Wait for Reply", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                            SendValuesForRept(data, "136");

                            // waiting for VehM reply 
                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                //Internal Trigger
                                //OnEventBlockControlQuery(new BlockControlQueryArg(data.BlockPassReqst));
                                // VehM Reply

                                if (Veh_VehM_Global.blBlockCtrl)            // w/wo BlockOuery Option
                                {
                                    // With BlockControl option
                                    //VehM signaled
                                    if (Veh_VehM_Global.vehBlockPassReply == (int)Status.OK)
                                    {
                                        //Veh_BlockControl_Move_Continue(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: Pass the block section {0} ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus, data.BlockSectionPassReqst);               // Roy*171030
                                    }
                                    else
                                    {
                                        //Veh_BlockControl_Move_Stop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Stop Moving", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                    }
                                }
                                else
                                {
                                    // No BlockControl option. Just Move 
                                    //Veh_BlockControl_Move_Continue(MoveType.single);                         // jason-- 181113
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: No Block Query Move Grant-Pass", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                }
                            }
                            else
                            {
                                //Raise Timeout Event
                                //Block Control Time out
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Time Out", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : TimeOut Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                            }
                        }
                        else
                        {
                            //Without connection to VehM
                            // OffLineTest is true
                            if (Veh_VehM_Global.vehBlockPassReply == (int)Status.OK)
                            {
                                Veh_BlockControl_Move_Continue(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Move Grant-Pass ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                            }
                            else
                            {
                                Veh_BlockControl_Move_Stop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                            }
                        }
                        break;

                    case (int)VehEventTypes.Address_Arrival:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            // With Connection to VehM
                            SendValuesForRept(data, "134");
                            CheckAlreadySend(data);
                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Arrived In.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Stop Moving");
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Arrived ELSE.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        }
                        break;

                    case (int)VehEventTypes.Moving_Restart:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Restarted~~~ VLVUL {2} : Address {1} Restarted.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            // With Connection to VehM
                            data.vehActionStatus = 1;
                            SendValuesForRept(data, "144");
                            CheckAlreadySend(data);

                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Restarted.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Stop Moving");
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Restarted.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        }
                        break;

                    case (int)VehEventTypes.Moving_Pause:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Paused~~~ VLVUL {2} : Address {1} Paused.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            // With Connection to VehM
                            SendValuesForRept(data, "144");

                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Paused.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Stop Moving");
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Paused.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        }
                        break;
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++                    // Roy+180308
                    case (int)VehEventTypes.Moving_Complete:
                        /*
                         * Delay for the other thread finish 
                         */
                        SpinWait.SpinUntil(() => false, 1000);
                        ///////////////////////
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Complete~~~ VLVUL {2} : Address {1} Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        Veh_VehM_Global_Property.cmd_Length_Check = (Int32)data.WalkLength;

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL DistanceFromSectionStart {2}: dist {1} Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), data.DistanceFromSectionStart);

                            // With Connection to VehM
                            SendValuesForRept(data, "144");
                            Veh_VehM_Global.can_abort_cancel = false;
                            if (Veh_VehM_Global.enActionType == Veh_VehM_Global.ActionType.Move || Veh_VehM_Global.enActionType == Veh_VehM_Global.ActionType.Load ||
                                Veh_VehM_Global.enActionType == Veh_VehM_Global.ActionType.UnLoad)
                            {
                                Veh_VehM_Global_Property.cmd_Length_Check = (Int32)data.WalkLength; //jason+190220  Use for save the cmd length 
                            }
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : Complete~~~ Veh_VehM_Global_Property.cmd_Length_Check = {0}.", Veh_VehM_Global_Property.cmd_Length_Check);

                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Complete.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL : data.Address = {0}.", data.Address);
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL : udtMove.Address = {0}.", DDS_Global.motionInfoInterCommSendData.udtMove.Address);

                                //if (data.Address == DDS_Global.motionInfoInterCommSendData.udtMove.Address)                // Roy+180319
                                if (Veh_VehM_Global_Property.abort_On_Check != true)                // Roy+180319
                                {
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL : data.Address == udtMove.Address bIsOk2GoOnTrigger = true.");

                                    bIsOk2GoOnTrigger = true;
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Single-Move' To address.");

                                }
                                else if (Veh_VehM_Global_Property.abort_On_Check == true)
                                {
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL : abort_On_Check = true => bIsOk2GoOnTrigger = true.");

                                    bIsOk2GoOnTrigger = false;  //190820 jason-+ 

                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : ActionTypeStatusMachine => Single-Move' To address no complete and is abort.");

                                }
                                else
                                {
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : VLVUL : => bIsOk2GoOnTrigger = false.");

                                    bIsOk2GoOnTrigger = false;

                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : ActionTypeStatusMachine => Single-Move' To address Not Complete.");

                                }
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {0} : VehM Connection TimeOut.", loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {0} : Stop Moving", loadStatus);
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {0} : Address {1} Complete ELSE.", loadStatus, data.Address.ToString());

                            if (data.Address == DDS_Global.motionInfoInterCommSendData.udtMove.Address)                // Roy+180319
                            {
                                bIsOk2GoOnTrigger = true;
                            }
                        }
                        #region Send Load Unload cmd to the vehicle.
                        if (bIsOk2GoOnTrigger)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {0} : Address {1} Moving_Complete ~ Relay/Trig signal ...", loadStatus, data.Address.ToString());
                            // 
                            //Veh_VehM_Global_Property.arrive_Complete_Check = true;
                            if (enload == LoadCommand.Load || enload == LoadCommand.UnLoad)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL => done = TrigEventLoadUnLoadingArrival ");               // Roy*171030
                                done = TrigEventLoadUnLoadingArrival(enload, data, cstID);
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL =>  TrigEventLoadUnLoadingArrival send down ");               // Roy*171030
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : enload = JustMove, VLVU => done = true ");               // Roy*171030
                                done = true;
                            }
                        }
                        else if (Veh_VehM_Global_Property.abort_On_Check == true)
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVU => abort_On_Check = true ");               // Roy*171030
                            done = true;
                            //Veh_VehM_Global_Property.arrive_Complete_Check = true;
                        }
                        else
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVU => abort_On_Check != true ");               // Roy*171030
                            done = true; // due to the car is stop.
                        }
                        #endregion
                        break;
                    //190315 jason-- cancel the no use case;
                    case (int)VehEventTypes.PostBlockNHidSectionExit:
                        //Send Request to VehM
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL {2} : Send PostBlockNHidSectionExit release Address {1}", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);               // Roy*171030
                        Console.WriteLine("{0} PostBlockNHidSectionExit the section [{2}]  the address [{1}]", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), data.Section.ToString());                                        // Send BlockQuery to VehM if OffLineTest is false
                        //if (data.BlockControlSection.Substring(0, 1) == "+")
                        //{
                        //    data.BlockControlSection = data.BlockControlSection.Substring(1);
                        //}
                        //if (data.BlockControlSection.Length == 4)
                        //{
                        //    data.BlockControlSection = "0" + data.BlockControlSection;
                        //}
                        //Veh_VehM_Global.BlockControlSection = data.BlockControlSection;                    // Roy+171128
                        Veh_VehM_Global.queryBlockSection = data.Section;
                        Veh_VehM_Global.queryVehAddres = data.Address;      //Use this address to tell the VehM the exit address.//181129

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL {2} : Send PostBlockNHidSectionExit release Address {1}", DateTime.Now.ToString("HH:mm:ss.fff"), Veh_VehM_Global.BlockControlSection.ToString(), loadStatus);               // Roy*171030

                        if (!Veh_VehM_Global.OffLineTest)           // w/wo Connection to VehM
                        {
                            // With Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: PostBlockNHidSectionExit release Wait for Reply", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                            SendValuesForRept(data, "136");

                            // waiting for VehM reply 
                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                //Internal Trigger
                                //OnEventBlockControlQuery(new BlockControlQueryArg(data.BlockPassReqst));
                                // VehM Reply

                                if (Veh_VehM_Global.blBlockCtrl)            // w/wo BlockOuery Option
                                {
                                    // With BlockControl option
                                    //VehM signaled
                                    if (Veh_VehM_Global.vehBlockPassReply == (int)Status.OK)
                                    {
                                        //Veh_BlockControl_Move_Continue(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: Pass the block section {0} ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus, data.BlockSectionPassReqst);               // Roy*171030
                                    }
                                    else
                                    {
                                        //Veh_BlockControl_Move_Stop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Stop Moving", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                    }
                                }
                                else
                                {
                                    // No BlockControl option. Just Move 
                                    //Veh_BlockControl_Move_Continue(MoveType.single);                         // jason-- 181113
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: No Block Query Move Grant-Pass", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                }
                            }
                            else
                            {
                                //Raise Timeout Event
                                //Block Control Time out
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Time Out", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : TimeOut Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                            }
                        }
                        else
                        {
                            //Without connection to VehM
                            // OffLineTest is true
                            if (Veh_VehM_Global.vehBlockPassReply == (int)Status.OK)
                            {
                                Veh_BlockControl_Move_Continue(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Move Grant-Pass ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                            }
                            else
                            {
                                Veh_BlockControl_Move_Stop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Block Query Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                            }
                        }
                        break;
                    case (int)VehEventTypes.ReserveSection_Query:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL : Send ReserveSection_Query request address {0}", data.Address.ToString());               // Roy*171030
                        Veh_VehM_Global.ReserveSection = data.ReserveSectionPassReqst.SectionList;
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, " @Veh_VehM : VLVUL : data.ReserveSectionPassReqst.SectionList = {0}", data.ReserveSectionPassReqst.SectionList[0]);               // Roy*171030
                        /*
                         * Check how many reserve should be take, and the next start point;
                         */
                        int reserveNum = Veh_VehM_Global.ReserveSection.Count();
                        Veh_VehM_Global_Property.reserve_Count_Check = reserveNum;
                        Veh_VehM_Global_Property.has_already_Count_Check = has_already_Count;
                        Veh_VehM_Global_Property.reserve_direction_List_Check = reserve_direction_list;

                        if (!Veh_VehM_Global.OffLineTest)        // w/wo Connection to VehM
                        {
                            // With Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: ReserveSection_Query request Wait for Reply", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                            SendValuesForRept(data, "136");
                            // waiting for VehM reply 
                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                if (Veh_VehM_Global.blBlockCtrl)
                                {
                                    // With BlockControl option
                                    //VehM signaled
                                    if (Veh_VehM_Global.vehReserveReply == (int)Status.OK)
                                    {
                                        Veh_ReserveControl_Continue(MoveType.single);
                                        has_already_Count++;
                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: Pass the Reserve section {2} ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus, data.ReserveSectionPassReqst);               // Roy*171030
                                    }
                                    else
                                    {
                                        Veh_ReserveControl_Stop(MoveType.single);
                                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Reserve Query Stop Moving", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                    }
                                }
                                else
                                {
                                    Veh_ReserveControl_Continue(MoveType.single);
                                    has_already_Count++;
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: No Reserve Query Move Grant-Pass", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                }
                            }
                            else
                            {
                                //Raise Timeout Event

                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Reserve Query Time Out", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : TimeOut Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                            }
                        }
                        else
                        {
                            //Without connection to VehM
                            // OffLineTest is true
                            if (Veh_VehM_Global.vehReserveReply == (int)Status.OK)
                            {
                                Veh_ReserveControl_Continue(MoveType.single);
                                has_already_Count++;
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Reserve Query Move Grant-Pass ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                            }
                            else
                            {
                                Veh_ReserveControl_Stop(MoveType.single);
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : Reserve Query Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                            }
                        }
                        break;
                }
                #endregion
            }

        }

        private void CheckAlreadySend(MotionInfo_Vehicle_Inter_Comm_ReportData data)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Ending 134", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL data.stopStatusForEvent = {0} .", data.stopStatusForEvent.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Veh_VehM_Global.send144check = {0} .", Veh_VehM_Global.send144check.ToString());

            if (data.stopStatusForEvent == 0 && Veh_VehM_Global.send144check == true)
            {
                SendValuesForRept(data, "144");
                Veh_VehM_Global.send144check = false;
            }
            else if (data.stopStatusForEvent != 0 && Veh_VehM_Global.send144check != true)
            {
                Veh_VehM_Global.send144check = true;
            }
        }

        private void requestBlockRecieve(MotionInfo_Vehicle_Inter_Comm_ReportData data)
        {
            if (Veh_VehM_Global.vehBlockPassReply_BlockReq == (int)Status.OK)
            {
                Veh_BlockControl_Move_Continue(MoveType.single);                         // Roy*180308 ... from cycle to single 
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1}: ####Block Query Move Grant-Pass ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                Veh_VehM_Global.vehBlockPassReply_BlockReq = (int)Status.NG;
            }
            else
            {
                Veh_BlockControl_Move_Stop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {1} : ####Block Query Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                Task.Run(() => resendBlockRequest(data));
            }
        }

        private void resendBlockRequest(MotionInfo_Vehicle_Inter_Comm_ReportData data)
        {
            bool checkContinueForBlockResend = false;
            bool check_resend_on = true;
            while (checkContinueForBlockResend == false)
            {
                SpinWait.SpinUntil(() => false, 500);
                if (Veh_VehM_Global_Property.abort_On_Check == true)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : resendBlockRequest STOP ~~ Due to Cancel Abort");
                    break;
                }
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : resendBlockRequest {1}: ####Block Query Wait for Reply", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                SendValuesForRept(data, "136", 0, check_resend_on);
                if (Veh_VehM_Global.vehBlockPassReply_BlockReq == (int)Status.OK)
                {
                    checkContinueForBlockResend = true;
                    Veh_Continue_Procedure_Restart_Block();
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : resendBlockRequest {1}: ####Block Query Move Grant-Pass ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);               // Roy*171030
                    Veh_VehM_Global.vehBlockPassReply_BlockReq = (int)Status.NG;
                    Veh_VehM_Global.resend136Check = true;
                }
                else
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : resendBlockRequest {1} : ####Block Query Stop Moving ", DateTime.Now.ToString("HH:mm:ss.fff"), loadStatus);
                }
            }
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : resendBlockRequest Block = {0} : #### End #### ", data.BlockControlSection.ToString());
        }

        protected void AnalyzeDDSFeedbackJustFor134(LoadCommand enload, MotionInfo_Vehicle_Inter_Comm_ReportData_134[] motionInfoInterCommReport)
        {
            foreach (MotionInfo_Vehicle_Inter_Comm_ReportData_134 data_ref in motionInfoInterCommReport)
            {
                ///                                                TEST Deep Clone
                ///
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 data = Veh_VehM_Global.Deep_Clone<MotionInfo_Vehicle_Inter_Comm_ReportData_134>(data_ref);
                ///
                ///
                int direct = 0;
                if (data.Section.StartsWith("+"))
                {
                    direct = 1;
                    data.Section = data.Section.Substring(1);
                }
                else if (data.Section.StartsWith("-"))
                {
                    direct = 2;
                    data.Section = data.Section.Substring(1);
                }
                //Transport report and Block Query
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL @@@@@@eventTypes = {1 }.", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);
                /*
                 * Check the error code for the Veh
                 */
                if (data.ErrorCode != 0 && data.ErrorCode != 1)
                {
                    //ErrorHandling(ref done);
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : AnalyzeDDSFeedback !!!!!! Error Code != 0 , Please check again.");

                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : AnalyzeDDSFeedback !!!!!! Error Code = {0}", data.ErrorCode.ToString());
                    bool checkError = false;
                    bool dd = ErrorCodeRead_134(data, ref checkError);
                    //done = true;
                    Veh_VehM_Global.checkError = true;

                    return;
                }
                #region Check Vehicle Motion Status for reporting to VehM
                switch (data.eventTypes)
                {
                    case (int)VehEventTypes.Address_Pass:

                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Passed.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            // With Connection to VehM
                            SendValuesForRept_134(data, "134");

                            CheckAlreadySend144_134(data);
                            //SendValuesForRept(data, "144");
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Ending 144.", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);

                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Passed Sent.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.cycle);
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Stop Moving");
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Passed.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        }
                        break;
                    case (int)VehEventTypes.Address_Arrival:
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Arrived.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            // With Connection to VehM
                            SendValuesForRept_134(data, "134");
                            CheckAlreadySend144_134(data);
                            if (!vehTcpComm.TcpIpTimeOut)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2}: Address {1} Arrived In.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                            }
                            else
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : VehM Connection TimeOut.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                                Veh_TcpIpComm_TimeOutStop(MoveType.single);                         // Roy*180308 ... from cycle to single 
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Stop Moving");
                            }
                        }
                        else
                        {
                            // Without Connection to VehM
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL {2} : Address {1} Arrived ELSE.", DateTime.Now.ToString("HH:mm:ss.fff"), data.Address.ToString(), loadStatus);
                        }
                        break;
                }
                #endregion
            }

        }

        private void CheckAlreadySend144_134(MotionInfo_Vehicle_Inter_Comm_ReportData_134 data)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Ending 134", DateTime.Now.ToString("HH:mm:ss.fff"), data.eventTypes);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL data.stopStatusForEvent = {0} .", data.stopStatusForEvent.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Send144check = {0} .", Veh_VehM_Global.send144check.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Resend136Check = {0} .", Veh_VehM_Global.resend136Check.ToString());
            if (data.stopStatusForEvent == 0 && Veh_VehM_Global.send144check == true)
            {
                SendValuesForRept_134(data, "144");
                Veh_VehM_Global.send144check = false;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Send144check = {0} .", Veh_VehM_Global.send144check.ToString());
            }
            else if (data.stopStatusForEvent != 0 && Veh_VehM_Global.send144check != true)
            {
                Veh_VehM_Global.send144check = true;
            }

            if (data.stopStatusForEvent == 0 && Veh_VehM_Global.resend136Check == true)
            {
                SendValuesForRept_134(data, "144");
                Veh_VehM_Global.resend136Check = false;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VLVUL Resend136Check = {0} .", Veh_VehM_Global.resend136Check.ToString());
            }

        }

        protected bool DDS_Abort_Func()
        {
            try
            {
                // Send the Guiding sections
                ////DDS_Global.motionInfoInterCommSendData.Move.Sections = Veh_VehM_Global.GuideSections;              
                ////DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = "";                 // Veh_VehM_Global.querySection;       
                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToAbort;
                //DDS_Global.motionInfoInterCommSendData.Move.Type = MoveType.single;            

                DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                DDS_Global.motionInfoInterCommSendData.isStop = (int)StopStatus.StopYes;
                DDS_Global.motionInfoInterCommSendData.isPause = (int)PauseStatus.PauseYes;
                VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);

                // Inform SendData Sent
                DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);

                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected bool Veh_Abort_Procedure()
        {
            //Wait for loading/unloading complete

            //Force to stop current move.

            if (VehCanStop())
            {
                Veh_VehM_Global_Property.abort_On_Check = true;
                Veh_VehM_Global_Property.IsCmdAbort = true;
                DDS_Abort_Func(); //Veh_OHTC_Global.Section
                //Thread.Sleep(200);
            }

            SpinWait.SpinUntil(() => Veh_VehM_Global_Property.arrive_Complete_Check, -1);

            return true;
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected bool Veh_Pause_Procedure()
        {
            try
            {
                // Send the Guiding sections
                //DDS_Global.motionInfoInterCommSendData.Move.Sections = Veh_VehM_Global.GuideSections;                 // Roy-180319
                //DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = "";                 // Veh_VehM_Global.querySection;                     // Roy-*180319
                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToPause;             // CmdType.Stop;                  // Roy*180302
                //DDS_Global.motionInfoInterCommSendData.Move.Type = MoveType.single;                 // Roy-180319

                DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
                DDS_Global.motionInfoInterCommSendData.isStop = (int)StopStatus.StopYes;
                DDS_Global.motionInfoInterCommSendData.isPause = (int)PauseStatus.PauseYes;
                VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
                // Inform SendData Sent
                DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
                Veh_VehM_Global_Property.pause_Complete_Check = false;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM :  Enter pause procedure.~~~~~~~~~~~~~~~");

                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++                 // Roy+180302
        protected bool Veh_Forcefinish_Procedure()
        {
            if (VehCanStop())
            {
                Veh_VehM_Global_Property.abort_On_Check = true;
                Veh_VehM_Global_Property.IsCmdAbort = true;
                DDS_forcefinish_Procedure();
            }

            SpinWait.SpinUntil(() => Veh_VehM_Global_Property.arrive_Complete_Check, -1);

            return true;
        }
        protected bool DDS_forcefinish_Procedure()
        {
            try
            {
                // Send the Guiding sections
                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToAvoid;
                DDS_Global.motionInfoInterCommSendData.isStop = (int)StopStatus.StopYes;
                DDS_Global.motionInfoInterCommSendData.isPause = (int)PauseStatus.PauseYes;
                VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : motionInfoInterCommSendData  F:Veh_Stop_Procedure eCmdType = {0}", DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType);

                // Inform SendData Sent
                DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
                DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
                DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);

                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++
        public void SendValuesForRept(MotionInfo_Vehicle_Inter_Comm_ReportData reptData, string sCmd, CheckBarcodeReaderID.BarcodeReaderID checkforBCR = 0, bool check_resend_on = false)
        {
            if (reptData.Address != "")
            {
                if (check_resend_on == true)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : check_resend_on = {0}", check_resend_on.ToString());
                }
                else if (check_resend_on == false)
                {
                    Veh_VehM_Global.Address = reptData.Address;
                }
            }
            if (reptData.Section != "")
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : reptData.Section = {0}", reptData.Section.ToString());
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : reptData.ConrtolMode = {0}", reptData.ConrtolMode.ToString());
                if (reptData.Section.Substring(0, 1) == "+")
                {
                    Veh_VehM_Global.Section = reptData.Section.Substring(1);
                }
                // A20.06.22 take off for the 
                //if (Veh_VehM_Global.Section.Length == 4)
                //{
                //    Veh_VehM_Global.Section = "0" + Veh_VehM_Global.Section;
                //}
                //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : Veh_VehM_Global.Section = {0}", Veh_VehM_Global.Section.ToString());
                
                Veh_VehM_Global.DriveDirection = new TransSectioncs().Setdirection(Veh_VehM_Global.Section);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : SendValuesForRept Section DriveDirection = {0}", Veh_VehM_Global.DriveDirection);

            }
            // Use charge statue to count the times;
            Veh_VehM_Global.PositionCheckTimes = reptData.ChargeStatus;
            //
            string allStopString = TransCommand_StopCommand.transCommand_StopCommand(reptData.stopStatusForEvent); //error stopcommand
            //Veh_VehM_Global.BlockControlSection = reptData.BlockControlSection;
            Veh_VehM_Global.cmpCode = reptData.cmpCode;
            Veh_VehM_Global.cmpStatus = reptData.cmpStatus;
            Veh_VehM_Global.DistanceFromSectionStart = (int)reptData.DistanceFromSectionStart;
            //Console.WriteLine("DistanceFromSectionStart = "+ reptData.DistanceFromSectionStart.ToString());
            Veh_VehM_Global.hasCst = (VhLoadCSTStatus)reptData.loadStatus.With_CST;              //For report to VehM
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : Veh_VehM_Global.hasCst = {0}", Veh_VehM_Global.hasCst.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : Veh_VehM_Global.eventTypes = {0}", Veh_VehM_Global.eventTypes.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : reptData.eventTypes = {0}", reptData.eventTypes.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value : reptData.loadStatus.With_CST = {0}", reptData.loadStatus.With_CST.ToString());

            Veh_VehM_Global.vehLeftGuideLockStatus = reptData.vehLeftGuideLockStatus;
            Veh_VehM_Global.vehRightGuideLockStatus = reptData.vehRightGuideLockStatus;
            Veh_VehM_Global.vehModeStatus_fromVehC = reptData.ConrtolMode;
            Veh_VehM_Global.vehLoadStatus = reptData.vehLoadStatus;
            Veh_VehM_Global.vehWalkLength = (Int32)reptData.WalkLength;
            if (reptData.BatteryCapacity != 0)
            {
                Veh_VehM_Global.BatteryCapacity = reptData.BatteryCapacity;
            }
            Veh_VehM_Global.BatteryTemperature = reptData.BatteryTemperature;
            switch (reptData.WheelAngle)
            {
                case VehWheelSteeringAngle.Zero:
                    Veh_VehM_Global.SteeringWheel = 0;
                    break;
                case VehWheelSteeringAngle.LeftNinety:
                    Veh_VehM_Global.SteeringWheel = 90;
                    break;
                case VehWheelSteeringAngle.RightNinety:
                    Veh_VehM_Global.SteeringWheel = -90;
                    break;
            }
            /*
             * Check the status of moving.
             */
            if (allStopString[8].ToString() == "1" || allStopString[16].ToString() == "1" || allStopString[15].ToString() == "1")  //8 => pauseStatus
            {
                Veh_VehM_Global.vehPauseStatus = 1;
                Veh_VehM_Global_Property.pause_Complete_Check = true;
            }
            else
                Veh_VehM_Global.vehPauseStatus = 0;

            if (allStopString[13].ToString() == "1" || allStopString[11].ToString() == "1")
                Veh_VehM_Global.vehBlockStopStatus = 1;
            else
                Veh_VehM_Global.vehBlockStopStatus = 0;

            if (allStopString[10].ToString() == "1")
                Veh_VehM_Global.vehObstStopStatus = 1;
            else
                Veh_VehM_Global.vehObstStopStatus = 0;
            /********************************/
            //Veh_VehM_Global.vehObstStopStatus = reptData.vehObstacleStopStatus;
            Veh_VehM_Global.eventTypes = reptData.eventTypes;
            Veh_VehM_Global.vehObstDist = reptData.vehObstDist;
            Veh_VehM_Global.vehModeStatus_fromVehC = reptData.ConrtolMode;

            Veh_VehM_Global.vehActionStatus = reptData.vehActionStatus;
            if (allStopString[10].ToString() == "1")
                Veh_VehM_Global.vehHIDStopStatus = 1;
            else
                Veh_VehM_Global.vehHIDStopStatus = 0;
            Veh_VehM_Global.HIDControlSection = reptData.HIDControlSection;
            string Local_cstID = Veh_VehM_Global.CSTID_Load;
            #region Conversion between EventTypes, CmpCode,..
            EventTypeConv(Veh_VehM_Global.eventTypes, ref eventTypes);
            CompleteStatusConv(Veh_VehM_Global.cmpStatus, ref cmpStatus);
            GuideStatusConv(Veh_VehM_Global.vehLeftGuideLockStatus, ref lGuideStatus);
            GuideStatusConv(Veh_VehM_Global.vehRightGuideLockStatus, ref rGuideStatus);
            StopStatusConv(Veh_VehM_Global.vehBlockStopStatus, ref blockStatus);
            StopStatusConv(Veh_VehM_Global.vehObstStopStatus, ref obstStatus);
            StopStatusConv(Veh_VehM_Global.vehPauseStatus, ref pauseStatus);
            LoadStatusConv(Veh_VehM_Global.vehLoadStatus, ref loadStatus);
            ModeStatusConv(Veh_VehM_Global.vehModeStatus_fromVehC, ref modeStatus);
            ActionStatusConv(Veh_VehM_Global.vehActionStatus, ref actionStatus);
            #endregion
            EventType transEventType = new EventType();
            loadStatus = Veh_VehM_Global.hasCst;
            BCRReadResult temp136BCRresult = BCRReadResult.BcrNormal;
            // Send Data to VehM
            string msg = string.Empty;
            transEventType = TransCommand.transCommand_EventType(Veh_VehM_Global.eventTypes);
            switch (sCmd)
            {
                case "134":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str134("ID_134", transEventType, Veh_VehM_Global.Section,
                            Veh_VehM_Global.Address, Veh_VehM_Global.BlockControlSection,
                            lGuideStatus, rGuideStatus, blockStatus, pauseStatus, obstStatus, loadStatus,
                            Veh_VehM_Global.DistanceFromSectionStart);
                    }

                    GetReptMsg("134", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));

                    break;

                case "143":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str143(Veh_VehM_Global.Address, Veh_VehM_Global.Section, modeStatus, actionStatus, 0, 0, 0, 0, 0, 0, 0, 0, "0", "0", "0", Veh_VehM_Global.BatteryTemperature);
                    }
                    GetReptMsg("143", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    break;
                case "144":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str144("ID_144",
                            Veh_VehM_Global.Address, Veh_VehM_Global.Section,
                            modeStatus, actionStatus, powerStatus, Veh_VehM_Global.hasCst, obstStatus, blockStatus,
                            pauseStatus, lGuideStatus, rGuideStatus, (int)Veh_VehM_Global.DistanceFromSectionStart,
                            Veh_VehM_Global.BatteryCapacity, Veh_VehM_Global.BatteryTemperature, Veh_VehM_Global.SteeringWheel, Veh_VehM_Global.ErrorStatus);
                    }

                    GetReptMsg("144", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));

                    break;

                case "132":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : WalkLength = {0}", Veh_VehM_Global.vehWalkLength);
                        Veh_VehM_Global.vehVehMomm.sned_Str132(
                            Veh_VehM_Global.command_ID_from_VehM,
                            activeType,
                            Veh_VehM_Global.CSTID_Load.ToString(),
                            Veh_VehM_Global.cmpCode,
                            cmpStatus,
                            Veh_VehM_Global.vehWalkLength
                            );
                    }
                    Veh_VehM_Global.cmdID = ""; /*Flush off the cmdID after send 132*/
                    GetReptMsg("132", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));

                    break;

                case "136":                                                         //jason++ 181025
                    if ((Veh_VehM_Global.hasCst == VhLoadCSTStatus.NotExist) && (transEventType == EventType.AdrOrMoveArrivals) && (Veh_VehM_Global.checkLoadUnloadArrived != true))
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-1 {0}:  BlockControlSection = {1}", loadStatus, Veh_VehM_Global.BlockControlSection);

                        Veh_VehM_Global.vehVehMomm.sned_Str136(
                            "ID_136",
                            EventType.LoadArrivals,
                            Veh_VehM_Global.Section,
                            Veh_VehM_Global.Address,
                            null, //Veh_VehM_Global.ReserveSection,
                            Veh_VehM_Global.BlockControlSection,
                            Veh_VehM_Global.HIDControlSection,
                            Veh_VehM_Global.hasCst,
                            Local_cstID,
                            Veh_VehM_Global.vehBlockPassReply.ToString(),
                            Veh_VehM_Global.vehHIDPassReply.ToString(),
                            (int)Veh_VehM_Global.DistanceFromSectionStart
                            );
                        Veh_VehM_Global.checkLoadUnloadArrived = true;
                    }
                    else if ((Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist) && (transEventType == EventType.AdrOrMoveArrivals) && (Veh_VehM_Global.checkLoadUnloadArrived != true))
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-2 {0}:  BlockControlSection = {1}", loadStatus, Veh_VehM_Global.BlockControlSection);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.UnloadArrivals,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
                            Veh_VehM_Global.checkLoadUnloadArrived = true;
                        }
                        GetReptMsg("136", ref msg);
                        OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    }
                    else if (Veh_VehM_Global.load_Process != null)
                    {
                        if (Veh_VehM_Global.load_Process == "starting")
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-load {0}: Load starting", loadStatus, Veh_VehM_Global.BlockControlSection);

                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.Vhloading,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
                            Veh_VehM_Global.checkLoadUnloadArrived = false;
                        }
                        if (Veh_VehM_Global.load_Process == "complete")
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-load {0}: Load Complete", loadStatus, Veh_VehM_Global.BlockControlSection);
                            temp136BCRresult = BCRReadResult.BcrNormal;
                            //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-load ");
                            switch (checkforBCR)
                            {
                                case CheckBarcodeReaderID.BarcodeReaderID.OK:
                                    temp136BCRresult = BCRReadResult.BcrNormal;
                                    break;
                                case CheckBarcodeReaderID.BarcodeReaderID.BarcodeMissMatch:
                                    temp136BCRresult = BCRReadResult.BcrMisMatch;
                                    break;
                                case CheckBarcodeReaderID.BarcodeReaderID.BarcodeReadFail:
                                    temp136BCRresult = BCRReadResult.BcrReadFail;
                                    Local_cstID = "";
                                    break;
                                case CheckBarcodeReaderID.BarcodeReaderID.ERORR:
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-BCR " + "something Error from VehL");
                                    break;
                            }
                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.LoadComplete,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart,
                                temp136BCRresult
                                );
                        }
                    }
                    else if (Veh_VehM_Global.unload_Process != null)
                    {
                        if (Veh_VehM_Global.unload_Process == "starting")
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-unload {0}: unload starting", loadStatus, Veh_VehM_Global.BlockControlSection);

                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.Vhunloading,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
                            Veh_VehM_Global.checkLoadUnloadArrived = false;
                        }
                        if (Veh_VehM_Global.unload_Process == "complete")
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-unload {0}: unload Complete", loadStatus, Veh_VehM_Global.BlockControlSection);

                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.UnloadComplete,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
                        }
                    }
                    else
                    {
                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            switch (transEventType)
                            {
                                case EventType.BlockReq:
                                    Veh_VehM_Global.vehVehMomm.sned_Str136(
                                    "ID_136",
                                    transEventType,
                                    Veh_VehM_Global.Section,
                                    Veh_VehM_Global.Address,
                                    null, //Veh_VehM_Global.ReserveSection,
                                    Veh_VehM_Global.BlockControlSection,
                                    Veh_VehM_Global.HIDControlSection,
                                    Veh_VehM_Global.hasCst,
                                    Local_cstID,
                                    Veh_VehM_Global.vehBlockPassReply.ToString(),
                                    Veh_VehM_Global.vehHIDPassReply.ToString(),
                                    (int)Veh_VehM_Global.DistanceFromSectionStart
                                    );
                                    break;
                                case EventType.BlockRelease:
                                    Veh_VehM_Global.vehVehMomm.sned_Str136(
                                    "ID_136",
                                    transEventType,
                                    Veh_VehM_Global.Section,
                                    Veh_VehM_Global.Address,
                                    null, //
                                    Veh_VehM_Global.BlockControlSection,
                                    Veh_VehM_Global.HIDControlSection,
                                    Veh_VehM_Global.hasCst,
                                    Local_cstID,
                                    Veh_VehM_Global.Address.ToString(),
                                    Veh_VehM_Global.vehHIDPassReply.ToString(),
                                    (int)Veh_VehM_Global.DistanceFromSectionStart
                                    );
                                    break;
                            }
                            if (Veh_VehM_Global.eventTypes == 18)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : block && HID release", loadStatus, Veh_VehM_Global.BlockControlSection);

                                Veh_VehM_Global.vehVehMomm.sned_Str136(
                                    "ID_136",
                                    EventType.BlockRelease,
                                    Veh_VehM_Global.Section,
                                    Veh_VehM_Global.Address,
                                    null, //Veh_VehM_Global.ReserveSection,
                                    Veh_VehM_Global.BlockControlSection,
                                    Veh_VehM_Global.HIDControlSection,
                                    Veh_VehM_Global.hasCst,
                                    Local_cstID,
                                    Veh_VehM_Global.Address.ToString(),
                                    Veh_VehM_Global.vehHIDPassReply.ToString(),
                                    (int)Veh_VehM_Global.DistanceFromSectionStart
                                    );
                                break;
                            }
                        }
                        GetReptMsg("136", ref msg);
                        OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    }
                    break;
                case "136_BCR":
                    temp136BCRresult = BCRReadResult.BcrNormal;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-BCR ");
                    switch (checkforBCR)
                    {
                        case CheckBarcodeReaderID.BarcodeReaderID.OK:
                            temp136BCRresult = BCRReadResult.BcrNormal;
                            break;
                        case CheckBarcodeReaderID.BarcodeReaderID.BarcodeMissMatch:
                            temp136BCRresult = BCRReadResult.BcrMisMatch;
                            break;
                        case CheckBarcodeReaderID.BarcodeReaderID.BarcodeReadFail:
                            temp136BCRresult = BCRReadResult.BcrReadFail;
                            Local_cstID = "";
                            break;
                        case CheckBarcodeReaderID.BarcodeReaderID.ERORR:
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-BCR " + "something Error from VehL");
                            break;
                    }
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-BCR End changed the BCRreadResult {0}", temp136BCRresult.ToString());

                    Veh_VehM_Global.vehVehMomm.sned_Str136(
                        "ID_136",
                        EventType.Bcrread,
                        Veh_VehM_Global.Section,
                        Veh_VehM_Global.Address,
                        null, //Veh_VehM_Global.ReserveSection,
                        Veh_VehM_Global.BlockControlSection,
                        Veh_VehM_Global.HIDControlSection,
                        Veh_VehM_Global.hasCst,
                        Local_cstID,
                        Veh_VehM_Global.vehBlockPassReply.ToString(),
                        Veh_VehM_Global.vehHIDPassReply.ToString(),
                        (int)Veh_VehM_Global.DistanceFromSectionStart,
                        temp136BCRresult
                        );

                    break;
                case "172":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str172(0);
                    }
                    GetReptMsg("172", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    break;
                case "174":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str174(Veh_VehM_Global.Address, 0);
                    }
                    GetReptMsg("174", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    break;
                case "194":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str194(reptData.ErrorCode, ErrorStatus.ErrSet);
                    }
                    GetReptMsg("194", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    break;
            }
        }
        public void SendValuesForRept_134(MotionInfo_Vehicle_Inter_Comm_ReportData_134 reptData, string sCmd, CheckBarcodeReaderID.BarcodeReaderID checkforBCR = 0, bool check_resend_on = false)
        {
            if (reptData.Address != "")
            {
                if (check_resend_on == true)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value 134 : check_resend_on = {0}", check_resend_on.ToString());
                }
                else if (check_resend_on == false)
                {
                    Veh_VehM_Global.Address = reptData.Address;
                }
            }
            if (reptData.Section != "")
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value 134 : reptData.Section = {0}", reptData.Section.ToString());
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value 134 : reptData.ConrtolMode = {0}", reptData.ConrtolMode.ToString());
                if (reptData.Section.Substring(0, 1) == "+")
                {
                    Veh_VehM_Global.Section = reptData.Section.Substring(1);
                }
                // A20.06.22 take off for the 
                //else if (reptData.Section.Length == 4)
                //{
                //    Veh_VehM_Global.Section = "0" + reptData.Section;
                //}
                //if (Veh_VehM_Global.Section.Length == 4)
                //{
                //    Veh_VehM_Global.Section = "0" + Veh_VehM_Global.Section;
                //}

                Veh_VehM_Global.DriveDirection = new TransSectioncs().Setdirection(Veh_VehM_Global.Section);
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value 134 : SendValuesForRept Section DriveDirection = {0}", Veh_VehM_Global.DriveDirection);

            }
            string allStopString = TransCommand_StopCommand.transCommand_StopCommand(reptData.stopStatusForEvent); //error stopcommand
            //Veh_VehM_Global.BlockControlSection = reptData.BlockControlSection;
            Veh_VehM_Global.cmpCode = reptData.cmpCode;
            Veh_VehM_Global.cmpStatus = reptData.cmpStatus;
            Veh_VehM_Global.DistanceFromSectionStart = (int)reptData.DistanceFromSectionStart;
            //Console.WriteLine("DistanceFromSectionStart = "+ reptData.DistanceFromSectionStart.ToString());
            Veh_VehM_Global.hasCst = (VhLoadCSTStatus)reptData.loadStatus.With_CST;              //For report to VehM
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value 134 : Veh_VehM_Global.hasCst = {0}", Veh_VehM_Global.hasCst.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : In Send Value 134 : reptData.loadStatus.With_CST = {0}", reptData.loadStatus.With_CST.ToString());

            Veh_VehM_Global.vehLeftGuideLockStatus = reptData.vehLeftGuideLockStatus;
            Veh_VehM_Global.vehRightGuideLockStatus = reptData.vehRightGuideLockStatus;
            Veh_VehM_Global.vehModeStatus_fromVehC = reptData.ConrtolMode;
            Veh_VehM_Global.vehLoadStatus = reptData.vehLoadStatus;
            Veh_VehM_Global.vehWalkLength = (Int32)reptData.WalkLength;
            if (reptData.BatteryCapacity != 0)
            {
                Veh_VehM_Global.BatteryCapacity = reptData.BatteryCapacity;
            }
            Veh_VehM_Global.BatteryTemperature = reptData.BatteryTemperature;
            switch (reptData.WheelAngle)
            {
                case VehWheelSteeringAngle.Zero:
                    Veh_VehM_Global.SteeringWheel = 0;
                    break;
                case VehWheelSteeringAngle.LeftNinety:
                    Veh_VehM_Global.SteeringWheel = 90;
                    break;
                case VehWheelSteeringAngle.RightNinety:
                    Veh_VehM_Global.SteeringWheel = -90;
                    break;
            }
            /*
             * Check the status of moving.
             */
            if (allStopString[8].ToString() == "1" || allStopString[16].ToString() == "1" || allStopString[15].ToString() == "1")  //8 => pauseStatus
            {
                Veh_VehM_Global.vehPauseStatus = 1;
                Veh_VehM_Global_Property.pause_Complete_Check = true;
            }
            else
                Veh_VehM_Global.vehPauseStatus = 0;

            if (allStopString[13].ToString() == "1" || allStopString[11].ToString() == "1")
                Veh_VehM_Global.vehBlockStopStatus = 1;
            else
                Veh_VehM_Global.vehBlockStopStatus = 0;

            if (allStopString[10].ToString() == "1")
                Veh_VehM_Global.vehObstStopStatus = 1;
            else
                Veh_VehM_Global.vehObstStopStatus = 0;
            /********************************/
            //Veh_VehM_Global.vehObstStopStatus = reptData.vehObstacleStopStatus;
            Veh_VehM_Global.eventTypes = reptData.eventTypes;
            Veh_VehM_Global.vehObstDist = reptData.vehObstDist;
            Veh_VehM_Global.vehModeStatus_fromVehC = reptData.ConrtolMode;

            Veh_VehM_Global.vehActionStatus = reptData.vehActionStatus;
            if (allStopString[10].ToString() == "1")
                Veh_VehM_Global.vehHIDStopStatus = 1;
            else
                Veh_VehM_Global.vehHIDStopStatus = 0;
            Veh_VehM_Global.HIDControlSection = reptData.HIDControlSection;
            string Local_cstID = Veh_VehM_Global.CSTID_Load;
            #region Conversion between EventTypes, CmpCode,..
            EventTypeConv(Veh_VehM_Global.eventTypes, ref eventTypes);
            CompleteStatusConv(Veh_VehM_Global.cmpStatus, ref cmpStatus);
            GuideStatusConv(Veh_VehM_Global.vehLeftGuideLockStatus, ref lGuideStatus);
            GuideStatusConv(Veh_VehM_Global.vehRightGuideLockStatus, ref rGuideStatus);
            StopStatusConv(Veh_VehM_Global.vehBlockStopStatus, ref blockStatus);
            StopStatusConv(Veh_VehM_Global.vehObstStopStatus, ref obstStatus);
            StopStatusConv(Veh_VehM_Global.vehPauseStatus, ref pauseStatus);
            LoadStatusConv(Veh_VehM_Global.vehLoadStatus, ref loadStatus);
            ModeStatusConv(Veh_VehM_Global.vehModeStatus_fromVehC, ref modeStatus);
            ActionStatusConv(Veh_VehM_Global.vehActionStatus, ref actionStatus);
            #endregion
            EventType transEventType = new EventType();
            loadStatus = Veh_VehM_Global.hasCst;
            BCRReadResult temp136BCRresult = BCRReadResult.BcrNormal;
            // Send Data to VehM
            string msg = string.Empty;
            transEventType = TransCommand.transCommand_EventType(Veh_VehM_Global.eventTypes);
            switch (sCmd)
            {
                case "134":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str134("ID_134", transEventType, Veh_VehM_Global.Section,
                            Veh_VehM_Global.Address, Veh_VehM_Global.BlockControlSection,
                            lGuideStatus, rGuideStatus, blockStatus, pauseStatus, obstStatus, loadStatus,
                            Veh_VehM_Global.DistanceFromSectionStart);
                    }

                    GetReptMsg("134", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));

                    break;

                case "143":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str143(Veh_VehM_Global.Address, Veh_VehM_Global.Section, modeStatus, actionStatus, 0, 0, 0, 0, 0, 0, 0, 0, "0", "0", "0", Veh_VehM_Global.BatteryTemperature);
                    }
                    GetReptMsg("143", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    break;
                case "144":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str144("ID_144",
                            Veh_VehM_Global.Address, Veh_VehM_Global.Section,
                            modeStatus, actionStatus, powerStatus, Veh_VehM_Global.hasCst, obstStatus, blockStatus,
                            pauseStatus, lGuideStatus, rGuideStatus, (int)Veh_VehM_Global.DistanceFromSectionStart,
                            Veh_VehM_Global.BatteryCapacity, Veh_VehM_Global.BatteryTemperature, Veh_VehM_Global.SteeringWheel, Veh_VehM_Global.ErrorStatus);
                    }

                    GetReptMsg("144", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));

                    break;

                case "132":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : WalkLength = {0}", Veh_VehM_Global.vehWalkLength);
                        Veh_VehM_Global.vehVehMomm.sned_Str132(
                            Veh_VehM_Global.command_ID_from_VehM,
                            activeType,
                            Veh_VehM_Global.CSTID_Load.ToString(),
                            Veh_VehM_Global.cmpCode,
                            cmpStatus,
                            Veh_VehM_Global.vehWalkLength
                            );
                    }
                    Veh_VehM_Global.cmdID = ""; /*Flush off the cmdID after send 132*/
                    GetReptMsg("132", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));

                    break;

                case "136":                                                         //jason++ 181025
                    if ((Veh_VehM_Global.hasCst == VhLoadCSTStatus.NotExist) && (transEventType == EventType.AdrOrMoveArrivals) && (Veh_VehM_Global.checkLoadUnloadArrived != true))
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-1 {0}:  BlockControlSection = {1}", loadStatus, Veh_VehM_Global.BlockControlSection);

                        Veh_VehM_Global.vehVehMomm.sned_Str136(
                            "ID_136",
                            EventType.LoadArrivals,
                            Veh_VehM_Global.Section,
                            Veh_VehM_Global.Address,
                            null, //Veh_VehM_Global.ReserveSection,
                            Veh_VehM_Global.BlockControlSection,
                            Veh_VehM_Global.HIDControlSection,
                            Veh_VehM_Global.hasCst,
                            Local_cstID,
                            Veh_VehM_Global.vehBlockPassReply.ToString(),
                            Veh_VehM_Global.vehHIDPassReply.ToString(),
                            (int)Veh_VehM_Global.DistanceFromSectionStart,
                            BCRReadResult.BcrNormal,
                            Veh_VehM_Global.PositionCheckTimes
                            );
                        Veh_VehM_Global.checkLoadUnloadArrived = true;
                    }
                    else if ((Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist) && (transEventType == EventType.AdrOrMoveArrivals) && (Veh_VehM_Global.checkLoadUnloadArrived != true))
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-2 {0}:  BlockControlSection = {1}", loadStatus, Veh_VehM_Global.BlockControlSection);

                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.UnloadArrivals,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart,
                                BCRReadResult.BcrNormal,
                                Veh_VehM_Global.PositionCheckTimes
                                );
                            Veh_VehM_Global.checkLoadUnloadArrived = true;
                        }
                        GetReptMsg("136", ref msg);
                        OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    }
                    else if (Veh_VehM_Global.load_Process != null)
                    {
                        if (Veh_VehM_Global.load_Process == "starting")
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-load {0}: Load starting", loadStatus, Veh_VehM_Global.BlockControlSection);

                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.Vhloading,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
                            Veh_VehM_Global.checkLoadUnloadArrived = false;
                        }
                        if (Veh_VehM_Global.load_Process == "complete")
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-load {0}: Load Complete", loadStatus, Veh_VehM_Global.BlockControlSection);
                            temp136BCRresult = BCRReadResult.BcrNormal;
                            //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-load ");
                            switch (checkforBCR)
                            {
                                case CheckBarcodeReaderID.BarcodeReaderID.OK:
                                    temp136BCRresult = BCRReadResult.BcrNormal;
                                    break;
                                case CheckBarcodeReaderID.BarcodeReaderID.BarcodeMissMatch:
                                    temp136BCRresult = BCRReadResult.BcrMisMatch;
                                    break;
                                case CheckBarcodeReaderID.BarcodeReaderID.BarcodeReadFail:
                                    temp136BCRresult = BCRReadResult.BcrReadFail;
                                    Local_cstID = "";
                                    break;
                                case CheckBarcodeReaderID.BarcodeReaderID.ERORR:
                                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-BCR " + "something Error from VehL");
                                    break;
                            }
                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.LoadComplete,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart,
                                temp136BCRresult
                                );
                        }
                    }
                    else if (Veh_VehM_Global.unload_Process != null)
                    {
                        if (Veh_VehM_Global.unload_Process == "starting")
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-unload {0}: unload starting", loadStatus, Veh_VehM_Global.BlockControlSection);

                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.Vhunloading,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
                            Veh_VehM_Global.checkLoadUnloadArrived = false;
                        }
                        if (Veh_VehM_Global.unload_Process == "complete")
                        {
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-unload {0}: unload Complete", loadStatus, Veh_VehM_Global.BlockControlSection);

                            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.UnloadComplete,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                Local_cstID,
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
                        }
                    }
                    else
                    {
                        if (!Veh_VehM_Global.OffLineTest)
                        {
                            switch (transEventType)
                            {
                                case EventType.BlockReq:
                                    Veh_VehM_Global.vehVehMomm.sned_Str136(
                                    "ID_136",
                                    transEventType,
                                    Veh_VehM_Global.Section,
                                    Veh_VehM_Global.Address,
                                    null, //Veh_VehM_Global.ReserveSection,
                                    Veh_VehM_Global.BlockControlSection,
                                    Veh_VehM_Global.HIDControlSection,
                                    Veh_VehM_Global.hasCst,
                                    Local_cstID,
                                    Veh_VehM_Global.vehBlockPassReply.ToString(),
                                    Veh_VehM_Global.vehHIDPassReply.ToString(),
                                    (int)Veh_VehM_Global.DistanceFromSectionStart
                                    );
                                    break;
                                case EventType.BlockRelease:
                                    Veh_VehM_Global.vehVehMomm.sned_Str136(
                                    "ID_136",
                                    transEventType,
                                    Veh_VehM_Global.Section,
                                    Veh_VehM_Global.Address,
                                    null, //
                                    Veh_VehM_Global.BlockControlSection,
                                    Veh_VehM_Global.HIDControlSection,
                                    Veh_VehM_Global.hasCst,
                                    Local_cstID,
                                    Veh_VehM_Global.Address.ToString(),
                                    Veh_VehM_Global.vehHIDPassReply.ToString(),
                                    (int)Veh_VehM_Global.DistanceFromSectionStart
                                    );
                                    break;
                            }
                            if (Veh_VehM_Global.eventTypes == 18)
                            {
                                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : block && HID release", loadStatus, Veh_VehM_Global.BlockControlSection);

                                Veh_VehM_Global.vehVehMomm.sned_Str136(
                                    "ID_136",
                                    EventType.BlockRelease,
                                    Veh_VehM_Global.Section,
                                    Veh_VehM_Global.Address,
                                    null, //Veh_VehM_Global.ReserveSection,
                                    Veh_VehM_Global.BlockControlSection,
                                    Veh_VehM_Global.HIDControlSection,
                                    Veh_VehM_Global.hasCst,
                                    Local_cstID,
                                    Veh_VehM_Global.Address.ToString(),
                                    Veh_VehM_Global.vehHIDPassReply.ToString(),
                                    (int)Veh_VehM_Global.DistanceFromSectionStart
                                    );
                                break;
                            }
                        }
                        GetReptMsg("136", ref msg);
                        OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    }
                    break;
                case "136_BCR":
                    temp136BCRresult = BCRReadResult.BcrNormal;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-BCR ");
                    switch (checkforBCR)
                    {
                        case CheckBarcodeReaderID.BarcodeReaderID.OK:
                            temp136BCRresult = BCRReadResult.BcrNormal;
                            break;
                        case CheckBarcodeReaderID.BarcodeReaderID.BarcodeMissMatch:
                            temp136BCRresult = BCRReadResult.BcrMisMatch;
                            break;
                        case CheckBarcodeReaderID.BarcodeReaderID.BarcodeReadFail:
                            temp136BCRresult = BCRReadResult.BcrReadFail;
                            Local_cstID = "";
                            break;
                        case CheckBarcodeReaderID.BarcodeReaderID.ERORR:
                            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-BCR " + "something Error from VehL");
                            break;
                    }
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : In Send Value : 136-BCR End changed the BCRreadResult {0}", temp136BCRresult.ToString());

                    Veh_VehM_Global.vehVehMomm.sned_Str136(
                        "ID_136",
                        EventType.Bcrread,
                        Veh_VehM_Global.Section,
                        Veh_VehM_Global.Address,
                        null, //Veh_VehM_Global.ReserveSection,
                        Veh_VehM_Global.BlockControlSection,
                        Veh_VehM_Global.HIDControlSection,
                        Veh_VehM_Global.hasCst,
                        Local_cstID,
                        Veh_VehM_Global.vehBlockPassReply.ToString(),
                        Veh_VehM_Global.vehHIDPassReply.ToString(),
                        (int)Veh_VehM_Global.DistanceFromSectionStart,
                        temp136BCRresult
                        );

                    break;
                case "172":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str172(0);
                    }
                    GetReptMsg("172", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    break;
                case "174":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str174(Veh_VehM_Global.Address, 0);
                    }
                    GetReptMsg("174", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    break;
                case "194":
                    if (!Veh_VehM_Global.OffLineTest)
                    {
                        Veh_VehM_Global.vehVehMomm.sned_Str194(reptData.ErrorCode, ErrorStatus.ErrSet);
                    }
                    GetReptMsg("194", ref msg);
                    OnEventMsgToVehM(new ReportMsgEventArg(msg));
                    break;
            }
        }
        //+++++++++++++++++++++++++++++++++++++++                   // Roy+180319
        protected void Veh_Restart_Procedure()
        {

            if (DDS_Global.checkblock == true)                   //For passing the block 
            {
                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToBlockSectionQueryResult;
                DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = DDS_Global.NGsection;
            }
            else
            {
                DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToRestart;
            }
            //DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.BlockSectionPassReply = Status.OK;               
            //DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = "";                 // DDS_Global.motionInfoInterCommReptData.BlockSectionPassReqst.Section;                     // Roy-*180319

            DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
            DDS_Global.motionInfoInterCommSendData.isStop = (int)Status.NG;
            DDS_Global.motionInfoInterCommSendData.isPause = (int)Status.NG;
            VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);

            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
        }
        //+++++++++++++++++++++++++++++++++++++++        
        protected bool Veh_PowerOn_Procedure()  // What should here do? 181008
        {
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }

        protected void Veh_Continue_Procedure_Restart()
        {
            MotionInfo_Inter_Comm_SendData temp_sendData = new MotionInfo_Inter_Comm_SendData();
            temp_sendData.udtCmdType.eCmdType = CmdType.CmdToRestart;
            temp_sendData.BlockSectionPassReply.BlockSectionPassReply = Status.OK;                     // Roy*180319
            //DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = DDS_Global.motionInfoInterCommReptData.BlockSectionPassReqst.Section;                     // Roy*180319

            temp_sendData.isContinue = (int)Status.OK;
            temp_sendData.isStop = (int)Status.NG;
            temp_sendData.isPause = (int)Status.NG;
            VehInterCommSendDataWrite_Func(temp_sendData);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : motionInfoInterCommSendData  F:Veh_Continue_Procedure_Restart Section = {0}", temp_sendData.BlockSectionPassReply.Section);

            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
        }
        protected void Veh_Continue_Procedure_Restart_Block()
        {
            MotionInfo_Inter_Comm_SendData temp_sendData = new MotionInfo_Inter_Comm_SendData();
            temp_sendData.udtCmdType.eCmdType = CmdType.CmdToContinue; //CmdType.CmdToBlockSectionQueryResult;      jason*181221
            temp_sendData.BlockSectionPassReply.Section = DDS_Global.NGsection;
            temp_sendData.BlockSectionPassReply.BlockSectionPassReply = Status.OK;                     // Roy*180319
            //DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = DDS_Global.motionInfoInterCommReptData.BlockSectionPassReqst.Section;                     // Roy*180319
            temp_sendData.isContinue = (int)Status.OK;
            temp_sendData.isStop = (int)Status.NG;
            temp_sendData.isPause = (int)Status.NG;
            VehInterCommSendDataWrite_Func(temp_sendData);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "@Veh_VehM : motionInfoInterCommSendData  F:Veh_Continue_Procedure_Restart_Block Section = {0}", temp_sendData.BlockSectionPassReply.Section);
            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++              // Roy+180319
        protected void Veh_HIDControl_Move_Continue(MoveType mType)
        {
            // Send Continue Message to Vehicle
            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToHIDSectionQueryResult;
            DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = mType;

            DDS_Global.motionInfoInterCommSendData.HIDSectionPassReply.Section = Veh_VehM_Global.HIDControlSection;

            DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
            DDS_Global.motionInfoInterCommSendData.HIDSectionPassReply.HIDSectionPassReply = Status.OK;
            DDS_Global.motionInfoInterCommSendData.isStop = (int)StopStatus.StopNo;
            DDS_Global.motionInfoInterCommSendData.isPause = (int)PauseStatus.PauseNo;
            DDS_Global.motionInfoInterCommSendData.HIDControlTimeOut = false;
            VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);

            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
            //
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected void Veh_HIDControl_Move_Stop(MoveType mType)
        {
            // Send Pause Message to Vehicle
            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToHIDSectionQueryResult;
            DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = mType;
            DDS_Global.motionInfoInterCommSendData.HIDSectionPassReply.Section = Veh_VehM_Global.queryHIDSection;

            DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.NG;
            DDS_Global.motionInfoInterCommSendData.isStop = (int)StopStatus.StopYes;
            DDS_Global.motionInfoInterCommSendData.isPause = (int)PauseStatus.PauseYes;
            DDS_Global.motionInfoInterCommSendData.HIDControlTimeOut = false;
            VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);

            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
            //
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected void Veh_BlockControl_Move_Continue(MoveType mType)
        {
            MotionInfo_Inter_Comm_SendData temp_sendData = new MotionInfo_Inter_Comm_SendData();
            // Send Continue Message to Vehicle
            temp_sendData.udtCmdType.eCmdType = CmdType.CmdToBlockSectionQueryResult;                     // Roy*180319
            temp_sendData.udtMove.eMoveType = mType;
            //DDS_Global.motionInfoInterCommSendData.BlockPassReply.Section = Veh_VehM_Global.querySection;                           // Roy-171002
            string sendbacktoLocal;
            sendbacktoLocal = Veh_VehM_Global.BlockControlSection;
            if (sendbacktoLocal.Substring(0, 1) != "+")
            {
                if (sendbacktoLocal.Length == 5 )
                {
                    sendbacktoLocal = "+" + sendbacktoLocal;
                }
                // A20.06.22 take off for the 
                //if (sendbacktoLocal.Length == 4)
                //{
                //    sendbacktoLocal = "+" + sendbacktoLocal;
                //}
            }

            temp_sendData.BlockSectionPassReply.Section = sendbacktoLocal;                   // Roy+171002                     // Roy*180319

            temp_sendData.isContinue = (int)Status.OK;
            temp_sendData.BlockSectionPassReply.BlockSectionPassReply = Status.OK;                     // Roy*180319
            temp_sendData.isStop = (int)StopStatus.StopNo;
            temp_sendData.isPause = (int)PauseStatus.PauseNo;
            temp_sendData.BlockControlTimeOut = false;
            VehInterCommSendDataWrite_Func(temp_sendData);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : motionInfoInterCommSendData  F:Veh_BlockControl_Move_Continue Section = {0}", temp_sendData.BlockSectionPassReply.Section);
            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
            //
        }

        private void VehInterCommSendDataWrite_Func(MotionInfo_Inter_Comm_SendData sendData)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VehInterCommSendDataWrite_Func Wait lock ! 1111");
            lock (sendDataDDS_obj)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VehInterCommSendDataWrite_Func lock in ! 2222");
                DDS.ReturnCode status = DDS_Global.motionInfo_VehInterCommSendDataWriter.Write(sendData);
                ErrorHandler.checkStatus(status, "motionInfo_VehInterCommSendDataWriter Error");
            }
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : VehInterCommSendDataWrite_Func lock out ! 3333");
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected void Veh_ReserveControl_Continue(MoveType mType)
        {
            // Send Continue Message to Vehicle
            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToReserveSectionQueryResult;                     // Roy*180319
            DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = mType;
            DDS_Global.motionInfoInterCommSendData.ReserveSectionPassReply.ReserveSectionPassReply = Status.OK;
            DDS_Global.motionInfoInterCommSendData.ReserveSectionPassReply.SectionList = Veh_VehM_Global.ReserveSection;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : motionInfoInterCommSendData  F:Veh_ReserveControl_Continue Veh_VehM_Global.ReserveSection = {0}", Veh_VehM_Global.ReserveSection);

            DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.OK;
            DDS_Global.motionInfoInterCommSendData.isStop = (int)StopStatus.StopNo;
            DDS_Global.motionInfoInterCommSendData.isPause = (int)PauseStatus.PauseNo;
            DDS_Global.motionInfoInterCommSendData.BlockControlTimeOut = false;
            VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : motionInfoInterCommSendData  F:Veh_ReserveControl_Continue Section = {0}", DDS_Global.motionInfoInterCommSendData.ReserveSectionPassReply.SectionList);

            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected void Veh_BlockControl_Move_Stop(MoveType mType)
        {
            // Send Pause Message to Vehicle
            MotionInfo_Inter_Comm_SendData temp_sendData = new MotionInfo_Inter_Comm_SendData();
            temp_sendData.udtCmdType.eCmdType = CmdType.CmdToBlockSectionQueryResult;                     // Roy*180319
            temp_sendData.udtMove.eMoveType = mType;
            string sendbacktoLocal;
            sendbacktoLocal = Veh_VehM_Global.BlockControlSection;
            if (sendbacktoLocal.Substring(0, 1) != "+")
            {
                // A20.06.22 take off for the 
                //if (sendbacktoLocal.Length == 5 && sendbacktoLocal.Substring(0, 1) == "0")
                //{
                //    sendbacktoLocal = sendbacktoLocal.Substring(1);
                //}

                if (sendbacktoLocal.Length == 5)
                {
                    sendbacktoLocal = "+" + sendbacktoLocal;
                }
            }

            temp_sendData.BlockSectionPassReply.Section = sendbacktoLocal;// Roy*180319

            DDS_Global.NGsection = temp_sendData.BlockSectionPassReply.Section; //jason++ 181219

            temp_sendData.isContinue = (int)Status.NG;
            temp_sendData.BlockSectionPassReply.BlockSectionPassReply = Status.NG;
            temp_sendData.isStop = (int)StopStatus.StopYes;
            temp_sendData.isPause = (int)PauseStatus.PauseYes;
            temp_sendData.BlockControlTimeOut = false;
            VehInterCommSendDataWrite_Func(temp_sendData);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : motionInfoInterCommSendData  F:Veh_BlockControl_Move_Stop Section = {0}", temp_sendData.BlockSectionPassReply.Section);

            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
            //
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected void Veh_ReserveControl_Stop(MoveType mType)
        {
            // Send Pause Message to Vehicle
            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToReserveSectionQueryResult;                     // Roy*180319
            DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = mType;
            //DDS_Global.motionInfoInterCommSendData.BlockSectionPassReply.Section = Veh_VehM_Global.BlockControlSection;                     // Roy*180319

            DDS_Global.motionInfoInterCommSendData.ReserveSectionPassReply.SectionList = Veh_VehM_Global.ReserveSection;//jason++ 181219
            DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.NG;
            DDS_Global.motionInfoInterCommSendData.ReserveSectionPassReply.ReserveSectionPassReply = Status.NG;
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : motionInfoInterCommSendData NG!! ");

            DDS_Global.motionInfoInterCommSendData.isStop = (int)StopStatus.StopYes;
            DDS_Global.motionInfoInterCommSendData.isPause = (int)PauseStatus.PauseYes;
            DDS_Global.motionInfoInterCommSendData.BlockControlTimeOut = false;
            VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM : motionInfoInterCommSendData NG!!  Veh_ReserveControl_Stop Section = {0}", DDS_Global.motionInfoInterCommSendData.ReserveSectionPassReply.SectionList[0]);

            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
            //
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected void Veh_TcpIpComm_TimeOutStop(MoveType mType)
        {
            // Send Pause Message to Vehicle
            DDS_Global.motionInfoInterCommSendData.udtCmdType.eCmdType = CmdType.CmdToStop;
            DDS_Global.motionInfoInterCommSendData.udtMove.eMoveType = mType;
            DDS_Global.motionInfoInterCommSendData.isContinue = (int)Status.NG;
            DDS_Global.motionInfoInterCommSendData.isStop = (int)StopStatus.StopYes;
            DDS_Global.motionInfoInterCommSendData.isPause = (int)PauseStatus.PauseYes;
            DDS_Global.motionInfoInterCommSendData.BlockControlTimeOut = true;
            VehInterCommSendDataWrite_Func(DDS_Global.motionInfoInterCommSendData);

            // Inform SendData Sent
            DDS_Global.motionInfoHandShakeTxData.cmdSend = 1;
            DDS_Global.motionInfoHandShakeTxData.cmdReceive = 0;
            DDS_Global.motionInfo_HandShakeSendDataWriter.Write(DDS_Global.motionInfoHandShakeTxData);
            //
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        protected void GetReptMsg(string id, ref string msg)
        {

            msg = string.Format(
                    @"Cmd_ID: {0},
                    Event Type: {1},
                    Current Section: {2},
                    Current Address: {3},
                    BlockCtrl Section: {4},
                    Left Guide Status: {5},
                    Right Guide Status: {6},
                    Block Status: {7},
                    Pause Status : {8},
                    Obstacle Stop Status: {9},
                    Load Cst Status: {10},
                    Distance From Section Start: {11}",
                    id, eventTypes.ToString(),
                    Veh_VehM_Global.Section, Veh_VehM_Global.Address, Veh_VehM_Global.BlockControlSection,
                    lGuideStatus.ToString(), rGuideStatus.ToString(), blockStatus.ToString(),
                    pauseStatus.ToString(), obstStatus.ToString(), loadStatus.ToString(),
                    Veh_VehM_Global.DistanceFromSectionStart.ToString());
        }

        protected void sendloading()
        {
            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.LoadArrivals,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                "",
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
            Veh_VehM_Global.checkLoadUnloadArrived = true;
            Thread.Sleep(500);
        }

        protected void sendunloading()
        {
            Veh_VehM_Global.vehVehMomm.sned_Str136(
                                "ID_136",
                                EventType.UnloadArrivals,
                                Veh_VehM_Global.Section,
                                Veh_VehM_Global.Address,
                                null, //Veh_VehM_Global.ReserveSection,
                                Veh_VehM_Global.BlockControlSection,
                                Veh_VehM_Global.HIDControlSection,
                                Veh_VehM_Global.hasCst,
                                "",
                                Veh_VehM_Global.vehBlockPassReply.ToString(),
                                Veh_VehM_Global.vehHIDPassReply.ToString(),
                                (int)Veh_VehM_Global.DistanceFromSectionStart
                                );
            Veh_VehM_Global.checkLoadUnloadArrived = true;
            Thread.Sleep(500);
        }
        #region Report Type Conversion
        public void EventTypeConv(int type, ref EventType eType)
        {
            switch (type)
            {
                case 0:
                    eType = EventType.LoadArrivals;
                    break;

                case 1:
                    eType = EventType.LoadComplete;
                    break;

                case 2:
                    eType = EventType.UnloadArrivals;
                    break;

                case 3:
                    eType = EventType.UnloadComplete;
                    break;

                //case 4:   //jason-- 180829
                //    eType = EventType.AdrArrivals;                  //.MovePause;                   // Roy*171128
                //    break;
                case 4:     //jason++ 180829
                    eType = EventType.AdrOrMoveArrivals;                  //.MovePause;                   // Roy*171128
                    break;
                case 5:
                    eType = EventType.AdrPass;
                    break;

                case 6:

                    //eType = EventType.MovePause;
                    break;

                case 7:
                    //eType = EventType.MoveRestart;
                    break;

                case 8:
                    eType = EventType.BlockReq;
                    break;
            }
        }

        public void CompleteStatusConv(int status, ref CompleteStatus cStatus)
        {
            switch (status)
            {
                case 0:
                    cStatus = CompleteStatus.CmpStatusMove;
                    break;

                case 1:
                    //jason-- 180829
                    cStatus = CompleteStatus.CmpStatusLoad;
                    break;

                case 2:
                    cStatus = CompleteStatus.CmpStatusUnload;
                    break;

                case 3:
                    cStatus = CompleteStatus.CmpStatusLoadunload;
                    break;
                case 4:
                    cStatus = CompleteStatus.CmpStatusHome;
                    break;
                case 5:
                    cStatus = CompleteStatus.CmpStatusOverride;
                    break;
                case 6:
                    cStatus = CompleteStatus.CmpStatusCstIdrenmae;
                    break;
                case 7:
                    cStatus = CompleteStatus.CmpStatusMtlhome;
                    break;
                case 10:
                    cStatus = CompleteStatus.CmpStatusMoveToMtl;
                    break;
                case 11:
                    cStatus = CompleteStatus.CmpStatusSystemOut;
                    break;
                case 12:
                    cStatus = CompleteStatus.CmpStatusSystemIn;
                    break;
                case 20:
                    cStatus = CompleteStatus.CmpStatusCancel;
                    break;
                case 21:
                    cStatus = CompleteStatus.CmpStatusAbort;
                    break;
                case 22:
                    cStatus = CompleteStatus.CmpStatusVehicleAbort;
                    break;
                case 23:
                    cStatus = CompleteStatus.CmpStatusIdmisMatch;
                    break;
                case 24:
                    cStatus = CompleteStatus.CmpStatusIdreadFailed;
                    break;
                case 25:
                    cStatus = CompleteStatus.CmpStatusIdreadDuplicate;
                    break;
                case 64:
                    cStatus = CompleteStatus.CmpStatusInterlockError;
                    break;
            }
        }

        public void ActiveTypeConv(int type, ref ActiveType aType)
        {
            switch (type)
            {
                case 0:
                    aType = ActiveType.Move;
                    break;

                case 1:
                    aType = ActiveType.Load;
                    break;

                case 2:
                    aType = ActiveType.Unload;
                    break;

                case 3:
                    aType = ActiveType.Loadunload;
                    break;

                //case 4:   //jason-- 180829
                //    aType = ActiveType.Teaching;
                //    break;

                case 4:     //jason++ 180829
                    aType = ActiveType.Home;
                    break;

                case 6:    //jason++ 180829 5-- 6++
                    aType = ActiveType.Round;
                    break;
            }
        }

        public void LoadStatusConv(int status, ref VhLoadCSTStatus lStatus)
        {
            switch (status)
            {
                case 0:
                    lStatus = VhLoadCSTStatus.NotExist;
                    break;

                case 1:
                    lStatus = VhLoadCSTStatus.Exist;
                    break;
            }
        }

        public void StopStatusConv(int status, ref VhStopSingle stopStatus)
        {
            switch (status)
            {
                case 0:
                    stopStatus = VhStopSingle.StopSingleOff;
                    break;

                case 1:
                    stopStatus = VhStopSingle.StopSingleOn;
                    break;
            }
        }

        public void GuideStatusConv(int status, ref VhGuideStatus gStatus)
        {
            switch (status)
            {
                case 0:
                    gStatus = VhGuideStatus.Unlock;
                    break;

                case 1:
                    gStatus = VhGuideStatus.Lock;
                    break;
            }
        }

        public void ModeStatusConv(VehControlMode status, ref VHModeStatus mstatus)
        {
            switch (status)
            {

                case VehControlMode.OnlineLocal:
                    mstatus = VHModeStatus.Manual;
                    break;

                case VehControlMode.OnlineRemote:
                    mstatus = VHModeStatus.AutoRemote;
                    break;

            }
        }

        public void ActionStatusConv(int status, ref VHActionStatus actStatus)
        {
            switch (status)
            {
                case 0:     //jason++ 180829
                    actStatus = VHActionStatus.NoCommand;
                    break;

                case 1:
                    actStatus = VHActionStatus.Commanding;
                    break;

                //case 2:
                //    actStatus = VHActionStatus.;
                //    break;

                case 3:
                    actStatus = VHActionStatus.Teaching;
                    break;

                //case 4:   //jason-- 180829
                //    actStatus = VHActionStatus.Home;
                //    break;
                case 4:     //jason++ 180829
                    actStatus = VHActionStatus.GripperTeaching;
                    break;

                //case 5:       //jason-- 180829
                //    actStatus = VHActionStatus.Teaching;
                //    break;
                case 5:     //jason++ 180829
                    actStatus = VHActionStatus.CycleRun;
                    break;

                    //case 6:       //jason-- 180829
                    //    actStatus = VHActionStatus.GripperTeaching;
                    //    break;

                    //case 7:
                    //    actStatus = VHActionStatus.CycleRun;
                    //    break;
            }
        }
        #endregion

        #region Event Raiser

        public void OnEventBlockControlQuery(BlockControlQueryArg e)
        {
            if (eventBlockQuery != null)
            {
                eventBlockQuery(this, e);
            }
        }

        public void OnEventMsgToVehM(ReportMsgEventArg e)
        {
            if (eventMsgToVehM != null)
            {
                eventMsgToVehM(this, e);
            }
        }
        #endregion
        protected bool cstERROR_func(string feedback_fromVeh)
        {
            bool errorType = true;
            switch (feedback_fromVeh)
            {
                case "ERROR1":
                    errorType = false;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : cstERROR_func :Error1 happened, fork Error.");
                    break;
                case "ERROR2":
                    errorType = false;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : cstERROR_func :Error2 happened, CST didn't detect.");
                    break;
                case "ERROR3":
                    errorType = false;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : cstERROR_func :Error3 happened, barcode read Fail");
                    break;
            }
            return errorType;
        }
        #region ErrorCode translate
        protected bool ErrorCodeRead(MotionInfo_Vehicle_Inter_Comm_ReportData data, ref bool _911point)
        {
            /*
             * Any type should give back 132 but not at the same feedback.
             * continuePoint set the continue to the rest flow.
             * Veh_VehM_Global_Property.IsCmdAbort set for check the complete status to do.
             */
            int errorCode = data.ErrorCode;
            errorCode = Math.Abs(errorCode);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : errorCode = " + errorCode);

            bool Wdone = false;
            string _194ErrorCode = "";
            //if (Veh_VehM_Global.check_4_BCRread)
            //{

            //}
            switch (errorCode)
            {
                case (101):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!The C:\\Storage is going to run off.");
                    _194ErrorCode = "-101";
                    WarningErroeCode(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (102):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!The D:\\Storage is going to run off.");
                    _194ErrorCode = "-102";
                    WarningErroeCode(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (103):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!The D:\\Storage is going to run off.");
                    _194ErrorCode = "-103";
                    AlarmStopErrorCode(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (104):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!The D:\\Storage is going to run off.");
                    _194ErrorCode = "-104";
                    AlarmStopErrorCode(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (150):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!Block index is wrong");
                    _194ErrorCode = "-150";
                    WarningErroeCode(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (910):
                    _194ErrorCode = "910";
                    errorCodeOfPIO(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (940):
                    _194ErrorCode = "940";
                    errorCodeOfPIO(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (941):
                    _194ErrorCode = "941";
                    errorCodeOfPIO(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (942):
                    _194ErrorCode = "942";
                    errorCodeOfPIO(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (943):
                    _194ErrorCode = "943";
                    errorCodeOfPIO(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (944):
                    _194ErrorCode = "944";
                    errorCodeOfPIO(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (945):
                    _194ErrorCode = "945";
                    errorCodeOfPIO(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (900):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL 900 BCR Fail.");
                    _911point = false;
                    Wdone = false;
                    break;
                case (901):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL 901 BCR Fail.");
                    _911point = false;
                    Wdone = false;
                    break;
                case (902):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL 902 BCR Fail.");
                    _911point = false;
                    Wdone = false;
                    break;
                case (920):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL pre position error.");
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    SendValuesForRept(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _911point = true;
                    _194ErrorCode = "-920";
                    SendValuesForRept(data, "194", 0);
                    /***********************************/
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept(data, "144");
                    Wdone = true;
                    break;
                case (921):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Load machine error.");
                    //This did have CST on the Veh.
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    SendValuesForRept(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _194ErrorCode = "-921";
                    SendValuesForRept(data, "194", 0);
                    /***********************************/
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept(data, "144");
                    Wdone = true;
                    break;
                case (922):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Unload machine error.");
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    SendValuesForRept(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _194ErrorCode = "-922";
                    SendValuesForRept(data, "194", 0);
                    /***********************************/
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept(data, "144");
                    Wdone = true;
                    break;
                case (911):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL PIO T7 Timeout.");
                    _194ErrorCode = "-911";
                    WarningErroeCode(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (98):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!Out of control monitor.");
                    _194ErrorCode = "98";
                    WarningErroeCode(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (912):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Fork EMS .");
                    Wdone = false;
                    break;
                case (987):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Fork EMS .");
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    SendValuesForRept(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _911point = true;
                    _194ErrorCode = "-987";
                    SendValuesForRept(data, "194", 0);
                    /***********************************/
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept(data, "144");
                    Wdone = true;
                    break;
                case (999):
                    AlarmStopErrorCode(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (0):
                    break;
                default:
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL  EMS .");
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    SendValuesForRept(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _911point = true;
                    _194ErrorCode = "-987";
                    SendValuesForRept(data, "194", 0);
                    /***********************************/
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept(data, "132");
                    Wdone = true;
                    break;
            }

            return Wdone;
        }

        private void AlarmStopErrorCode(MotionInfo_Vehicle_Inter_Comm_ReportData data, out bool _911point, out bool Wdone, string _194ErrorCode)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL System TimeOut!!!!! ");
            //This did have CST on the Veh.
            Veh_VehM_Global.start_loading_or_unloading = false;
            Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
            Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
            SendValuesForRept(data, "144");
            _911point = true;
            SendValuesForRept(data, "194", 0);
            /***********************************/
            data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
            SendValuesForRept(data, "132");
            data.vehActionStatus = 0;
            SendValuesForRept(data, "144");
            Wdone = true;
        }

        private void WarningErroeCode(MotionInfo_Vehicle_Inter_Comm_ReportData data, out bool _911point, out bool Wdone, string errorCode)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Warning ErrorCode = {0}.", errorCode.ToString());
            Veh_VehM_Global.start_loading_or_unloading = false;
            Wdone = false;
            _911point = true;
            SendValuesForRept(data, "194", 0);
        }

        private void WarningErroeCode_134(MotionInfo_Vehicle_Inter_Comm_ReportData_134 data, out bool _911point, out bool Wdone, string errorCode)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Warning ErrorCode = {0}.", errorCode.ToString());
            Veh_VehM_Global.start_loading_or_unloading = false;
            Wdone = false;
            _911point = true;
            SendValuesForRept_134(data, "194", 0);
        }

        private void errorCodeOfPIO(MotionInfo_Vehicle_Inter_Comm_ReportData data, out bool _911point, out bool Wdone, string errorCode)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL PIO T1-T4 Timeout. errorCode = {0}", errorCode.ToString());
            Veh_VehM_Global_Property.abort_On_Check = true;
            Veh_VehM_Global.start_loading_or_unloading = false;
            _911point = false;
            Wdone = true;
            SendValuesForRept(data, "194", 0);
            data.cmpStatus = 64;
            SendValuesForRept(data, "132");
            data.vehActionStatus = 0;
            SendValuesForRept(data, "144");
        }

        protected bool ErrorCodeRead_134(MotionInfo_Vehicle_Inter_Comm_ReportData_134 data, ref bool _911point)
        {
            /*
             * Any type should give back 132 but not at the same feedback.
             * continuePoint set the continue to the rest flow.
             * Veh_VehM_Global_Property.IsCmdAbort set for check the complete status to do.
             */
            int errorCode = data.ErrorCode;
            errorCode = Math.Abs(errorCode);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : errorCode = " + errorCode);

            bool Wdone = false;
            string _194ErrorCode = "";
            switch (errorCode)
            {
                case (101):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!The C:\\Storage is going to run off.");
                    _194ErrorCode = "101";
                    WarningErroeCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (102):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!The D:\\Storage is going to run off.");
                    _194ErrorCode = "102";
                    WarningErroeCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (103):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!The D:\\Storage is going to run off.");
                    _194ErrorCode = "103";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (104):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!The D:\\Storage is going to run off.");
                    _194ErrorCode = "104";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (150):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : Warning!!!!!!!Block index is wrong");
                    _194ErrorCode = "150";
                    WarningErroeCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (801):
                    _194ErrorCode = "801";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (802):
                    _194ErrorCode = "802";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (803):
                    _194ErrorCode = "803";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (804):
                    _194ErrorCode = "804";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (805):
                    _194ErrorCode = "805";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (806):
                    _194ErrorCode = "806";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (807):
                    _194ErrorCode = "807";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (808):
                    _194ErrorCode = "808";
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (910):
                    _194ErrorCode = "910";
                    errorCodeOfPIO_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (940):
                    _194ErrorCode = "940";
                    errorCodeOfPIO_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (941):
                    _194ErrorCode = "941";
                    errorCodeOfPIO_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (942):
                    _194ErrorCode = "942";
                    errorCodeOfPIO_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (943):
                    _194ErrorCode = "943";
                    errorCodeOfPIO_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (944):
                    _194ErrorCode = "944";
                    errorCodeOfPIO_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (945):
                    _194ErrorCode = "945";
                    errorCodeOfPIO_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
                case (900):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL BCR Fail.");

                    //Veh_VehM_Global_Property.abort_On_Check = false;
                    _911point = false;
                    Wdone = false;
                    break;
                case (920):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL pre position error.");
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    SendValuesForRept_134(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _911point = true;
                    _194ErrorCode = "-920";
                    SendValuesForRept_134(data, "194", 0);
                    /***********************************/
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept_134(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept_134(data, "144");
                    Wdone = true;
                    break;
                case (921):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Load machine error.");
                    //This did have CST on the Veh.
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    SendValuesForRept_134(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _194ErrorCode = "-921";
                    SendValuesForRept_134(data, "194", 0);
                    /***********************************/
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept_134(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept_134(data, "144");
                    Wdone = true;
                    break;
                case (922):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Unload machine error.");
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    SendValuesForRept_134(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _194ErrorCode = "-922";
                    SendValuesForRept_134(data, "194", 0);
                    /***********************************/
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept_134(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept_134(data, "144");
                    Wdone = true;
                    break;
                case (911):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL PIO T7 Timeout.");
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Wdone = false;
                    _911point = true;
                    _194ErrorCode = "-911";
                    SendValuesForRept_134(data, "194", 0);
                    break;
                case (912):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Fork EMS .");
                    Wdone = false;
                    break;
                case (987):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Fork EMS .");
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    SendValuesForRept_134(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _911point = true;
                    _194ErrorCode = "-987";
                    SendValuesForRept_134(data, "194", 0);
                    /***********************************/
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept_134(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept_134(data, "144");
                    Wdone = true;
                    break;
                case (999):
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Fork TimeOut!!!!! ");
                    //This did have CST on the Veh.
                    Veh_VehM_Global.start_loading_or_unloading = false;
                    Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
                    Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
                    SendValuesForRept_134(data, "144");
                    /*
                     * Need to define ErrorCode.
                     */
                    _911point = true;
                    _194ErrorCode = "-999";
                    SendValuesForRept_134(data, "194", 0);
                    /***********************************/
                    data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
                    SendValuesForRept_134(data, "132");
                    data.vehActionStatus = 0;
                    SendValuesForRept_134(data, "144");
                    Wdone = true;
                    break;
                case (0):
                    break;
                default:
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL  EMS .");
                    _194ErrorCode = errorCode.ToString();
                    AlarmStopErrorCode_134(data, out _911point, out Wdone, _194ErrorCode);
                    break;
            }

            return Wdone;
        }


        private void AlarmStopErrorCode_134(MotionInfo_Vehicle_Inter_Comm_ReportData_134 data, out bool _911point, out bool Wdone, string _194ErrorCode)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL Abnormal Stop errorCode = {0}", _194ErrorCode);
            //This did have CST on the Veh.
            Veh_VehM_Global.start_loading_or_unloading = false;
            Veh_VehM_Global_Property.abort_On_Check = true; //abort all comand below.
            Veh_VehM_Global.ErrorStatus = VhStopSingle.StopSingleOn;
            SendValuesForRept_134(data, "144");
            _911point = true;
            SendValuesForRept_134(data, "194", 0);
            /***********************************/
            data.cmpStatus = (int)Veh_VehM_Global.completeStatus.CmpStatusVehicleAbort;
            SendValuesForRept_134(data, "132");
            data.vehActionStatus = 0;
            SendValuesForRept_134(data, "144");
            Wdone = true;
        }

        private void errorCodeOfPIO_134(MotionInfo_Vehicle_Inter_Comm_ReportData_134 data, out bool _911point, out bool Wdone, string errorCode)
        {
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL PIO T1-T4 Timeout. errorCode = {0}", errorCode);
            Veh_VehM_Global_Property.abort_On_Check = true;
            Veh_VehM_Global.start_loading_or_unloading = false;
            _911point = false;
            Wdone = true;
            SendValuesForRept_134(data, "194", 0);
            data.cmpStatus = 64;
            SendValuesForRept_134(data, "132");
            data.vehActionStatus = 0;
            SendValuesForRept_134(data, "144");
        }

        #endregion
    }


    public class MessageEventArg : EventArgs
    {
        WrapperMessage wMessage = new WrapperMessage();
        WrapperMessage.MsgOneofCase _Case;
        Veh_VehM_Global.SequenceEvents _seqEvents;

        public MessageEventArg()
        {
            _Case = wMessage.MsgCase;

        }

        public WrapperMessage.MsgOneofCase enCase
        { get { return _Case; } }
    }


    public class ReportMsgEventArg : EventArgs
    {
        public string Msg { get; set; }

        public ReportMsgEventArg(string msg)
        {
            Msg = msg;
        }

    }


    public class BlockControlQueryArg : EventArgs               // Roy*180319
    {

        public MotionInfo_BlockSectionPassReqst Query { get; set; }

        public BlockControlQueryArg(MotionInfo_BlockSectionPassReqst query)
        {
            Query = query;
        }

        public MotionInfo_BlockSectionPassReply Reply { get; set; }

        public BlockControlQueryArg(MotionInfo_BlockSectionPassReply reply)
        {
            Reply = reply;
        }

    }
    public class CheckError
    {
        public int CkeckErrorCode(int error_code)
        {
            int return_num = 0;
            if (error_code != 0)
            {
                return_num = error_code;
                /*
                 * Here can write the Nlog File.
                 */
            }
            return return_num;
        }
    }
    public class CheckBarcodeReaderID
    {
        public enum BarcodeReaderID
        {
            ERORR = 999,
            OK = 0,
            BarcodeMissMatch = 1,
            BarcodeReadFail = 2
        }
        public BarcodeReaderID Check_BarcodeReaderID(string barcodeFromVehC, string barcodeFromVehL)
        {
            BarcodeReaderID return_code = 0;
            if (barcodeFromVehL == "")
            {
                return_code = (BarcodeReaderID)2;
            }
            else if (barcodeFromVehL == barcodeFromVehC)
            {
                return_code = (BarcodeReaderID)0;
            }
            else if (barcodeFromVehL != barcodeFromVehC)
            {
                return_code = (BarcodeReaderID)1;
            }
            else if (barcodeFromVehL == "INTERLOCK_ERROR")
            {
                return_code = (BarcodeReaderID)999;
            }
            return return_code;
        }
    }

}