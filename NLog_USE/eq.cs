using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MirleOHT.類別;

namespace OHTM.NLog_USE
{
    public static class eq
    {
        public static IntPtr GpHand = new IntPtr();
        public static IntPtr GpHand_2 = new IntPtr();
        public static IntPtr DeviceHandle = IntPtr.Zero;
        public static bool bIsSimMode = false;                  // Roy+170501
        public static bool bSimWalkPlay = false;                  // Roy+171128

        public static bool bMuteBuzzer4Walk = false;                  // Roy+180131
        public static bool bMuteBuzzer4Port = false;                  // Roy+180212

        public static bool bIsLine_OHT = false;                  // Roy+180809

        public static bool bShowOtherVeh = false;                // Roy+180918

        public static bool bIsVehMViaMirle300_TCPIP = false;                  // Roy+171219
        public static bool bIsVehMViaMirleA00_DDS = true;                  // Roy+171219

        public static bool bEnforcedToNowStop_All = false;               // Roy+170622
        public static bool bRequestToSlowStop_All = false;               // Roy+170721

        public static bool bBlockCtrlZonePausing_Walk4AcqTokenLater = false;               // Roy+180115

        public static bool bEnforcedToNowStop_Walk4ResumeLater = false;               // Roy+180109
        public static bool bRequestToSlowStop_Walk4ResumeLater = false;               // Roy+180109
        public static bool bRequestToSlowerMove_Walk = false;                          // use 25% speed ...          // Roy+180109

        public static bool bIsFunSetPos4WalkDone = false;                // Roy+180727

        public static long iWalkCurveNoSnrCnt = 0;                  // Roy+180705
        public static long iWalkCurveTactTimeMax_InMS = 0;                  // Roy+180705

        //+++++++++++++++++++++             // Roy+180904
        public static string sCmdName = "";
        public static string sCmdMemo = "";
        public static string sActName = "";
        public static string sActMemo = "";
        public static string sAct2Name = "";
        public static string sAct2Memo = "";
        public static string sFnName = "";
        public static string sFnMemo = "";

        public static string sAuxMemo = "";

        public static int iTargetAdjLoopIndex = 0;
        //+++++++++++++++++++++

        public static MirleOHT.類別.DCPS.HybridStopStatusForEvent oHybridStopStatusForEvent = new MirleOHT.類別.DCPS.HybridStopStatusForEvent();                 // Roy+180705

        //++++++++++++++++++++++++++++++++++++++++++                  // Roy+180612
        public static long iIntByExtCancelAbortTimes = 0;
        public static long iIntByExtPauseTimes = 0;
        public static long iIntByHidCtrlSectionTimes = 0;
        public static long iIntByBlockCtrlSectionTimes = 0;
        public static long iIntByHidCtrlZoneTimes = 0;                  // Roy+180705
        public static long iIntByBlockCtrlZoneTimes = 0;
        public static long iIntBySnrLidarTimes = 0;
        public static long iIntByHwGuardStopTimes = 0;
        public static long iIntByManualPauseTimes = 0;                  // Roy+180705
        public static long iIntByManualStopTimes = 0;
        public static long iIntByManualEmsTimes = 0;
        public static long iIntByInsideWalkableStopTimes = 0;                  // Roy+180705
        public static long iIntByInsideWalkableEmsTimes = 0;                  // Roy+180705
        public static long iIntByInsideBannedWalkStopTimes = 0;                  // Roy+180705
        public static long iIntByInsideBannedWalkEmsTimes = 0;                  // Roy+180705
        public static long iIntByUnknownTimes = 0;

