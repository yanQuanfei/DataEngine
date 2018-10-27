using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Engine.Tool
{
    public class ClassifyConfig
    {
        static ClassifyConfig()
        {
            //System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\tmp.txt");
            // ConfigFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Classify.json";
            ConfigFileName = Directory.GetCurrentDirectory() + "\\Classify.json";
        }

        private static String ConfigFileName = "";
        private static Object m_lock = new object();

        public static string GetClassifyStr(int Classify)
        {
            lock (m_lock)
            {
                if (File.Exists(ConfigFileName))
                {
                    string configStr = File.ReadAllText(ConfigFileName);
                    JObject configObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(configStr);

                    return configObj[Classify.ToString()].ToString();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}