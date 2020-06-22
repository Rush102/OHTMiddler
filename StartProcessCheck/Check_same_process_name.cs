using OHTM.NLog_USE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHTM.StartProcessCheck
{
    class Check_same_process_name
    {
        private string MName;
        private string PName;
        /// <summary>
        /// Confirm whether to open itself repeatedly.
        /// If the answer is yes, close the process.
        /// </summary>
        public bool CheckSameProcesExist()
        {
            try
            {
                MName = Process.GetCurrentProcess().MainModule.ModuleName;
                PName = System.IO.Path.GetFileNameWithoutExtension(MName);
                Process[] myProcess = Process.GetProcessesByName(PName);
                Process current = Process.GetCurrentProcess();
                if (myProcess.Length > 1)
                {
                    Console.WriteLine("本程序一次只能執行一個！", "提示");
                    current.Close();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return false;
            }
        }
    }
}
