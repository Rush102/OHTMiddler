using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using TcpIpClientSample;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using OHTM.NLog_USE;

namespace OHTM.StatusMachine
{
    public static class Veh_VehM_Global
    {
        private static bool checkNewCmdIDProcess = false;

        public static bool CheckNewCmdIDProcess { get => checkNewCmdIDProcess; set => checkNewCmdIDProcess = value; }

        public static bool resend136Check = false;
        public static string cmd_CstIDfromOHTC = "";
        public static string keep_AddressID = "";
        public static bool reallyStop = true;
        public static bool send144check = true;
        public static bool startCheck = false;
        public static VhStopSingle ErrorStatus = VhStopSingle.StopSingleOff;
        public static bool check136 = false;
        public static bool checkError = false;
        public static bool can_abort_cancel = false;
        public static string lastCmdID = "";
        public static DateTime time_end_Ten;
        public static DateTime time_start_Ten;
        public static DateTime time_end_One;
        public static DateTime time_start_One;
        public static DateTime lul_time_end;
        public static DateTime lul_time_start;
        public static bool tenminuteHasSent_Ten = false;
        public static bool oneminuteHasSent_One = false;
        public static bool lul_HasSent = false;
        public static bool start_loading_or_unloading = false;             //check if the robot neel is running;
        public static bool sendRecvException = false;
        public static bool check143sendBeforeTimer = false;
        public static int PositionCheckTimes = 0;
        public enum completeStatus
        {
            CmpStatusMove = 0,
            CmpStatusLoad = 1,
            CmpStatusUnload = 2,
            CmpStatusLoadunload = 3,
            CmpStatusHome = 4,
            CmpStatusOverride = 5,
            CmpStatusCstIdrenmae = 6,
            CmpStatusMtlhome = 7,
            CmpStatusMoveToMtl = 10,
            CmpStatusSystemOut = 11,
            CmpStatusSystemIn = 12,
            CmpStatusTechingMove = 13,
            CmpStatusCancel = 20,
            CmpStatusAbort = 21,
            CmpStatusVehicleAbort = 22,
            CmpStatusIdmisMatch = 23,
            CmpStatusIdreadFailed = 24,
            CmdStatusIdReadDuplicate = 25,
            CmpStatusInterlockError = 64,
        }
        public  enum SequenceEvents
        {
            enIdle=0,
            enVeh_PowerOn,
            enVeh_Data,
            enControl_Start_Stop,
            enTransferRequest,
            enEventsOnDriving,
            enTransferCancel,
            enChangeMode,
            enChangePower,
            enErrorOccur_Clear,
            enAutoDrivingTeaching,
            enGripperTeaching,
            enVehLogUpLoad,
            enRecoverFromUnExpPowerFailure,
        }

        //public enum CmdID { cmd31 = 31, cmd131 = 131, cmd32 = 32, cmd132 = 132, cmd39 = 39, cmd37 = 37, cmd35 = 35, cmd34 = 34 }                    // Roy-180302
        public static bool cmdRunning { get; set; }
        public static bool fakeID { get; set; }
        public static bool fakeMap { get; set; }
        public static bool fake144chargecheck { get; set; }
        public static bool chargeport { get; set; }

        public static string cmdID { get; set; }
        public static string block_continue_section { get; set; }
        

        public enum StartendCheck {
            None = 0,
            startpoint = 1,
            endpoint = 2
        };
        //++++++++++++++++++                     // Roy+180302
        public enum CmdID
        { 
            None = 0,

            cmd31 = 31, 
            cmd131 = 131, 

            cmd32 = 32, 
            cmd132 = 132, 

            cmd33 = 33,
            cmd133 = 133,

            cmd34 = 34,
            cmd134 = 134,                   // +++

            cmd35 = 35,
            cmd135 = 135,

            cmd36 = 36,
            cnd136 = 136,

            cmd37 = 37,
            cmd137 = 137,                   // +++

            cmd39 = 39,
            cmd139 = 139,                   // +++

            cmd41 = 41,
            cmd141 = 141,                   // +++

            cmd43 = 43,
            cmd143 = 143,                   // +++

            cmd44 = 44,
            cmd144 = 144,                   // +++

            cmd45 = 45,
            cmd145 = 145,                   // +++

            cmd51 = 51,
            cmd151 = 151,

            cmd52 = 52,
            cmd152 = 152,

            cmd71 = 71,
            cmd171 = 171,                   // +++

            cmd72 = 72,
            cmd172 = 172,                   // +++

            cmd74 = 74,
            cmd174 = 174,                   // +++

            cmd91 = 91,
            cmd191 = 191,                   // +++

