using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OHTM.NLog_USE;
using OHTM.StatusMachine;
using MirleOHT.類別.DCPS;

namespace OHTM.ReadCsv
{
    class TransSectioncs
    {
        
        public String[] TransMap4Veh(String[] FromVehCSections, String[] FromVehCAddresses, out int store_start_Length, ref int [] reserve_direction_List_out)
        {
            Veh_VehM_Global.p_FromVehCSections = FromVehCSections;
            Veh_VehM_Global.p_FromVehCAddresses = FromVehCAddresses;
            int[] reserve_direction_list = null;
            store_start_Length = 0;
            String[] feedback4Veh = null;
            if (Veh_VehM_Global.fakeMap == true)
            {
                feedback4Veh = TransSections_Force_For_OHT(FromVehCSections, FromVehCAddresses);
            }
            else
            {
                feedback4Veh = TransSections_ForRealVehC(FromVehCSections, FromVehCAddresses, out store_start_Length, ref reserve_direction_list);
                reserve_direction_List_out = reserve_direction_list;
            }
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "**** The transSection = ");
            for (int i = 0; i < feedback4Veh.Length; i++)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "feedback : {0}", feedback4Veh[i].ToString());
            }
            return feedback4Veh;
        }
        public String[] TransMap4Veh_NewCmd(String[] FromVehCSections, String[] FromVehCAddresses, out int store_start_Length, ref int[] reserve_direction_List_out)
        {
            Veh_VehM_Global.p_FromVehCSections = FromVehCSections;
            Veh_VehM_Global.p_FromVehCAddresses = FromVehCAddresses;
            int[] reserve_direction_list = null;
            store_start_Length = 0;
            String[] feedback4Veh = null;
            if (Veh_VehM_Global.fakeMap == true)
            {
                feedback4Veh = TransSections_Force_For_OHT(FromVehCSections, FromVehCAddresses);
            }
            else
            {
                feedback4Veh = TransSections_ForRealVehC_New_Cmd(FromVehCSections, FromVehCAddresses, out store_start_Length, ref reserve_direction_list);
                reserve_direction_List_out = reserve_direction_list;
                String temp = "";
                String temp1 = "";
                int countsegment = 0;
                int[] tempdirection = new int[100];
                for (int j = 0; j < FromVehCSections.Count(); j++)
                {
                    temp = FromVehCSections[j].Remove(3);
                    if(temp != temp1)
                    {
                        tempdirection[countsegment] = reserve_direction_list[j];
                        countsegment++;
                    }
                    temp1 = temp;
                }
                reserve_direction_List_out = tempdirection;
            }
            eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "**** The transSection = ");
            for (int i = 0; i < feedback4Veh.Length; i++)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "feedback : {0}", feedback4Veh[i].ToString());
            }
            return feedback4Veh;
        }
        public int Setdirection(string sectionNum)
        {
            int temp = 0;
            int direction = 1;
            if (Veh_VehM_Global.p_FromVehCSections != null)
            {
                for (int i = 0; i < Veh_VehM_Global.p_FromVehCSections.Count(); i++)
                {
                    if (sectionNum == Veh_VehM_Global.p_FromVehCSections[i])
                    {
                        temp = i;
                    }
                }
                int[] restore_the_direction = GetDirection4Sections_New_Cmd(Veh_VehM_Global.p_FromVehCSections, Veh_VehM_Global.p_FromVehCAddresses);
                if (temp != null)
                {
                    direction = restore_the_direction[temp];
                }
                else
                {
                    eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "No same section name , sectionNum =  {0}", sectionNum);
                }
            }
            return direction;
        }
        protected String[] TransSections_Force_For_OHT(String[] FromVehCSections, String[] FromVehCAddresses)
        {
            int tempcount = 0;
            String[] tempSections = new string[100];
            String[] ToOHTSections = null;

            for (int i = 0; i < FromVehCSections.Count(); i++)
            {
                CutSection_Force_For_OHT(FromVehCSections[i], ref tempSections, ref tempcount);
            }
            ToOHTSections = new string[tempcount];
            for (int i = 0; i < tempcount; i++)
            {
                ToOHTSections[i] = tempSections[i];
            }

            return ToOHTSections;
        }
        protected String[] TransSections_ForRealVehC(String[] FromVehCSections, String[] FromVehCAddresses, out int store_startLength, ref int [] reserve_direction_list)
        {
            string forward_reverse = "";
            int tempcount = 0;
            Veh_VehM_Global.StartendCheck startendCheck = Veh_VehM_Global.StartendCheck.None;
            int startpointnum = 0;
            int endpointnum = 0;
            int directionOfThisSection = 0;
            store_startLength = 0;
            String[] tempSections = new string[500];
            String[] ToOHTSections = null;
            int sectionnum = FromVehCSections.Count();
            int[] restore_the_direction = GetDirection4Sections(FromVehCSections, FromVehCAddresses);
            reserve_direction_list = restore_the_direction;
            DataTable datable4VehMAP = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.VehC2VehMap);
            DefineStartEndPoint(ref startpointnum, ref endpointnum, FromVehCAddresses, datable4VehMAP, restore_the_direction, sectionnum, out store_startLength);
            if (Veh_VehM_Global.fakeMap != true)
            {
                for (int i = 0; i < FromVehCSections.Count(); i++)
                {
                    if (i == 0)
                    {
                        startendCheck = Veh_VehM_Global.StartendCheck.startpoint;
                    }
                    else if (i == FromVehCSections.Count() - 1)
                    {
                        startendCheck = Veh_VehM_Global.StartendCheck.endpoint;
                    }
                    else
                    {
                        startendCheck = Veh_VehM_Global.StartendCheck.None;
                    }
                    directionOfThisSection = restore_the_direction[i];
                    CutSection_ForRealVehC(startpointnum, endpointnum, startendCheck, FromVehCSections[i], datable4VehMAP, directionOfThisSection, ref tempSections, ref tempcount, FromVehCSections.Count());
                }
            }
            ToOHTSections = new string[tempcount];
            for (int i = 0; i < tempcount; i++)
            {
                ToOHTSections[i] = tempSections[i];
            }
            return ToOHTSections;
        }
        protected String[] TransSections_ForRealVehC_New_Cmd(String[] FromVehCSections, String[] FromVehCAddresses, out int store_startLength, ref int[] reserve_direction_list)
        {
            int tempcount = 0;
            int startpointnum = 0;
            int endpointnum = 0;
            int directionOfThisSection = 0;
            store_startLength = 0;
            String[] tempSections = new string[500];
            String[] ToOHTSections = null;
            int sectionnum = FromVehCSections.Count();
            int[] restore_the_direction = GetDirection4Sections_New_Cmd(FromVehCSections, FromVehCAddresses);
            reserve_direction_list = restore_the_direction;
            DataTable datable4VehMAP = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.VehCSections);
            DefineStartEndPoint(ref startpointnum, ref endpointnum, FromVehCAddresses, datable4VehMAP, restore_the_direction, sectionnum, out store_startLength);
            if (Veh_VehM_Global.fakeMap != true)
            {
                for (int i = 0; i < FromVehCSections.Count(); i++)
                {
                    directionOfThisSection = restore_the_direction[i];
                    CutSection_ForRealVehC_New_Cmd(FromVehCSections[i], directionOfThisSection, ref tempSections, ref tempcount);
                }
            }
            ToOHTSections = new string[tempcount];
            for (int i = 0; i < tempcount; i++)
            {
                ToOHTSections[i] = tempSections[i];
            }
            return ToOHTSections;
        }

        protected int[] GetDirection4Sections(String[] FromVehCSections, String[] FromVehCAddresses)
        {
            try
            {
                DataTable addressChecktb = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.VehCAddress);
                DataTable dt = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.VehCSections);
                int[] DirectionOfSections = new int[100];
                bool tempcheckflag = false;
                bool startpointchck = false; // if the startpoint isn't the node point;
                int temp = 0;
                int[] firstnum = new int[FromVehCAddresses.Count()];
                /*
                //search for each sections on the datatable SEC_ID
                */
                for (int i = 0; i < FromVehCSections.Count(); i++)
                {
                    startpointchck = false;
                    tempcheckflag = false;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if ((string)dt.Rows[j][0] == FromVehCSections[i])
                        {
                            temp = j;
                            tempcheckflag = true;
                            break;
                        }
                    }
                    if (tempcheckflag != true)
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@TransSectioncs : GetDirection4Sections : There is no that section.");
                        break;
                    }
                    /*
                     * Search the fromAdr to check the section direction;
                     * Check the default direction.
                    */
                    int start_adr;
                    if ((string)dt.Rows[temp][5] == "1")
                    {
                        start_adr = 1;
                    }
                    else
                    {
                        start_adr = 2;
                    }
                    if ((string)dt.Rows[temp][start_adr] == FromVehCAddresses[i])
                    {
                        DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.forward;
                        startpointchck = true;
                    }
                    else if ((string)dt.Rows[temp][start_adr] == FromVehCAddresses[i + 1])
                    {
                        DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.reverse;
                        startpointchck = true;
                    }
                    if (startpointchck == false)
                    {
                        if ((string)dt.Rows[temp][3 - start_adr] == FromVehCAddresses[i])
                        {
                            DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.reverse;
                            startpointchck = true;
                        }
                        else if ((string)dt.Rows[temp][3 - start_adr] == FromVehCAddresses[i + 1])
                        {
                            DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.forward;
                            startpointchck = true;
                        }
                    }
                    if (startpointchck == false)
                    {
                        for (int j = 0; j < addressChecktb.Rows.Count; j++)
                        {
                            if ((string)addressChecktb.Rows[j][0] == FromVehCAddresses[i])
                            {
                                firstnum[i] = int.Parse((string)addressChecktb.Rows[j][5]);
                            }
                            if ((string)addressChecktb.Rows[j][0] == FromVehCAddresses[i + 1])
                            {
                                firstnum[i + 1] = int.Parse((string)addressChecktb.Rows[j][5]);
                            }
                        }
                        if (firstnum[i + 1] > firstnum[i])
                        {
                            DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.forward;
                            startpointchck = true;
                        }
                        else if (firstnum[i] > firstnum[i + 1])
                        {
                            DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.reverse;
                            startpointchck = true;
                        }
                    }
                    if (startpointchck == false)
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "TransSectioncs : GetDirection4Sections : The address and the map matching must be wrong");
                    }

                }
                return DirectionOfSections;
            }
            catch (Exception e)
            {
                int[] temp = null;
                return temp;
            }
        }
        protected int[] GetDirection4Sections_New_Cmd(String[] FromVehCSections, String[] FromVehCAddresses)
        {
            try
            {
                DataTable addressChecktb = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.VehCAddress);
                DataTable dt = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.VehCSections);
                int[] DirectionOfSections = new int[100];
                bool tempcheckflag = false;
                bool startpointchck = false; // if the startpoint isn't the node point;
                int temp = 0;
                int[] firstnum = new int[FromVehCAddresses.Count()];
                /*
                //search for each sections on the datatable SEC_ID
                */
                for (int i = 0; i < FromVehCSections.Count(); i++)
                {
                    startpointchck = false;
                    tempcheckflag = false;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if ((string)dt.Rows[j][0] == FromVehCSections[i])
                        {
                            temp = j;
                            tempcheckflag = true;
                            break;
                        }
                    }
                    if (tempcheckflag != true)
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Error, null, "@TransSectioncs : GetDirection4Sections : There is no that section.");
                        break;
                    }
                    /*
                     * Search the fromAdr to check the section direction;
                     * Check the default direction.
                    */
                    int start_adr;
                    Console.WriteLine((string)dt.Rows[temp][2]);
                    if ((string)dt.Rows[temp][2] == FromVehCAddresses[i])
                    {
                        DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.forward;
                        startpointchck = true;
                    }
                    else if ((string)dt.Rows[temp][2] == FromVehCAddresses[i + 1])
                    {
                        DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.reverse;
                        startpointchck = true;
                    }
                    #region Use by Old Cmd ( Have Segment Only)
                    //if (startpointchck == false)
                    //{
                    //    if ((string)dt.Rows[temp][3 - start_adr] == FromVehCAddresses[i])
                    //    {
                    //        DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.reverse;
                    //        startpointchck = true;
                    //    }
                    //    else if ((string)dt.Rows[temp][3 - start_adr] == FromVehCAddresses[i + 1])
                    //    {
                    //        DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.forward;
                    //        startpointchck = true;
                    //    }
                    //}
                    //if (startpointchck == false)
                    //{
                    //    for (int j = 0; j < addressChecktb.Rows.Count; j++)
                    //    {
                    //        if ((string)addressChecktb.Rows[j][0] == FromVehCAddresses[i])
                    //        {
                    //            firstnum[i] = int.Parse((string)addressChecktb.Rows[j][5]);
                    //        }
                    //        if ((string)addressChecktb.Rows[j][0] == FromVehCAddresses[i + 1])
                    //        {
                    //            firstnum[i + 1] = int.Parse((string)addressChecktb.Rows[j][5]);
                    //        }
                    //    }
                    //    if (firstnum[i + 1] > firstnum[i])
                    //    {
                    //        DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.forward;
                    //        startpointchck = true;
                    //    }
                    //    else if (firstnum[i] > firstnum[i + 1])
                    //    {
                    //        DirectionOfSections[i] = (Int32)Veh_VehC_Global.direction.reverse;
                    //        startpointchck = true;
                    //    }
                    //}
                    #endregion
                    if (startpointchck == false)
                    {
                        eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "TransSectioncs : GetDirection4Sections : The address and the map matching must be wrong");
                    }

                }
                return DirectionOfSections;
            }
            catch (Exception e)
            {
                int[] temp = null;
                return temp;
            }
        }
        protected void CutSection_Force_For_OHT(String FromVehCSection, ref String[] ToVehSevtion, ref int tempcount_1)
        {
            FromVehCSection = FromVehCSection.Substring(1);
            switch (FromVehCSection)
            {
                case "041":
                    ToVehSevtion[tempcount_1] = ToVehSevtion[tempcount_1] + "0410";
                    tempcount_1++;
                    break;
                case "040":
                    ToVehSevtion[tempcount_1] = "0401";
                    tempcount_1++;
                    ToVehSevtion[tempcount_1] = "0402";
                    tempcount_1++;
                    break;
                case "042":
                    ToVehSevtion[tempcount_1] = "0412";
                    tempcount_1++;
                    break;
            }
        }
        protected void CutSection_ForRealVehC(int startpointnum, int endpointnum, Veh_VehM_Global.StartendCheck startend_Check, String FromVehCSection, DataTable dataTable, int directionOfThisSection, ref String[] ToVehSevtion, ref int tempcount_1, int sectionCountnum)
        {
            int tempsection = 0;
            int startnum = 0;
            int endnum = 0;
            /*
             * Find the exact section row.
             */
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (FromVehCSection == (string)dataTable.Rows[i][0])
                {
                    tempsection = i;
                    break;
                }
            }
            string segmentnum = (string)dataTable.Rows[tempsection][3];
            /*
             * Keep the VehSection on it;
             */
            if (startend_Check == Veh_VehM_Global.StartendCheck.startpoint)
            {
                startnum = startpointnum;
                if (sectionCountnum == 1)
                {
                    if (endpointnum == 0 && startnum != 0)
                    {
                        endnum = Int32.Parse(segmentnum);
                    }
                    else
                    {
                        endnum = endpointnum;
                    }
                }
                else if (sectionCountnum > 1)
                {
                    endnum = Int32.Parse(segmentnum); //the segment number;
                }
            }
            else if (startend_Check == Veh_VehM_Global.StartendCheck.endpoint)
            {
                startnum = 0;
                endnum = endpointnum;
                if (endnum == 0) //due to the endpoint can't be 0 , it represent that this section should be all count.
                {
                    endnum = Int32.Parse(segmentnum); //the segment number;
                }
            }
            else if (startend_Check == Veh_VehM_Global.StartendCheck.None)
            {
                startnum = 0;
                endnum = Int32.Parse(segmentnum); //the segment number;
            }
            Enterthesection(startnum, endnum, directionOfThisSection, segmentnum, ref ToVehSevtion, ref tempcount_1, dataTable, tempsection);

        }
        /*
         * Set the segment into the secion array for Veh
         * Segment = little section for Veh local
         */
        protected void CutSection_ForRealVehC_New_Cmd(String FromVehCSection, int directionOfThisSection, ref String[] ToVehSevtion, ref int tempcount_1)
        {

            Enterthesection_New_Cmd(FromVehCSection, directionOfThisSection, ref ToVehSevtion, ref tempcount_1);

        }

        protected void Enterthesection(int startnum, int endnum, int directionOfThisSection, string segmentnum, ref String[] ToVehSevtion, ref int tempcount_1, DataTable dataTable, int tempsection)
        {
            if (startnum == 0 && endnum == 0)
            {
                if (directionOfThisSection == 1)
                {
                    for (int j = startnum; j < Int32.Parse(segmentnum); j++)
                    {
                        ToVehSevtion[tempcount_1] = "+" + (string)dataTable.Rows[tempsection][3 + 1 + j];
                        tempcount_1++;
                    }
                }
                else if (directionOfThisSection == 2)
                {
                    for (int j = startnum; j < Int32.Parse(segmentnum); j++)
                    {
                        ToVehSevtion[tempcount_1] = (string)dataTable.Rows[tempsection][3 + 10 + Int32.Parse(segmentnum) - 1 - j]; //Change the 6 -> 10 for the little section.
                        tempcount_1++;
                    }
                }
            }
            else
            {
                if (directionOfThisSection == 1)
                {
                    for (int j = startnum; j < endnum; j++)
                    {
                        ToVehSevtion[tempcount_1] = "+" + (string)dataTable.Rows[tempsection][3 + 1 + j];
                        tempcount_1++;
                    }
                }
                else if (directionOfThisSection == 2)
                {
                    for (int j = startnum; j < endnum; j++)
                    {
                        ToVehSevtion[tempcount_1] = (string)dataTable.Rows[tempsection][3 + 10 + Int32.Parse(segmentnum) - 1 - j]; //Change the 6 -> 10 for the little section.
                        tempcount_1++;
                    }
                }
            }

        }
        protected void Enterthesection_New_Cmd(string FromVehCSection, int directionOfThisSection, ref String[] ToVehSevtion, ref int tempcount_1)
        {
            if (directionOfThisSection == 1)
            {
                ToVehSevtion[tempcount_1] = "+" + FromVehCSection;
                tempcount_1++;
            }
            else if (directionOfThisSection == 2)
            {
                ToVehSevtion[tempcount_1] = "-" + FromVehCSection;
                tempcount_1++;
            }
        }
        /*
         * Set the start & end number
         */
        protected void DefineStartEndPoint(ref int startpointnum, ref int endpointnum, String[] FromVehCAddresses, DataTable datable4VehMAP, int[] restore_the_direction, int section_num, out int storestartLength)
        {
            int forwardlength = 0;
            int reverselength = 0;
            bool startrecheck = false;
            bool endrecheck = false;
            storestartLength = 0;
            DataTable datable4VehAddress = new ReadCsv()._ReadParamsFromVhSectionData(CsvGlobal.VehCAddress);
            for (int i = 0; i < datable4VehAddress.Rows.Count; i++)
            {
                if ((string)datable4VehAddress.Rows[i][0] == FromVehCAddresses[0])
                {
                    forwardlength = int.Parse((string)datable4VehAddress.Rows[i][3]);
                    reverselength = int.Parse((string)datable4VehAddress.Rows[i][4]);
                    if (restore_the_direction[0] == 1)
                    {
                        if ((string)datable4VehAddress.Rows[i][1] != "1")
                        {
                            storestartLength = forwardlength;
                            startpointnum = Int32.Parse((string)datable4VehAddress.Rows[i][5]);
                        }
                    }
                    else if (restore_the_direction[0] == 2)
                    {
                        if ((string)datable4VehAddress.Rows[i][1] != "1")
                        {
                            storestartLength = reverselength;
                            startpointnum = Int32.Parse((string)datable4VehAddress.Rows[i][6]);
                        }
                    }
                    startrecheck = true;
                }
                if ((string)datable4VehAddress.Rows[i][0] == FromVehCAddresses[FromVehCAddresses.Count() - 1])
                {
                    if (restore_the_direction[section_num - 1] == 1)
                    {
                        if ((string)datable4VehAddress.Rows[i][1] != "1")
                        {
                            endpointnum = Int32.Parse((string)datable4VehAddress.Rows[i][5]);
                        }
                    }
                    else if (restore_the_direction[section_num - 1] == 2)
                    {
                        if ((string)datable4VehAddress.Rows[i][1] != "1")
                        {
                            endpointnum = Int32.Parse((string)datable4VehAddress.Rows[i][6]);
                        }
                    }
                    endrecheck = true;
                }
            }
            if (endrecheck == false || startrecheck == false)
            {
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "TransSectioncs : TransSections_ForRealVehC : The address must be wrong");
            }
        }
    }
}
