using OHTM.NLog_USE;
using OHTM.StatusMachine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpIpClientSample;

namespace OHTM.ReadCsv
{
    class CheckSections //jason++ 181022
    {
        public static void ClassifySections(ref string[] LoadSections, ref string[] UnLoadSections)
        {
            DataTable dt = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.NowFileDir);
            string loadAdr = Veh_VehM_Global.LoadAddress;
            string unloadAdr = Veh_VehM_Global.UnloadAddress;
            string[] passSections = Veh_VehM_Global.GuideSections;
            DataRow[] drselectload = dt.Select("TO_ADR_ID = '" + loadAdr + "'");
            DataRow[] drselectunload = dt.Select("TO_ADR_ID = '" + unloadAdr + "'");
            int splitnum = -1;
            bool check = false;
            int halfsignal;
            /////////////////////////////////
            #region Load Sections
            //Load Sections
            /////////////////////////////////
            for (int i = 0; i < drselectload.Count(); i++)
            {
                string loadsectemp = (string)drselectload[i].ItemArray[0];
                string loadsect = loadsectemp.PadLeft(4, '0');
                check = false;

                for (int j = 0; j < passSections.Count(); j++)
                {

                    if (loadsect == passSections[j])
                    {
                        splitnum = j + 1;
                        check = true;
                        break;
                    }
                }

                if (check == true) // 181022 Due to that if there has the redrive section, we shouldpick the first one;
                {
                    break;
                }
            }

            if (splitnum == -1)
            {
                Console.WriteLine("Something Wrong ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Something Wrong ~~ there should have a section that end at the load address.");
                Console.WriteLine("Something Wrong ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            else
            {
                string[] loadSecArray = new string[splitnum];

                for (int i = 0; i < splitnum; i++)
                {
                    loadSecArray[i] = passSections[i];
                }
                LoadSections = loadSecArray;
            }
            ///<summary>
            ///if there is no section from now to load address , the halfsignal should be 0 ; 
            ///</summary>
            if (splitnum != -1)
            {
                halfsignal = splitnum;
            }
            else
            {
                halfsignal = 0;
            }
            #endregion
            /////////////////////////////////
            #region Unload Sections
            //Unload Sections
            /////////////////////////////////
            int unloadStart = 0;
            if (splitnum != -1)
            {
                unloadStart = splitnum - 1;
            }

            for (int i = 0; i < drselectunload.Count(); i++)
            {
                string unloadsectemp = (string)drselectunload[i].ItemArray[0];
                string unloadsect = unloadsectemp.PadLeft(4, '0');
                check = false;

                for (int j = unloadStart; j < passSections.Count(); j++)
                {

                    if (unloadsect == passSections[j])
                    {
                        splitnum = j + 1;
                        check = true;
                        break;
                    }
                }

                if (check == true) // 181022 Due to that if there has the redrive section, we shouldpick the first one;
                {
                    break;
                }
            }

            if (splitnum == -1)
            {
                Console.WriteLine("Something Wrong ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Something Wrong ~~ there should have a section that end at the unload address.");
                Console.WriteLine("Something Wrong ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            else
            {
                string[] unloadSecArray = new string[splitnum - halfsignal];

                for (int i = halfsignal; i < splitnum; i++)
                {
                    unloadSecArray[i - halfsignal] = passSections[i];
                }
                UnLoadSections = unloadSecArray;
            }
            #endregion
            /////////////////////////////////
        }
        public static bool check_the_start_Address_ForOHT(string section_Num)
        {

            bool checkthestartaddress = false;
            DataTable dt = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.NowFileDir);
            int tempsection = Int32.Parse(section_Num);
            string tempsection_1 = tempsection.ToString();
            DataRow[] drselectsection = dt.Select("SEC_ID = '" + tempsection_1 + "'");
            if (drselectsection[0].ItemArray[0] != null)
            {
                string temp = (string)drselectsection[0].ItemArray[6];
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data :temp = {0}", temp.ToString());
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "@Veh_VehM_Comm_Data :Veh_VehM_Global.Address = {0}", Veh_VehM_Global.Address.ToString());
                if (temp == Veh_VehM_Global.Address)
                {
                    checkthestartaddress = true;
                }
            }
            return checkthestartaddress;
        }
        public static string FindSectionOfAddress(string address)
        {
            try
            {
                DataTable dt = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.NowFileDir);
                DataRow[] rowsHaveTargetAddress = dt.Select("TO_ADR_ID = '" + address + "'");

                foreach (var row in rowsHaveTargetAddress)
                {
                    string testSection = row.ItemArray[0].ToString().PadLeft(4, '0');
                    if (Veh_VehM_Global.GuideSections.Contains(testSection))
                    {
                        return testSection;
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                string err = ex.Message + Environment.NewLine + ex.StackTrace;
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, err);
                return err;  
            }
        }
        public static void OverrideAnalyze(Veh_VehM_Global.ActionType Last_Cmd, ref string[] LoadSections, ref string[] UnLoadSections)
        {
            DataTable dt = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.NowFileDir);
            //string[] passSections = Veh_VehM_Global.GuideSections;//0304
            int splitnum = -1;
            bool check = false;
            int halfsignal;
            //DataTable dt = ReadCsv._ReadParamsFromVhSectionData();
            string moveAdr = Veh_VehM_Global.Address;
            string loadAdr = Veh_VehM_Global.LoadAddress;
            string unloadAdr = Veh_VehM_Global.UnloadAddress;
            string aim_Adr;
            switch (Last_Cmd)
            {
                case Veh_VehM_Global.ActionType.Move:
                    aim_Adr = moveAdr;

                    break;
                case Veh_VehM_Global.ActionType.Load:
                    aim_Adr = loadAdr;

                    LoadSections = Veh_VehM_Global.GuideSectionsStartToLoad;
                    break;
                case Veh_VehM_Global.ActionType.UnLoad:
                    aim_Adr = unloadAdr;
                    UnLoadSections = Veh_VehM_Global.GuideSections;
                    break;
                case Veh_VehM_Global.ActionType.Load_Unload:
                    if (Veh_VehM_Global.hasCst == VhLoadCSTStatus.Exist)
                    {

                    }
                    else
                    {
                        CheckSections Temp = new CheckSections();
                        Temp.Set_New_Section(loadAdr, unloadAdr, ref LoadSections, ref UnLoadSections);
                    }
                    break;
            }
        }
        protected void Set_New_Section(string loadAdr, string unloadAdr, ref string[] LoadSections, ref string[] UnLoadSections)
        {
            DataTable dt = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.NowFileDir);
            string[] passSections = Veh_VehM_Global.GuideSections; //0304
            DataRow[] drselectload = dt.Select("TO_ADR_ID = '" + loadAdr + "'");
            DataRow[] drselectunload = dt.Select("TO_ADR_ID = '" + unloadAdr + "'");
            int splitnum = -1;
            bool check = false;
            int halfsignal;
            /////////////////////////////////
            #region Load Sections
            //Load Sections
            /////////////////////////////////
            for (int i = 0; i < drselectload.Count(); i++)
            {
                string loadsectemp = (string)drselectload[i].ItemArray[0];
                string loadsect = loadsectemp.PadLeft(4, '0');
                check = false;

                for (int j = 0; j < passSections.Count(); j++)
                {

                    if (loadsect == passSections[j])
                    {
                        splitnum = j + 1;
                        check = true;
                        break;
                    }
                }

                if (check == true) // 181022 Due to that if there has the redrive section, we shouldpick the first one;
                {
                    break;
                }
            }

