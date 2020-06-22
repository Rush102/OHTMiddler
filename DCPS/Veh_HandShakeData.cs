using DDS;
using System;
using System.Runtime.InteropServices;

namespace Veh_HandShakeData
{
    public struct MAX_NAME { public static readonly int value = 32; }

    #region Status
    public enum Status
    {
        OK,
        NG,
        TimeOut
    };
    #endregion

    #region OnOff
    public enum OnOff
    {
        OFF,
        ON
    };
    #endregion

    #region PauseStatus
    public enum PauseStatus
    {
        PauseNo,
        PauseYes
    };
    #endregion

    #region StopStatus
    public enum StopStatus
    {
        StopNo,
        StopYes
    };
    #endregion

    #region StopStatusForEvent
    public enum StopStatusForEvent
    {
        StopStatus_NotDefinedYet,
        StopStatus_NotApplicable,
        StopStatus_InsideBannedWalk_EMS,
        StopStatus_InsideBannedWalk_Stop,
        StopStatus_InsideWalkable_EMS,
        StopStatus_InsideWalkable_Stop,
        StopStatus_Manual_EMS,
        StopStatus_Manual_Stop,
        StopStatus_Manual_Pause,
        StopStatus_HwGuard_Stop,
        StopStatus_LiDAR,
        StopStatus_BlockCtrl_Zone,
        StopStatus_HIDCtrl_Zone,
        StopStatus_BlockCtrl_Section,
        StopStatus_HIDCtrl_Section,
        StopStatus_ReserveCtrl_Section,
        StopStatus_External_Pause,
        StopStatus_External_CancelAbort
    };
    #endregion

    #region OperationMode
    public enum OperationMode
    {
        Auto,
        Manual
    };
    #endregion

    #region CommandMode
    public enum CommandMode
    {
        Disabled,
        Enabled
    };
    #endregion

    #region ReplyCode
    public enum ReplyCode
    {
        No,
        Yes
    };
    #endregion

    #region LocationType
    public enum LocationType
    {
        Arrived,
        Passed
    };
    #endregion

    #region MoveType
    public enum MoveType
    {
        single,
        cycle
    };
    #endregion

    #region VehCompleteFlag
    public enum VehCompleteFlag
    {
        Unfinished,
        Finished
    };
    #endregion

    #region LockUnLock
    public enum LockUnLock
    {
        Locked,
        UnLocked
    };
    #endregion

    #region RequestType
    public enum RequestType
    {
        Cancel,
        Abort
    };
    #endregion

    #region RequestPauseType
    public enum RequestPauseType
    {
        Released,
        Paused
    };
    #endregion

    #region VehPowerStatus
    public enum VehPowerStatus
    {
        OFF,
        ON
    };
    #endregion

    #region VehLoadedStatus
    public enum VehLoadedStatus
    {
        NotExisted,
        Existed
    };
    #endregion

    #region VehObstacleStopStatus
    public enum VehObstacleStopStatus
    {
        NonStop,
        Stop
    };
    #endregion

    #region VehBlockStopStatus
    public enum VehBlockStopStatus
    {
        NonStop,
        Stop
    };
    #endregion

    #region VehHIDStopStatus
    public enum VehHIDStopStatus
    {
        NonStop,
        Stop
    };
    #endregion

    #region VehPauseStatus
    public enum VehPauseStatus
    {
        NonStop,
        Stop
    };
    #endregion

    #region VehLeftGuideLockStatus
    public enum VehLeftGuideLockStatus
    {
        UnLocked,
        Locked
    };
    #endregion

    #region VehRightGuideLockStatus
    public enum VehRightGuideLockStatus
    {
        UnLocked,
        Locked
    };
    #endregion

    #region VehCompleteCode
    public enum VehCompleteCode
    {
        Normal,
        Abnormal
    };
    #endregion

    #region VehControlMode
    public enum VehControlMode
    {
        OnlineRemote,
        OnlineLocal,
        Offline
    };
    #endregion

    #region VehWheelSteeringAngle
    public enum VehWheelSteeringAngle
    {
        Zero,
        RightNinety,
        LeftNinety
    };
    #endregion

    #region CmdType
    public enum CmdType
    {
        NotDefinedYet,
        CmdToMove,
        CmdToLoad,
        CmdToUnload,
        CmdToBlockSectionQueryResult,
        CmdToHIDSectionQueryResult,
        CmdToReserveSectionQueryResult,
        CmdToStop,
        CmdToPause,
        CmdToContinue,
        CmdToRestart,
        CmdToCancel,
        CmdToAbort,
        CmdToOverride,
        CmdToAvoid,
        CmdToMaintenance,
        CmdForChangeStatus
    };
    #endregion

