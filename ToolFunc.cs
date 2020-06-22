using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OHTM
{
    class ToolFunc
    {
        public string DelFirstCharString(string str)
        {
            str = str.Substring(1);
            return str;
        }
        public string DelLastCharString(string str)
        {
            str = str.Substring(0, str.Length - 1);
            return str;
        }
        public string DelLastAndFirstCharString(string str)
        {
            str = DelFirstCharString(str);
            str = DelLastCharString(str);
            return str;
        }
        public string readTargetFile(string targetFileName)
        {
            string targetFileContent = "";
            string targetFilePath = targetFileName;

            StreamReader targetFileReader = new StreamReader(targetFilePath);
            while (!targetFileReader.EndOfStream)
            {
                string lastLineOfTargetFile = targetFileReader.ReadLine();
                targetFileContent = lastLineOfTargetFile;
            }
            targetFileReader.Close();

            return targetFileContent;
        }
        public bool writeTargetFile(string targetFileName, string writeContent)
        {
            ///////
            FileStream fileStream = null;//根据不同情况使用不同的文件指针
            string targetFilePath = targetFileName;
            if (targetFilePath == "")//如果用户没读取过文件，那就获取一个文件路径写入
            {
                targetFilePath = targetFileName;//获取保存路径
                fileStream = new FileStream(targetFilePath, FileMode.Create);//以创建的方式打开这个文件
                fileStream = new FileStream(targetFilePath, FileMode.Truncate);//以覆盖写入的方式打开这个文件
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(writeContent);//开始写入
                streamWriter.Flush();//清空缓冲区
                                     //关闭流
                streamWriter.Close();
                fileStream.Close();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
