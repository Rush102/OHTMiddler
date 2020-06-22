using OHTM.NLog_USE;
using OHTM.StatusMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veh_HandShakeData;

namespace OHTM.ErrorCode
{
    public class ErrorCode
    {
        public ErrorCode()
        {

        }
        //public void errorCodeOfPIO(MotionInfo_Vehicle_Inter_Comm_ReportData data, out bool _911point, out bool Wdone, string errorCode)
        //{
        //    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@Veh_VehM : !!!!!!!VLVUL PIO T1-T4 Timeout. errorCode = {0}", errorCode);
        //    Veh_VehM_Global_Property.abort_On_Check = true;
        //    Veh_VehM_Global.start_loading_or_unloading = false;
        //    _911point = false;
        //    Wdone = true;
        //    SendValuesForRept(data, "194", 0);
        //    data.cmpStatus = 64;
        //    SendValuesForRept(data, "132");
        //    data.vehActionStatus = 0;
        //    SendValuesForRept(data, "144");
        //}
    }
}
