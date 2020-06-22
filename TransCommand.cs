using Veh_HandShakeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpIpClientSample;
using OHTM.NLog_USE;
using OHTM.StatusMachine;

namespace OHTM
{
    class TransCommand
    {
        public static EventType transCommand_EventType(int oht_EventType )
        {
            EventType VehM_EventType = 0;
            switch ((VehEventTypes)oht_EventType)
            {
                case VehEventTypes.Load_Arrived:
                    VehM_EventType = EventType.LoadArrivals;
                    break;
                //case VehEventTypes.Load_Start:
                //    VehM_EventType = EventType.
                //    break;
                case VehEventTypes.Load_Pick:
                    VehM_EventType = EventType.Vhloading;
                    break;
                case VehEventTypes.Load_Complete:
                    VehM_EventType = EventType.LoadComplete;
                    break;
                case VehEventTypes.Unload_Arrived:
                    VehM_EventType = EventType.UnloadArrivals;
                    break;
                //case VehEventTypes.Unload_Start:
                //    VehM_EventType = EventType
                //    break;
                case VehEventTypes.Unload_Place:
                    VehM_EventType = EventType.Vhunloading;
                    break;
                case VehEventTypes.Unload_Complete:
                    VehM_EventType = EventType.UnloadComplete;
                    break;
                case VehEventTypes.Address_Arrival:
                    VehM_EventType = EventType.AdrPass;
                    break;
                case VehEventTypes.Address_Pass:
                    VehM_EventType = EventType.AdrPass;
                    break;
                case VehEventTypes.Moving_Pause:
                    //VehM_EventType = EventType.MovePause;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "Moving_Pause Error");
                    break;
                case VehEventTypes.Moving_Restart:
                    //VehM_EventType = EventType.MoveRestart;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "Moving_Restart Error");
                    break;
                case VehEventTypes.BlockSection_Query:
                    VehM_EventType = EventType.BlockReq;
                    break;
                case VehEventTypes.HIDSection_Query:
                    //VehM_EventType = EventType.Hidreq;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "HIDSection_Query Error");
                    break;
                case VehEventTypes.PostBlockSectionExit:
                    VehM_EventType = EventType.BlockRelease;
                    break;
                case VehEventTypes.PostHIDSectionExit:
                    //VehM_EventType = EventType.Hidrelease;
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "PostHIDSectionExit Error");
                    break;
                case VehEventTypes.Moving_Complete:
                    VehM_EventType = EventType.AdrOrMoveArrivals;
                    break;
                //case VehEventTypes.ReserveSection_Query:
                //    VehM_EventType = EventType.ReserveReq;
                //    break;
            }
            return VehM_EventType;
        }
    }

    class TransCommand_StopCommand
    {
        public static string transCommand_StopCommand(int stopCommand)
        {
            string tempString = "";
            string allStopString = "";
            tempString = Convert.ToString(stopCommand, 2);
            //Console.WriteLine(tempString);
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "stopCommand = {0}", stopCommand);
            if(stopCommand != 0)
            {
                Veh_VehM_Global.reallyStop = true;
            }
            else
            {
                Veh_VehM_Global.reallyStop = false;
            }
            //Console.WriteLine("stopCommand = {0}",stopCommand);
            for(int i = 0; i< tempString.Length; i++)
            {
                //Console.WriteLine(tempString[i]);
                allStopString = allStopString + tempString[tempString.Length - i - 1];
                //Console.WriteLine(allStopString);
            }
            if(allStopString.Length < 17)
            {
                for (int i = allStopString.Length; i < 17; i++)
                {
                    //Console.WriteLine(tempString[i]);
                    allStopString = allStopString + "0";
                    Console.WriteLine(allStopString);
                }
            }
            //string a = allStopString[13].ToString();
            //string b = allStopString[11].ToString();
            //Console.WriteLine();
            return allStopString;
        }
    }
}
