using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tool
{
  public  class DataBaseConfig
    {
        static DataBaseConfig()
        {
            //System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\tmp.txt");
            // ConfigFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Classify.json";
            WIN_ConfigFileName = Directory.GetCurrentDirectory() + "\\DataBaseConfig.json";
            LINUX_ConfigFileName = Directory.GetCurrentDirectory() + "/DataBaseConfig.json";
        }

        private static String WIN_ConfigFileName = "";
        private static String LINUX_ConfigFileName = "";
        private static Object m_lock = new object();

        public static string GetDataBaseStr(string str)
        {
            lock (m_lock)
            {
                if (File.Exists(WIN_ConfigFileName))
                {
                    string configStr = File.ReadAllText(WIN_ConfigFileName);
                    JObject configObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(configStr);
                    return configObj[str.ToString()].ToString();
                }
                else if (File.Exists(LINUX_ConfigFileName))
                {
                    string configStr = File.ReadAllText(LINUX_ConfigFileName);
                    JObject configObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(configStr);

                    return configObj[str.ToString()].ToString();
                }
                else
                {
                    return null;
                }
            }
        }



    }
}