    #region VehCompleteStatus
    public enum VehCompleteStatus
    {
        CmpAsNormal,
        CmpAsCancel,
        CmpAsAbort,
        CmpAsOverride
    };
    #endregion

    #region VehEventTypes
    public enum VehEventTypes
    {
        NotDefinedYet,
        Load_Arrived,
        Load_Start,
        Load_Pick,
        Load_Complete,
        Unload_Arrived,
        Unload_Start,
        Unload_Place,
        Unload_Complete,
        Address_Arrival,
        Address_Pass,
        Moving_Pause,
        Moving_Restart,
        BlockSection_Query,
        HIDSection_Query,
        BlockNHidSection_Query,
        PostBlockSectionExit,
        PostHIDSectionExit,
        PostBlockNHidSectionExit,
        Moving_Complete,
        ReserveSection_Query,
        BatteryReport
    };
    #endregion

    #region ActionTypes
    public enum ActionTypes
    {
        NotDefinedYet,
        ActToMove,
        ActToLoad,
        ActToUnload,
        ActToLoad_Unload,
        ActToHome_Calbration_Point,
        ActToContinue,
        ActToCycle,
        ActToOverride,
        ActToAvoid,
        ActToAbort,
        ActToCancel
    };
    #endregion

    #region VehModeStatus
    public enum VehModeStatus
    {
        ModeAsStop,
        ModeAsStarting,
        ModeAsManual,
        ModeAsAuto_Moving,
        ModeAsAuto,
        ModeAsHome_Calibration_Point,
        ModeAsTeaching
    };
    #endregion

    #region VehActionStatus
    public enum VehActionStatus
    {
        ActAsStopping,
        ActAsMoving,
        ActAsLoad,
        ActAsUnLoad,
        ActAsHoming,
        ActAsTeach,
        ActAsGripper_Teaching,
        ActAsCycle_Run
    };
    #endregion

    #region MotionInfo_Client
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_Client
    {
        public int vehID;
        public string vehName = string.Empty;
        public int NO;
        public int En_TYPE;
        public int CUR_ADR;
        public int CUR_SEC;
        public int IS_CARRY;
        public int IS_OBST;
        public int IS_BLOCK;
        public int IS_HID;
        public int IS_PAUSE;
        public int IS_LGUIDE;
        public int IS_RGUIDE;
        public int SEC_DISTANCE;
        public int BLOCK_SEC;
        public int HID_SEC;
        public int ACT_TYPE;
        public int RPLY_CODE;
    };
    #endregion

    #region MotionInfo_Server
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_Server
    {
        public int vehID;
        public string vehName = string.Empty;
        public int NO;
        public int ACT_TYPE;
        public string FROM_ADR_ID = string.Empty;
        public string TO_ADR_ID = string.Empty;
        public string CAST_ID = string.Empty;
        public int RPLY_CODE;
    };
    #endregion

    #region MotionInfo_Vehicle_Comm
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_Vehicle_Comm
    {
        public int vehID;
        public string vehName = string.Empty;
        public double front_wheel_torque;
        public double rear_wheel_torque;
        public double front_wheel_speed;
        public double rear_wheel_speed;
        public double front_wheel_acc;
        public double front_wheel_dec;
        public double rear_wheel_acc;
        public double rear_wheel_dec;
        public double front_wheel_dist;
        public double rear_wheel_dist;
        public double ave_dist;
        public double ave_speed;
        public double ave_torque;
        public double ave_acc;
        public double ave_dec;
        public double current_pos;
        public double target_speed;
        public double max_speed;
        public double min_spedd;
        public bool right_guide_up;
        public bool left_guide_up;
        public bool right_guide_down;
        public bool left_guide_down;
        public bool right_guide_detection;
        public bool left_guide_detection;
        public bool long_range_obst;
        public bool short_range_obst;
        public bool mid_range_obst;
        public string current_address = string.Empty;
        public string from_address = string.Empty;
        public string to_address = string.Empty;
        public string current_stage = string.Empty;
        public string from_stage = string.Empty;
        public string to_stage = string.Empty;
        public string port_address = string.Empty;
        public string barcode_read_address = string.Empty;
    };
    #endregion

