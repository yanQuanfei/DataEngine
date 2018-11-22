using Dapper;
using DataEngine.Models;
using Engine.Tool;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Engine
{
    /// <summary>
    /// 基础引擎
    /// </summary>
    public class BasicsEngine
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
            if (string.IsNullOrEmpty(Initiator) && string.IsNullOrEmpty(UserJID))
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


        //考虑在修改信息地，添加消息推送


        /// <summary>
        /// 处理修改信息流
        /// 需要状态和 ID
        /// </summary>
        /// <returns><c>true</c>, if message flow was upded, <c>false</c> otherwise.</returns>
        /// <param name="msgFlow">Message flow.</param>
        public static bool ProcessMsgFlow(MsgFlow msgFlow)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    msgFlow.AuditTime = DateTime.Now.ToString("yyyy-MM-dd");
                    string sqlCommandText =
                    @"UPDATE MsgFlow SET State=@State,AuditTime=@AuditTime WHERE ID=@ID and  state =0";
                    int result = conn.Execute(sqlCommandText, msgFlow);

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
        /// 过滤修改信息流
        /// 需要规则 ID 和路线图 ID 和消息 ID
        /// </summary>
        /// <returns><c>true</c>, if message flow was filtered, <c>false</c> otherwise.</returns>
        /// <param name="msgFlow">Message flow.</param>
        public static bool FilterMsgFlow(MsgFlow msgFlow)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                    @"UPDATE MsgFlow SET RulesID=@RulesID,RouteID=@RouteID WHERE ID=@ID";
                    int result = conn.Execute(sqlCommandText, msgFlow);

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


        //考虑在添加审批时，添加消息推送

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
        public static AuditFlow UpdAuditFlow(int ID, AuditState state, string Opinion)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                    @"UPDATE AuditFlow SET AuditState=@AuditState,AuditOpinion=@AuditOpinion WHERE ID=@ID and AuditState=0";
                    int result = conn.Execute(sqlCommandText, new { ID = ID, AuditState = (int)state, AuditOpinion = Opinion });
                    if (result > 0)
                    {
                        sqlCommandText = @"SELECT  [ID],[MsgID] ,[AuditorJID] ,[AuditState]   FROM [AuditFlow] WHERE ID=@ID";

                        AuditFlow auditFlow = conn.Query<AuditFlow>(sqlCommandText, new { ID = ID }).FirstOrDefault();

                        return auditFlow;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("审批信息报错：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 未审批的改为无效
        /// 批量修改审批信息为无效
        /// 主要是会签和或签
        /// </summary>
        /// <param name="audits"></param>
        /// <returns></returns>
        public static bool UpdAuditFormNilToInvalid(int msgid)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"UPDATE AuditFlow SET AuditState=@NewState,AuditOpinion=@AuditOpinion WHERE AuditState=@OldState and MsgID=@MsgID";
                    int result = conn.Execute(sqlCommandText, new { NewState = (int)AuditState.Invalid, AuditOpinion = "已有人处理", OldState = (int)AuditState.Nil, MsgID = msgid });
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
                Log.ToFile("批量审批信息报错（会签和或签）：" + ex.Message);
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
        public static List<WorkRules> GetWorkRules(int ClassifyID)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    List<WorkRules> workRulesList = new List<WorkRules>();

                    string sqlCommandText =
                        @"SELECT  [ID],[ClassifyID] ,[ClassifyName] ,[AuditorRules] ,[CopyRules] ,[Premise] ,[Priority]  ,[AuditMethod] FROM [WorkRules] WHERE ClassifyID=@ClassifyID order by Priority desc";
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
        public static bool AddAuditRoute(AuditRoute auditRoute)
        {
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

        /// <summary>
        /// 获取指定消息的路线图
        /// </summary>
        /// <returns>The auditroute.</returns>
        /// <param name="msgId">消息 ID</param>
        public static AuditRoute GetAuditRoute(int msgId)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    AuditRoute auditRoute = new AuditRoute();

                    string sqlCommandText =
                        @"SELECT  [ID],[MsgID] ,[RulesID] ,[AuditMethod] ,[Auditor] ,[Copies]  FROM [Route] WHERE MsgID=@MsgID";
                    auditRoute = conn.Query<AuditRoute>(sqlCommandText, new { MsgID = msgId }).FirstOrDefault();
                    return auditRoute;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取路线图报错ex：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据消息 ID 获取审批流
        /// </summary>
        /// <returns>The audit flows.</returns>
        /// <param name="msgId">Message identifier.</param>
        public static List<AuditFlow> GetAuditFlows(int msgId)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    List<AuditFlow> AuditFlowsList = new List<AuditFlow>();
                    string sqlCommandText =
                        @"SELECT  [ID],[MsgID] ,[AuditorJID] ,[AuditState]   FROM [AuditFlow] WHERE MsgID=@MsgID";
                    AuditFlowsList = conn.Query<AuditFlow>(sqlCommandText, new { MsgID = msgId }).ToList();
                    return AuditFlowsList;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取审批状态报错ex：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取实体表的信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetJobject(int ID, int type)
        {
            try
            {
                string TableName = ClassifyConfig.GetClassifyStr(type);

                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"SELECT  *  FROM  " + TableName + " WHERE ID=@ID";
                    object ob = conn.Query(sqlCommandText, new { ID = ID }).FirstOrDefault();

                    return ob;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取详细数据报错ex：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据角色获取人
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static string GetUserForRole(JArray roles)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    List<string> users = new List<string>();

                    string sqlCommandText =
                        @"SELECT  UserCode  FROM User_Role_Mapping  WHERE RoleCode = @RoleCode";
                    foreach (string role in roles)
                    {
                        List<string> users1 = new List<string>();
                        users1 = conn.Query<string>(sqlCommandText, new { RoleCode = role }).ToList();

                        users = users.Union(users1).ToList<string>();
                    }

                    return Newtonsoft.Json.JsonConvert.SerializeObject(users);
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("根据角色获取员工报错ex：" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取某人的上级领导
        /// </summary>
        /// <param name="name">userjid</param>
        /// <param name="level">向上几级</param>
        /// <param name="type">1.直接向上，2.间隔向上</param>
        /// <returns>领导的UserJID的数组</returns>
        public static string GetUserForLevel(string name,int level,int type)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    List<string> users = new List<string>();

                    string sqlCommandText =
                        @"　SELECT e1.* FROM employeesTree e1,employeesTree e2 WHERE e2.ename='小天' AND e2.path like concat(e1.path,'/%')";


                    foreach (string role in roles)
                    {
                        List<string> users1 = new List<string>();
                        users1 = conn.Query<string>(sqlCommandText, new { RoleCode = role }).ToList();

                        users = users.Union(users1).ToList<string>();
                    }

                    return Newtonsoft.Json.JsonConvert.SerializeObject(users);
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("根据角色获取员工报错ex：" + ex.Message);
                return null;
            }
        }


        /// <summary>
        /// 撤回申请
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DelMsg(int id)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = @"UPDATE MsgFlow SET State=3,AuditTime=@AuditTime WHERE ID=@ID and  state =0";
                    int result = conn.Execute(sqlCommandText, new { ID = id, AuditTime = DateTime.Now.ToString("yyyy-MM-dd") });
                    if (result > 0)
                    {
                        sqlCommandText = @"DELETE FROM [AuditFlow] WHERE MsgID=@ID ";
                        result = conn.Execute(sqlCommandText, new { ID = id });
                        sqlCommandText = @"DELETE FROM [Route] WHERE MsgID=@ID";
                        result = conn.Execute(sqlCommandText, new { ID = id });

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
                Log.ToFile("删除消息报错ex：" + ex.Message);
                return false;
            }
        }
    }
}