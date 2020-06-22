using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDS;
using DDS.OpenSplice;
using Veh_HandShakeData;

namespace MirleOHT.類別.DCPS
{
    public static class DDS_Global
    {
        public static DomainParticipantFactory dpf;
        public static string ownerID;

        public static MotionInfo_ClientDataWriter motionInfo_ClientWriter;
        public static MotionInfo_ClientDataReader motionInfo_ClientReader;
        public static MotionInfo_ServerDataWriter motionInfo_ServerWriter;
        public static MotionInfo_ServerDataReader motionInfo_ServerReader;
        public static MotionInfo_Vehicle_CommDataReader motionInfo_VehCommReader;
        public static MotionInfo_Vehicle_CommDataWriter motionInfo_VehCommWriter;
        public static MotionInfo_Inter_Comm_SendDataDataReader motionInfo_VehInterCommSendDataReader;
        public static MotionInfo_Inter_Comm_SendDataDataWriter motionInfo_VehInterCommSendDataWriter;

        public static MotionInfo_Vehicle_Inter_Comm_ReportDataDataReader motionInfo_VehInterCommReptDataReader;
        public static MotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter motionInfo_VehInterCommReptDataWriter;
        public static MotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader motionInfo_VehInterCommReptData_134Reader;                 // Roy+191113
        public static MotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter motionInfo_VehInterCommReptData_134Writer;                 // Roy+191113

        public static MotionInfo_HandShake_SendDataDataReader motionInfo_HandShakeSendDataReader;
        public static MotionInfo_HandShake_SendDataDataWriter motionInfo_HandShakeSendDataWriter;
        public static MotionInfo_HandShake_RecieveDataDataReader motionInfo_HandShakeRecieveDataReader;
        public static MotionInfo_HandShake_RecieveDataDataWriter motionInfo_HandShakeRecieveDataWriter;

        //+++++++++++++++               // Roy+180205
        public static Between_Vehicle_DataDataWriter between_Vehicle_DataWriter;
        public static Between_Vehicle_DataDataReader between_Vehicle_DataReader;

        public static DDS.InstanceHandle betweenVehicleDataInst;
        public static Dictionary<string, DDS.InstanceHandle> dctBetweenVehicleDataInst;

        public static Between_Vehicle_Data betweenVehicleData;
        public static Between_Vehicle_Data betweenVehicleData4TestOnly;                     // Roy+180430
        //+++++++++++++++

        //++++++++++++++++++++++++++++++++++               // Roy+180222
        public static LoadPort_PIO_FromVehicleDataWriter loadPort_PIO_FromVehicleWriter;
        public static LoadPort_PIO_FromVehicleDataReader loadPort_PIO_FromVehicleReader;

        public static DDS.InstanceHandle loadPortPioFromVehicleInst;
        public static Dictionary<string, DDS.InstanceHandle> dctLoadPortPioFromVehicleInst;

        public static LoadPort_PIO_FromVehicle loadPortPioFromVehicle;

        //~~~~~~~~~~~~

        public static LoadPort_PIO_ToVehicleDataWriter loadPort_PIO_ToVehicleWriter;
        public static LoadPort_PIO_ToVehicleDataReader loadPort_PIO_ToVehicleReader;

        public static DDS.InstanceHandle loadPortPioToVehicleInst;
        public static Dictionary<string, DDS.InstanceHandle> dctLoadPortPioToVehicleInst;

        public static LoadPort_PIO_ToVehicle loadPortPioToVehicle;

        //~~~~~~~~~~~~

        public static EQ_Stages_Interface_IODataWriter eQ_Stages_Interface_IOWriter;
        public static EQ_Stages_Interface_IODataReader eQ_Stages_Interface_IOReader;

        public static DDS.InstanceHandle eQStagesInterfaceIOInst;
        public static Dictionary<string, DDS.InstanceHandle> dctEQStagesInterfaceIOInst;

        public static EQ_Stages_Interface_IO eQStagesInterfaceIO;

        //~~~~~~~~~~~~

        public static InterVehicles_BlockZones_ControlDataWriter interVehicles_BlockZones_ControlWriter;
        public static InterVehicles_BlockZones_ControlDataReader interVehicles_BlockZones_ControlReader;

        public static DDS.InstanceHandle interVehiclesBlockZonesControlInst;
        public static Dictionary<string, DDS.InstanceHandle> dctInterVehiclesBlockZonesControlInst;

        public static InterVehicles_BlockZones_Control interVehiclesBlockZonesControl;
        //++++++++++++++++++++++++++++++++++

        public static InstanceHandle userInstance;
        public static DDS.InstanceHandle vehCommDataInst;
        public static DDS.InstanceHandle vehInterCommSendDataInst;

