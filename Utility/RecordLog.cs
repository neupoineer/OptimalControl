using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utility
{
    public static class RecordLog
    {
        /// <summary>
        /// The log file
        /// </summary>
        private static string _logFile = ConfigAppSettings.GetSettingString("LogFile", AppDomain.CurrentDomain.BaseDirectory + "cache\\event.log"); //错误日志文件

        /// <summary>
        /// 保存日志文件.
        /// </summary>
        /// <param name="fileName">日志文件名.</param>
        /// <param name="category">日志类型.</param>
        /// <param name="content">日志内容.</param>
        public static void WriteLogFile(string category, string content)
        {
            try
            {
                if (!File.Exists(_logFile))
                {
                    string directory = _logFile.Substring(0, _logFile.LastIndexOf("\\", System.StringComparison.Ordinal));
                    if (!Directory.Exists(directory)) //检查cache目录是否已创建
                        Directory.CreateDirectory(directory); //若尚未创建，则创建目录
                    FileStream f = File.Create(_logFile);
                    f.Close();
                    f.Dispose();
                }

                StreamWriter fs = new StreamWriter(_logFile, true, System.Text.Encoding.GetEncoding("gb2312"));
                string timeNow = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");
                fs.WriteLine(timeNow + " " + category + ": " + content);

                fs.Close(); //关闭文件
                fs.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

    }
}
