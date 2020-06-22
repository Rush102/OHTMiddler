using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using MirleOHT.類別.定義;
//using MirleOHT.系統;
//using Cognex.VisionPro;
//using Cognex.VisionPro.Display;
using System.IO;
using System.Threading.Tasks;                       // Roy+170721
using Newtonsoft.Json.Linq;             // Roy+180508
using NLog;             // Roy+180508
using System.Threading;                       // Roy+180606
using System.IO.Ports;

namespace OHTM.NLog_USE
{
    public static class eqTool
    {
        private static System.Object oLock = new System.Object();

        private static SerialPort Rs232Port;
        private static string ReciveData;

        //++++++++++++++++++                       // Roy+180508
        private static string msSite = "后里6F-LCD";
        private static string msToolID = "";
        //++++++++++++++++++


        //+++++++++++++++++++++++++++++++++++++++++                       // Roy+180621                 
        //public static bool gbEnableExtraRawFileSaving { get; set; } = true;                     // in Visual Studio 2015 and c# 6 
        private static bool x_gbEnableExtraRawFileSaving = true;

        public static bool gbEnableExtraRawFileSaving
        {
            get
            {
                return x_gbEnableExtraRawFileSaving;
            }

            set
            {
                x_gbEnableExtraRawFileSaving = value;
            }
        }

        //public static bool gbEnableExtraCsvFileSaving { get; set; } = true;                     // in Visual Studio 2015 and c# 6 
        private static bool x_gbEnableExtraCsvFileSaving = true;

        public static bool gbEnableExtraCsvFileSaving
        {
            get
            {
                return x_gbEnableExtraCsvFileSaving;
            }

            set
            {
                x_gbEnableExtraCsvFileSaving = value;
            }
        }

        //public static bool gbEnableMeasJsonFileSaving { get; set; } = false;            // true;                     // in Visual Studio 2015 and c# 6 
        private static bool x_gbEnableMeasJsonFileSaving = false;            // true;               // Roy*180918

        public static bool gbEnableMeasJsonFileSaving
        {
            get
            {
                return x_gbEnableMeasJsonFileSaving;
            }

            set
            {
                x_gbEnableMeasJsonFileSaving = value;
            }
        }

        //public static bool gbEnableProcJsonFileSaving { get; set; } = false;            // true;                     // in Visual Studio 2015 and c# 6 
        private static bool x_gbEnableProcJsonFileSaving = false;            // true;               // Roy*180918

        public static bool gbEnableProcJsonFileSaving
        {
            get
            {
                return x_gbEnableProcJsonFileSaving;
            }

            set
            {
                x_gbEnableProcJsonFileSaving = value;
            }
        }

        private static LogEventInfo mLogEvInfo_MeasData4Walk = new LogEventInfo();
        private static LogEventInfo mLogEvInfo_MeasData4Hoist = new LogEventInfo();
        //+++++++++++++++++++++++++++++++++++++++++

        //+++++++++++++++++++++++++++               // Roy+180904
        private static LogEventInfo mLogEvInfo_GeneralProc = new LogEventInfo();

        private static LogEventInfo mLogEvInfo_ProcTag_Hoist = new LogEventInfo();
        private static LogEventInfo mLogEvInfo_ProcTag_OdoRel = new LogEventInfo();
        //+++++++++++++++++++++++++++

        //+++++++++++++++++++++++++++++++++++++++++++++++                       // Roy+180105
        public static string Fun_ConvertToBitwiseText(int xInputNum, int xBitLen = 8)
        {
            int iBitLen = xBitLen;
            byte[] ayX = BitConverter.GetBytes(xInputNum);

            string sX = "";
            string sBit = "";

            if ((iBitLen <= 0) || (iBitLen > 8))
                iBitLen = 8;

            for (int i = 0; i < iBitLen; i++)
            {
                sBit = (((ayX[0] >> i) & 1) % 2).ToString();
                sX = sBit + sX;
            }

            return sX;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                   // Roy+180606
        /// <span class="code-SummaryComment"><summary></span>
        /// Executes a shell command synchronously.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="command">string command</param></span>
        /// <span class="code-SummaryComment"><returns>string, as output of the command.</returns></span>
        public static void ExecuteCommandSync(object command)
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;

                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;

                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();

                // Get the output into a string
                string result = proc.StandardOutput.ReadToEnd();

                // Display the command output.
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                // Log the exception
            }
        }


