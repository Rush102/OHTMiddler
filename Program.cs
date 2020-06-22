using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using MirleOHT.類別.DCPS;
using Veh_HandShakeData;
using OHTM.NLog_USE;
using Console = System.Diagnostics.Debug;                   // Roy+180524 ... for VS2017~ 
using OHTM.StartProcessCheck;

namespace OHTM
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG

#endif

            #region Check whether the process is opened repeatedly.
            Check_same_process_name start_check_obj = new Check_same_process_name();
            if (start_check_obj.CheckSameProcesExist() == false)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "Check same proces has already exist.");
                return;
            }
            #endregion

            #region Check whether DDS and  initial OK.
            MotionInfos_Params motionInfos_ = new MotionInfos_Params();
            if (motionInfos_.InitialResult == false)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "DDS Parameter Initial fails.");
            }
            #endregion

            StartWinForm();
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Warn, null, "StartWinForm succeed.");
        }
        
        static void StartWinForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        internal static class NativeMethod
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern int AllocConsole();
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern int FreeConsole();
        }


    }
}