    #region MotionInfo_Move
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_Move
    {
        public Veh_HandShakeData.MoveType eMoveType;
        public string Address = string.Empty;
        public string Stage = string.Empty;
        public string[] GuidingSections = new string[0];
        public string[] GuidingAddresses = new string[0];
        public int ForLoading;
        public int ForUnLoading;
        public int ForMaintain;
    };
    #endregion

    #region MotionInfo_Load
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public sealed class MotionInfo_Load
    {
        public string MCS_CSTID = string.Empty;
        public string MCS_CST2ID = string.Empty;
        public string Veh_CSTID = string.Empty;
        public string Veh_CST2ID = string.Empty;
        public int VerPort_OK;
        public int VerPort_NG;
        public int VerCST_OK;
        public int VerCST_NG;
        public int VerCST2_OK;
        public int VerCST2_NG;
        public int With_CST;
        public int Without_CST;
        public int With_CST2;
        public int Without_CST2;
        public Veh_HandShakeData.VehLoadedStatus LoadStatus;
    };
    #endregion

    #region MotionInfo_UnLoad
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public sealed class MotionInfo_UnLoad
    {
        public string MCS_CSTID = string.Empty;
        public string MCS_CST2ID = string.Empty;
        public string Veh_CSTID = string.Empty;
        public string Veh_CST2ID = string.Empty;
        public int VerPort_OK;
        public int VerPort_NG;
        public int VerCST_OK;
        public int VerCST_NG;
        public int VerCST2_OK;
        public int VerCST2_NG;
        public int With_CST;
        public int Without_CST;
        public int With_CST2;
        public int Without_CST2;
        public Veh_HandShakeData.VehLoadedStatus LoadStatus;
    };
    #endregion

    #region MotionInfo_BlockSectionPassReply
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_BlockSectionPassReply
    {
        public string Section = string.Empty;
        public Veh_HandShakeData.Status BlockSectionPassReply;
    };
    #endregion

    #region MotionInfo_HIDSectionPassReply
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_HIDSectionPassReply
    {
        public string Section = string.Empty;
        public Veh_HandShakeData.Status HIDSectionPassReply;
    };
    #endregion

    #region MotionInfo_ReserveSectionPassReply
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_ReserveSectionPassReply
    {
        public string[] SectionList = new string[0];
        public Veh_HandShakeData.Status ReserveSectionPassReply;
    };
    #endregion

    #region MotionInfo_BlockSectionPassReqst
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public sealed class MotionInfo_BlockSectionPassReqst
    {
        public string Section = string.Empty;
        public Veh_HandShakeData.Status BlockSectionPassReqst;
    };
    #endregion

    #region MotionInfo_HIDSectionPassReqst
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public sealed class MotionInfo_HIDSectionPassReqst
    {
        public string Section = string.Empty;
        public Veh_HandShakeData.Status HIDSectionPassReqst;
    };
    #endregion

    #region MotionInfo_ReserveSectionPassReqst
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public sealed class MotionInfo_ReserveSectionPassReqst
    {
        public string[] SectionList = new string[0];
        public Veh_HandShakeData.Status ReserveSectionPassReqst;
    };
    #endregion

    #region VehCmdType
    [StructLayout(LayoutKind.Sequential)]
    public sealed class VehCmdType
    {
        public Veh_HandShakeData.CmdType eCmdType;
    };
    #endregion

    #region MotionInfo_Inter_Comm_SendData
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_Inter_Comm_SendData
    {
        public int vehID;
        public string vehName = string.Empty;
        public int cmd_Send;
        public int cmd_Receive;
        public int Proc_ON;
        public Veh_HandShakeData.VehCmdType udtCmdType = new Veh_HandShakeData.VehCmdType();
        public Veh_HandShakeData.VehControlMode udtControlMode;
        public Veh_HandShakeData.MotionInfo_Move udtMove = new Veh_HandShakeData.MotionInfo_Move();
        public Veh_HandShakeData.MotionInfo_Load udtLoad = new Veh_HandShakeData.MotionInfo_Load();
        public Veh_HandShakeData.MotionInfo_UnLoad udtUnLoad = new Veh_HandShakeData.MotionInfo_UnLoad();
        public int isContinue;
        public int isStop;
        public int isPause;
        public bool BlockControlTimeOut;
        public bool HIDControlTimeOut;
        public bool ReserveSectionTimeOut;
        public Veh_HandShakeData.MotionInfo_ReserveSectionPassReply ReserveSectionPassReply = new Veh_HandShakeData.MotionInfo_ReserveSectionPassReply();
        public Veh_HandShakeData.MotionInfo_BlockSectionPassReply BlockSectionPassReply = new Veh_HandShakeData.MotionInfo_BlockSectionPassReply();
        public Veh_HandShakeData.MotionInfo_HIDSectionPassReply HIDSectionPassReply = new Veh_HandShakeData.MotionInfo_HIDSectionPassReply();
    };
    #endregion

