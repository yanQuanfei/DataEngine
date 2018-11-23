using Tool;
using Newtonsoft.Json;
using RestSharp;
using System;


namespace Inform
{
    public class Inform
    {
        private static string oawebsite = DataBaseConfig.GetDataBaseStr("OAWebSite");

        public static string OAWebSite
        {
            get { return oawebsite; }
            set { oawebsite = DataBaseConfig.GetDataBaseStr("OAWebSite"); }
        }

        public static bool AddInform(string UserJID, string Content)
        {
            try
            {
                string url = string.Format("{0}/api/HomeApi/PostAddMsg", OAWebSite);

                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                var json = JsonConvert.SerializeObject(new { UserJID = UserJID, Content = Content });

                request.AddParameter("application/json", json, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                string contentPost = response.Content;
            }
            catch (Exception ex)
            {
                Log.ToFile("向页面网站推送消息出现错误："+ex.Message+ "\r\n发送的消息"+ Content+ "\r\n消息目标人" + UserJID);
            }

            return true;
        }
    }
}