        public static long iInterruptTotalTimes
        {
            get
            {
                //return (iIntByExtCancelAbortTimes + iIntByExtPauseTimes + iIntByHidCtrlSectionTimes + iIntByBlockCtrlSectionTimes + iIntByBlockCtrlZoneTimes
                //            + iIntBySnrLidarTimes + iIntByHwGuardStopTimes + iIntByManualStopTimes + iIntByManualEmsTimes + iIntByUnknownTimes);                  // Roy-180705

                return (iIntByExtCancelAbortTimes + iIntByExtPauseTimes + iIntByHidCtrlSectionTimes + iIntByBlockCtrlSectionTimes + iIntByHidCtrlZoneTimes + iIntByBlockCtrlZoneTimes
                            + iIntBySnrLidarTimes + iIntByHwGuardStopTimes + iIntByManualPauseTimes + iIntByManualStopTimes + iIntByManualEmsTimes
                            + iIntByInsideWalkableStopTimes + iIntByInsideWalkableEmsTimes + iIntByInsideBannedWalkStopTimes + iIntByInsideBannedWalkEmsTimes + iIntByUnknownTimes);                  // Roy+180705
            }
        }

        //~~~~~~~~~

        public static TimeSpan tsRecordRouteDuration = new TimeSpan();                     // Roy+180621
        public static TimeSpan tsRecordHoistDuration = new TimeSpan();                     // Roy+180621

        public static long iRecordWalkRouteCnt = 0;
        public static long iRecordWalkCurveCnt = 0;
        public static long iRecordLiftLeftCnt = 0;
        public static long iRecordLiftRightCnt = 0;
        public static long iRecordHoistLoadCnt = 0;
        public static long iRecordHoistUnloadCnt = 0;

        //~~~~~~~~~

        private static double x_rOdometerLastStop_InM = 0.0;
        private static double x_rOdometerCurrentMove_InM = 0.0;

        public static double rOdometerLastStop_InM
        {
            get
            {
                return x_rOdometerLastStop_InM;
            }

            set
            {
                x_rOdometerLastStop_InM = value;
            }
        }

