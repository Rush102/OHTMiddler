using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHTM.ReadCsv
{
    public static class CsvGlobal
    {
        public static string NowDataDir = @"..\..\";
        public static string NowFileDir;

        public static string UpdirectoryPath = @"..\..\";
        public static string SectionPath = @"SectionAddressData\ASECTION.csv";
        public static string AddressPath = @"SectionAddressData\AADDRESS.csv";
        public static string Veh_Map_path = @"SectionAddressData\VehC_MAP_DATA.csv";
        public static string VehC2VehMap = @"SectionAddressData\TransMapdataFromVehC2Veh.csv";
        public static string VehCAddress = @"SectionAddressData\VehCAddress.csv";
        public static string VehCSections = @"SectionAddressData\New_Cmd_Sections.csv";
        //public static DataTable dataTable = new DataTable();
    }
}