    #region MotionInfo_Vehicle_Inter_Comm_ReportData_134
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public sealed class MotionInfo_Vehicle_Inter_Comm_ReportData_134
    {
        public int vehID;
        public string vehName = string.Empty;
        public Veh_HandShakeData.MotionInfo_Load loadStatus = new Veh_HandShakeData.MotionInfo_Load();
        public Veh_HandShakeData.MotionInfo_UnLoad unLoadStatus = new Veh_HandShakeData.MotionInfo_UnLoad();
        public Veh_HandShakeData.MotionInfo_BlockSectionPassReqst BlockSectionPassReqst = new Veh_HandShakeData.MotionInfo_BlockSectionPassReqst();
        public Veh_HandShakeData.MotionInfo_HIDSectionPassReqst HIDSectionPassReqst = new Veh_HandShakeData.MotionInfo_HIDSectionPassReqst();
        public Veh_HandShakeData.MotionInfo_ReserveSectionPassReqst ReserveSectionPassReqst = new Veh_HandShakeData.MotionInfo_ReserveSectionPassReqst();
        public Veh_HandShakeData.VehWheelSteeringAngle WheelAngle;
        public Veh_HandShakeData.VehControlMode ConrtolMode;
        public int WhichType;
        public int LocationType;
        public string Section = string.Empty;
        public string Address = string.Empty;
        public string Stage = string.Empty;
        public double DistanceFromSectionStart;
        public double WalkLength;
        public double PowerConsume;
        public int Guiding;
        public string ReserveSection = string.Empty;
        public string BlockControlSection = string.Empty;
        public string HIDControlSection = string.Empty;
        public int Proc_ON;
        public int cmd_Send;
        public int cmd_Receive;
        public int cmpCode;
        public int cmpStatus;
        public int stopStatusForEvent;
        public int vehModeStatus;
        public int vehActionStatus;
        public int eventTypes;
        public int actionType;
        public int vehLeftGuideLockStatus;
        public int vehRightGuideLockStatus;
        public int vehPauseStatus;
        public int vehBlockStopStatus;
        public int vehReserveStopStatus;
        public int vehHIDStopStatus;
        public int vehObstacleStopStatus;
        public int vehBlockSectionPassReqst;
        public int vehHIDSectionPassReqst;
        public int vehReserveSectionPassReqst;
        public int locationTypes;
        public int vehLoadStatus;
        public int vehObstDist;
        public int vehPowerStatus;
        public int ChargeStatus;
        public int BatteryCapacity;
        public int BatteryTemperature;
        public int ErrorCode;
        public int ErrorStatus;
    };
    #endregion

    #region MotionInfo_Vehicle_Inter_Comm_ReportData
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public sealed class MotionInfo_Vehicle_Inter_Comm_ReportData
    {
        public int vehID;
        public string vehName = string.Empty;
        public Veh_HandShakeData.MotionInfo_Load loadStatus = new Veh_HandShakeData.MotionInfo_Load();
        public Veh_HandShakeData.MotionInfo_UnLoad unLoadStatus = new Veh_HandShakeData.MotionInfo_UnLoad();
        public Veh_HandShakeData.MotionInfo_BlockSectionPassReqst BlockSectionPassReqst = new Veh_HandShakeData.MotionInfo_BlockSectionPassReqst();
        public Veh_HandShakeData.MotionInfo_HIDSectionPassReqst HIDSectionPassReqst = new Veh_HandShakeData.MotionInfo_HIDSectionPassReqst();
        public Veh_HandShakeData.MotionInfo_ReserveSectionPassReqst ReserveSectionPassReqst = new Veh_HandShakeData.MotionInfo_ReserveSectionPassReqst();
        public Veh_HandShakeData.VehWheelSteeringAngle WheelAngle;
        public Veh_HandShakeData.VehControlMode ConrtolMode;
        public int WhichType;
        public int LocationType;
        public string Section = string.Empty;
        public string Address = string.Empty;
        public string Stage = string.Empty;
        public double DistanceFromSectionStart;
        public double WalkLength;
        public double PowerConsume;
        public int Guiding;
        public string ReserveSection = string.Empty;
        public string BlockControlSection = string.Empty;
        public string HIDControlSection = string.Empty;
        public int Proc_ON;
        public int cmd_Send;
        public int cmd_Receive;
        public int cmpCode;
        public int cmpStatus;
        public int stopStatusForEvent;
        public int vehModeStatus;
        public int vehActionStatus;
        public int eventTypes;
        public int actionType;
        public int vehLeftGuideLockStatus;
        public int vehRightGuideLockStatus;
        public int vehPauseStatus;
        public int vehBlockStopStatus;
        public int vehReserveStopStatus;
        public int vehHIDStopStatus;
        public int vehObstacleStopStatus;
        public int vehBlockSectionPassReqst;
        public int vehHIDSectionPassReqst;
        public int vehReserveSectionPassReqst;
        public int locationTypes;
        public int vehLoadStatus;
        public int vehObstDist;
        public int vehPowerStatus;
        public int ChargeStatus;
        public int BatteryCapacity;
        public int BatteryTemperature;
        public int ErrorCode;
        public int ErrorStatus;
    };
    #endregion