        public static double rOdometerCurrentMove_InM
        {
            get
            {
                return x_rOdometerCurrentMove_InM;
            }

            set
            {
                x_rOdometerCurrentMove_InM = value;
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++             


        /// <summary>
        /// m_iNowSectID + m_fOffset
        /// </summary>
        public static System.Threading.ManualResetEvent[] awhSafeLock_MapLoc_SectionOffset;                     //Roy+180508

        public static System.Threading.ManualResetEvent[] awhSafeLock_DDS_betweenVehicleData;                     //Roy+180508
        public static System.Threading.ManualResetEvent[] awhSafeLock_StnData_EQ_CtrlTurnParam;                     //Roy+180524

        public static System.Threading.ManualResetEvent[] awhSafeLock_VehM_BlockPassQryNReply;                     //Roy+180621
        public static System.Threading.ManualResetEvent[] awhSafeLock_VehM_HidPassQryNReply;                     //Roy+180621
        public static System.Threading.ManualResetEvent[] awhSafeLock_AddressBarcodeJustVisited;                     //Roy+180727

        public static System.Threading.ManualResetEvent[] awhSafeLock01;                     //Roy+180508
        public static System.Threading.ManualResetEvent[] awhSafeLock02;                     //Roy+180508
        public static System.Threading.ManualResetEvent[] awhSafeLock03;                     //Roy+180508

        public static System.Threading.ManualResetEvent[] awhSafeLock__Deterioration;       //Add，For 劣化診斷使用        2018/07/10-----steven

        //public static bool bPingCompleted = true;                   // Roy-+180423
        //public static System.Threading.ManualResetEvent whPingCompleted = new System.Threading.ManualResetEvent(true);                   // Roy-+180423
        public static System.Threading.ManualResetEvent[] awhPingCompleted;                   // Roy+180423
        public static string sRefIpAddress = "0.0.0.0";                   // Roy+180423
        public static long lPingReplyTime_InMS = 999;                   // Roy+180423
        public static System.Net.NetworkInformation.IPStatus eIPStatus = System.Net.NetworkInformation.IPStatus.Success;                   // Roy+180423

        //+++++++++++++++++++++++++++++             // Roy+180904
        public static string sApMAC = "";
        public static double rApSignalQuality_InPercentage = 0.0;
        public static double rApRSSI_InDBm = 0.0;
        public static long lApChannel = 0;
        //+++++++++++++++++++++++++++++

        public static double rSettlingTime4RestoreFullSpeed_InMS = 999.9;                  // Roy+180131

        /// <summary>
        /// 100 <= Vel < 1100
        /// </summary>
        public static double rRatio4SlowerMove_WalkSpeedLow = 0.5;               // 0.25;                          // use 50% speed ...          // Roy+180109                   // Roy*180125

        /// <summary>
        /// 1100 <= Vel < 2050
        /// </summary>
        public static double rRatio4SlowerMove_WalkSpeedMid = 0.5;                   // Roy+180125

        /// <summary>
        /// 2050 <= Vel <= 3500
        /// 若用Vel.Cmd可用3500, 但若用Vel.Fbk考慮會值會波動, 需用多一點的3500 
        /// </summary>
        public static double rRatio4SlowerMove_WalkSpeedHigh = 0.28;                // 0.57;                     // Roy+180125              // Roy*180205

        /// <summary>
        /// Curve Only
        /// </summary>
        public static double rRatio4SlowerMove_WalkSpeedCurve = 0.3;                   // Roy+180129


        //+++++++++++++++++++++++++++++++             // Roy+180705
        public static string[] asBtVhcVehicle_ID;
        public static string[] asBtVhcCurrent_Section_ID;
        public static int[] aiBtVhcCurrent_Section_Offset;
        public static DDS.InstanceStateKind[] aeBtVhcInstanceState;             // Roy+180918
        public static DateTime[] adtBtVhcReceptionTimestamp;

        public static double rHbLifeTime = 6.0;
        //+++++++++++++++++++++++++++++++        

        //+++++++++++++++++++++++++++++++++++++++++             // Roy+180918
        public static string[] asIntVehBlkZnCtrl_BlockZoneID;
        public static string[] asIntVehBlkZnCtrl_HasLockedBy_VehID;
        public static string[] asIntVehBlkZnCtrl_HasAcquiredBy_VehID;
        public static DDS.InstanceStateKind[] aeIntVehBlkZnCtrl_InstanceState;
        public static string[] asIntVehBlkZnCtrl_Locking_Timestamp;
        //+++++++++++++++++++++++++++++++++++++++++        

        public static bool bIgnore障礙物偵測處理4Walk = false;                    // Roy+180109
        public static bool bIgnore障礙物偵測處理4Port = false;                    // Roy+180109

        public static bool bIsMsgBoxPopup = false;                  // Roy+180212

        public static bool bIgnoreHangingStatus = false;                  // Roy+180109
        public static bool bIsJedecMode = false;               // Roy+171114

        public static string sMsgKept01 = "";                    // Roy+171023
        public static string sMsgKept02 = "";                    // Roy+171023
        public static string sMsgKept03 = "";                    // Roy+171023
        public static string sMsgKept04 = "";                    // Roy+171023
        public static string sMsgKept05 = "";                    // Roy+171023

        public static int iIdReaderNgContCnt = 0;                    // Roy+171026


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++             // Roy+180524
        /// <summary>
        /// 1a. 任一項目 剛超上限(異常)時, 列印 Warn 輸出, 並 啟動聲音警報;   
        /// 1b. 若所有項目(or) 異常持續5sec未解除, 列印 Error 輸出, 並結束聲音警報+觸發 緩停+打斷流程! 
        /// 2a. 任一項目 剛落上限(正常)時, 列印 Warn 輸出;
        /// 2b. 若任一項目 剛落上限(正常)時, 所有項目(and) 皆正常時, 結束聲音警報。
        /// </summary>
        public static double rHwGuardBufferTime_InMS = 5000.0;                  // Roy+180606
        public static System.Diagnostics.Stopwatch swHwGuardBufferTimeCheck = new System.Diagnostics.Stopwatch();                  // Roy+180606

        //++++++++++++++++++++++++             // Roy+180606
        public static double fPowerCurrentPrev_inA = 0.0;
        public static double fPowerVoltagePrev_inV = 0.0;
        public static double fPowerPrev_inKW = 0.0;
        public static double fWorkPrev_inKWH = 0.0;

        public static double fThermoValuePrev_超級電容_indC = 0.0;

        public static double fThermoValuePrev_走行驅動器_indC = 0.0;
        public static double fThermoValuePrev_走行馬達_indC = 0.0;
        public static double fThermoValuePrev_走行回升電阻_indC = 0.0;
        //++++++++++++++++++++++++

        public static double fPowerCurrentMax_inA = 5.0;                // 3.5;                //3.0;                // 2.0;                // Roy*180705                  // Roy*180727
        public static double fPowerVoltageMax_inV = 330.0;              // 310.0;                  // Roy*180727
        public static double fPowerMax_inKW = 1.6;              // 1.0;              // 0.7;                      // 0.5;                // Roy*180705                  // Roy*180727
        //public static double fWorkMax_inKWH = xxx;

        public static double fThermoValueMax_超級電容_indC = 47.0;

        public static double fThermoValueMax_走行驅動器_indC = 59.0;            // 45.0;                // Roy*180705
        public static double fThermoValueMax_走行馬達_indC = 78.0;              // 39.0;                // Roy*180705
        public static double fThermoValueMax_走行回升電阻_indC = 59.0;            // 38.0;                // Roy*180705
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++


        public static bool bBypassAlert_載台偏擺檢知 = false;                    // Roy+171023
        public static bool bBypassAlert_皮帶張力檢知 = false;                    // Roy+171023

        public static int iAlertCnt_載台偏擺檢知 = 0;                    // Roy+171023
        public static int iAlertCnt_皮帶張力檢知 = 0;                    // Roy+171023

        public static bool bRequestToShowCleanWholeMapGraphics = false;                      // Roy+170817
        public static bool bDoneToShowCleanWholeMapGraphics = false;                      // Roy+170817
        public static bool bRequestToUpdateLiveRoutingMapInfo = false;                      // Roy+170817
        public static bool bDoneToUpdateLiveRoutingMapInfo = false;                      // Roy+170817

        public static bool bRequestToUpdateLiveRoutingGraphics = false;                      // Roy+180105
        public static bool bDoneToUpdateLiveRoutingGraphics = false;                      // Roy+180105

        public static int iVehM_OfflineHoistProcessCycleIdx = -1;                      // Roy+170821

        public static bool bVehM_OfflineButEnableCycleSections = false;                      // Roy+170815
        public static bool bVehM_OfflineButInclHoistProcess = false;                      // Roy+170815
        public static bool bVehM_OfflineButActionStartRequest = false;                      // Roy+170815
        public static bool bVehM_OfflineButActionProcessing = false;                      // Roy+170815

        public static bool bVehM_OfflineBlockCtrlPass = true;                      // Roy+170915

        public static bool b抵消到位停止時之打滑現象 = false;                                       //true;                                                     // Roy+171114
        public static bool b依目前與終站條碼距離差值動態修正移動量 = true;                              //false;                        // Roy+171114
        public static int i依目前與終站條碼距離差值動態修正移動量Idx = 0;                       // Roy+171128

        //+++++++++++++++++++++++++++++++++++             // Roy+180516
        public static bool bHID01_OutOfService = false;
        public static bool bHID01_EQ_Online = false;
        public static bool bHID01_EQ_Error = false;
        //+++++++++++++++++++++++++++++++++++

        //+++++++++++++++++++++             // Roy+171204
        public static string DdsBcSection = "";
        public static string DdsLockBy = "";
        public static string DdsAcqBy = "";
        //+++++++++++++++++++++


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                  // Roy+180606
        private static bool x_bUnderFreezingMove = false;

        public static bool bUnderFreezingMove
        {
            get
            {
                x_bUnderFreezingMove = ((bEnforcedToNowStop_All || bRequestToSlowStop_All)
                                                                || (bBlockCtrlZonePausing_Walk4AcqTokenLater || bEnforcedToNowStop_Walk4ResumeLater || bRequestToSlowStop_Walk4ResumeLater)
                                                                || (bVehM_ActionInternalPauseSinceBcNotOkRequest || bVehM_ActionInternalPauseSinceBcNotOkAlready)
                                                                || (bVehM_ActionExternalPauseRequest || bVehM_ActionExternalPauseAlready)
                                                                || (bVehM_ActionStop4MoveCycleRequest || bVehM_ActionStop4MoveCycleAlready)
                                                                || (bVehM_ActionInternalHaltSinceExternalCancelOrAbortRequest || bVehM_ActionInternalHaltSinceExternalCancelOrAbortAlready)
                                                                || (bVehM_ActionInternalPauseSinceHcNotOkRequest || bVehM_ActionInternalPauseSinceHcNotOkAlready)
                                                                || (bVehM_ActionCancelB4LoadingRequest || bVehM_ActionCancelB4LoadingAlready)
                                                                || (bVehM_ActionAbortB4UnloadingRequest || bVehM_ActionAbortB4UnloadingAlready));

                return x_bUnderFreezingMove;
            }

            //set
            //{
            //    x_bUnderFreezingMove = value;
            //}
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        //++++++++++++++++++++++++++++++             // Roy+170804
        public static bool bVehM_OnlineRequest = false;
        public static bool bVehM_OnlineAlready = false;

        //public static bool bVehM_ActionInternalPauseSinceBcNotOkRequest = false;                     // 'Pause' can be 'Continue' later ...                  // Roy-180705

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                  // Roy+180705
        private static bool x_bVehM_ActionInternalPauseSinceBcNotOkRequest = false;

        public static bool bVehM_ActionInternalPauseSinceBcNotOkRequest
        {
            get
            {
                return x_bVehM_ActionInternalPauseSinceBcNotOkRequest;
            }

            set
            {
                x_bVehM_ActionInternalPauseSinceBcNotOkRequest = value;

                if (value)
                {
                    iIntByBlockCtrlSectionTimes++;
                    oHybridStopStatusForEvent.bStopStatus_BlockCtrl_Section = true;
                }
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static bool bVehM_ActionInternalPauseSinceBcNotOkAlready = false;                    // useless so far ...

        //+++++++++++++++++++++             // Roy+180302
        public static bool bVehM_ActionExternalPauseRequest = false;                     // 'Pause' can be 'Continue' later ...           
        //public static bool bVehM_ActionExternalPauseAlready = false;                  // Roy-180705

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                  // Roy+180705
        private static bool x_bVehM_ActionExternalPauseAlready = false;

        public static bool bVehM_ActionExternalPauseAlready
        {
            get
            {
                return x_bVehM_ActionExternalPauseAlready;
            }

            set
            {
                x_bVehM_ActionExternalPauseAlready = value;

                if (value)
                {
                    iIntByExtPauseTimes++;
                    oHybridStopStatusForEvent.bStopStatus_External_Pause = true;
                }
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /// <summary>
        /// a.k.a. Restart 
        /// </summary>
        public static bool bVehM_ActionContinue4ExternalPauseRequest = false;                     // 'Continue' uses for 'Pause' exclusively ...
                                                                                                  //public static bool bVehM_ActionContinue4ExternalPauseAlready = false;                 // Roy-180705

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                  // Roy+180705
        private static bool x_bVehM_ActionContinue4ExternalPauseAlready = false;

        public static bool bVehM_ActionContinue4ExternalPauseAlready
        {
            get
            {
                return x_bVehM_ActionContinue4ExternalPauseAlready;
            }

            set
            {
                x_bVehM_ActionContinue4ExternalPauseAlready = value;

                if (value)
                {
                    oHybridStopStatusForEvent.bStopStatus_Manual_Pause = false;
                    oHybridStopStatusForEvent.bStopStatus_External_Pause = false;
                }
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //+++++++++++++++++++++           

        public static bool bVehM_ActionStop4MoveCycleRequest = false;                     // 'Stop' equals to 'Cancel', cannot  be 'Continue' exactly ...
        //public static bool bVehM_ActionStop4MoveCycleAlready = false;                  // Roy-180705

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                  // Roy+180705
        private static bool x_bVehM_ActionStop4MoveCycleAlready = false;

        public static bool bVehM_ActionStop4MoveCycleAlready
        {
            get
            {
                return x_bVehM_ActionStop4MoveCycleAlready;
            }

            set
            {
                x_bVehM_ActionStop4MoveCycleAlready = value;

                if (value)
                {
                    //oHybridStopStatusForEvent.StopStatus_External_CancelAbort = true;
                }
            }
        }

        //~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~

        private static bool x_bCurveInletSnrRightTrig = false;

        public static bool bCurveInletSnrRightTrig
        {
            get
            {
                return x_bCurveInletSnrRightTrig;
            }

            set
            {
                if (x_bCurveInletSnrRightTrig != value)
                {
                    if (value)
                    {
                        // Rising-Edge Trigger

                    }
                    else
                    {
                        // Falling-Edge Trigger

                    }
                }

                x_bCurveInletSnrRightTrig = value;
            }
        }

        //~~~~~~~~~~

        private static bool x_bCurveInletSnrLeftTrig = false;

        public static bool bCurveInletSnrLeftTrig
        {
            get
            {
                return x_bCurveInletSnrLeftTrig;
            }

            set
            {
                if (x_bCurveInletSnrLeftTrig != value)
                {
                    if (value)
                    {
                        // Rising-Edge Trigger

                    }
                    else
                    {
                        // Falling-Edge Trigger

                    }
                }

                x_bCurveInletSnrLeftTrig = value;
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        //+++++++++++++++++++++++++++++++++++++                 // Roy+180809
        private static bool x_b左下軌道超速檢知Trig = false;

        public static bool b左下軌道超速檢知Trig
        {
            get
            {
                return x_b左下軌道超速檢知Trig;
            }

            set
            {
                if (x_b左下軌道超速檢知Trig != value)
                {
                    if (value)
                    {
                        // Rising-Edge Trigger

                    }
                    else
                    {
                        // Falling-Edge Trigger

                    }
                }

                x_b左下軌道超速檢知Trig = value;
            }
        }

        //~~~~~~~~~~

        private static bool x_b右下軌道超速檢知Trig = false;

        public static bool b右下軌道超速檢知Trig
        {
            get
            {
                return x_b右下軌道超速檢知Trig;
            }

            set
            {
                if (x_b右下軌道超速檢知Trig != value)
                {
                    if (value)
                    {
                        // Rising-Edge Trigger

                    }
                    else
                    {
                        // Falling-Edge Trigger

                    }
                }

                x_b右下軌道超速檢知Trig = value;
            }
        }
        //+++++++++++++++++++++++++++++++++++++


        /// <summary>
        /// 需考慮 若因VehM要求Pause, 後再Continue, 會不會使 (將)詢問 之 BcSection 直接不等回覆, 當成錯誤的 放行 ... ?!
        /// </summary>
        public static bool bVehM_ActionContinue4BcNotOkRequest = false;                     // 'Continue' uses for 'Pause' exclusively ...
        public static bool bVehM_ActionContinue4BcNotOkAlready = false;

        public static bool bVehM_ActionMoveSingleRequest = false;
        public static bool bVehM_ActionMoveSingleAlready = false;
        public static bool bVehM_ActionMoveCycleRequest = false;
        public static bool bVehM_ActionMoveCycleProcessing = false;

        //++++++++++++++++++++++++++++++             // Roy+180319
        public static bool bVehM_ActionInternalHaltSinceExternalCancelOrAbortRequest = false;
        //public static bool bVehM_ActionInternalHaltSinceExternalCancelOrAbortAlready = false;                   // ... iIntByExtCancelAbortTimes                  // Roy-180705

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                  // Roy+180705
        private static bool x_bVehM_ActionInternalHaltSinceExternalCancelOrAbortAlready = false;

        public static bool bVehM_ActionInternalHaltSinceExternalCancelOrAbortAlready
        {
            get
            {
                return x_bVehM_ActionInternalHaltSinceExternalCancelOrAbortAlready;
            }

            set
            {
                x_bVehM_ActionInternalHaltSinceExternalCancelOrAbortAlready = value;

                if (value)
                {
                    iIntByExtCancelAbortTimes++;
                    oHybridStopStatusForEvent.bStopStatus_External_CancelAbort = true;
                }
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //public static bool bVehM_ActionInternalPauseSinceHcNotOkRequest = false;                     // 'Pause' can be 'Continue' later ...                  // Roy-180705

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                  // Roy+180705
        private static bool x_bVehM_ActionInternalPauseSinceHcNotOkRequest = false;

        public static bool bVehM_ActionInternalPauseSinceHcNotOkRequest
        {
            get
            {
                return x_bVehM_ActionInternalPauseSinceHcNotOkRequest;
            }

            set
            {
                x_bVehM_ActionInternalPauseSinceHcNotOkRequest = value;

                if (value)
                {
                    iIntByHidCtrlSectionTimes++;
                    oHybridStopStatusForEvent.bStopStatus_HIDCtrl_Section = true;
                }
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public static bool bVehM_ActionInternalPauseSinceHcNotOkAlready = false;                    // useless so far ...

        /// <summary>
        /// 需考慮 若因VehM要求Pause, 後再Continue, 會不會使 (將)詢問 之 HcSection 直接不等回覆, 當成錯誤的 放行 ... ?!
        /// </summary>
        public static bool bVehM_ActionContinue4HcNotOkRequest = false;                     // 'Continue' uses for 'Pause' exclusively ...
        public static bool bVehM_ActionContinue4HcNotOkAlready = false;

        /// <summary>
        /// (w/o Tray inside OHT)
        /// </summary>
        public static bool bVehM_ActionCancelB4LoadingRequest = false;
        public static bool bVehM_ActionCancelB4LoadingAlready = false;

        /// <summary>
        /// (w/ Tray(s) inside OHT)
        /// </summary>
        public static bool bVehM_ActionAbortB4UnloadingRequest = false;
        public static bool bVehM_ActionAbortB4UnloadingAlready = false;
        //++++++++++++++++++++++++++++++       

        public static bool bVehM_ActionLoadRequest = false;
        public static bool bVehM_ActionLoadAlready = false;
        public static bool bVehM_ActionUnloadRequest = false;
        public static bool bVehM_ActionUnloadAlready = false;
        //++++++++++++++++++++++++++++++

        //++++++++++++++++              // Roy+180612
        public static bool b在XX秒後自動關機Request = false;
        public static bool b在XX秒後自動關機Already = false;
        public static bool b立刻結束程式Request = false;
        public static bool b立刻結束程式Already = false;
        //++++++++++++++++   

        //public static Def_Common.tyPIO PIO_Ctrl = new Def_Common.tyPIO();
        //public static Def_EQ.enOhtStatus OhtStatus = Def_EQ.enOhtStatus.OHT_UnknownStatus;               // Roy+170707

        //public static Def_Common.tyParams Param;                                                  //參數層定義
        //public static Def_Common.tyParamsFun ParamFun;                                            //參數功能層定義
        //public static Def_Common.tyCtl Ctl;                                                       //控制層定義
        //public static Def_Common.tyDrv Drv;                                                       //驅動層定義
        //public static Def_PLC.tyPLCData PLC_DATA;                                                 //放置讀取PL之C資料
        //public static Dictionary<Def_EQ.enStnNo, Stn> StnQ;                                         //放置站控制操作物件
        //public static Def_Common.tyStn Stn;                                                       //放置站控制操作物件
        ////public static Dictionary<Def_EQ.enStnNo, Def_Common.tyStnStsAreaData> StnSts;             //放置站狀態區之資料[PLC]
        //public static Def_Common.tyEQData StnData;                                                //放置站控制操作物件
        //public static Def_EQ.enEQSts Sts = Def_EQ.enEQSts.NONE;                                   //機台狀態
        //public static Def_Motion.tyMotionStatus MotionStatus = new Def_Motion.tyMotionStatus();

        ////
        //public static Def_BarCode.tyLabelProperties BarCode_Data = new Def_BarCode.tyLabelProperties();
        //public static Def_BarCode.tyRouteData RouteData = new Def_BarCode.tyRouteData();
        ////
        ////public static Def_Common.tyQueue Queue;                                                   //放置Command Queue資料
        //public static frm_Main MainForm = new frm_Main();                                         //主表單物件
        //public static bool WarningON = false;
        //public static bool AlarmON = false;
        //public static Sys_Define.enPasswordType 登入權限 = Sys_Define.enPasswordType.尚未登入;
        //public static Sys_Define.enLanguageType 操作語言 = Sys_Define.enLanguageType.中文;
        //public static Dictionary<Def_CCD.enCCD, Cognex.VisionPro.Display.CogDisplay> ImageDisplay;
        //public static Ctl_LKIF2 Laser = new Ctl_LKIF2();
        ////安全檢查20130705
        ////public static Ctl_SafeCheck SafeCheck = new Ctl_SafeCheck();

        //public static Ctl_Message MSG = new Ctl_Message();

        //public static bool 全域異常 = false;

        //public static Def_Common.tyForm 表單 = new Def_Common.tyForm();

        //public static bool ALIGNMENT_TEST_MODE = true;

        //public static Def_MultiMeter.tyPowerData PowerData = new Def_MultiMeter.tyPowerData();                //Ted+180222 智慧電表資料結構


        #region 常數定義
        public const string cnt_DATA_PATH = @"D:\MirleOHTData\";
        public const string PROC_LOG_PATH = @"D:\PROC_300OHTM\PROC_LOG";     //機台資料放置位置

        //+++++++++++++++                 // Roy+171114
        public const string PROC_LOG01_PATH = @"D:\PROC_300OHTM\PROC_LOG01";     //機台資料放置位置
        public const string PROC_LOG02_PATH = @"D:\PROC_300OHTM\PROC_LOG02";     //機台資料放置位置
        public const string PROC_LOG03_PATH = @"D:\PROC_300OHTM\PROC_LOG03";     //機台資料放置位置
        public const string PROC_LOG04_PATH = @"D:\PROC_300OHTM\PROC_LOG04";     //機台資料放置位置
        public const string PROC_LOG05_PATH = @"D:\PROC_300OHTM\PROC_LOG05";     //機台資料放置位置
        //+++++++++++++++

        public const string PROC_CCD_PATH = @"D:\PROC_300OHTM\PROC_CCD";     //機台資料放置位置
        public const string PROC_權限_PATH = @"D:\PROC_300OHTM\PROC_權限";     //機台資料放置位置
        public const string PATTERN_PATH = @"D:\PATTERN\";     //機台資料放置位置
        public const string cnt_DB_PATH = @"D:\MirleOHTData\MirleOHT.mdb";                        //資料庫位置
        public const string cnt_ERR_LOG_DB_PATH = @"D:\MirleOHTData\ErrLog.mdb";                        //資料庫位置
        public const string cnt_LASER_PATTERN_PATH = @"D:\MirleOHTData\LaserPattern";
        public const string cnt_SUBTACTTIME_LOG_PATH = @"D:\MirleOHTData\SubTactTime";
        public const int cnt_MAX_STN = 2;                                                         //站總數
        public const int cnt_MAX_MOTION = 16;                                                      //馬達總軸數
        public const int cnt_MAX_CCD = 1;                                                         //CCD總數



        public const int cnt_WORD_ADDR_COUNT = 1120;                                              //PLC WORD讀取總數
        public const int cnt_LOGWORD_ADDR_COUNT = 32;                                              //PLC log WORD讀取總數
        public const int cnt_LOGBIT_ADDR_COUNT = 32;                                                 //PLC log Bit讀取總數(2*16)
        public const int cnt_MAX_QUEUE = 20;                                                      //最大使用Queue數
        public const int cnt_MAX_COMMAND_LEN = 100;                                               //每一命令最大長度(Word)
        public const int cnt_LogicalStationNumber = 1;

        public static Def_Common.tyTacTimeLog tl = new Def_Common.tyTacTimeLog();
        public static Def_Common.tyTacTimeLog tq = new Def_Common.tyTacTimeLog();

        #endregion
    }
}
