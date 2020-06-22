using MirleOHT.類別.DCPS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veh_HandShakeData;

namespace OHTM.StartProcessCheck
{
    class Generate_new_class
    {
        public Generate_new_class()
        {
            //++++++++++++++++++++++++++++++++++++++++++                    // Roy+171204
            DDS_Global.motionInfoClientData = new Veh_HandShakeData.MotionInfo_Client();
            DDS_Global.motionInfoServerData = new Veh_HandShakeData.MotionInfo_Server();
            //++++++++++++++++++++++++++++++++++++++++++ 

            MotionInfo_Vehicle_Comm vehCommData = new MotionInfo_Vehicle_Comm();

            DDS_Global.motionInfoInterCommReptData = new MotionInfo_Vehicle_Inter_Comm_ReportData();
            DDS_Global.motionInfoInterCommSendData = new MotionInfo_Inter_Comm_SendData();
            DDS_Global.motionInfoHandShakeRxData = new MotionInfo_HandShake_RecieveData();
            DDS_Global.motionInfoHandShakeTxData = new MotionInfo_HandShake_SendData();

            DDS_Global.vehHandShakeRxDataInst = new DDS.InstanceHandle();
            DDS_Global.vehHandShakeSendDataInst = new DDS.InstanceHandle();
            DDS_Global.vehInterCommReptDataInst = new DDS.InstanceHandle();
            DDS_Global.vehInterCommSendDataInst = new DDS.InstanceHandle();
        }
    }
}
