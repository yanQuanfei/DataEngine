
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataEngine.Models;


namespace DataEngine.Engine
{
    class Engine
    {
        public static string[] ClassifyArrey { get; set; }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="Initiator">发起人</param>
        /// <param name="UserJID">UserJID</param>
        /// <param name="Classify">类别编号</param>
        /// <param name="RecordID">记录ID</param>
        /// <returns>成功与否</returns>
        public bool AddMsg(string Initiator, string UserJID, int Classify, int RecordID)
        {

            //if (string.IsNullOrEmpty(Initiator) && string.IsNullOrEmpty(Initiator))
            //{
            //    Log.ToFile("添加消息报错：参数为空");
            //    return false;
            //}

            //string[] k = new string[] {"Initiator", "UserJID", "Classify",             "RecordID" ,           "State" ,               "LaunchTime" };
            //string[] v = new string[] {Initiator,     UserJID ,   Classify.ToString() , RecordID.ToString(), MsgState.Nil.ToString(),DateTime.Now.ToString("yyyy-MM-dd") };
            //try
            //{
            //    TBResult result = GBL.Engine.MdcAddNewData("MsgFlow", k, v);
            //    if (!result.Success)
            //    {
            //        Log.ToFile("添加消息报错：" + result.Message);
            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Log.ToFile("添加消息报错："+ex.Message);
               return false;
            //}           
        }


       // public 

            



    }
}
