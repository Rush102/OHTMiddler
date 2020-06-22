using OHTM.StatusMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Veh_HandShakeData;

namespace OHTM
{
    class Fake144ForChargeMode
    {
        public void Fake144forChargeMode(bool done)
        {
            changing_chargestatus fakecheck = new changing_chargestatus();
            /*
             * if fake144 check == checked;
             */
            if (done == true && Veh_VehM_Global.chargeport == true)
            {
                Thread TStart_changing_chargestatus = new Thread(
                new ThreadStart(fakecheck.Start_changing_chargestatus));

                // Start the thread.
                TStart_changing_chargestatus.Start();
            }
        }
        
    }
    public class changing_chargestatus
    {
        public void Start_changing_chargestatus()
        {
            if(Veh_VehM_Global.fake144chargecheck == true)
            {
                MotionInfo_Vehicle_Inter_Comm_ReportData tempfor144 = new MotionInfo_Vehicle_Inter_Comm_ReportData();

                tempfor144.Address = Veh_VehM_Global.Address;
                tempfor144.Section = Veh_VehM_Global.Section;
                tempfor144.BlockControlSection = Veh_VehM_Global.BlockControlSection;
                tempfor144.cmpCode = Veh_VehM_Global.cmpCode;
                tempfor144.cmpStatus = Veh_VehM_Global.cmpStatus;
                tempfor144.DistanceFromSectionStart = Veh_VehM_Global.DistanceFromSectionStart;
                tempfor144.WalkLength = Veh_VehM_Global.vehWalkLength;
                
                tempfor144.vehLeftGuideLockStatus = Veh_VehM_Global.vehLeftGuideLockStatus;
                tempfor144.vehRightGuideLockStatus = Veh_VehM_Global.vehRightGuideLockStatus;
                tempfor144.ConrtolMode = Veh_VehM_Global.vehModeStatus_fromVehC;
                tempfor144.vehLoadStatus = Veh_VehM_Global.vehLoadStatus;
                tempfor144.vehPauseStatus = Veh_VehM_Global.vehPauseStatus;
                tempfor144.eventTypes = Veh_VehM_Global.eventTypes;
                tempfor144.vehBlockStopStatus = Veh_VehM_Global.vehBlockStopStatus;
                tempfor144.vehObstacleStopStatus = Veh_VehM_Global.vehObstStopStatus;
                tempfor144.vehObstDist = Veh_VehM_Global.vehObstDist;
                tempfor144.loadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_Load;
                tempfor144.unLoadStatus.Veh_CSTID = Veh_VehM_Global.CSTID_UnLoad;
                tempfor144.vehActionStatus = Veh_VehM_Global.vehActionStatus;
                tempfor144.HIDControlSection = Veh_VehM_Global.HIDControlSection;
                tempfor144.ChargeStatus = 1;



                //new Veh_VehM().SendValuesForRept(nothing, "132");
                ///
                /// Write the situation to the host.
                ///
                Veh_VehM_Global.vehVehM.SendValuesForRept(tempfor144, "144");

                Thread.Sleep(1000);
                tempfor144.ChargeStatus = 2;
                Veh_VehM_Global.vehVehM.SendValuesForRept(tempfor144, "144");

                
            }
        }
    }
}
