using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tool
{
    public class Log
    {
        static Log()
        {
            //System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\tmp.txt");
            m_sLogFileName = AppDomain.CurrentDomain.BaseDirectory  + "DataEngine.log";
            
        }

        private static String m_sLogFileName = "";
        static Object m_lock = new object();

        /// <summary>
        /// 写日志到文件
        /// </summary>
        /// <param name="strContent"></param>
        static public void ToFile(String strContent)
        {
            DateTime time = new DateTime();
            time = DateTime.Now;

            lock (m_lock)
            {
                if (!File.Exists(m_sLogFileName))
                    File.WriteAllText(m_sLogFileName, time.ToString() + "::\t" + strContent);
                else
                    File.AppendAllText(m_sLogFileName, "\r\n" + time.ToString() + "::\t" + strContent);
            }
        }
    }
}
