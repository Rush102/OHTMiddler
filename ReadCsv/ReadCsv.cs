using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHTM.ReadCsv
{
    /// <summary>
    /// Map
    /// For teaching Get section from address
    /// </summary>
    static class Map
    {
        static public string filename = "ASECTION.csv";
        static public string SectionPath = null;
        static public DataTable map = new DataTable();
        static Map()
        {
            GetMap();
        }
        static private void GetMap()
        {
            SectionPath = Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "../..", "SectionAddressData", filename)); ;
            FileStream fs = new FileStream(SectionPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            //StreamReader sr = new StreamReader(fs, encoding);
            //string fileContent = sr.ReadToEnd();
            //記錄每次讀取的一行記錄
            string strLine = "";
            //記錄每行記錄中的各字段內容
            string[] header = null;
            //標示列數
            //逐行讀取CSV中的數據
            header = sr.ReadLine().Split(',');
            foreach (string colName in header)
            {
                map.Columns.Add(colName, typeof(String));
            }
            while ((strLine = sr.ReadLine()) != null)
            {
                map.Rows.Add(strLine.Split(','));
            }
            sr.Close();
            fs.Close();
        }

        static public string GetSection(string startAddr, string endAddr)
        {
            DataRow[] row = map.Select("FROM_ADR_ID = '" + startAddr + "'" + " and TO_ADR_ID = '" + endAddr + "'");
            string res = row[0]["SEC_ID"].ToString().PadLeft(4, '0'); //4bit SectionID for VehM spec 
            return res;
        }


    }
    /// <summary>
    /// For load unload
    /// </summary>
    class ReadCsv
    {
        public DataTable _ReadParamsFromVhSectionData(string recentFileDir)
        {
            bool bRet = false;
            clsFileCsv objCsv = new clsFileCsv();
            string sFilePath = CsvGlobal.NowDataDir + recentFileDir;//CsvGlobal.NowFileDir;
            DataTable dataTable = new DataTable();  //jason++ 181022 Have added global static in the CsvGlobal.cs but here use for new it.
            FileStream fs = new FileStream(sFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            //StreamReader sr = new StreamReader(fs, encoding);
            //string fileContent = sr.ReadToEnd();
            //記錄每次讀取的一行記錄
            string strLine = "";
            //記錄每行記錄中的各字段內容
            string[] aryLine = null;
            string[] tableHead = null;
            //標示列數
            int columnCount = 0;
            //標示是否是讀取的第一行
            bool IsFirst = true;
            //逐行讀取CSV中的數據
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //創建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        tableHead[i] = tableHead[i].Replace("\"", "");
                        Console.WriteLine("tableHead[{0}] = {1}" , i , tableHead[i]);
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dataTable.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dataTable.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j].Replace("\"", "");
                    }
                    dataTable.Rows.Add(dr);
                }
            }
            //if (aryLine != null && aryLine.Length > 0)
            //{
            //    dataTable.DefaultView.Sort = tableHead[2] + " " + "DESC";
            //}
            Console.WriteLine("raws num = " + dataTable.Rows.Count);
            //string a = "2";

            //DataRow[] drselect = dataTable.Select("TO_ADR_ID = '"+a+"'");
            //Console.WriteLine("drselect[0] = " + drselect[0].ItemArray[0]);
            //Console.WriteLine("drselect[1] = " + drselect[1].ItemArray[0]);
            sr.Close();
            fs.Close();
            return dataTable;

        }

    }
}