    #region MotionInfo_HandShake_SendData
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_HandShake_SendData
    {
        public int vehID;
        public string vehName = string.Empty;
        public int cmdSend;
        public int cmdReceive;
    };
    #endregion

    #region MotionInfo_HandShake_RecieveData
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MotionInfo_HandShake_RecieveData
    {
        public int vehID;
        public string vehName = string.Empty;
        public int cmdSend;
        public int cmdReceive;
    };
    #endregion

    #region Between_Vehicle_Data
    [StructLayout(LayoutKind.Sequential)]
    public sealed class Between_Vehicle_Data
    {
        public string Vehicle_ID = string.Empty;
        public bool InService;
        public bool EQ_Online;
        public bool EQ_Error;
        public bool IsMoving;
        public bool IsHoisting;
        public string Current_Zone_ID = string.Empty;
        public string Current_Zone_ID2 = string.Empty;
        public string Current_Zone_ID3 = string.Empty;
        public string Current_Section_ID = string.Empty;
        public int Current_Section_Offset;
        public double Current_Map_HeadingPose_Angle;
        public int Current_Map_AbsPos_X;
        public int Current_Map_AbsPos_Y;
        public string Current_Address_ID = string.Empty;
        public string Current_Stage_ID = string.Empty;
        public string BlockQry_Zone_ID_1 = string.Empty;
        public string BlockQry_Zone_ID_2 = string.Empty;
        public string BlockQry_Zone_ID_3 = string.Empty;
        public string BlockQry_Zone_ID_4 = string.Empty;
        public string BlockQry_Zone_ID_5 = string.Empty;
        public string BlockQry_Zone_ID_6 = string.Empty;
        public string BlockQry_Zone_ID_7 = string.Empty;
        public string BlockQry_Zone_ID_8 = string.Empty;
        public string BlockQry_Zone_ID_9 = string.Empty;
        public string Blocking_Zone_Owner = string.Empty;
        public string Blocking_Zone_ID = string.Empty;
        public int Blocking_ZoneEntry_Distance;
        public string[] UniqueSections_FromNow2JustB4QryBlockZoneExit = new string[0];
        public string[] UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit = new string[0];
        public string[] UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge = new string[0];
        public string[] UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby = new string[0];
        public string[] UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay = new string[0];
        public int Status1;
        public int Status2;
        public string Start_Time = string.Empty;
        public string Software_Version = string.Empty;
        public string Updating_Timestamp = string.Empty;
    };
    #endregion

    #region PortOwnerType
    public enum PortOwnerType
    {
        NotDefinedYet,
        Equipment,
        Stocker,
        MaintainLifter,
        OverHeadBuffer,
        HidControlBox,
        ChargeDock,
        Others
    };
    #endregion

    #region LoadPort_PIO_FromVehicle
    [StructLayout(LayoutKind.Sequential)]
    public sealed class LoadPort_PIO_FromVehicle
    {
        public string EQ_Name = string.Empty;
        public string Port_Name = string.Empty;
        public string PIO_ID = string.Empty;
        public int PIO_Channel;
        public Veh_HandShakeData.PortOwnerType PortType;
        public bool InService;
        public bool DO01_VALID;
        public bool DO02_CS_0;
        public bool DO03_CS_1;
        public bool DO04_nil;
        public bool DO05_TR_REQ;
        public bool DO06_BUSY;
        public bool DO07_COMPT;
        public bool DO08_CONT;
        public bool SELECT;
    };
    #endregion

