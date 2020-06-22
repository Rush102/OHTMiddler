using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace OHTM.NLog_USE
{
    public class Def_Common  //依專案定義雜七雜八的系統變數
    {
        //public struct tyStn             // 應 可連結對應至 Def_EQ.enStnNo 的 enum 
        //{
        //    public Stn TOS;

        //    //+++++++++++++             // Roy+170603
        //    public Stn 障礙物;
        //    public Stn PIO;
        //    //public Stn 台車;                // Roy-170707
        //    public Stn 軌道;
        //    public Stn 捲揚;

        //    public Stn 轉向;
        //    public Stn 過彎;
        //    public Stn 走行變位;
        //    public Stn 走行調速;
        //    public Stn 導航;
        //    //+++++++++++++           

        //    /*              // Roy-170603
        //    public Stn EQ;
        //    public Stn 台車;
        //    public Stn CST2;
        //    public Stn ROBOT;
        //    public Stn TURN;
        //    public Stn BUFFER_上;
        //    public Stn BUFFER_下;
        //    */
        //}

        //public struct tyParamsFun
        //{
        //    public Param_CCD CCD;
        //    public Param_Recipe RECIPE;
        //    public Param_SystemRecipe SYSTEM;
        //    public Param_IO IO;
        //    public Param_PLC PLC;
        //    public Param_Motion MOTION;
        //    public Param_Err ERR;
        //}

        public struct tyTacTimeLog
        {
            public List<string> item;
            public List<DateTime> Start;
            public List<DateTime> End;
            public string SubTitle;
            public DateTime SubStart;
            public DateTime SubEnd;
        }

        //public struct tyParams
        //{
        //    public List<Def_CCD.tyParams_CCD> CCD;

        //    public Def_IO.tyParams_IO IO;

        //    public List<Def_Motion.tyParams_Motion> MOTION;                     // ...... why List<> ?
        //    public Dictionary<Def_Motion.enMotion, Def_Motion.tyParams_Motion> MotionENUM;                     // Roy+171003 .... 精簡程式碼

        //    public Def_PLC.tyParams_PlcAddr PLC;
        //    //public Dictionary<Def_Command.enCMDList, Def_Command.tyCmdDefine> CMD;
        //    public Def_PLC.tyAddrDefine ADDR;
        //    public Def_Recipe.tyParams_Recipe RECIPE;
        //    public Def_SystemRecipe.tyParams_SystemRecipe SYSTEM;
        //    public Def_ErrData.tyErrParam ERR;
        //}

        //public struct tyCtl
        //{
        //    public Ctl_CCD CCD;
        //    public Ctl_IO IO;
        //    public Ctl_PLC PLC;
        //    public Ctl_Motion MOTION;
        //    public Dictionary<Def_Motion.enMotion, Def_Motion.tyMotionStatus> AXIS_STS;
        //    public Def_ErrData.tyErr ERR_DATA;
        //    public Ctl_ErrHandle ERR;
        //    //public Dictionary<Def_EQ.enStnNo, List<string>> ERR_CODE;
        //    public Ctl_SignalTower SIGNAL_TOWER;
        //    public Ctl_LKIF2 LKIF2;
        //    //
        //    //public Ctl_IdReader BL1301;                  // Roy-170817
        //    //
        //    //public Ctl_CCDLight CCDLight;                 // Roy-170501

        //    //public Ctl_Robot Robot;
        //    //public OHTTMap Map;

        //    public OHTMap map;

        //    public Ctl_VehM VehM;                   // Roy+170804

        //    public Ctl_MultiMeter MultiMeter;       //Ted+180222
        //}

        //public struct tyDrv
        //{
        //    public Dictionary<Def_CCD.enCCD, Drv_CCD_GigE> CCD;
        //    //public Drv_PLC PLC;               // Roy-170728
        //}

        public struct tyPIO
        {
            public bool bIsByPassPIO;               //是否Pass PIO
            public int iHandShakeHD_Type;           //PIO通訊應用硬體方式       0: RFID     1:DDS
            public string sPIO_ID;                  //PIO 調頻ID 
            public int iPIO_Channnel;               //PIO 調頻Channel
            public double TD1;
            public double TA1;
            public double TA2;
            public double TA3;
            public double TA4;
            public double TP1;
            public double TP2;
            public double TP3;
            public double TP4;
            public double TP5;
            public double TP6;
        }

        #region 表單物件
        //public struct tyForm
        //{
        //    public frm_Login f_Login;
        //    public frm_JogPitch f_Jog;
        //    public frm_IOMonitor f_IOMonitor;
        //    public frm_Recipe f_Recipe;
        //    public frm_SystemParams f_Systemparams;
        //    //public frm_TrainPattern f_TrainPattern;                  // Roy-170501 ... 沒用到, 先暴力遮掉 ...!
        //    public frm_ErrList f_ErrList;
        //    public frm_Auto f_Auto;

        //    //public frm_PLC測試 f_PLC測試;

        //    //public frm_角度測試 f_角度測試;                  // Roy-170501 ... 沒用到, 先暴力遮掉 ...!
        //    //public frm_精度量測 f_精度量測;                  // Roy-170501 ... 沒用到, 先暴力遮掉 ...!

        //    //public frm_Notch找尋 f_Notch找尋;
        //    //public frm_Report f_Report;

        //    //public frm_SemiAuto f_SemiAuto;               // Roy-170603

        //    //public frm_DisplayPlcData f_DisplayPlcData;          
        //    //public frm_LaserValueDisplay f_LaserValueDisplay;
        //    //public frm_ManualAlignment f_ManualAlignment;

        //    public frm_ErrLogQuery f_ErrLogQuery;

        //    //public frm_上片投入站 f_上片投入站;                  // Roy-170501 ... 沒用到, 先暴力遮掉 ...!
        //    //public frm_左右轉工作點 f_ROBOT站;                  // Roy-170501 ... 沒用到, 先暴力遮掉 ...!

        //    //public frm_腔體 f_腔體;
        //    //public frm_CCD工作點 f_CCD工作點;
        //    //public frm_IO手動 f_IO手動;
        //    //public frm_CCD f_CCD;
        //    //public frm_CCDControls f_CCDControls;
        //    //public frm_OCRTrain f_OCRTrain;

        //    //public DDS_Comm f_DDS_Comm;                   // Roy-180222

        //}
        
        #endregion

        #region 機台變數
        /*                  // Roy-170603
        public enum enStnStation
        {
            Chassis=0,
            CST1 ,
            CST2,
            Aligner,
            Buffer_上,
            Buffer_下,
            Buffer_上_1,
            Buffer_下_1,
            Equipment,
            Equipment_1,
        }
        */

        public struct DPoint_XY
        {
            public double x;
            public double y;
        }

        public struct tyEQData
        {
            public tyEQData_EQ EQ;
            public tyEQData_Chasis Chassis;

            //public tyEQData_CST1 CST1;
            //public tyEQData_CST2 CST2;
            //public tyEQData_Turn Turn;
            //public tyEQData_ROBOT ROBOT;
            //public tyEQData_Buffer_上 Buffer_上;
            //public tyEQData_Buffer_下 Buffer_下;

            public tyEQData_CCD CCD;
            public tyEQData_TOS TOS;
        }

        /*                  // Roy-170603
        public enum enStnHS
        {
            NONE = 0,
            Unload_REQ,
            Load_REQ,
            Unload_CMP,
            Load_CMP,
            動作中,
            動作完成,
        }
        */

        public enum enTackTimeItem                           // ...... ???
        {
            上片載入,
            下片載入,
            面膠,
            UV_雷射,
            ROBOT取上片,
            ROBOT取下片,
            腔體放片,
            CCD對位,
            貼合,
            成品載出,
            抽真空,
            破真空,
        }

        public struct tyEQData_EQ
        {
            /*                  // Roy-170603
            public int Wafer_ID;
            public bool 量測動作中;
            public enStnHS HS;
            */

            //+++++++++++++++++++++             // Roy+170603
            //public string[,] aryResult;

            //public OHTMAP.SectionType eSectTypeKept;                    // Roy+180212

            //+++++++++++++++++++++++++++++++++++             // Roy+180809
            public double rSmoothAccRatio;
            public double rSmoothDecRatio;

            public double rSmoothAccRatio_R;
            public double rSmoothDecRatio_R;
            public double rSmoothAccRatio_U;
            public double rSmoothDecRatio_U;
            public double rSmoothAccRatio_S_in;
            public double rSmoothDecRatio_S_in;
            public double rSmoothAccRatio_S_out;
            public double rSmoothDecRatio_S_out;
            //+++++++++++++++++++++++++++++++++++

            public double[,] arCtrlTurnParams;                    // Roy+171114
            public double[,] arCtrlTurnParams_R;
            public double[,] arCtrlTurnParams_U;                    // Roy+171114
            public double[,] arCtrlTurnParams_S_in;                    // Roy+171114
            public double[,] arCtrlTurnParams_S_out;                    // Roy+171225

            /*              //Roy-180606
            //public int iColumn;                    // Roy+171114
            //public int iColumn_R;
            //public int iColumn_U;                    // Roy+171114
            //public int iColumn_S_in;                    // Roy+171114
            //public int iColumn_S_out;                    // Roy+171225
            */
            //+++++++++++++++++++++
        }

        /*                  // Roy-170603
        public struct tyEQData_CST1
        {
            public enStnHS HS;
            public int Wafer_ID;
            public string total_detective;
            public int total_量測片數;

            public int Now_Wafer_Index;
            public bool Mapping中;
            public bool Mapping完成;

            public bool Wafer_Set_Ok;

            public string[] Mapping_No;
            public string[] Mapping_Data;

            public string have_wafer;
            public string no_wafer;
            public string error_wafer;
            public string wafer_depth_error;

            public int Wafer_已放片數;

            public bool Wafer已全部取完;
            public bool only_one;

            public bool Robot_Get_CST1;

            public bool CST1_等待人員按開門鈕;
            public bool CST1_動作中;
            public bool CST1_晶圓抽檢確認_繼續;
            public bool CST1_執行晶圓抽檢確認流程;
            //public bool CST1_人員拿走後有放新CST;
        }

        public struct tyEQData_CST2
        {
            public enStnHS HS;
            public int Wafer_ID;
            public string total_detective;
            public int total_量測片數;

            public int Now_Wafer_Index;
            public bool Mapping中;
            public bool Mapping完成;

            public bool Wafer_Set_Ok;

            public string[] Mapping_No;
            public string[] Mapping_Data;

            public string have_wafer;
            public string no_wafer;
            public string error_wafer;
            public string wafer_depth_error;

            public int Wafer_已放片數;

            public bool Wafer已全部取完;

            public bool only_one;

            public bool Robot_Get_CST2;

            public bool CST2_等待人員按開門鈕;
            public bool CST2_動作中;
            public bool CST2_晶圓抽檢確認_繼續;
            public bool CST2_執行晶圓抽檢確認流程;
        }

        public struct tyEQData_Turn
        {
            public enStnHS HS;
            public int Wafer_ID;
            //public double pos;

            public bool CCD_尋邊找Notch_異常;
            public bool CCD_OCR讀取_異常;


            public int retry_times;
            public int Pic_No;
            public double Offset;
            public int tt;
            public bool trigger_on;

            public bool CCD_對位中;
            public bool CCD_補償中;
            public bool CCD_OCR讀取中;
            public bool CCD_尋邊找Notch中;
            public bool CCD_Save_Image;

            public int step_index;

            public double offset_angle;
            public double offset_x;
            public bool semi_test;
            public bool OCR_轉180;

            public string OCR_Result;
        }

        public struct tyEQData_ROBOT
        {
            public enStnHS HS;

            public bool Act_自CST取片至Turn放片;
            public bool Act_自Turn取片至Buffer_上放片;

            public bool Act_自Turn取片至CST放片;

            public bool Act_自Buffer_上取片至EQ放片;

            public bool Act_自EQ取片至Buffer_下放片;
            public bool Act_自Buffer_下取片至CST放片;

            public bool Act_自CST取片至Buffer_上放片;

            public int Wafer_ID;

            public string tmp_jel;

            public bool Act_自Buffer_上取片至Buffer_下放片;

            public bool Arm_Have_Wafer;

            public bool Act_Buffer_上_取片失敗;
            public bool Act_Buffer_下_取片失敗;
            public bool Act_Aligner_取片失敗;
            public bool Act_EQ_取片失敗;
            public int Int_CST_取片失敗;

            public bool Robot_Pause_req;
            public bool Robot_Pause_cmp;
            public bool Robot_Pause_ok;
        }

        public struct tyEQData_Buffer_上
        {
            public enStnHS HS;
            public int Wafer_ID_上;
            //public int Wafer_ID_下;
            public bool Buffer_上;
            //public bool Buffer_下;

            public bool Buffer_上_等待Robot放片;
            public bool Buffer_上_吸真空;
            public bool Buffer_上_等待Robot取片;
        }

        public struct tyEQData_Buffer_下
        {
            public enStnHS HS;
            //public int Wafer_ID_上;
            public int Wafer_ID_下;
            //public bool Buffer_上;
            public bool Buffer_下;

            public bool Buffer_下_等待Robot放片;
            public bool Buffer_下_吸真空;
            public bool Buffer_下_等待Robot取片;

        }
        */


        public struct tyEQData_TOS                  // ......  tyEQData_Chasis ......  重覆定義/使用 ??? 
        {
            //public enStnHS HS;                // Roy-170603

            public bool Monitor_Warning;
            public bool all_home;
            public bool Auto_Run;

            public bool Safe_Door;

            public bool Interlock;

            public bool Auto_Continue;

            /*                  // Roy-170603
            public string Semi_Mapping_No;
            public string Semi_Mapping_Data;

            public string Error_slot;

            public bool slot_lock;

            public bool all_wafer_cmp;
            */

            public bool tmp_1;
            public bool tmp_2;
            public bool tmp_3;
            public bool tmp_4;
            public bool tmp_5;
            public bool tmp_6;
            public bool tmp_7;
            public bool tmp_8;
            public bool tmp_9;
            public bool tmp_10;
            public bool tmp_11;
            public bool tmp_12;
            public bool tmp_13;
            public bool tmp_14;
            public bool tmp_15;
            public bool tmp_16;
            public bool tmp_17;
            public bool tmp_18;
            public bool tmp_19;
            public bool tmp_20;
            public bool tmp_21;
            public bool tmp_22;
            public bool tmp_23;
            public bool tmp_24;
            public bool tmp_25;
            //
            public string section_name;
            public int length;

            public string currentLabel;
            public string currentBarCodeValue;

            //public bool Monitor_Warning;

        }


        public struct tyEQData_CCD
        {
            public bool MANUAL_ALIGNMENT_ON;
            public bool MANUAL_ALIGNMENT_OK;
            public bool MANUAL_ALIGNMENT_CANCEL;
            public double MANUAL_ALIGNMENT_X;
            public double MANUAL_ALIGNMENT_Y;

            public int PatternType;
        }


        public struct tyEQData_Chasis                  // ......  tyEQData_TOS ......  重覆定義/使用 ??? 
        {
            public string CurrentZoneName;                       // Roy+180430
            public string CurrentSectionName;                       // Roy*170718
            public int OffsetFromCurrentSectionHead;                       // Roy*170718

            public int OffsetFromCurrentSectionHead4Map;                // Roy+170718

            //public string BarCode;                        // Roy-170815
            //public int step_index;                        // Roy-170815

            //public bool auto_test;                      // Roy-170815 ... useless so far ... !
        }



        #endregion
    }
}
