using Dapper;
using DataEngine.Models;
using Engine;
using Engine.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataEngine.Engine
{
    internal class Engine
    {
        //        public static string[] ClassifyArrey { get; set; }
        //
        //        /// <summary>
        //        /// 添加消息
        //        /// </summary>
        //        /// <param name="Initiator">发起人</param>
        //        /// <param name="UserJID">UserJID</param>
        //        /// <param name="Classify">类别编号</param>
        //        /// <param name="RecordID">记录ID</param>
        //        /// <returns>成功与否</returns>
        //        public bool AddMsg(string Initiator, string UserJID, int Classify, int RecordID)
        //        {
        //
        //            //if (string.IsNullOrEmpty(Initiator) && string.IsNullOrEmpty(Initiator))
        //            //{
        //            //    Log.ToFile("添加消息报错：参数为空");
        //            //    return false;
        //            //}
        //
        //            //string[] k = new string[] {"Initiator", "UserJID", "Classify",             "RecordID" ,           "State" ,               "LaunchTime" };
        //            //string[] v = new string[] {Initiator,     UserJID ,   Classify.ToString() , RecordID.ToString(), MsgState.Nil.ToString(),DateTime.Now.ToString("yyyy-MM-dd") };
        //            //try
        //            //{
        //            //    TBResult result = GBL.Engine.MdcAddNewData("MsgFlow", k, v);
        //            //    if (!result.Success)
        //            //    {
        //            //        Log.ToFile("添加消息报错：" + result.Message);
        //            //        return false;
        //            //    }
        //            //    else
        //            //    {
        //            //        return true;
        //            //    }
        //            //}
        //            //catch(Exception ex)
        //            //{
        //            //    Log.ToFile("添加消息报错："+ex.Message);
        //               return false;
        //            //}
        //        }

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="Initiator">发起人</param>
        /// <param name="UserJID">UserJID</param>
        /// <param name="Classify">类别编号</param>
        /// <param name="RecordID">记录ID</param>
        /// <returns>成功与否</returns>
        public bool AddMsgFlow(string Initiator, string UserJID, int Classify, int RecordID)
        {
            if (string.IsNullOrEmpty(Initiator) && string.IsNullOrEmpty(Initiator))
            {
                Log.ToFile("添加消息流报错：参数为空");
                return false;
            }

            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    MsgFlow msg = new MsgFlow();
                    msg.Initiator = Initiator;
                    msg.UserJID = UserJID;
                    msg.RecordID = RecordID;
                    msg.Classify = Classify;
                    msg.LaunchTime = DateTime.Now.ToString();
                    msg.State = (int)MsgState.Nil;

                    string sqlCommandText =
                        @"INSERT INTO MsgFlow(Initiator,UserJID,RecordID,State,Classify,LaunchTime)VALUES(@Initiator,@UserJID,@RecordID,@State,@Classify,@LaunchTime)";
                    int result = conn.Execute(sqlCommandText, msg);

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("添加信息流报错：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 添加审批流
        /// </summary>
        /// <param name="AuditorJID">审批人JID</param>
        /// <param name="MsgID">消息ID</param>
        /// <returns>是否添加成功</returns>
        private bool AddAuditFlow(string AuditorJID, int MsgID)
        {
            if (string.IsNullOrEmpty(AuditorJID))
            {
                Log.ToFile("添加审批流报错：参数为空");
                return false;
            }

            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    AuditFlow audit = new AuditFlow();

                    audit.AuditorJID = AuditorJID;
                    audit.MsgID = MsgID;
                    audit.AuditState = (int)AuditState.Nil;

                    string sqlCommandText =
                        @"INSERT INTO AuditFlow(AuditorJID,MsgID,AuditState)VALUES(@AuditorJID,@MsgID,@AuditState)";
                    int result = conn.Execute(sqlCommandText, audit);

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("添加审批流报错：" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 批量添加审批流
        /// </summary>
        /// <param name="audits">List&lt;AuditFlow&gt;</param>
        /// <returns></returns>
        private bool AddAuditFlow(List<AuditFlow> audits)
        {
           
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                   

                    string sqlCommandText =
                        @"INSERT INTO AuditFlow(AuditorJID,MsgID,AuditState)VALUES(@AuditorJID,@MsgID,@AuditState)";
                    int result = conn.Execute(sqlCommandText, audits);

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("批量添加审批流报错：" + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 添加抄送流
        /// </summary>
        /// <param name="AuditorJID">抄送人JID</param>
        /// <param name="MsgID">消息ID</param>
        /// <returns>是否添加成功</returns>
        private bool AddCopyFlow(string CopyJID, int MsgID)
        {
            if (string.IsNullOrEmpty(CopyJID))
            {
                Log.ToFile("添加抄送流报错：参数为空");
                return false;
            }

            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    CopyFlow copy = new CopyFlow();

                    copy.CopyJID = CopyJID;
                    copy.MsgID = MsgID;

                    string sqlCommandText =
                        @"INSERT INTO CopyFlow(CopyJID,MsgID)VALUES(@CopyJID,@MsgID)";
                    int result = conn.Execute(sqlCommandText, copy);

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("添加抄送流报错：" + ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 批量添加抄送流
        /// </summary>
        /// <param name="copies">List&lt;CopyFlow&gt;</param>
        /// <returns></returns>
        private bool AddCopyFlow(List<CopyFlow> copies)
        {
          

            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                   
                    string sqlCommandText =
                        @"INSERT INTO CopyFlow(CopyJID,MsgID)VALUES(@CopyJID,@MsgID)";
                    int result = conn.Execute(sqlCommandText, copies);

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("添加抄送流报错：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取某类别的工作规则
        /// </summary>
        /// <param name="ClassifyID"></param>
        /// <returns></returns>
        private List<WorkRules> GetWorkRules(int ClassifyID)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    List<WorkRules> workRulesList = new List<WorkRules>();

                    string sqlCommandText =
                        @"SELECT  [ID],[ClassifyID] ,[ClassifyName] ,[AuditorRules] ,[CopyRules] ,[Premise] ,[Priority]  ,[AuditMethod] FROM [SysOffice].[tbobjects].[WorkRules] WHERE ClassifyID=@ClassifyID";
                    workRulesList = conn.Query<WorkRules>(sqlCommandText, new { ClassifyID = ClassifyID }).ToList();
                    return workRulesList;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取工作规则报错ex：" + ex.Message);
                return null;
            }
        }






    }
}