    #region LoadPort_PIO_ToVehicle
    [StructLayout(LayoutKind.Sequential)]
    public sealed class LoadPort_PIO_ToVehicle
    {
        public string EQ_Name = string.Empty;
        public string Port_Name = string.Empty;
        public string PIO_ID = string.Empty;
        public int PIO_Channel;
        public Veh_HandShakeData.PortOwnerType PortType;
        public bool InService;
        public bool DI01_L_REQ;
        public bool DI02_U_REQ;
        public bool DI03_nil;
        public bool DI04_READY;
        public bool DI05_nil;
        public bool DI06_nil;
        public bool DI07_HO_AVBL;
        public bool DI08_ES;
        public bool READY_GO;
        public bool Tray_Detection;
    };
    #endregion

    #region EQ_Stages_Interface_IO
    [StructLayout(LayoutKind.Sequential)]
    public sealed class EQ_Stages_Interface_IO
    {
        public string EQ_Name = string.Empty;
        public Veh_HandShakeData.PortOwnerType PortType;
        public bool InService;
        public bool EQ_Online;
        public bool EQ_Error;
        public string ST01_Port_Name = string.Empty;
        public int ST01_Stage_ID;
        public bool ST01_Ready;
        public bool ST01_Loaded;
        public string ST02_Port_Name = string.Empty;
        public int ST02_Stage_ID;
        public bool ST02_Ready;
        public bool ST02_Loaded;
        public string ST03_Port_Name = string.Empty;
        public int ST03_Stage_ID;
        public bool ST03_Ready;
        public bool ST03_Loaded;
        public string ST04_Port_Name = string.Empty;
        public int ST04_Stage_ID;
        public bool ST04_Ready;
        public bool ST04_Loaded;
        public string ST05_Port_Name = string.Empty;
        public int ST05_Stage_ID;
        public bool ST05_Ready;
        public bool ST05_Loaded;
        public string ST06_Port_Name = string.Empty;
        public int ST06_Stage_ID;
        public bool ST06_Ready;
        public bool ST06_Loaded;
        public string ST07_Port_Name = string.Empty;
        public int ST07_Stage_ID;
        public bool ST07_Ready;
        public bool ST07_Loaded;
        public string ST08_Port_Name = string.Empty;
        public int ST08_Stage_ID;
        public bool ST08_Ready;
        public bool ST08_Loaded;
        public string ST09_Port_Name = string.Empty;
        public int ST09_Stage_ID;
        public bool ST09_Ready;
        public bool ST09_Loaded;
        public string ST10_Port_Name = string.Empty;
        public int ST10_Stage_ID;
        public bool ST10_Ready;
        public bool ST10_Loaded;
        public string ST11_Port_Name = string.Empty;
        public int ST11_Stage_ID;
        public bool ST11_Ready;
        public bool ST11_Loaded;
        public string ST12_Port_Name = string.Empty;
        public int ST12_Stage_ID;
        public bool ST12_Ready;
        public bool ST12_Loaded;
        public string ST13_Port_Name = string.Empty;
        public int ST13_Stage_ID;
        public bool ST13_Ready;
        public bool ST13_Loaded;
        public string ST14_Port_Name = string.Empty;
        public int ST14_Stage_ID;
        public bool ST14_Ready;
        public bool ST14_Loaded;
        public string ST15_Port_Name = string.Empty;
        public int ST15_Stage_ID;
        public bool ST15_Ready;
        public bool ST15_Loaded;
        public string ST16_Port_Name = string.Empty;
        public int ST16_Stage_ID;
        public bool ST16_Ready;
        public bool ST16_Loaded;
    };
    #endregion

    #region InterVehicles_BlockZones_Control
    [StructLayout(LayoutKind.Sequential)]
    public sealed class InterVehicles_BlockZones_Control
    {
        public string BlockZoneID = string.Empty;
        public string HasLockedBy_VehID = string.Empty;
        public string RequestUnlockingBy_VehID = string.Empty;
        public string HasUnlockedBy_VehID = string.Empty;
        public string HasAcquiredBy_VehID = string.Empty;
        public string Locking_Timestamp = string.Empty;
        public string RequestUnlocking_Timestamp = string.Empty;
        public string Unlocking_Timestamp = string.Empty;
        public string Acquiring_Timestamp = string.Empty;
        public string ServerHeartbeating_Timestamp = string.Empty;
        public string SeverInstanceID = string.Empty;
        public bool InService;
    };
    #endregion

}

