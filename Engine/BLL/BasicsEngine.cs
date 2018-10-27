using Dapper;
using DataEngine.Models;
using Engine;
using Engine.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// 基础引擎
    /// </summary>
    public  class BasicsEngine
    {


        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="Initiator">发起人</param>
        /// <param name="UserJID">UserJID</param>
        /// <param name="Classify">类别编号</param>
        /// <param name="RecordID">记录ID</param>
        /// <returns>添加的ID</returns>
        public static int AddMsgFlow(string Initiator, string UserJID, int Classify, int RecordID)
        {
            if (string.IsNullOrEmpty(Initiator) && string.IsNullOrEmpty(Initiator))
            {
                Log.ToFile("添加消息流报错：参数为空");
                return 0;
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
                        
                    sqlCommandText += "SELECT CAST(SCOPE_IDENTITY() as int)";
                    int result = conn.Query<int>(sqlCommandText, msg).FirstOrDefault();

                    if (result > 0)
                    {
                        return result;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("添加信息流报错：" + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// 添加审批流
        /// </summary>
        /// <param name="AuditorJID">审批人JID</param>
        /// <param name="MsgID">消息ID</param>
        /// <returns>是否添加成功</returns>
        public static bool AddAuditFlow(string AuditorJID, int MsgID)
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
        public static bool AddAuditFlow(List<AuditFlow> audits)
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
        /// 审批信息
        /// </summary>
        /// <param name="audits"></param>
        /// <returns></returns>
        public static bool UpdAuditFlow(AuditFlow audits)
        {

            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    
                    string sqlCommandText =                  
                    @"UPDATE AuditFlow SET AuditState=@AuditState,AuditOpinion=@AuditOpinion WHERE ID=@ID";
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
                Log.ToFile("审批信息报错：" + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 添加抄送流
        /// </summary>
        /// <param name="AuditorJID">抄送人JID</param>
        /// <param name="MsgID">消息ID</param>
        /// <returns>是否添加成功</returns>
        public static bool AddCopyFlow(string CopyJID, int MsgID)
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
        public static bool AddCopyFlow(List<CopyFlow> copies)
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
        public static List<WorkRules> GetWorkRules( int ClassifyID)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    List<WorkRules> workRulesList = new List<WorkRules>();

                    string sqlCommandText =
                        @"SELECT  [ID],[ClassifyID] ,[ClassifyName] ,[AuditorRules] ,[CopyRules] ,[Premise] ,[Priority]  ,[AuditMethod] FROM [WorkRules] WHERE ClassifyID=@ClassifyID";
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

        /// <summary>
        /// 添加到路线图
        /// </summary>
        /// <returns><c>true</c>, if audit route was added, <c>false</c> otherwise.</returns>
        /// <param name="auditRoute">Audit route.</param>
        public static bool AddAuditRoute(AuditRoute auditRoute){

            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {

                    string sqlCommandText =
                        @"INSERT INTO Route(MsgID,RulesID,AuditMethod,Auditor,Copies)VALUES(@MsgID,@RulesID,@AuditMethod,@Auditor,@Copies)";
                    int result = conn.Execute(sqlCommandText, auditRoute);

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
                Log.ToFile("添加路线图报错：" + ex.Message);
                return false;
            }

        }





    }
}