            cmd94 = 94,
            cmd194 = 194,                   // +++

            dark31 = -31                   // +++
        }
        //++++++++++++++++++


        public static SequenceEvents seqEvents;
        public static bool normal_pause = false;
        public static bool safety_pause = false;
        public static bool earthquake_pause = false;
        public static bool HID_pause = false;

        /// <summary>
        /// ( Direct Command for OHT ... )
        /// </summary>
        public enum ActionType
        {
            //Move = 0, Load = 1, UnLoad = 2, Load_Unload = 3, Home_Calibration, Continue, Cycle, Pause, Stop                // Roy-180319
            Move = 0, Load = 1, UnLoad = 2, Load_Unload = 3, Home_Calibration, Continue, Restart, Cycle, Pause, Stop, Cancel, Abort, Override,                 // Roy+180319
            Teaching, AvoidRequest, Movetocharger, Movetomtl, Mtlhome, Systemin,Systemout,Forcefinish, Unknow
        }

        public static ActiveType NowActiveType;

        public static ActionType enActionType;
        //
        public static bool OffLineTest = false;

        //public static string fromAddress_LoadPort, toAddress_UnloadPort, queryVehAddres, queryBlockSection;             // Roy-180319       
        public static string queryVehAddres;             // Roy+180319
        public static string queryBlockSection;             // Roy+180319
        public static string queryHIDSection;             // Roy+180319

        public static CmdID enCmdID = 0;
        //
        public static OHTM.StatusMachine.Veh_VehM_Comm_Data vehVehMomm;
        public static OHTM.StatusMachine.Veh_VehM vehVehM;
        public static ConfigClass timeout_Config;
        //
        public static string[] GuideSections = null;                 // Roy+180319
        public static string[] GuideSections2nd = null;
        public static string[] GuideSectionsStartToLoad = null;
        public static string[] GuideSectionsToDestination = null;
        public static string[] GuideAddressesStartToLoad = null;
        public static string[] GuideAddressesToDestination = null;

        public static string[] CycleSections = null;
        //
        public static int DriveDirection = 0;
        public static string Section = string.Empty;
        public static string Address = string.Empty;
        public static string LoadAddress = string.Empty;
        public static string UnloadAddress = string.Empty;                 // Roy+180319
        public static string FromAdr = string.Empty;
        public static string ToAdr = string.Empty;
        public static double DistanceFromSectionStart;
        public static int Guiding;
        public static string BlockControlSection = string.Empty;
        public static string HIDControlSection = string.Empty;                 // Roy+180319
        public static string[] ReserveSection = null;
        public static int Proc_ON;
        public static int cmd_Send;
        public static int cmd_Receive;
        public static int cmpCode;
        public static int cmpStatus;
        public static Veh_HandShakeData.VehControlMode vehModeStatus_fromVehC;
        public static Veh_HandShakeData.VehControlMode Modestatus_from300;
        public static int vehActionStatus;
        public static int eventTypes;
        public static int actionType;
        public static int vehLeftGuideLockStatus;
        public static int vehRightGuideLockStatus;
        public static int vehPauseStatus;
        public static int vehBlockStopStatus;
        public static int vehObstStopStatus;
        public static int vehWalkLength;

        public static int vehHIDStopStatus;                     // Roy+180319
        public static int vehHIDPassReqst;                     // Roy+180319

        public static int vehBlockPassReqst;
        public static int locationTypes;
        public static int vehLoadStatus;
        public static int vehObstDist;
        public static string CSTID_Load = "";                     // Roy*180319
        public static string CSTID_UnLoad;                     // Roy*180319
        //
        public static bool blSendDataReceived = false;
        public static bool blRxDataSent = false;
        //
        public static bool blSendDataSent = false;
        public static bool blRxDataReceived = false;
        //
        public static bool blCycleRun = false;
        public static int vehBlockPassReply;
        public static int vehBlockPassReply_BlockReq;
        public static int vehReserveReply;
        public static bool blReceiveBlockReply = false;

        public static int vehHIDPassReply;                     // Roy+180319
        public static bool blReceiveHIDReply = false;                     // Roy+180319

        public static string cstID;                             //jason++ 181025

        public static VhLoadCSTStatus hasCst = VhLoadCSTStatus.NotExist;

        public static ManualResetEvent mResetEvent;
       //
        public static bool blBlockCtrl = false;
        public static bool blHIDCtrl = false;                     // Roy+180319

        public static bool checkLoadUnloadArrived = false;
        public static bool check_LoadStart = false;
        public static bool check_LoadPlaced = false;
        public static bool check_LoadComplete = false;

