using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace OHTM.ReadCsv
{
    public class clsFileCsv
    {

        #region "定数"

        /// <summary>
        /// クラス名称
        /// </summary>
        private const string CLASS_NAME = "CSV";

        #endregion

        #region "内部変数"

        /// <summary>
        /// 常時書込み(短時間連続書込み用)
        /// </summary>
        private StreamWriter m_objStreamWriter = null;

        /// <summary>
        /// 常時読込み(短時間連続書込み用)
        /// </summary>
        private StreamReader m_objStreamReader = null;

        private string m_sWriterFlushPath = "";
        private string m_sWriterFlushFileName = "";
        private string m_sWriterFlushSaveDir = "";
        private string m_sReaderFlushPath = "";

        private int m_iErrorCode = 0;
        private string m_sErrorMessage = "";

        #endregion

        #region "イベント"
        #endregion

        #region "プロパティ"

        /// <summary>
        /// 異常コード
        /// </summary>
        public int ErrorCode
        {
            get { return (this.m_iErrorCode); }
        }

        /// <summary>
        /// 異常メッセージ
        /// </summary>
        public string ErrorMessage
        {
            get { return (this.m_sErrorMessage); }
        }

        /// <summary>
        /// ファイル書込み操作中確認
        /// </summary>
        /// <returns></returns>
        public bool IsWriterOpen
        {
            get
            {
                if (this.m_sWriterFlushPath != "")
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
        }

        /// <summary>
        /// ファイル読出し操作中確認
        /// </summary>
        /// <returns></returns>
        public bool IsReaderOpen
        {
            get
            {
                if (this.m_sReaderFlushPath != "")
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
        }

        #endregion

        #region "公開関数"

        #region "判定処理"

        /// <summary>
        /// 指定のCSVファイルが存在するか判定
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <returns>False:なし, True:あり</returns>
        public bool IsFileExist(string sFilePath)
        {
            return (File.Exists(sFilePath));
        }

        /// <summary>
        /// 現在連続書込み用に開いているファイルとの一致判定
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <returns>False:不一致, True:一致</returns>
        public bool IsOpenWriterFile(string sFilePath)
        {
            return (this.m_sWriterFlushPath.Equals(sFilePath));
        }

        /// <summary>
        /// 現在連続読出し用に開いているファイルとの一致判定
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <returns>False:不一致, True:一致</returns>
        public bool IsOpenReaderFile(string sFilePath)
        {
            return (this.m_sReaderFlushPath.Equals(sFilePath));
        }

        #endregion

        #region "書込み処理"

        /// <summary>
        /// CSVファイル読込み
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <param name="asGetData">取得文字配列</param>
        /// <returns>0:正常,0以外:異常</returns>
        public bool PrcReadAll(string sFilePath, ref string[] asGetData)
        {
            bool bRet = false;
            char[] cSep = new char[2] { Convert.ToChar(0x0D), Convert.ToChar(0x0A) };
            List<string> lstGetData = new List<string>();
            long count = 0;

            try
            {
                // ﾌｧｲﾙが存在しない場合、ｴﾗｰを返す
                if (!File.Exists(sFilePath))
                {
                    this.m_iErrorCode = -1;
                    return (bRet);
                }

                StreamReader sr = new StreamReader(sFilePath, Encoding.Default);

                while (sr.Peek() >= 0)
                {
                    lstGetData.Add(sr.ReadLine());

                    count++;
                    if (count == 10000)
                    {
                        count = 0;
                        Thread.Sleep(1);
                    }
                }

                asGetData = lstGetData.ToArray();

                sr.Close();

                bRet = true;
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -2;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        /// <summary>
        /// CSVファイル書込み(1行)
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <param name="sWrtData">書込みデータ</param>
        /// <param name="bAppend">True:上書き, False:常に新規</param>
        /// <returns>False:異常, True:正常</returns>
        public bool PrcWriteAtOnce(string sFilePath, string sWrtData, bool bAppend)
        {
            bool bRet = false;

            try
            {
                StreamWriter sw = new StreamWriter(sFilePath, bAppend, Encoding.Default);

                sw.WriteLine(sWrtData);

                sw.Close();
                bRet = true;
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -2;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        /// <summary>
        /// CSVファイル書込み(複数行)
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <param name="sWrtData">書込みデータ</param>
        /// <param name="bAppend">True:上書き, False:常に新規</param>
        /// <returns>False:異常, True:正常</returns>
        public bool PrcWriteAtOnce(string sFilePath, string[] sWrtDatas, bool bAppend)
        {
            bool bRet = false;

            if (sWrtDatas == null) return (false);

            try
            {
                StreamWriter sw = new StreamWriter(sFilePath, bAppend, Encoding.Default);

                for (int ii = 0; ii < sWrtDatas.Length; ii++)
                {
                    sw.WriteLine(sWrtDatas[ii]);
                }

                sw.Close();
                bRet = true;
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -2;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        /// <summary>
        /// 連続書込みファイルを開く
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <returns>False:異常, True:正常</returns>
        public bool PrcOpenFlushWriter(string sFilePath)
        {
            bool bRet = false;

            try
            {
                if (this.m_objStreamWriter != null)
                {
                    this.m_objStreamWriter.Close();
                }

                this.m_sWriterFlushPath = sFilePath;
                this.m_sWriterFlushSaveDir = Path.GetDirectoryName(this.m_sWriterFlushPath);
                this.m_sWriterFlushFileName = Path.GetFileName(this.m_sWriterFlushPath);
                this.m_objStreamWriter = new StreamWriter(this.m_sWriterFlushPath, true, Encoding.Default);
                bRet = true;
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -3;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        /// <summary>
        /// 連続書込みファイルを閉じる
        /// </summary>
        /// <returns>False:異常, True:正常</returns>
        public bool PrcCloseFlushWriter()
        {
            bool bRet = false;

            try
            {
                if (this.m_objStreamWriter != null)
                {
                    m_objStreamWriter.Close();
                }

                this.m_objStreamWriter = null;
                this.m_sWriterFlushPath = "";

                bRet = true;
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -4;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        /// <summary>
        /// 連続書込みファイルへの書込み
        /// </summary>
        /// <param name="sWrtData">書込みデータ</param>
        /// <returns>False:異常, True:正常</returns>
        public bool PrcWriteFlushWriter(string sWrtData)
        {
            bool bRet = false;

            try
            {
                this.m_objStreamWriter.WriteLine(sWrtData);

                bRet = true;
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -5;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        #endregion

        #region "読出し処理"

        /// <summary>
        /// 連続読出しファイルを開く
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <returns>False:異常, True:正常</returns>
        public bool PrcOpenFlushReader(string sFilePath)
        {
            bool bRet = false;

            try
            {
                if (this.m_objStreamReader != null)
                {
                    this.m_objStreamReader.Close();
                }

                // ﾌｧｲﾙが存在しない場合、ｴﾗｰを返す
                if (!File.Exists(sFilePath))
                {
                    this.m_iErrorCode = -1;
                    return (bRet);
                }

                m_sReaderFlushPath = sFilePath;
                m_objStreamReader = new StreamReader(this.m_sReaderFlushPath, Encoding.ASCII);
                bRet = true;
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -6;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        /// <summary>
        /// 連続読出しファイルを閉じる
        /// </summary>
        /// <returns>False:異常, True:正常</returns>
        public bool PrcCloseFlushReader()
        {
            bool bRet = false;

            try
            {
                if (this.m_objStreamReader != null)
                {
                    m_objStreamReader.Close();
                }

                m_objStreamReader = null;
                m_sReaderFlushPath = "";

                bRet = true;
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -7;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        /// <summary>
        /// 連続書込みファイルの読出し
        /// </summary>
        /// <param name="sGetData">読出しデータ</param>
        /// <returns>False:異常, True:正常</returns>
        public bool PrcReadFlushReader(ref string sGetData)
        {
            bool bRet = false;

            try
            {
                if (this.m_objStreamReader.Peek() >= 0)
                {
                    sGetData = m_objStreamReader.ReadLine();
                    bRet = true;
                }
                else
                {
                    sGetData = "";
                    bRet = false;
                }
            }
            catch (Exception ex)
            {
                this.m_iErrorCode = -8;
                this.m_sErrorMessage = ex.Message;
            }

            return (bRet);
        }

        #endregion

        #region "その他"

        /// <summary>
        /// 指定ファイルの更新日付
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <returns>更新日付(時刻)</returns>
        public DateTime PrcGetLastWriteDate(string sFilePath)
        {
            DateTime dt = DateTime.Now;

            if (File.Exists(sFilePath))
            {
                dt = File.GetLastWriteTime(sFilePath);
            }

            return (dt);
        }

        /// <summary>
        /// 指定ファイルの更新日付
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <param name="sFormat">出力フォーマット</param>
        /// <returns>更新日付(文字列)</returns>
        public string PrcGetLastWriteDate(string sFilePath, string sFormat)
        {
            string sRet = "";
            DateTime dt = DateTime.Now;

            if (File.Exists(sFilePath))
            {
                dt = File.GetLastWriteTime(sFilePath);
                sRet = dt.ToString(sFormat);
            }

            return (sRet);
        }

        /// <summary>
        /// フォルダ有無判定
        /// </summary>
        /// <param name="sFilePath">ファイルパス</param>
        /// <param name="bCreate">False:なし, True:ない場合は新規生成</param>
        /// <returns></returns>
        public bool PrcCheckFolder(string sFilePath, bool bCreate)
        {
            bool bRet;
            string sDirPath;

            sDirPath = Path.GetDirectoryName(sFilePath);

            bRet = Directory.Exists(sDirPath);
            if (!bRet)
            {
                if (bCreate)
                {
                    bRet = PrcCreateFolder(sDirPath);
                }
            }

            return (bRet);
        }

        /// <summary>
        /// 指定ディレクトリ作成
        /// </summary>
        /// <param name="sDirPath">ディレクトリパス</param>
        /// <returns></returns>
        public bool PrcCreateFolder(string sDirPath)
        {
            bool bRet = false;

            try
            {
                Directory.CreateDirectory(sDirPath);
                bRet = true;
            }
            catch
            {
            }

            return (bRet);
        }

        #endregion

        #endregion

    }
}