        /// <span class="code-SummaryComment"><summary></span>
        /// Execute the command Asynchronously.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="command">string command.</param></span>
        public static void ExecuteCommandAsync(string command)
        {
            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                Thread objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));

                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;

                //Start the thread.
                objThread.Start(command);
            }
            catch (ThreadStartException ex)
            {
                // Log the exception
            }
            catch (ThreadAbortException ex)
            {
                // Log the exception
            }
            catch (Exception ex)
            {
                // Log the exception
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        /*
        public static bool Fun_CCD影像座標轉對位座標(Def_CCD.enCCD CCD, Sys_Define.tyAXIS_XY Img, out Sys_Define.tyAXIS_XY CDR, bool b轉全域 = true)
        {
            CDR.X = 0; CDR.Y = 0;

            //Sys_Define.tyAXIS_XY Pixel = new Sys_Define.tyAXIS_XY();

            //Sys_Define.tyAXIS_XY NowPos前;
            //Sys_Define.tyAXIS_XY NowPos後;

            //NowPos前.Y = eq.Ctl.AXIS_STS[Def_Motion.enMotion.Y1_前CCD].NOW_POS;
            //NowPos前.X = eq.Ctl.AXIS_STS[Def_Motion.enMotion.X_CCD].NOW_POS;
            //NowPos後.Y = eq.Ctl.AXIS_STS[Def_Motion.enMotion.Y2_後CCD].NOW_POS;
            //NowPos後.X = eq.Ctl.AXIS_STS[Def_Motion.enMotion.X_CCD].NOW_POS;

            switch (CCD)
            {
                case Def_CCD.enCCD.上_CCD:
                    //CDR.X = NowPos前.X + (Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左_CCD].PIXEL_UNIT);
                    //CDR.Y = NowPos前.Y + (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左_CCD].PIXEL_UNIT);
                    break;

                //case Def_CCD.enCCD.LCM_左CCD:
                //   if (b轉全域 == true)
                //   {
                //      if (eq.Param.RECIPE.CCD.上片對位方式 == Def_Recipe.enCCD對位方式.LINE)
                //      {
                //         CDR.X = NowPos後.X + Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT;
                //         CDR.Y = eq.Param.RECIPE.CCD.LINE對位方式.上片.端點距離_DY + eq.Param.RECIPE.CCD.LINE對位方式.上片.H.前CCD_水平取像位_Y +
                //                 (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      }
                //      else
                //      {
                //         CDR.X = NowPos後.X + Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT;
                //         CDR.Y = eq.Param.RECIPE.CCD.MARK對位方式.上片.Mark_距離_DY + eq.Param.RECIPE.CCD.MARK對位方式.上片.前CCD_取像位_Y +
                //                 (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      }
                //   }
                //   else
                //   {
                //      CDR.X = NowPos後.Y + (Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      CDR.Y = NowPos後.Y + (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //   }
                //   break;
                //case Def_CCD.enCCD.LCM_左CCD:
                //   CDR.X = NowPos前.X + (Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //   CDR.Y = NowPos前.Y + (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //   break;
                //case Def_CCD.enCCD.LCM_左CCD:
                //   if (b轉全域 == true)
                //   {
                //      if (eq.Param.RECIPE.CCD.下片對位方式 == Def_Recipe.enCCD對位方式.LINE)
                //      {
                //         CDR.X = NowPos後.X + Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT;
                //         CDR.Y = eq.Param.RECIPE.CCD.LINE對位方式.下片.端點距離_DY + +eq.Param.RECIPE.CCD.LINE對位方式.下片.H.前CCD_水平取像位_Y +
                //                 (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      }
                //      else
                //      {
                //         CDR.X = NowPos後.X + Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT;
                //         CDR.Y = eq.Param.RECIPE.CCD.MARK對位方式.下片.Mark_距離_DY + eq.Param.RECIPE.CCD.MARK對位方式.下片.前CCD_取像位_Y +
                //                 (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      }
                //   }
                //   else
                //   {
                //      CDR.X = NowPos後.X + (Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      CDR.Y = NowPos後.Y + (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //   }
                //   break;

                default:
                    return false;
            }

            return true;
        }

        public static bool Fun_CCD影像座標轉對位座標_手動對位(Def_CCD.enCCD CCD, Sys_Define.tyAXIS_XY Img, out Sys_Define.tyAXIS_XY CDR, bool b轉全域 = true)
        {
            CDR.X = 0; CDR.Y = 0;
            //Sys_Define.tyAXIS_XY Pixel = new Sys_Define.tyAXIS_XY();

            //Sys_Define.tyAXIS_XY NowPos前;
            //Sys_Define.tyAXIS_XY NowPos後;

            //NowPos前.Y = eq.Ctl.AXIS_STS[Def_Motion.enMotion.Y1_前CCD].NOW_POS;
            //NowPos前.X = eq.Ctl.AXIS_STS[Def_Motion.enMotion.X_CCD].NOW_POS;
            //NowPos後.Y = eq.Ctl.AXIS_STS[Def_Motion.enMotion.Y2_後CCD].NOW_POS;
            //NowPos後.X = eq.Ctl.AXIS_STS[Def_Motion.enMotion.X_CCD].NOW_POS;

            switch (CCD)
            {
                case Def_CCD.enCCD.上_CCD:
                    //CDR.X = NowPos前.X + (Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左_CCD].PIXEL_UNIT);
                    //CDR.Y = NowPos前.Y + (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左_CCD].PIXEL_UNIT);
                    break;

                //case Def_CCD.enCCD.LCM_左CCD:
                //   if (b轉全域 == true)
                //   {
                //      if (eq.Param.RECIPE.CCD.上片對位方式 == Def_Recipe.enCCD對位方式.LINE)
                //      {
                //         CDR.X = NowPos後.X + Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT;
                //         CDR.Y = eq.Param.RECIPE.CCD.LINE對位方式.上片.端點距離_DY + eq.Param.RECIPE.CCD.LINE對位方式.上片.H.前CCD_水平取像位_Y +
                //                 (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      }
                //      else
                //      {
                //         CDR.X = NowPos後.X + Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT;
                //         CDR.Y = eq.Param.RECIPE.CCD.MARK對位方式.上片.Mark_距離_DY + eq.Param.RECIPE.CCD.MARK對位方式.上片.前CCD_取像位_Y +
                //                 (eq.Param.RECIPE.CCD.MARK對位方式.上片.後CCD_取像位_Y - NowPos後.Y) +
                //                 (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      }
                //   }
                //   else
                //   {
                //      CDR.X = NowPos後.Y + (Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      CDR.Y = NowPos後.Y + (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //   }
                //   break;
                //case Def_CCD.enCCD.LCM_左CCD:
                //   CDR.X = NowPos前.X + (Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //   CDR.Y = NowPos前.Y + (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //   break;
                //case Def_CCD.enCCD.LCM_左CCD:
                //   if (b轉全域 == true)
                //   {
                //      if (eq.Param.RECIPE.CCD.下片對位方式 == Def_Recipe.enCCD對位方式.LINE)
                //      {
                //         CDR.X = NowPos後.X + Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT;
                //         CDR.Y = eq.Param.RECIPE.CCD.LINE對位方式.下片.端點距離_DY + +eq.Param.RECIPE.CCD.LINE對位方式.下片.H.前CCD_水平取像位_Y +
                //                 (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      }
                //      else
                //      {
                //         CDR.X = NowPos後.X + Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT;
                //         CDR.Y = eq.Param.RECIPE.CCD.MARK對位方式.下片.Mark_距離_DY + eq.Param.RECIPE.CCD.MARK對位方式.下片.前CCD_取像位_Y +
                //                 (eq.Param.RECIPE.CCD.MARK對位方式.下片.後CCD_取像位_Y - NowPos後.Y) +
                //                 (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      }
                //   }
                //   else
                //   {
                //      CDR.X = NowPos後.X + (Img.X * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //      CDR.Y = NowPos後.Y + (Img.Y * eq.Param.CCD[(int)Def_CCD.enCCD.LCM_左CCD].PIXEL_UNIT);
                //   }
                //   break;

                default:
                    return false;
            }
            return true;
        }
        */
        
        /*
        public static bool Fun_TransferScreenToCogDisplayCoordinate(CogDisplay oDisplay, double X, double Y, out double rX, out double rY, int Round = 0, string SpaceName = "")
        //轉換cogDisplay之座標至影像之座標(回傳Image座標)
        {
            CogTransform2DLinear t;
            double tX, tY;

            if (SpaceName == "")
                t = (CogTransform2DLinear)oDisplay.GetTransform(oDisplay.UserPixelTree.RootName, oDisplay.UserDisplayTree.RootName);
            else
                t = (CogTransform2DLinear)oDisplay.GetTransform(SpaceName, oDisplay.UserDisplayTree.RootName);

            t.MapPoint(X, Y, out tX, out tY);

            rX = tX;
            rY = tY;

            if (Round > 0)
            {
                rX = Math.Round(rX, Round);
                rY = Math.Round(rY, Round);
            }

            return true;
        }
        */

        public static bool Fun_PST_CountAdd()
        {
            Sys_DataBase oDB = new Sys_DataBase(eq.cnt_DB_PATH);

            string SQL = "";

            SQL = "Update PSTCount Set NowPSTCount=NowPSTCount+1,TotalPSTCount=TotalPSTCount+1";
            if (oDB.Fun_ExecSQL(SQL) == false)
            {
                oDB.Fun_CloseDB();
                oDB = null;
                return false;
            }

            oDB.Fun_CloseDB();
            oDB = null;
            return true;
        }

        public static bool Fun_PST_NowCountClear()
        {
            Sys_DataBase oDB = new Sys_DataBase(eq.cnt_DB_PATH);

            string SQL = "";

            SQL = "Update PSTCount Set NowPSTCount=0";
            if (oDB.Fun_ExecSQL(SQL) == false)
            {
                oDB.Fun_CloseDB();
                oDB = null;
                return false;
            }

            //eq.StnData.此次貼合片數 = 0;

            oDB.Fun_CloseDB();
            oDB = null;
            return true;
        }

        public static bool Fun_PST_GetCount()
        {
            Sys_DataBase oDB = new Sys_DataBase(eq.cnt_DB_PATH);
            System.Data.Common.DbDataReader oRs = null;

            string SQL = "";

            SQL = "Select * From PSTCount";
            if (oDB.Fun_RsSQL(SQL, ref oRs) == false)
            {
                oDB.Fun_CloseRS(ref oRs);
                oRs = null;
                oDB.Fun_CloseDB();
                oDB = null;
                return false;
            }

            oRs.Read();

            oDB.Fun_CloseRS(ref oRs);
            oRs = null;
            oDB.Fun_CloseDB();
            oDB = null;
            return true;
        }

        public static bool Fun_Log_時間紀錄(Def_Common.enTackTimeItem item)
        {
            lock (oLock)
            {

                try
                {
                    if (Directory.Exists(eq.cnt_DATA_PATH + "時間紀錄") == false)
                        Directory.CreateDirectory(eq.cnt_DATA_PATH + "時間紀錄");
                    string filename = DateTime.Now.ToString("yyyyMMdd");

                    filename = filename + "_" + item.ToString() + ".csv";

                    StreamWriter oF = new StreamWriter(eq.cnt_DATA_PATH + "時間紀錄" + @"\" + filename, true);

                    //switch (item)
                    //{
                    //case Def_Common.enTackTimeItem.上片載入:
                    //   oF.WriteLine(eq.StnData.時間紀錄.上片載入開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.上片載入結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.上片載入_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.下片載入:
                    //   oF.WriteLine(eq.StnData.時間紀錄.下片載入開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.下片載入結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.下片載入_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.面膠:
                    //   oF.WriteLine(eq.StnData.時間紀錄.面膠開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.面膠結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.面膠_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.UV_雷射:
                    //   oF.WriteLine(eq.StnData.時間紀錄.UV_雷射開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.UV_雷射結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.UV_雷射_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.ROBOT取上片:
                    //   oF.WriteLine(eq.StnData.時間紀錄.ROBOT取上片開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.ROBOT取上片結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.ROBOT取上片_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.ROBOT取下片:
                    //   oF.WriteLine(eq.StnData.時間紀錄.ROBOT取下片開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.ROBOT取下片結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.ROBOT取下片_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.腔體放片:
                    //   oF.WriteLine(eq.StnData.時間紀錄.腔體放片開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.腔體放片結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.腔體放片_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.CCD對位:
                    //   oF.WriteLine(eq.StnData.時間紀錄.CCD對位開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.CCD對位結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.CCD對位_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.貼合:
                    //   oF.WriteLine(eq.StnData.時間紀錄.貼合開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.貼合結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.貼合_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.成品載出:
                    //   oF.WriteLine(eq.StnData.時間紀錄.成品載出開始.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.成品載出結束.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.成品載出_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.抽真空:
                    //   oF.WriteLine(eq.StnData.時間紀錄.抽真空開始時間.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.抽真空結束時間.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.抽真空_使用時間.ToString());
                    //   break;
                    //case Def_Common.enTackTimeItem.破真空:
                    //   oF.WriteLine(eq.StnData.時間紀錄.破真空開始時間.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.破真空結束時間.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                    //                eq.StnData.時間紀錄.破真空_使用時間.ToString());
                    //   break;
                    //default:
                    //   oF.Close();
                    //   return false;

                    //}

                    oF.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }

        public static void Fun_Log_時間紀錄_Delete()
        {
            string filename = DateTime.Now.AddDays(-60).ToString("yyyyMMdd");

            if (Directory.Exists(eq.cnt_DATA_PATH + "時間紀錄") == false) return;

            string[] LogFile = Directory.GetFiles(eq.cnt_DATA_PATH + "時間紀錄");
            if (LogFile.Length > 0)
            {
                for (int i = 0; i < LogFile.Length; i++)
                {

                    if (string.Compare(filename, Path.GetFileNameWithoutExtension(LogFile[i])) == 1)
                    {
                        File.Delete(LogFile[i]);
                    }
                }
            }

        }

        public static bool Fun_Log_Sub時間紀錄(Def_Common.tyTacTimeLog tl)
        {
            lock (oLock)
            {
                try
                {
                    if (Directory.Exists(eq.cnt_DATA_PATH) == false)
                        Directory.CreateDirectory(eq.cnt_DATA_PATH);
                    string filename = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

                    filename = tl.SubTitle + "_" + filename + ".log";

                    StreamWriter oF = new StreamWriter(eq.cnt_SUBTACTTIME_LOG_PATH + @"\" + filename, true);
                    TimeSpan tmp = new TimeSpan();

                    for (int i = 0; i < tl.item.Count(); i++)
                    {
                        tmp = eq.tl.End[i] - eq.tl.Start[i];
                        oF.WriteLine(eq.tl.item[i].ToString() + " ," +
                           eq.tl.Start[i].ToString("HH:mm:ss:fff") + " ," + eq.tl.End[i].ToString("HH:mm:ss:fff")
                           + " ," + tmp.TotalSeconds.ToString());

                        //oF.WriteLine(eq.StnData.時間紀錄.破真空開始時間.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                        //                eq.StnData.時間紀錄.破真空結束時間.ToString("yyyy/MM/dd HH:mm:ss") + "," +
                        //                eq.StnData.時間紀錄.破真空_使用時間.ToString());
                    }
                    tmp = eq.tl.SubEnd - eq.tl.SubStart;
                    oF.WriteLine(eq.tl.SubTitle + "總共使用時間(ss)" + " : " +
                       tmp.TotalSeconds.ToString());

                    oF.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++               // Roy-+170721
        /*
        /// <summary>
       /// 非同步檔案 I/O
       ///  https://msdn.microsoft.com/zh-tw/library/kztecsys(v=vs.110).aspx
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="DestinationSW"></param>
        /// <returns></returns>
       public async Task CopyFilesAsync(string Source, StreamWriter DestinationSW)                // for .NET Framework 4.5  +  VS2112 ~ 
       {
           await DestinationSW.WriteAsync(Source);
       } 
        */
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        //++++++++++++++++++++++++++++++++++++++++++                  // Roy+171114
        public static bool Fun_WrProcLog01(string log, StreamWriter xSW = null)
        {
            lock (oLock)
            {
                if (log == "") return true;

                try
                {
                    if (xSW == null)
                        Directory.CreateDirectory(eq.PROC_LOG01_PATH);

                    if (xSW == null)
                    {
                        using (StreamWriter sw = File.AppendText(eq.PROC_LOG01_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv"))
                        {
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                            //await CopyFilesAsync(log, sw);
                        }
                    }
                    else
                    {
                        xSW.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.Flush();

                        //xSW.WriteLineAsync(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.FlushAsync();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }


        public static bool Fun_WrProcLog02(string log, StreamWriter xSW = null)
        {
            lock (oLock)
            {
                if (log == "") return true;

                try
                {
                    if (xSW == null)
                        Directory.CreateDirectory(eq.PROC_LOG02_PATH);

                    if (xSW == null)
                    {
                        using (StreamWriter sw = File.AppendText(eq.PROC_LOG02_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv"))
                        {
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                            //await CopyFilesAsync(log, sw);
                        }
                    }
                    else
                    {
                        xSW.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.Flush();

                        //xSW.WriteLineAsync(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.FlushAsync();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }


        public static bool Fun_WrProcLog03(string log, StreamWriter xSW = null)
        {
            lock (oLock)
            {
                if (log == "") return true;

                try
                {
                    if (xSW == null)
                        Directory.CreateDirectory(eq.PROC_LOG03_PATH);

                    if (xSW == null)
                    {
                        using (StreamWriter sw = File.AppendText(eq.PROC_LOG03_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv"))
                        {
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                            //await CopyFilesAsync(log, sw);
                        }
                    }
                    else
                    {
                        xSW.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.Flush();

                        //xSW.WriteLineAsync(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.FlushAsync();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }


        public static bool Fun_WrProcLog04(string log, StreamWriter xSW = null)
        {
            lock (oLock)
            {
                if (log == "") return true;

                try
                {
                    if (xSW == null)
                        Directory.CreateDirectory(eq.PROC_LOG04_PATH);

                    if (xSW == null)
                    {
                        using (StreamWriter sw = File.AppendText(eq.PROC_LOG04_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv"))
                        {
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                            //await CopyFilesAsync(log, sw);
                        }
                    }
                    else
                    {
                        xSW.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.Flush();

                        //xSW.WriteLineAsync(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.FlushAsync();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }


        public static bool Fun_WrProcLog05(string log, StreamWriter xSW = null)
        {
            lock (oLock)
            {
                if (log == "") return true;

                try
                {
                    if (xSW == null)
                        Directory.CreateDirectory(eq.PROC_LOG05_PATH);

                    if (xSW == null)
                    {
                        using (StreamWriter sw = File.AppendText(eq.PROC_LOG05_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".csv"))
                        {
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                            //await CopyFilesAsync(log, sw);
                        }
                    }
                    else
                    {
                        xSW.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.Flush();

                        //xSW.WriteLineAsync(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.FlushAsync();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }
        //++++++++++++++++++++++++++++++++++++++++++


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                       // Roy+180508
        static Logger mLoggerProc_JSON = LogManager.LogFactory.GetLogger("OHT_Proc_JSON");
        static Logger mLoggerMeas_JSON4Walk = LogManager.LogFactory.GetLogger("OHT_Meas_JSON4Walk");
        static Logger mLoggerMeas_JSON4Hoist = LogManager.LogFactory.GetLogger("OHT_Meas_JSON4Hoist");              // Roy+180621

        static Logger mLoggerProc_Raw_300OHTM = LogManager.LogFactory.GetLogger("OHT_Proc_Raw_300VehM");
        static Logger mLoggerMeas_Csv4Walk = LogManager.LogFactory.GetLogger("OHT_Meas_Csv4Walk");
        static Logger mLoggerMeas_Csv4Hoist = LogManager.LogFactory.GetLogger("OHT_Meas_Csv4Hoist");              // Roy+180621


        public enum MyLogKind
        {
            GeneralProcess,
            ProcessTag_OdometerRelative,                       // Roy+180516
            ProcessTag_Hoist,                                         // Roy+180516

            MeasurementData4Walk,
            MeasurementData4Hoist
        }


        //+++++++++++++++++++++++++++++++                       // Roy+180516                   // Roy*180612               // Roy*180705
        internal static string msProcTag_OdoRel = "FromSectionID,FromSectionOffset(mm),ToSectionID,ToSectionOffset(mm),WalkLength(m),StartUpTimestamp,ElapsedDuration(s),NormalCompleted"
                                                                                       + ",InterruptTotalTimes,IntByExtCancelAbortTimes,IntByExtPauseTimes,IntByHidCtrlSectionTimes,IntByBlockCtrlSectionTimes,IntByHidCtrlZoneTimes,IntByBlockCtrlZoneTimes"
                                                                                       + ",IntBySnrLidarTimes,IntByHwGuardStopTimes,IntByManualPauseTimes,IntByManualStopTimes,IntByManualEmsTimes,IntByUnknownTimes"
                                                                                       + ",WalkRouteCnt,WalkCurveCnt,LiftLeftCnt,LiftRightCnt,HoistLoadCnt,HoistUnloadCnt";
        internal static string[] masProcTag_OdoRel;

        internal static string msProcTag_Hoist = "AddressID,PnP_Status,MultiHoist_UseMode,HoistLength(m),StartUpTimestamp,ElapsedDuration(s),NormalCompleted,Hoist1_CarrierID,Hoist2_CarrierID"
                                                                                  + ",InterruptTotalTimes,IntByExtCancelAbortTimes,IntByExtPauseTimes,IntByHidCtrlSectionTimes,IntByBlockCtrlSectionTimes,IntByHidCtrlZoneTimes,IntByBlockCtrlZoneTimes"
                                                                                  + ",IntBySnrLidarTimes,IntByHwGuardStopTimes,IntByManualPauseTimes,IntByManualStopTimes,IntByManualEmsTimes,IntByUnknownTimes"
                                                                                  + ",WalkRouteCnt,WalkCurveCnt,LiftLeftCnt,LiftRightCnt,HoistLoadCnt,HoistUnloadCnt";
        internal static string[] masProcTag_Hoist;
        //+++++++++++++++++++++++++++++++

        internal static string msMeasData4Walk = "";
        //internal static string msMeasData4Walk = "Category,CurveOffset(mm),Current_PosCmd(mm),Current_VelCmd(mm/s),mrTargetPos_MM_4WalkRear(mm),Current_PosFbk(mm),Current_VelFbk(mm/s),Current_Torque(%),sCurrentStation,b左下軌道超速檢知放大器_Now,b右下軌道超速檢知放大器_Now,b前左車入彎檢知_Now,b前右車入彎檢知_Now,CostInMs(ms),JobThreadTimestampEnter,BarcodeCurrentPos_InMM(mm),BarcodeAveragePos_InMM(mm),iCtrlPtIdx,b前車左轉檢知_前車轉向左上右下位檢知,b前車右轉檢知_前車轉向左下右上位檢知,b後車左轉檢知_後車轉向左上右下位檢知,b後車右轉檢知_後車轉向左下右上位檢知,b前左走行側軌道檢知,b前右走行側軌道檢知,b後左走行側軌道檢知,b後右走行側軌道檢知,b前左上軌道檢知,b前右上軌道檢知,b後左上軌道檢知,b後右上軌道檢知,msSectNameKept,VehM_BlockPassQuerySectionPrevious,VehM_BlockPassQuerySectionCurrent,meSectTypeKept,VehM_BlockPassQuerySectionType,bDI_AreaSnr4Walk_Notify1_Far,bDI_AreaSnr4Walk_Notify2_Midway,bDI_AreaSnr4Walk_Notify3_Near,bDI_AreaSnr4Walk_MalFunction,sCurrentSection,iSectionOffset(mm),RefIpAddress,PingIpStatus,NetworkQuality(ms),fPowerCurrent(A),fPowerVoltage(V),fPower(kW),fWork(kWH),fThermoValue00(dC),fThermoValue01(dC),fThermoValue02(dC),fThermoValue03(dC),fThermoValue04(dC),fThermoValue05(dC),fThermoValue06(dC),fThermoValue07(dC),AxisWalk內部編碼器溫度(dC),AxisWalk速度_Now(rpm),AxisWalk轉矩指令Mean_Now(%),AxisWalk轉矩指令STD_Now(%),AxisWalk劣化診斷狀態,AxisWalk慣量比_Now(%),AxisWalk偏載重_Now(%),AxisWalk動摩擦_Now(%),AxisWalk黏性摩擦_Now(% /10000rpm),HID_01總電箱異常_OutOfService,HID_01總電箱異常_EQ_Online,HID_01總電箱異常_EQ_Error,Regulator磁場輸入異常,Regulator溫度保護異常";
        internal static string[] masMeasData4Walk;

        internal static string msMeasData4Hoist = "";                   // Roy+180621
        internal static string[] masMeasData4Hoist;                   // Roy+180621

        ////internal static string msMeasData4Power = "fPowerCurrent(A),fPowerVoltage(V),fPower(kW),fWork(kWH)" 
        ////                                                                        + ",fThermoValue00(dC),fThermoValue01(dC),fThermoValue02(dC),fThermoValue03(dC),fThermoValue04(dC),fThermoValue05(dC),fThermoValue06(dC),fThermoValue07(dC)";
        ////internal static string[] masMeasData4Power;

        // internal static string[] masMeasData4Walk = { "fPowerCurrent(A)", "fPowerVoltage(V)", "fPower(kW)", "fWork(kWH)", "fThermoValue00(dC)", "fThermoValue01(dC)", "fThermoValue02(dC)", "fThermoValue03(dC)", "fThermoValue04(dC)", "fThermoValue05(dC)", "fThermoValue06(dC)", "fThermoValue07(dC)" };
        ////internal static string[] masMeasData4Walk = { "fPowerCurrent_A", "fPowerVoltage_V", "fPower_kW", "fWork_kWH", "fThermoValue00_dC", "fThermoValue01_dC", "fThermoValue02_dC", "fThermoValue03_dC", "fThermoValue04_dC", "fThermoValue05_dC", "fThermoValue06_dC", "fThermoValue07_dC" };


        //++++++++++++++++++++++++++++++++++++++++                       // Roy+180621
        ////internal static JObject mLogEntry_MeasData4Walk = new JObject();
        internal static dynamic mLogEntry_MeasData4Walk = new JObject();
        internal static dynamic mLogEntry_MeasData4Hoist = new JObject();

        internal static dynamic mLogEntry_ProcTag_OdoRel = new JObject();
        internal static dynamic mLogEntry_ProcTag_Hoist = new JObject();
        internal static dynamic mLogEntry_GeneralProc = new JObject();
        //++++++++++++++++++++++++++++++++++++++++


        public static int Fun_GetLogPairsSize(MyLogKind eLogKind)
        {
            int iLogPairsSize = 0;

            if (!eq.bIsSimMode)
            {
               // msToolID = eq.Param.SYSTEM.機台.機台名稱;
            }
            else
            {
                //msToolID = System.Environment.MachineName.ToLowerInvariant() + ":" + eq.Param.SYSTEM.機台.機台名稱;
            }

            //~~~~~~~~~~~~

            if ((masMeasData4Walk == null) || (masMeasData4Walk.Count() == 0))
            {
                //+++++++++++
                //var config = new NLog.Config.LoggingConfiguration();
                NLog.Config.LoggingConfiguration config = LogManager.Configuration;

                if (config.Variables.ContainsKey("snrCsvHeader4Walk"))
                {
                    msMeasData4Walk = config.Variables["snrCsvHeader4Walk"].Text;
                }
                //+++++++++++

                masMeasData4Walk = msMeasData4Walk.Split(',');
            }

            //+++++++++++++++++++++++++++++++++++++++++++++++++++++             // Roy+180621
            if ((masMeasData4Hoist == null) || (masMeasData4Hoist.Count() == 0))
            {
                //var config = new NLog.Config.LoggingConfiguration();
                NLog.Config.LoggingConfiguration config = LogManager.Configuration;

                if (config.Variables.ContainsKey("snrCsvHeader4Hoist"))
                {
                    msMeasData4Hoist = config.Variables["snrCsvHeader4Hoist"].Text;
                }

                masMeasData4Hoist = msMeasData4Hoist.Split(',');
            }
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++

            ////if ((masMeasData4Hoist == null) || (masMeasData4Hoist.Count() == 0))
            ////{
            ////    masMeasData4Hoist = msMeasData4Hoist.Split(',');
            ////}

            if ((masProcTag_OdoRel == null) || (masProcTag_OdoRel.Count() == 0))
            {
                masProcTag_OdoRel = msProcTag_OdoRel.Split(',');
            }

            if ((masProcTag_Hoist == null) || (masProcTag_Hoist.Count() == 0))
            {
                masProcTag_Hoist = msProcTag_Hoist.Split(',');
            }

            //~~~~~~~~~~~~

            switch (eLogKind)
            {
                case MyLogKind.MeasurementData4Walk:
                    iLogPairsSize = masMeasData4Walk.Count();
                    break;

                case MyLogKind.MeasurementData4Hoist:
                    iLogPairsSize = masMeasData4Hoist.Count();
                    break;

                //+++++++++++++++++++++++++++++++                       // Roy+180516
                case MyLogKind.ProcessTag_OdometerRelative:
                    iLogPairsSize = masProcTag_OdoRel.Count();
                    break;

                case MyLogKind.ProcessTag_Hoist:
                    iLogPairsSize = masProcTag_Hoist.Count();
                    break;
                //+++++++++++++++++++++++++++++++                  

                //case MyLogKind.GeneralProcess:
                default:
                    iLogPairsSize = 0;
                    break;
            }

            return iLogPairsSize;
        }


        /*                  // Roy-180516
        internal static void Fun_Log_Generic(MyLogKind eLogKind, string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, string sMsgBody, params object[] aoInputVar)
        {
            dynamic logEntry_MeasData4Walk = new JObject();
            ////JObject logEntry_MeasData4Walk = new JObject();

            string sMsgBodyCsv = "";

            DateTime dtNowSure;

            if (dtNow == null)
                dtNowSure = DateTime.Now;
            else
                dtNowSure = (DateTime)dtNow;

            ////logEntry_MeasData4Walk.RPT_TIME = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);
            logEntry_MeasData4Walk.Add("@timestamp", dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture));
            //logEntry_MeasData4Walk["@timestamp"] = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);

            logEntry_MeasData4Walk.Site = sSite;
            logEntry_MeasData4Walk.ToolID = sToolID;
            logEntry_MeasData4Walk.LogLevel = eLogLv.ToString();

            switch (eLogKind)
            {
                case MyLogKind.MeasurementData4Walk:
                    if ((masMeasData4Walk == null) || (masMeasData4Walk.Count() == 0))
                    {
                        masMeasData4Walk = msMeasData4Walk.Split(',');
                    }

                    if ((sMsgBody == "") && (aoInputVar != null))
                    {
                        if (masMeasData4Walk.Count() <= aoInputVar.Count())
                        {
                            for (int i = 0; i < masMeasData4Walk.Count(); i++)
                            {
                                ////logEntry_MeasData4Walk.Add(masMeasData4Walk[i], aoInputVar[i]);
                                ////logEntry_MeasData4Walk.Add(masMeasData4Walk[i], aoInputVar[i] as JToken);
                                logEntry_MeasData4Walk.Add(masMeasData4Walk[i], JToken.FromObject(aoInputVar[i]));

                                ////logEntry_MeasData4Walk[masMeasData4Walk[i]] = aoInputVar[i] as JToken;
                                //logEntry_MeasData4Walk[masMeasData4Walk[i]] = JToken.FromObject(aoInputVar[i]);
                            }
                        }

                        sMsgBodyCsv = String.Join(",", aoInputVar);                       // extra ... 
                        mLoggerMeas_Csv4Walk.Log(eLogLv, sMsgBodyCsv);                       // extra ... 
                    }
                    else
                    {
                        if ((sMsgBody != "") && ((aoInputVar == null) || (aoInputVar.Count() == 0)))
                        {
                            object[] aoInputVarEx = sMsgBody.Split(',');

                            if (masMeasData4Walk.Count() <= aoInputVarEx.Count())
                            {
                                for (int i = 0; i < masMeasData4Walk.Count(); i++)
                                {
                                    ////logEntry_MeasData4Walk.Add(masMeasData4Walk[i], aoInputVarEx[i]);
                                    ////logEntry_MeasData4Walk.Add(masMeasData4Walk[i], aoInputVarEx[i] as JToken);
                                    logEntry_MeasData4Walk.Add(masMeasData4Walk[i], JToken.FromObject(aoInputVarEx[i]));

                                    ////logEntry_MeasData4Walk[masMeasData4Walk[i]] = aoInputVarEx[i] as JToken;
                                    //logEntry_MeasData4Walk[masMeasData4Walk[i]] = JToken.FromObject(aoInputVarEx[i]);
                                }
                            }

                            mLoggerMeas_Csv4Walk.Log(eLogLv, sMsgBody);                       // extra ... 
                        }           // # if ((sMsgBody != "") && (aoInputVar == null))
                    }

                    break;


                case MyLogKind.MeasurementData4Hoist:
                    if ((sMsgBody == "") && (aoInputVar != null))
                    {
                        if ((masMeasData4Hoist == null) || (masMeasData4Hoist.Count() == 0))
                        {
                            masMeasData4Hoist = msMeasData4Hoist.Split(',');
                        }

                        if (masMeasData4Hoist.Count() <= aoInputVar.Count())
                        {
                            for (int i = 0; i < masMeasData4Hoist.Count(); i++)
                            {
                                ////logEntry_MeasData4Walk.Add(masMeasData4Hoist[i], aoInputVar[i]);
                                ////logEntry_MeasData4Walk.Add(masMeasData4Hoist[i], aoInputVar[i] as JToken);
                                logEntry_MeasData4Walk.Add(masMeasData4Hoist[i], JToken.FromObject(aoInputVar[i]));

                                ////logEntry_MeasData4Walk[masMeasData4Hoist[i]] = aoInputVar[i] as JToken;
                                //logEntry_MeasData4Walk[masMeasData4Hoist[i]] = JToken.FromObject(aoInputVar[i]);
                            }
                        }

                        sMsgBodyCsv = String.Join(",", aoInputVar);                       // extra ... 
                        mLoggerMeas_Csv4Walk.Log(eLogLv, sMsgBodyCsv);                       // extra ... 
                    }           // # if ((sMsgBody == "") && (aoInputVar != null))

                    break;


                //case MyLogKind.GeneralProcess:
                default:
                    string sMsg = String.Format(sMsgBody, aoInputVar);
                    logEntry_MeasData4Walk.Msg = sMsg;

                    mLoggerProc_Raw.Log(eLogLv, sMsg);                       // extra ... 
                    break;
            }


            var json = logEntry_MeasData4Walk.ToString(Newtonsoft.Json.Formatting.None);
            ////json = json.Replace("RPT_TIME", "@timestamp");


            switch (eLogKind)
            {
                case MyLogKind.MeasurementData4Walk:
                case MyLogKind.MeasurementData4Hoist:
                    //LogManager.GetLogger("OHT_Meas_JSON4Walk").Info(json);
                    mLoggerMeas_JSON4Walk.Log(eLogLv, json);
                    break;

                //case MyLogKind.GeneralProcess:
                default:
                    //LogManager.GetLogger("OHT_Proc_JSON").Info(json);
                    mLoggerProc_JSON.Log(eLogLv, json);
                    break;
            }
            
            return;
        }
        */


        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++                       // Roy+180516
        ////internal static void Fun_Log_MeasData4Walk(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraCsvFileSaving, string sMsgBody, params object[] aoInputVar)
        //internal static void Fun_Log_MeasData4Walk(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraCsvFileSaving, params object[] aoInputVar)               // Roy-180621
        internal static void Fun_Log_MeasData4Walk(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraCsvFileSaving, bool bEnableMeasJsonFileSaving, params object[] aoInputVar)               // Roy+180621
        {
            Object thisLock = new Object();

            lock (thisLock)
            {
                dynamic logEntry_MeasData4Walk = new JObject();
                ////JObject logEntry_MeasData4Walk = new JObject();

                string sMsgBodyCsv = "";

                DateTime dtNowSure;

                if (dtNow == null)
                    dtNowSure = DateTime.Now;
                else
                    dtNowSure = (DateTime)dtNow;

                //++++++++++++++
                if (bEnableExtraCsvFileSaving)                       // extra ...                   // Roy+180621
                {
                    //LogEventInfo mLogEvInfo_MeasData4Walk = new LogEventInfo();                  // Roy-180621

                    mLogEvInfo_MeasData4Walk.Properties["myTime"] = dtNowSure;                             // XML 若加 :format, 就會 沒(時間)值出現 ...... 怪事?!
                }
                //++++++++++++++

                if ((masMeasData4Walk == null) || (masMeasData4Walk.Count() == 0))
                {
                    //+++++++++++
                    //var config = new NLog.Config.LoggingConfiguration();
                    NLog.Config.LoggingConfiguration config = LogManager.Configuration;

                    if (config.Variables.ContainsKey("snrCsvHeader4Walk"))
                    {
                        msMeasData4Walk = config.Variables["snrCsvHeader4Walk"].Text;
                    }
                    //+++++++++++

                    masMeasData4Walk = msMeasData4Walk.Split(',');
                }

                if (bEnableMeasJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                {
                    ////logEntry_MeasData4Walk.RPT_TIME = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);
                    logEntry_MeasData4Walk.Add("@timestamp", dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture));
                    //logEntry_MeasData4Walk["@timestamp"] = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);

                    logEntry_MeasData4Walk.Site = sSite;
                    logEntry_MeasData4Walk.ToolID = sToolID;
                    logEntry_MeasData4Walk.LogLevel = eLogLv.ToString();
                }

                //if ((sMsgBody == "") && (aoInputVar != null))
                if (aoInputVar != null)
                {
                    if (bEnableMeasJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                    {
                        if (masMeasData4Walk.Count() <= aoInputVar.Count())
                        {
                            for (int i = 0; i < masMeasData4Walk.Count(); i++)
                            {
                                ////logEntry_MeasData4Walk.Add(masMeasData4Walk[i], aoInputVar[i]);
                                ////logEntry_MeasData4Walk.Add(masMeasData4Walk[i], aoInputVar[i] as JToken);
                                logEntry_MeasData4Walk.Add(masMeasData4Walk[i], JToken.FromObject(aoInputVar[i]));

                                ////logEntry_MeasData4Walk[masMeasData4Walk[i]] = aoInputVar[i] as JToken;
                                //logEntry_MeasData4Walk[masMeasData4Walk[i]] = JToken.FromObject(aoInputVar[i]);
                            }
                        }
                    }

                    if (bEnableExtraCsvFileSaving)                       // extra ... 
                    {
                        sMsgBodyCsv = String.Join(",", aoInputVar);
                    }
                }
                /*
                else
                {
                    if ((sMsgBody != "") && ((aoInputVar == null) || (aoInputVar.Count() == 0)))
                    {
                        object[] aoInputVarEx = sMsgBody.Split(',');

                        if (masMeasData4Walk.Count() <= aoInputVarEx.Count())
                        {
                            for (int i = 0; i < masMeasData4Walk.Count(); i++)
                            {
                                ////logEntry_MeasData4Walk.Add(masMeasData4Walk[i], aoInputVarEx[i]);
                                ////logEntry_MeasData4Walk.Add(masMeasData4Walk[i], aoInputVarEx[i] as JToken);
                                logEntry_MeasData4Walk.Add(masMeasData4Walk[i], JToken.FromObject(aoInputVarEx[i]));

                                ////logEntry_MeasData4Walk[masMeasData4Walk[i]] = aoInputVarEx[i] as JToken;
                                //logEntry_MeasData4Walk[masMeasData4Walk[i]] = JToken.FromObject(aoInputVarEx[i]);
                            }
                        }

                        if (bEnableExtraCsvFileSaving)
                        {
                            sMsgBodyCsv = sMsgBody;                       // extra ... 
                        }
                    }           // # if ((sMsgBody != "") && ((aoInputVar == null) || (aoInputVar.Count() == 0)))
                }           // # if ((sMsgBody == "") && (aoInputVar != null))
                */

                if (bEnableMeasJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                {
                    var json = logEntry_MeasData4Walk.ToString(Newtonsoft.Json.Formatting.None);
                    ////json = json.Replace("RPT_TIME", "@timestamp");

                    //LogManager.GetLogger("OHT_Meas_JSON4Walk").Info(json);
                    mLoggerMeas_JSON4Walk.Log(eLogLv, json);
                }

                if (bEnableExtraCsvFileSaving)                       // extra ... 
                {
                    //mLoggerMeas_Csv4Walk.Log(eLogLv, sMsgBodyCsv);

                    //++++++++++++++
                    mLogEvInfo_MeasData4Walk.Level = eLogLv;
                    //mLogEvInfo_MeasData4Walk.LoggerName = "OHT_Meas_Csv4Walk";
                    mLogEvInfo_MeasData4Walk.Message = sMsgBodyCsv;

                    mLogEvInfo_MeasData4Walk.TimeStamp = dtNowSure;

                    mLoggerMeas_Csv4Walk.Log(mLogEvInfo_MeasData4Walk);
                    //++++++++++++++
                }
            }

            return;
        }


        //internal static void Fun_Log_MeasData4Hoist(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraCsvFileSaving, params object[] aoInputVar)               // Roy-180621
        internal static void Fun_Log_MeasData4Hoist(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraCsvFileSaving, bool bEnableMeasJsonFileSaving, params object[] aoInputVar)               // Roy+180621
        {
            Object thisLock = new Object();

            lock (thisLock)
            {
                dynamic logEntry_MeasData4Hoist = new JObject();
                ////JObject logEntry_MeasData4Hoist = new JObject();

                string sMsgBodyCsv = "";

                DateTime dtNowSure;

                if (dtNow == null)
                    dtNowSure = DateTime.Now;
                else
                    dtNowSure = (DateTime)dtNow;

                //++++++++++++++
                if (bEnableExtraCsvFileSaving)                       // extra ...                   // Roy+180621
                {
                    //LogEventInfo mLogEvInfo_MeasData4Hoist = new LogEventInfo();                  // Roy-180621

                    mLogEvInfo_MeasData4Hoist.Properties["myTime"] = dtNowSure;                             // XML 若加 :format, 就會 沒(時間)值出現 ...... 怪事?!
                }
                //++++++++++++++

                if ((masMeasData4Hoist == null) || (masMeasData4Hoist.Count() == 0))
                {
                    //var config = new NLog.Config.LoggingConfiguration();
                    NLog.Config.LoggingConfiguration config = LogManager.Configuration;

                    if (config.Variables.ContainsKey("snrCsvHeader4Hoist"))
                    {
                        msMeasData4Hoist = config.Variables["snrCsvHeader4Hoist"].Text;
                    }

                    masMeasData4Hoist = msMeasData4Hoist.Split(',');
                }

                if (bEnableMeasJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                {
                    ////logEntry_MeasData4Hoist.RPT_TIME = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);
                    logEntry_MeasData4Hoist.Add("@timestamp", dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture));
                    //logEntry_MeasData4Hoist["@timestamp"] = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);

                    logEntry_MeasData4Hoist.Site = sSite;
                    logEntry_MeasData4Hoist.ToolID = sToolID;
                    logEntry_MeasData4Hoist.LogLevel = eLogLv.ToString();
                }

                //if ((sMsgBody == "") && (aoInputVar != null))
                if (aoInputVar != null)
                {
                    if (bEnableMeasJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                    {
                        if (masMeasData4Hoist.Count() <= aoInputVar.Count())
                        {
                            for (int i = 0; i < masMeasData4Hoist.Count(); i++)
                            {
                                ////logEntry_MeasData4Hoist.Add(masMeasData4Hoist[i], aoInputVar[i]);
                                ////logEntry_MeasData4Hoist.Add(masMeasData4Hoist[i], aoInputVar[i] as JToken);
                                logEntry_MeasData4Hoist.Add(masMeasData4Hoist[i], JToken.FromObject(aoInputVar[i]));

                                ////logEntry_MeasData4Hoist[masMeasData4Hoist[i]] = aoInputVar[i] as JToken;
                                //logEntry_MeasData4Hoist[masMeasData4Hoist[i]] = JToken.FromObject(aoInputVar[i]);
                            }
                        }
                    }

                    if (bEnableExtraCsvFileSaving)                       // extra ... 
                    {
                        sMsgBodyCsv = String.Join(",", aoInputVar);
                    }
                }

                if (bEnableMeasJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                {
                    var json = logEntry_MeasData4Hoist.ToString(Newtonsoft.Json.Formatting.None);
                    ////json = json.Replace("RPT_TIME", "@timestamp");

                    //LogManager.GetLogger("OHT_Meas_JSON4Hoist").Info(json);
                    mLoggerMeas_JSON4Hoist.Log(eLogLv, json);
                }

                if (bEnableExtraCsvFileSaving)                       // extra ... 
                {
                    //mLoggerMeas_Csv4Hoist.Log(eLogLv, sMsgBodyCsv);

                    mLogEvInfo_MeasData4Hoist.Level = eLogLv;
                    //mLogEvInfo_MeasData4Hoist.LoggerName = "OHT_Meas_Csv4Hoist";
                    mLogEvInfo_MeasData4Hoist.Message = sMsgBodyCsv;

                    mLogEvInfo_MeasData4Hoist.TimeStamp = dtNowSure;

                    mLoggerMeas_Csv4Hoist.Log(mLogEvInfo_MeasData4Hoist);
                }
            }

            return;
        }


        //internal static void Fun_Log_ProcTag_OdoRel(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraRawFileSaving, params object[] aoInputVar)               // Roy-180621
        internal static void Fun_Log_ProcTag_OdoRel(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraRawFileSaving, bool bEnableProcJsonFileSaving, params object[] aoInputVar)               // Roy+180621
        {
            Object thisLock = new Object();

            lock (thisLock)
            {
                dynamic logEntry_ProcTag_OdoRel = new JObject();
                ////JObject logEntry_ProcTag_OdoRel = new JObject();

                DateTime dtNowSure;

                if (dtNow == null)
                    dtNowSure = DateTime.Now;
                else
                    dtNowSure = (DateTime)dtNow;

                ////logEntry_ProcTag_OdoRel.RPT_TIME = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);
                logEntry_ProcTag_OdoRel.Add("@timestamp", dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture));
                //logEntry_ProcTag_OdoRel["@timestamp"] = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);

                logEntry_ProcTag_OdoRel.Site = sSite;
                logEntry_ProcTag_OdoRel.ToolID = sToolID;
                logEntry_ProcTag_OdoRel.LogLevel = eLogLv.ToString();

                if ((masProcTag_OdoRel == null) || (masProcTag_OdoRel.Count() == 0))
                {
                    masProcTag_OdoRel = msProcTag_OdoRel.Split(',');
                }

                if (aoInputVar != null)
                {
                    if (masProcTag_OdoRel.Count() <= aoInputVar.Count())
                    {
                        for (int i = 0; i < masProcTag_OdoRel.Count(); i++)
                        {
                            ////logEntry_ProcTag_OdoRel.Add(masProcTag_OdoRel[i], aoInputVar[i]);
                            ////logEntry_ProcTag_OdoRel.Add(masProcTag_OdoRel[i], aoInputVar[i] as JToken);
                            logEntry_ProcTag_OdoRel.Add(masProcTag_OdoRel[i], JToken.FromObject(aoInputVar[i]));

                            ////logEntry_ProcTag_OdoRel[masProcTag_OdoRel[i]] = aoInputVar[i] as JToken;
                            //logEntry_ProcTag_OdoRel[masProcTag_OdoRel[i]] = JToken.FromObject(aoInputVar[i]);
                        }
                    }
                }           // # if (aoInputVar == null)

                var json = logEntry_ProcTag_OdoRel.ToString(Newtonsoft.Json.Formatting.None);
                ////json = json.Replace("RPT_TIME", "@timestamp");

                if (bEnableProcJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                {
                    //LogManager.GetLogger("OHT_Meas_JSON4Walk").Info(json);
                    mLoggerMeas_JSON4Walk.Log(eLogLv, json);
                }

                if (bEnableExtraRawFileSaving)
                {
                    mLoggerProc_Raw_300OHTM.Log(eLogLv, json);                       // extra ... 

                    //++++++++++++++                // Roy-+180904
                    //mLogEvInfo_ProcTag_OdoRel.Level = eLogLv;
                    ////mLogEvInfo_ProcTag_OdoRel.LoggerName = "OHT_Proc_OdoRel_Raw4Walk";
                    //mLogEvInfo_ProcTag_OdoRel.Message = json;

                    //mLogEvInfo_ProcTag_OdoRel.TimeStamp = dtNowSure;

                    //mLoggerProc_Raw.Log(mLogEvInfo_ProcTag_OdoRel);
                    //++++++++++++++
                }
            }

            return;
        }


        //internal static void Fun_Log_ProcTag_Hoist(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraRawFileSaving, params object[] aoInputVar)               // Roy-180621
        internal static void Fun_Log_ProcTag_Hoist(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraRawFileSaving, bool bEnableProcJsonFileSaving, params object[] aoInputVar)               // Roy+180621
        {
            Object thisLock = new Object();

            lock (thisLock)
            {
                dynamic logEntry_ProcTag_Hoist = new JObject();
                ////JObject logEntry_ProcTag_Hoist = new JObject();

                DateTime dtNowSure;

                if (dtNow == null)
                    dtNowSure = DateTime.Now;
                else
                    dtNowSure = (DateTime)dtNow;

                ////logEntry_ProcTag_Hoist.RPT_TIME = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);
                logEntry_ProcTag_Hoist.Add("@timestamp", dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture));
                //logEntry_ProcTag_Hoist["@timestamp"] = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);

                logEntry_ProcTag_Hoist.Site = sSite;
                logEntry_ProcTag_Hoist.ToolID = sToolID;
                logEntry_ProcTag_Hoist.LogLevel = eLogLv.ToString();

                if ((masProcTag_Hoist == null) || (masProcTag_Hoist.Count() == 0))
                {
                    masProcTag_Hoist = msProcTag_Hoist.Split(',');
                }

                if (aoInputVar != null)
                {
                    if (masProcTag_Hoist.Count() <= aoInputVar.Count())
                    {
                        for (int i = 0; i < masProcTag_Hoist.Count(); i++)
                        {
                            ////logEntry_ProcTag_Hoist.Add(masProcTag_Hoist[i], aoInputVar[i]);
                            ////logEntry_ProcTag_Hoist.Add(masProcTag_Hoist[i], aoInputVar[i] as JToken);
                            logEntry_ProcTag_Hoist.Add(masProcTag_Hoist[i], JToken.FromObject(aoInputVar[i]));

                            ////logEntry_ProcTag_Hoist[masProcTag_Hoist[i]] = aoInputVar[i] as JToken;
                            //logEntry_ProcTag_Hoist[masProcTag_Hoist[i]] = JToken.FromObject(aoInputVar[i]);
                        }
                    }
                }           // # if (aoInputVar == null)

                var json = logEntry_ProcTag_Hoist.ToString(Newtonsoft.Json.Formatting.None);
                ////json = json.Replace("RPT_TIME", "@timestamp");

                if (bEnableProcJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                {
                    //LogManager.GetLogger("OHT_Meas_JSON4Hoist").Info(json);
                    mLoggerMeas_JSON4Hoist.Log(eLogLv, json);
                }

                if (bEnableExtraRawFileSaving)
                {
                    mLoggerProc_Raw_300OHTM.Log(eLogLv, json);                       // extra ... 

                    //++++++++++++++                // Roy-+180904
                    //mLogEvInfo_ProcTag_Hoist.Level = eLogLv;
                    ////mLogEvInfo_ProcTag_Hoist.LoggerName = "OHT_Proc_Hoist_Raw4Walk";
                    //mLogEvInfo_ProcTag_Hoist.Message = json;

                    //mLogEvInfo_ProcTag_Hoist.TimeStamp = dtNowSure;

                    //mLoggerProc_Raw.Log(mLogEvInfo_ProcTag_Hoist);
                    //++++++++++++++
                }
            }

            return;
        }


        //internal static void Fun_Log_GeneralProc(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraRawFileSaving, string sMsgBody, params object[] aoInputVar)               // Roy-180621
        internal static void Fun_Log_GeneralProc(string sSite, string sToolID, LogLevel eLogLv, DateTime? dtNow, bool bEnableExtraRawFileSaving, bool bEnableProcJsonFileSaving, string sMsgBody, params object[] aoInputVar)               // Roy+180621
        {
            Object thisLock = new Object();

            lock (thisLock)
            {
                dynamic logEntry_GeneralProc = new JObject();
                ////JObject logEntry_GeneralProc = new JObject();

                DateTime dtNowSure;

                if (dtNow == null)
                    dtNowSure = DateTime.Now;
                else
                    dtNowSure = (DateTime)dtNow;

                if (bEnableProcJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                {
                    ////logEntry_GeneralProc.RPT_TIME = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);
                    logEntry_GeneralProc.Add("@timestamp", dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture));
                    //logEntry_GeneralProc["@timestamp"] = dtNowSure.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz", System.Globalization.CultureInfo.InvariantCulture);

                    logEntry_GeneralProc.Site = sSite;
                    logEntry_GeneralProc.ToolID = sToolID;
                    logEntry_GeneralProc.LogLevel = eLogLv.ToString();
                }

                string sMsg = String.Format(sMsgBody, aoInputVar);

                if (bEnableProcJsonFileSaving)                       // toggle switch ...                   // Roy+180621
                {
                    logEntry_GeneralProc.Msg = sMsg;

                    var json = logEntry_GeneralProc.ToString(Newtonsoft.Json.Formatting.None);
                    ////json = json.Replace("RPT_TIME", "@timestamp");

                    //LogManager.GetLogger("OHT_Proc_JSON").Info(json);
                    mLoggerProc_JSON.Log(eLogLv, json);
                }

                if (bEnableExtraRawFileSaving)
                {
                    mLoggerProc_Raw_300OHTM.Log(eLogLv, sMsg);                       // extra ... 

                    //++++++++++++++                // Roy-+180904
                    //mLogEvInfo_GeneralProc.Level = eLogLv;
                    ////mLogEvInfo_GeneralProc.LoggerName = "OHT_Proc_Raw4Walk";
                    //mLogEvInfo_GeneralProc.Message = sMsg;

                    //mLogEvInfo_GeneralProc.TimeStamp = dtNowSure;

                    //mLoggerProc_Raw.Log(mLogEvInfo_GeneralProc);
                    //++++++++++++++
                }
            }

            return;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++    
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        public static void Fun_Log(MyLogKind eLogKind, LogLevel eLogLv, DateTime? dtNow, string sMsgBody, params object[] aoInputVar)
        {
            //if (!eq.bIsSimMode)
            //{
            //    msToolID = eq.Param.SYSTEM.機台.機台名稱;
            //}
            //else
            //{
            //    msToolID = System.Environment.MachineName.ToLowerInvariant() + ":" + eq.Param.SYSTEM.機台.機台名稱;
            //}

            //Fun_Log_Generic(eLogKind, msSite, msToolID, eLogLv, dtNow, sMsgBody, aoInputVar);             // Roy-180516

            //+++++++++++++++++++++++++++++++++++++             // Roy+180516
            switch (eLogKind)
            {
                case MyLogKind.MeasurementData4Walk:
                    ////Fun_Log_MeasData4Walk(msSite, msToolID, eLogLv, dtNow, true, sMsgBody, aoInputVar);
                    //Fun_Log_MeasData4Walk(msSite, msToolID, eLogLv, dtNow, true, aoInputVar);               // Roy-180621
                    Fun_Log_MeasData4Walk(msSite, msToolID, eLogLv, dtNow, gbEnableExtraCsvFileSaving, gbEnableMeasJsonFileSaving, aoInputVar);               // Roy+180621
                    break;

                case MyLogKind.MeasurementData4Hoist:
                    //Fun_Log_MeasData4Hoist(msSite, msToolID, eLogLv, dtNow, true, aoInputVar);               // Roy-180621
                    Fun_Log_MeasData4Hoist(msSite, msToolID, eLogLv, dtNow, gbEnableExtraCsvFileSaving, gbEnableMeasJsonFileSaving, aoInputVar);               // Roy+180621
                    break;

                case MyLogKind.ProcessTag_OdometerRelative:
                    //Fun_Log_ProcTag_OdoRel(msSite, msToolID, eLogLv, dtNow, true, aoInputVar);               // Roy-180621
                    Fun_Log_ProcTag_OdoRel(msSite, msToolID, eLogLv, dtNow, gbEnableExtraRawFileSaving, gbEnableProcJsonFileSaving, aoInputVar);               // Roy+180621
                    break;

                case MyLogKind.ProcessTag_Hoist:
                    //Fun_Log_ProcTag_Hoist(msSite, msToolID, eLogLv, dtNow, true, aoInputVar);               // Roy-180621
                    Fun_Log_ProcTag_Hoist(msSite, msToolID, eLogLv, dtNow, gbEnableExtraRawFileSaving, gbEnableProcJsonFileSaving, aoInputVar);               // Roy+180621
                    break;

                //case MyLogKind.GeneralProcess:
                default:
                    //Fun_Log_GeneralProc(msSite, msToolID, eLogLv, dtNow, true, sMsgBody, aoInputVar);               // Roy-180621
                    Fun_Log_GeneralProc(msSite, msToolID, eLogLv, dtNow, gbEnableExtraRawFileSaving, gbEnableProcJsonFileSaving, sMsgBody, aoInputVar);               // Roy+180621
                    break;
            }
            //+++++++++++++++++++++++++++++++++++++

            return;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        //public async static void Fun_WrProcLog(string log, bool isFail = false)
        //public static bool Fun_WrProcLog(string log, bool isFail = false)                   // Roy-170721
        public static bool Fun_WrProcLog(string log, StreamWriter xSW = null)                    // Roy+170721
        {
            lock (oLock)
            {
                if (log == "") return true;

                try
                {
                    if (xSW == null)              // Roy+170721
                        Directory.CreateDirectory(eq.PROC_LOG_PATH);

                    /*                // Roy-170721
                   StreamWriter sw = new StreamWriter(eq.PROC_LOG_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                   sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);                          // Roy*170622
                   sw.Close();
                    */

                    //+++++++++++++++          // Roy+170721
                    if (xSW == null)
                    {
                        using (StreamWriter sw = File.AppendText(eq.PROC_LOG_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                        {
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                            //await CopyFilesAsync(log, sw);
                        }
                    }
                    else
                    {
                        xSW.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.Flush();

                        //xSW.WriteLineAsync(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.FlushAsync();
                    }
                    //+++++++++++++++

                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }

        public static bool Fun_WrProc權限(string log, bool isFail = false)
        {
            lock (oLock)
            {
                if (log == "") return true;

                try
                {
                    Directory.CreateDirectory(eq.PROC_權限_PATH);

                    StreamWriter sw = new StreamWriter(eq.PROC_權限_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);                          // Roy*170718
                    sw.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    string err = ex.Message + Environment.NewLine + ex.StackTrace;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null,err);
                    return false;
                }
            }
        }

        //public static bool Fun_WrCCDLog(string log, bool isFail = false)                   // Roy-170721
        public static bool Fun_WrCCDLog(string log, StreamWriter xSW = null)                   // Roy+170721
        {
            lock (oLock)
            {
                if (log == "") return true;

                try
                {
                    if (xSW == null)              // Roy+170721
                        Directory.CreateDirectory(eq.PROC_CCD_PATH);

                    /*                // Roy-170721
                    StreamWriter sw = new StreamWriter(eq.PROC_CCD_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                    sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);                          // Roy*170718
                    sw.Close();
                    */

                    //+++++++++++++++          // Roy+170721
                    if (xSW == null)
                    {
                        using (StreamWriter sw = File.AppendText(eq.PROC_CCD_PATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt"))
                        {
                            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        }
                    }
                    else
                    {
                        xSW.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.Flush();

                        //xSW.WriteLineAsync(DateTime.Now.ToString("HH:mm:ss.fff") + "    " + log);
                        ////xSW.FlushAsync();
                    }
                    //+++++++++++++++

                    return true;
                }
                catch (Exception ex)                  // Roy*170721
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@eqTool:  Fun_WrCCDLog \\  Exception = " + ex.Message);                  // Roy+170721
                    return false;
                }
            }
        }

        public static bool Fun_Log_Sub志銘版時間紀錄(Def_Common.tyTacTimeLog tq)
        {
            lock (oLock)
            {

                try
                {
                    if (Directory.Exists(eq.cnt_DATA_PATH) == false)
                        Directory.CreateDirectory(eq.cnt_DATA_PATH);
                    string filename = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

                    filename = tq.SubTitle + "_" + filename + ".log";

                    StreamWriter oF = new StreamWriter(eq.cnt_SUBTACTTIME_LOG_PATH + "_2" + @"\" + filename, true);
                    TimeSpan tmp = new TimeSpan();

                    for (int i = 0; i < tq.item.Count(); i++)
                    {
                        tmp = eq.tq.End[i] - eq.tq.Start[i];
                        oF.WriteLine(eq.tq.item[i].ToString() + " ," +
                           eq.tq.Start[i].ToString("HH:mm:ss:fff") + " ," + eq.tq.End[i].ToString("HH:mm:ss:fff")
                           + " ," + tmp.TotalSeconds.ToString());
                    }
                    tmp = eq.tq.SubEnd - eq.tq.SubStart;
                    oF.WriteLine(eq.tq.SubTitle + "總共使用時間(ss)" + " : " +
                       tmp.TotalSeconds.ToString());

                    oF.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static void Fun_Rs232_OpenPort(int PortNum, int Baudrate)
        {
            string PortName = "";

            PortName = "COM" + PortNum.ToString();

            Rs232Port = new SerialPort(PortName, Baudrate, Parity.None, 8, StopBits.One);
            //Rs232Port.DataReceived += new SerialDataReceivedEventHandler(Fun_Rs232_ReceivedData);
            if (Rs232Port.IsOpen == false)
                Rs232Port.Open();       //打開

        }

        public static void Fun_Rs232_ClosePort()
        {
            Rs232Port.Close();
        }

        public static void Fun_Rs232_ReceivedData(object sender, SerialDataReceivedEventArgs e)
        {
            //讀入字串
            if (Rs232Port.IsOpen == true)
                ReciveData = Rs232Port.ReadExisting();
            //Console.WriteLine("Receive: " + data);

            /*
             //讀入位元組
            int bytes = port.BytesToRead;
            byte[] comBuffer = new byte[bytes];
            port.Read(comBuffer, 0, bytes);

            Console.WriteLine(comBuffer);
            */
        }

        public static void Fun_Rs232_SendData(string DataString)
        {
            //方法一
            Rs232Port.Write(DataString);
            //////方法二
            ////Rs232Port.Write(new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x02 }, 0, 6);

        }

        public static bool Fun_RFPIO_Set_CH(string Channel)
        {
            string str = "";
            int i;

            Rs232Port.ReadExisting();

            str = "<C=" + Channel + ">";
            Fun_Rs232_SendData(str);
            Thread.Sleep(500);
            //Fun_Rs232_SendData(str);

            ReciveData = "";
            ReciveData = Rs232Port.ReadExisting();

            if (ReciveData == "")
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_CH 通訊失敗");
                return false;
            }

            char[] value = ReciveData.ToCharArray();

            str = "";
            for (i = 3; i < 20; i++)
            {
                if (value.Length < i)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_CH Wrong Data");
                    return false;
                }

                if (value[i] != ']')
                    str += value[i];
                else
                {
                    if (Channel != Convert.ToString(str))      //發送與接收不相同
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_CH Fail");
                        return false;
                    }
                    else
                        break;
                }
            }
            return true;
        }

        public static bool Fun_RFPIO_Set_ID(string Address)
        {
            string str = "";
            int i;

            Rs232Port.ReadExisting();

            str = "<A=900A-" + Address + ">";
            Fun_Rs232_SendData(str);
            Thread.Sleep(500);
            //Fun_Rs232_SendData(str);

            ReciveData = "";
            ReciveData = Rs232Port.ReadExisting();

            if (ReciveData == "")
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_ID 通訊失敗");
                return false;
            }

            char[] value = ReciveData.ToCharArray();

            str = "";
            for (i = 8; i < 20; i++)
            {
                if (value.Length < i)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_ID Wrong Data");
                    return false;
                }

                if (value[i] != ']')
                    str += value[i];
                else
                {
                    if (Address != Convert.ToString(str))      //發送與接收不相同
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_ID Fail");
                        return false;
                    }
                    else
                        break;
                }
            }
            return true;
        }

        public static bool Fun_RFPIO_Set_CH_ID(string Channel, string Address)
        {
            string str = "";
            int i, j;

            Rs232Port.ReadExisting();

            str = "<B=900A-" + Address + ":" + Channel + ">";
            Fun_Rs232_SendData(str);
            Thread.Sleep(500);
            //Fun_Rs232_SendData(str);

            ReciveData = "";
            ReciveData = Rs232Port.ReadExisting();

            if (ReciveData == "")
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_CH_ID 通訊失敗");
                return false;
            }

            char[] value = ReciveData.ToCharArray();

            str = "";
            for (i = 8; i < 20; i++)
            {
                if (value.Length < i)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_CH_ID Wrong Data");
                    return false;
                }

                if (value[i] != ':')
                    str += value[i];
                else
                {
                    if (Address != Convert.ToString(str))      //發送與接收不相同
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_CH_ID Fail");
                        return false;
                    }
                    else
                        break;
                }
            }

            str = "";
            for (j = i + 1; j < 20; j++)
            {
                if (value.Length < i)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_CH_ID Wrong Data");
                    return false;
                }

                if (value[j] != ']')
                    str += value[j];
                else
                {
                    if (Channel != Convert.ToString(str))      //發送與接收不相同
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_Set_CH_ID Fail");
                        return false;
                    }
                    else
                        break;
                }
            }
            return true;
        }

        public static bool Fun_RFPIO_RealTime_Watch(string Value)
        {
            string str = "";
            int i;

            Rs232Port.ReadExisting();

            str = "<D=" + Value + ">";
            Fun_Rs232_SendData(str);
            Thread.Sleep(500);
            //Fun_Rs232_SendData(str);

            ReciveData = "";
            ReciveData = Rs232Port.ReadExisting();

            if (ReciveData == "")
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_RealTime_Watch 通訊失敗");
                return false;
            }

            char[] value = ReciveData.ToCharArray();

            str = "";
            for (i = 3; i < 20; i++)
            {
                if (value.Length < i)
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_RealTime_Watch Wrong Data");
                    return false;
                }

                if (value[i] != ']')
                    str += value[i];
                else
                {
                    if (Value != Convert.ToString(str))      //發送與接收不相同
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@eqTool:  Fun_RFPIO_RealTime_Watch Fail");
                        return false;
                    }
                    else
                        break;
                }
            }
            return true;
        }
    }
}