            if (splitnum == -1)
            {
                Console.WriteLine("Something Wrong ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Something Wrong ~~ there should have a section that end at the load address.");
                Console.WriteLine("Something Wrong ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            else
            {
                string[] loadSecArray = new string[splitnum];

                for (int i = 0; i < splitnum; i++)
                {
                    loadSecArray[i] = passSections[i];
                }
                LoadSections = loadSecArray;
            }
            ///<summary>
            ///if there is no section from now to load address , the halfsignal should be 0 ; 
            ///</summary>
            if (splitnum != -1)
            {
                halfsignal = splitnum;
            }
            else
            {
                halfsignal = 0;
            }
            #endregion
            /////////////////////////////////
            #region Unload Sections
            //Unload Sections
            /////////////////////////////////
            int unloadStart = 0;
            if (splitnum != -1)
            {
                unloadStart = splitnum - 1;
            }

            for (int i = 0; i < drselectunload.Count(); i++)
            {
                string unloadsectemp = (string)drselectunload[i].ItemArray[0];
                string unloadsect = unloadsectemp.PadLeft(4, '0');
                check = false;

                for (int j = unloadStart; j < passSections.Count(); j++)
                {

                    if (unloadsect == passSections[j])
                    {
                        splitnum = j + 1;
                        check = true;
                        break;
                    }
                }

                if (check == true) // 181022 Due to that if there has the redrive section, we shouldpick the first one;
                {
                    break;
                }
            }

            if (splitnum == -1)
            {
                Console.WriteLine("Something Wrong ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Something Wrong ~~ there should have a section that end at the unload address.");
                Console.WriteLine("Something Wrong ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            else
            {
                string[] unloadSecArray = new string[splitnum - halfsignal];

                for (int i = halfsignal; i < splitnum; i++)
                {
                    unloadSecArray[i - halfsignal] = passSections[i];
                }
                UnLoadSections = unloadSecArray;
            }
            #endregion
            /////////////////////////////////
        }
    }
}
