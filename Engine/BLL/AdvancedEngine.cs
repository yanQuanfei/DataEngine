using DataEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Engine.Tool;

namespace Engine
{
    /// <summary>
    /// 高级引擎
    /// </summary>
    public class AdvancedEngine
    {
        /// <summary>
        /// 过滤消息到路线图
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool FilterMsgToAuditRoute(MsgFlow msg)
        {

            try
            {
                List<WorkRules> rulesList = BasicsEngine.GetWorkRules(msg.Classify);





                AuditRoute auditRoute = new AuditRoute();
                auditRoute.AuditMethod = 1;
                auditRoute.MsgID = 23;
                auditRoute.RulesID = 1;
                auditRoute.Auditor = "[{1:123@com}]";
                auditRoute.Copies = "[{1:123@com}]";


                bool b = BasicsEngine.AddAuditRoute(auditRoute);
                
                return b;
            }
            catch (Exception ex)
            {
                Log.ToFile("过滤生成路线图报错ex：" + ex.Message);
                return false;
               
            }
          





        }


    }
}