        public static DDS.InstanceHandle vehInterCommReptDataInst;
        public static DDS.InstanceHandle vehInterCommReptData_134Inst;                 // Roy+191113

        public static DDS.InstanceHandle vehHandShakeSendDataInst;
        public static DDS.InstanceHandle vehHandShakeRxDataInst;

        //++++++++++++++++++++++++++++++++++++++++++                    // Roy+171204
        public static DDS.InstanceHandle vehClientDataInst;
        public static DDS.InstanceHandle vehServerDataInst;
        //public static List<Dictionary<string, DDS.InstanceHandle>> lstdctVehServerDataInst;          
        public static Dictionary<string, DDS.InstanceHandle> dctVehServerDataInst;                   // Roy+171204

        public static MotionInfo_Client motionInfoClientData;
        public static MotionInfo_Server motionInfoServerData;
        //++++++++++++++++++++++++++++++++++++++++++

        public static MotionInfo_Vehicle_Comm motionInfoCommData;
        public static MotionInfo_Inter_Comm_SendData motionInfoInterCommSendData;

        public static MotionInfo_Vehicle_Inter_Comm_ReportData motionInfoInterCommReptData;
        public static MotionInfo_Vehicle_Inter_Comm_ReportData_134 motionInfoInterCommReptData_134;                 // Roy+191113

        public static MotionInfo_HandShake_RecieveData motionInfoHandShakeRxData;
        public static MotionInfo_HandShake_SendData motionInfoHandShakeTxData;
        //
        public static IQueryCondition singleUser;
        public static IReadCondition newUser;
        public static IStatusCondition leftUser;
        public static GuardCondition escape;
        //
        public static WaitSet userLoadWS;
        public static ICondition[] guardList;
        public static SampleInfo[] nsInfo_UserLoad;
        //
        public static bool blClient = false;
        public static bool blServer = false;
        public const string clientPartitionName = "Client";
        public const string serverPartionName = "Server";
        //
        public static string segment_name;
        public static int segment_length;
        public static int current_pos;
        public const int segMaxCount = 100;
        public const int secMaxCount = 100;

        //++++++++++++++++++++++++++++++++++++++++++                    // Roy+180705
        public static bool checkblock { get; set; }
        public static string NGsection { get; set; }
        public static string fakeCstID { get; set; }

        //++++++++++++++++++++++++++++++++++++++++++              
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                    // Roy+180705
    public class HybridStopStatusForEvent
    {
        public bool bStopStatus_NotDefinedYet { get; set; }

        public bool bStopStatus_NotApplicable { get; set; }

        public bool bStopStatus_InsideBannedWalk_EMS { get; set; }                    //+++

        /// <summary>
        /// 目前 多餘的 ...
        /// </summary>
        public bool bStopStatus_InsideBannedWalk_Stop { get; set; }                    //+++
        public bool bStopStatus_InsideWalkable_EMS { get; set; }                    //+++

        /// <summary>
        /// 目前 多餘的 ...
        /// </summary>
        public bool bStopStatus_InsideWalkable_Stop { get; set; }                    //+++

        public bool bStopStatus_Manual_EMS { get; set; }
        public bool bStopStatus_Manual_Stop { get; set; }
        public bool bStopStatus_Manual_Pause { get; set; }                    //+++
        public bool bStopStatus_HwGuard_Stop { get; set; }
        public bool bStopStatus_LiDAR { get; set; }
        public bool bStopStatus_BlockCtrl_Zone { get; set; }
        public bool bStopStatus_HIDCtrl_Zone { get; set; }                    //+++
        public bool bStopStatus_BlockCtrl_Section { get; set; }
        public bool bStopStatus_HIDCtrl_Section { get; set; }

        public bool bStopStatus_ReserveCtrl_Section { get; set; }                       // Roy+190311

        public bool bStopStatus_External_Pause { get; set; }
        public bool bStopStatus_External_CancelAbort { get; set; }