        public static bool check_UnloadArrived = false;
        public static bool check_UnloadStart = false;
        public static bool check_UnloadReleased = false;
        public static bool check_UnloadComplete = false;

        public static bool check_recieve_36 = false;

        public static string command_ID_from_VehM;
        public static PauseEvent now_Pause;
        public static string load_Process;
        public static string unload_Process;

        public static int Store_Start_Length = 0;
        public static string pre_Section = "0";
        public static String[] p_FromVehCSections = null;
        public static String[] p_FromVehCAddresses = null;
        public static int veh_ChargeStatus = 0;
        public static int BatteryCapacity = 0;
        public static int BatteryTemperature = 0;
        public static bool checkForNoMoveSend144 = false;
        public static bool chekcForModeChangeThread = false;
        public static int SteeringWheel = 0;
        //public static VhChargeStatus ChargeStatus = VhChargeStatus.ChargeStatusNone;
        public static int BarcodeReadResult = 0;
        public static bool check_4_BCRread = true;
        public static CMDCancelType CancelType4Report = CMDCancelType.CmdNone;
        /// <summary>
        /// Deep_Clone : Using for clone all the object by [Serializable]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Deep_Clone<T>(T RealObject)
        {
            try
            {
                using (Stream objectStream = new MemoryStream())
                {
                    //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制     
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(objectStream, RealObject);
                    objectStream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(objectStream);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return RealObject;
            }
        }
    }
    public class Veh_VehM_Global_Property
    {
        /*
         * Use for reserve sections;
         */
        private static bool Cmd_Complete_inpause = false;
        public static bool Cmd_Complete_inpause_Check
        {
            get { return Cmd_Complete_inpause; }
            set { Cmd_Complete_inpause = value; }
        }
        private static int has_already_Count;
        public static int has_already_Count_Check
        {
            get { return  has_already_Count; }
            set { has_already_Count = value; }
        }
        private static int[] reserve_direction_List;
        public static int [] reserve_direction_List_Check
        {
            get { return  reserve_direction_List; }
            set { reserve_direction_List = value; }
        }
        private static int reserve_Count;
        public static int reserve_Count_Check
        {
            get { return  reserve_Count; }
            set { reserve_Count = value; }
        }
        /********/
        private static bool cancel_Complete;
        public  static bool cancel_Complete_Check
        {
            get { return cancel_Complete; }
            set { cancel_Complete = value; }
        }
        private static bool pause_Complete;
        public static bool pause_Complete_Check
        {
            get { return pause_Complete; }
            set { pause_Complete = value; }
        }
        private static bool abort_On;
        public static bool abort_On_Check
        {
            get { return abort_On; }
            set { abort_On = value; }
        }
        private static bool arrive_Complete;
        public static bool arrive_Complete_Check
        {
            get { return arrive_Complete; }
            set { arrive_Complete = value; }
        }
        private static PauseType pause_Type;
        public static PauseType pause_Type_Check
        {
            get { return pause_Type; }
            set { pause_Type = value; }
        }
        private static bool same_pause_Command;
        public static bool same_pause_Command_Check
        {
            get { return same_pause_Command; }
            set { same_pause_Command = value; }
        }
        private static bool already_have_command = false;
        public static bool already_have_command_Check
        {
            get { return already_have_command; }
            set { already_have_command = value; }
        }
        private static Veh_VehM_Global.ActionType lastMoveType_4_Override;
        public static Veh_VehM_Global.ActionType lastMoveType_4_Override_Check
        {
            get { return lastMoveType_4_Override; }
            set { lastMoveType_4_Override = value; }
        }
        private static int cmd_Length;
        public static int cmd_Length_Check
        {
            get { return cmd_Length; }
            set { cmd_Length = value; }
        }
        private static int Cmd_Power_Consume;
        public static int Cmd_Power_Consume_Check
        {
            get { return Cmd_Power_Consume; }
            set { Cmd_Power_Consume = value; }
        }
        public static bool IsCmdAbort { get; set; }
        public static Veh_VehM_Global.ActionType Pre31CmdType { get; set; } = Veh_VehM_Global.ActionType.Unknow;
        public static Pre31CmdSteps Pre31CmdStep { get; set; } = Pre31CmdSteps.Unknow;
        public static string curSection { get; set; } = "Unknow";
        public static bool OverrideFail { get; set; } = false;
        public static string FailReason { get; set; }
    }
    public enum Pre31CmdSteps
    {
        Unknow,
        Moving,
        MoveComplete,
        Loading,
        LoadComplete,
        Unloading,
        UnloadingComplete,
        Idle
    }
}
