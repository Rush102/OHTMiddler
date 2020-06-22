using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace MirleOHT.類別.DCPS
{
    public static class Veh_VehC_Global
    {
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


        //++++++++++++++++++                     // Roy+180302
        public enum CmdID
        {
            cmd31 = 31,
            cmd131 = 131,

            cmd32 = 32,
            cmd132 = 132,

            cmd39 = 39,
            cmd139 = 139,                   // +++

            cmd37 = 37,
            cmd137 = 137,                   // +++

            cmd35 = 35,
            cmd135 = 135,                   // +++

            cmd34 = 34,
            cmd134 = 134,                   // +++

            dark31 = -31                   // +++
        }
        //++++++++++++++++++

        public static SequenceEvents seqEvents;

        /// <summary>
        /// ( Direct Command for OHT ... )
        /// </summary>
        public enum ActionType
        {
            //Move = 0, Load = 1, UnLoad = 2, Load_Unload = 3, Home_Calibration, Continue, Cycle, Pause, Stop                // Roy-180319
            Move = 0, Load = 1, UnLoad = 2, Load_Unload = 3, Home_Calibration, Continue, Restart, Cycle, Pause, Stop, Cancel, Abort                // Roy+180319
        }

        public enum direction
        {
            forward = 1,
            reverse = 2
        }
        //// public static ActiveType VehActiveType;

        public static ActionType enActionType;
        //
        public static bool OffLineTest = false;

        //public static string fromAddress_LoadPort, toAddress_UnloadPort, queryVehAddres, queryBlockSection;             // Roy-180319       
        public static string queryVehAddres;             // Roy+180319
        public static string queryBlockSection;             // Roy+180319
        public static string queryHIDSection;             // Roy+180319

        public static CmdID enCmdID;
        //
        //public static VehM_Vehicle_Communication.StatusMachine.Veh_VehM_Comm_Data vehVehMomm;
        //public static VehM_Vehicle_Communication.StatusMachine.Veh_VehM vehVehM;
        //
        public static string[] GuideSections2nd = null;                 // Roy+180319
        public static string[] GuideSections = null;
        public static string[] CycleSections = null;
        //
        public static string Section = string.Empty;
        public static string Address = string.Empty;

        public static string LoadAddress = string.Empty;                 // Roy+180319
        public static string UnloadAddress = string.Empty;                 // Roy+180319
        //public static string ToAddress = string.Empty;                // Roy-+180319 ... useless so far ...

        public static double DistanceFromSectionStart;
        public static int Guiding;
        public static string BlockControlSection = string.Empty;
        public static string HIDControlSection = string.Empty;                 // Roy+180319
        public static int Proc_ON;
        public static int cmd_Send;
        public static int cmd_Receive;
        public static int cmpCode;
        public static int cmpStatus;
        public static int vehModeStatus;
        public static int vehActionStatus;
        public static int eventTypes;
        public static int actionType;
        public static int vehLeftGuideLockStatus;
        public static int vehRightGuideLockStatus;
        public static int vehPauseStatus;
        public static int vehBlockStopStatus;
        public static int vehObstStopStatus;

        public static int vehHIDStopStatus;                     // Roy+180319
        public static int vehHIDPassReqst;                     // Roy+180319

        public static int vehBlockPassReqst;
        public static int locationTypes;
        public static int vehLoadStatus;
        public static int vehObstDist;

        public static string CSTID_Load;                     // Roy+180319
        public static string CSTID_UnLoad;                     // Roy+180319
        //
        public static bool blSendDataReceived = false;
        public static bool blRxDataSent = false;
        //
        public static bool blSendDataSent = false;
        public static bool blRxDataReceived = false;

        //++++++++++++++++++++++++++++++++                     // Roy+180319
        public static bool blCycleRun = false;
        public static int vehBlockPassReply;
        public static bool blReceiveBlockReply = false;

        public static int vehHIDPassReply;
        public static bool blReceiveHIDReply = false;           

        public static ManualResetEvent mResetEvent;
        //
        public static bool blBlockCtrl = false;
        public static bool blHIDCtrl = false;              
        //++++++++++++++++++++++++++++++++
    }
}