        public void ResetAll()
        {
            bStopStatus_NotDefinedYet = false;

            bStopStatus_NotApplicable = false;

            bStopStatus_InsideBannedWalk_EMS = false;                    //+++
            bStopStatus_InsideBannedWalk_Stop = false;                    //+++
            bStopStatus_InsideWalkable_EMS = false;                    //+++
            bStopStatus_InsideWalkable_Stop = false;                    //+++

            bStopStatus_Manual_EMS = false;
            bStopStatus_Manual_Stop = false;
            bStopStatus_Manual_Pause = false;                    //+++
            bStopStatus_HwGuard_Stop = false;
            bStopStatus_LiDAR = false;
            bStopStatus_BlockCtrl_Zone = false;
            bStopStatus_HIDCtrl_Zone = false;                    //+++
            bStopStatus_BlockCtrl_Section = false;
            bStopStatus_HIDCtrl_Section = false;

            bStopStatus_ReserveCtrl_Section = false;                // Roy+190311

            bStopStatus_External_Pause = false;
            bStopStatus_External_CancelAbort = false;
        }

        


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++               // Roy+190828               // Roy*190907
        public string GetHybridSummary(int iHybridValue)
        {
            string sHybridSummary = "";

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_NotDefinedYet)) == ((int)StopStatusForEvent.StopStatus_NotDefinedYet))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_NotDefinedYet)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_NotDefinedYet).ToString("00") + "] NotDefinedYet,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_NotApplicable)) == ((int)StopStatusForEvent.StopStatus_NotApplicable))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_NotApplicable)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_NotApplicable).ToString("00") + "] NotApplicable,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_InsideBannedWalk_EMS)) == ((int)StopStatusForEvent.StopStatus_InsideBannedWalk_EMS))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_InsideBannedWalk_EMS)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_InsideBannedWalk_EMS).ToString("00") + "] InsideBannedWalk_EMS,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_InsideBannedWalk_Stop)) == ((int)StopStatusForEvent.StopStatus_InsideBannedWalk_Stop))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_InsideBannedWalk_Stop)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_InsideBannedWalk_Stop).ToString("00") + "] InsideBannedWalk_Stop,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_InsideWalkable_EMS)) == ((int)StopStatusForEvent.StopStatus_InsideWalkable_EMS))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_InsideWalkable_EMS)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_InsideWalkable_EMS).ToString("00") + "] InsideWalkable_EMS,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_InsideWalkable_Stop)) == ((int)StopStatusForEvent.StopStatus_InsideWalkable_Stop))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_InsideWalkable_Stop)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_InsideWalkable_Stop).ToString("00") + "] InsideWalkable_Stop,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_Manual_EMS)) == ((int)StopStatusForEvent.StopStatus_Manual_EMS))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_Manual_EMS)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_Manual_EMS).ToString("00") + "] Manual_EMS,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_Manual_Stop)) == ((int)StopStatusForEvent.StopStatus_Manual_Stop))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_Manual_Stop)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_Manual_Stop).ToString("00") + "] Manual_Stop,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_Manual_Pause)) == ((int)StopStatusForEvent.StopStatus_Manual_Pause))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_Manual_Pause)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_Manual_Pause).ToString("00") + "] Manual_Pause,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_HwGuard_Stop)) == ((int)StopStatusForEvent.StopStatus_HwGuard_Stop))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_HwGuard_Stop)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_HwGuard_Stop).ToString("00") + "] HwGuard_Stop,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_LiDAR)) == ((int)StopStatusForEvent.StopStatus_LiDAR))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_LiDAR)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_LiDAR).ToString("00") + "] LiDAR,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_BlockCtrl_Zone)) == ((int)StopStatusForEvent.StopStatus_BlockCtrl_Zone))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_BlockCtrl_Zone)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_BlockCtrl_Zone).ToString("00") + "] BlockCtrl_Zone,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_HIDCtrl_Zone)) == ((int)StopStatusForEvent.StopStatus_HIDCtrl_Zone))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_HIDCtrl_Zone)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_HIDCtrl_Zone).ToString("00") + "] HIDCtrl_Zone,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_BlockCtrl_Section)) == ((int)StopStatusForEvent.StopStatus_BlockCtrl_Section))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_BlockCtrl_Section)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_BlockCtrl_Section).ToString("00") + "] BlockCtrl_Section,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_HIDCtrl_Section)) == ((int)StopStatusForEvent.StopStatus_HIDCtrl_Section))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_HIDCtrl_Section)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_HIDCtrl_Section).ToString("00") + "] HIDCtrl_Section,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_ReserveCtrl_Section)) == ((int)StopStatusForEvent.StopStatus_ReserveCtrl_Section))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_ReserveCtrl_Section)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_ReserveCtrl_Section).ToString("00") + "] ReserveCtrl_Section,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_External_Pause)) == ((int)StopStatusForEvent.StopStatus_External_Pause))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_External_Pause)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_External_Pause).ToString("00") + "] External_Pause,";
            }

            //if ((iHybridValue & ((int)StopStatusForEvent.StopStatus_External_CancelAbort)) == ((int)StopStatusForEvent.StopStatus_External_CancelAbort))              // Roy-190916
            if (((iHybridValue >> ((int)StopStatusForEvent.StopStatus_External_CancelAbort)) % 2) == 1)                 // Roy+190916
            {
                sHybridSummary += " [" + ((int)StopStatusForEvent.StopStatus_External_CancelAbort).ToString("00") + "] External_CancelAbort,";
            }

            if (sHybridSummary.Length > 2)                 // Roy*190830
            {
                sHybridSummary = sHybridSummary.Substring(1, sHybridSummary.Length - 2);                 // Roy*190830
            }

            return (sHybridSummary);
        }
        
    }
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++      

}
