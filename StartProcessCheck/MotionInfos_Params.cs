using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OHTM.NLog_USE;
using static System.Net.Mime.MediaTypeNames;

namespace OHTM.StartProcessCheck
{
    public class MotionInfos_Params
    {
        private bool initialResult = false;
        
        public bool InitialResult
        {
            get
            {
                return initialResult;
            }
        }
        
        public MotionInfos_Params()
        {
            initialResult = Initial_motionInfo_params();
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "initialResult = {0}", InitialResult.ToString());
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "InitialResult = {0}", InitialResult.ToString());
        }

        public bool Initial_motionInfo_params()
        {
            try
            {
                #region Declare new class
                Generate_new_class _New_Class = new Generate_new_class();
                #endregion
                return true;
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
