using DataEngine.Models;
using Engine.Tool;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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
                //获取类别的所有审批规则
                List<WorkRules> rulesList = BasicsEngine.GetWorkRules(msg.Classify);
                //获取该条消息
                object ob = BasicsEngine.GetJobject(msg.RecordID, msg.Classify);
                string a = JsonConvert.SerializeObject(ob);
                JObject job = JsonConvert.DeserializeObject<JObject>(a);
                WorkRules workRules = new WorkRules();
                bool b = false;
                //匹配优先级
                foreach (WorkRules rules in rulesList)
                {
                    //走过滤
                    if (rules.Premise != "0")
                    {
                        JObject premise = JsonConvert.DeserializeObject<JObject>(rules.Premise);

                        string name = premise.Value<string>("name");
                        string type = premise.Value<string>("type");
                        string where = premise.Value<string>("where");
                        string value = premise.Value<string>("value");

                        //判断是否满足条件
                        bool YoN = Compare(type, where, job.Value<string>(name), value);

                        if (YoN)
                        {
                            workRules = rules;
                            break;
                        }
                    }
                    else//默认不走条件过滤，其他
                    {
                        workRules = rules;
                        break;
                    }
                }


                if (workRules != null)
                {

                    string Auditor = GetAuditoArr(workRules.AuditorRules);
                    string copy = GetAuditoArr(workRules.CopyRules);

                    if (!string.IsNullOrWhiteSpace(Auditor) && !string.IsNullOrWhiteSpace(copy))
                    {
                        AuditRoute auditRoute = new AuditRoute();
                        auditRoute.AuditMethod = workRules.AuditMethod;
                        auditRoute.MsgID = msg.ID;
                        auditRoute.RulesID = msg.RecordID;
                        auditRoute.Auditor = Auditor;
                        auditRoute.Copies = copy;

                        b = BasicsEngine.AddAuditRoute(auditRoute);
                    }


                }


               

                return b;
            }
            catch (Exception ex)
            {
                Log.ToFile("过滤生成路线图报错ex：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <returns></returns>
        public static bool Compare(string type, string where, string data, string value)
        {
            if (type == "int")
            {
                int whereInt = Convert.ToInt32(where);
                double valueInt = Convert.ToDouble(value);
                double dataInt = Convert.ToDouble(data);

                switch (whereInt)
                {
                    case 1://小于
                        {
                            if (dataInt < valueInt)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;

                    case 2://大于
                        {
                            if (dataInt > valueInt)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;

                    case 3:// <=
                        {
                            if (dataInt <= valueInt)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;

                    case 4://  ==
                        {
                            if (dataInt == valueInt)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;

                    case 5://>=
                        {
                            if (dataInt >= valueInt)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;

                    case 6://两数之间，先不做，跳过
                        {
                            return false;
                        }
                        break;

                    case 7://!=
                        {
                            if (dataInt != valueInt)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                }
            }
            else if (type == "string")
            {
                int whereInt = Convert.ToInt32(where);

                switch (whereInt)
                {
                    case 4: //==
                        {
                            if (data.Equals(value))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;

                    case 7: //!==
                        {
                            if (!data.Equals(value))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// 获取审批人数组
        /// </summary>
        /// <param name="auditorRules">审批规则</param>
        /// <returns>"[\"1111@com\",\"2222@com\",\"3333@com\"]"</returns>
        public static string GetAuditoArr(string auditorRules)
        {
            JObject premise = JsonConvert.DeserializeObject<JObject>(auditorRules);

         
            int type = premise.Value<int>("type");//审批类别 
            int level = premise.Value<int>("level");//主管级别
            string AuditoArr = null;

            switch (type)
            {
                case 1://指定主管一级
                {

                }
                    break;
                case 2://多级主管
                {

                }
                    break;
                case 3://角色
                {
                   JArray name = premise.Value<JArray>("name");//角色或者指定人的数组

                    AuditoArr = BasicsEngine.GetUserForRole(name);
                }
                    break;
                case 4://指定成员
                {
                    JArray name = premise.Value<JArray>("name");//角色或者指定人的数组

                    AuditoArr = JsonConvert.SerializeObject(name);
                }
                    break;
            }
            

            return AuditoArr;
        }

        /// <summary>
        /// 获取抄送人数组
        /// </summary>
        /// <param name="copyRules">抄送规则</param>
        /// <returns>"[\"1111@com\",\"2222@com\",\"3333@com\"]"</returns>
        public static string GetCopyArr(string copyRules)
        {
            JObject premise = JsonConvert.DeserializeObject<JObject>(copyRules);


            int type = premise.Value<int>("type");//审批类别 
            int level = premise.Value<int>("level");//主管级别
            string copyArr = null;

            switch (type)
            {
                case 1://指定成员
                {
                    JArray name = premise.Value<JArray>("name");//角色或者指定人的数组

                    copyArr = JsonConvert.SerializeObject(name);
                    }
                    break;
                case 2://角色
                {
                    JArray name = premise.Value<JArray>("name");//角色或者指定人的数组

                    copyArr = BasicsEngine.GetUserForRole(name);
                    }
                    break;
                case 3://主管
                {
                   
                }
                    break;
               
            }


            return copyArr;
        }


        /// <summary>
        /// 处理审批和抄送
        /// 需要消息 ID
        /// 接口 API 调用时，直接传整个审批流
        /// </summary>
        /// <param name="auditFlow">Audit flow.</param>
        public static void ProcessMsgToAuditAndCopy(AuditFlow auditFlow)
        {
            AuditRoute auditRoute = BasicsEngine.GetAuditRoute(auditFlow.MsgID);

            switch (auditRoute.AuditMethod)
            {
                case (int)AuditMethod.yici:
                    yici(auditRoute, auditFlow);
                    break;

                case (int)AuditMethod.hui:
                    huiqian(auditRoute, auditFlow);
                    break;

                case (int)AuditMethod.huo:
                    huoqian(auditRoute, auditFlow);
                    break;
            }
        }

        /// <summary>
        /// 推抄送
        /// </summary>
        /// <param name="copies"></param>
        /// <param name="id"></param>
        private static void PushCopies(string[] copies, int id)
        {
            if (copies.Length > 0)
            {
                List<CopyFlow> copyFlows = new List<CopyFlow>();

                foreach (string str in copies)
                {
                    CopyFlow copyFlow = new CopyFlow();
                    copyFlow.CopyJID = str;
                    copyFlow.MsgID = id;
                    copyFlows.Add(copyFlow);
                }

                if (!BasicsEngine.AddCopyFlow(copyFlows))
                {
                    Log.ToFile("推送抄送失败");
                }
            }
        }

        /// <summary>
        /// 依次审批
        /// 需要所有信息
        /// </summary>
        private static void yici(AuditRoute auditRoute, AuditFlow auditFlow)
        {
            try
            {
                //获取审核人数组
                string[] AuditorArray = JsonConvert.DeserializeObject<string[]>(auditRoute.Auditor);
                string[] CopiesArray = JsonConvert.DeserializeObject<string[]>(auditRoute.Copies);
                //获取审核表状态
                List<AuditFlow> auditFlows = BasicsEngine.GetAuditFlows(auditRoute.MsgID);
                //如果有审核内容
                if (auditFlows.Count > 0)
                {
                    //如果拒绝，改变审批状态
                    if (auditFlow.AuditState == (int)MsgState.NO)
                    {
                        MsgFlow msgFlow = new MsgFlow();
                        msgFlow.State = (int)MsgState.NO;
                        msgFlow.ID = auditRoute.MsgID;
                        bool b = BasicsEngine.ProcessMsgFlow(msgFlow);
                        BasicsEngine.UpdAuditFormNilToInvalid(auditRoute.MsgID);
                    }
                    //如果同意，发给下一个人审批、或者审批通过，抄送
                    else if (auditFlow.AuditState == (int)MsgState.Yes)
                    {
                        //获取当前是第几个人
                        int index = AuditorArray.ToList().IndexOf(auditFlow.AuditorJID);
                        //如果小于数组，接着发送审核
                        if (index < AuditorArray.Length - 1)
                        {
                            bool b = BasicsEngine.AddAuditFlow(AuditorArray[index + 1], auditRoute.MsgID);
                        }
                        //如果已经不小于，说明是最后一个人了，改变状态，抄送
                        else
                        {
                            MsgFlow msgFlow = new MsgFlow();
                            msgFlow.State = (int)MsgState.Yes;
                            msgFlow.ID = auditRoute.MsgID;

                            bool b = BasicsEngine.ProcessMsgFlow(msgFlow);

                            //进入抄送环节
                            PushCopies(CopiesArray, auditRoute.MsgID);
                        }
                    }
                }
                else
                {
                    if (AuditorArray.Length > 0)
                    {
                        //如果没有审核内容，发送数组第一个人到审核流
                        bool b = BasicsEngine.AddAuditFlow(AuditorArray[0], auditRoute.MsgID);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("会签过程出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 或签
        /// </summary>
        /// <param name="auditRoute">Audit route.</param>
        private static void huoqian(AuditRoute auditRoute, AuditFlow auditFlow)
        {
            try
            {
                //获取审核人数组
                string[] AuditorArray = JsonConvert.DeserializeObject<string[]>(auditRoute.Auditor);
                string[] CopiesArray = JsonConvert.DeserializeObject<string[]>(auditRoute.Copies);
                //获取所有审核的表
                List<AuditFlow> auditFlows = BasicsEngine.GetAuditFlows(auditRoute.MsgID);
                //如果已经有，就看第一个人是同意还是不同意，就决定状态
                if (auditFlows.Count > 0)
                {
                    if (auditFlow.AuditState == (int)MsgState.NO)
                    {
                        MsgFlow msgFlow = new MsgFlow();
                        msgFlow.State = (int)MsgState.NO;
                        msgFlow.ID = auditRoute.MsgID;
                        bool b = BasicsEngine.ProcessMsgFlow(msgFlow);

                        BasicsEngine.UpdAuditFormNilToInvalid(auditRoute.MsgID);
                    }
                    else if (auditFlow.AuditState == (int)MsgState.Yes)
                    {
                        MsgFlow msgFlow = new MsgFlow();
                        msgFlow.State = (int)MsgState.Yes;
                        msgFlow.ID = auditRoute.MsgID;

                        bool b = BasicsEngine.ProcessMsgFlow(msgFlow);

                        //进入抄送环节
                        PushCopies(CopiesArray, auditRoute.MsgID);
                    }
                }
                else
                {
                    List<AuditFlow> auditFlowList = new List<AuditFlow>();
                    for (int i = 0; i < AuditorArray.Length; i++)
                    {
                        AuditFlow flow = new AuditFlow();
                        flow.AuditorJID = AuditorArray[i];
                        flow.MsgID = auditRoute.MsgID;
                        flow.AuditState = (int)AuditState.Nil;
                        auditFlowList.Add(flow);
                    }

                    bool b = BasicsEngine.AddAuditFlow(auditFlowList);
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("或签过程出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 会签
        /// </summary>
        /// <param name="auditRoute">Audit route.</param>
        private static void huiqian(AuditRoute auditRoute, AuditFlow auditFlow)
        {
            try
            {
                //获取审核人数组
                string[] AuditorArray = JsonConvert.DeserializeObject<string[]>(auditRoute.Auditor);
                string[] CopiesArray = JsonConvert.DeserializeObject<string[]>(auditRoute.Copies);
                //获取所有审核的表
                List<AuditFlow> auditFlows = BasicsEngine.GetAuditFlows(auditRoute.MsgID);
                //如果已经有，不同意，就改状态，同意，就再等等
                if (auditFlows.Count > 0)
                {
                    if (auditFlow.AuditState == (int)MsgState.NO)
                    {
                        MsgFlow msgFlow = new MsgFlow();
                        msgFlow.State = (int)MsgState.NO;
                        msgFlow.ID = auditRoute.MsgID;
                        bool b = BasicsEngine.ProcessMsgFlow(msgFlow);
                        BasicsEngine.UpdAuditFormNilToInvalid(auditRoute.MsgID);
                    }
                    else if (auditFlow.AuditState == (int)MsgState.Yes)
                    {
                        if (auditFlows.FindAll(a => a.AuditState == (int)MsgState.NO).Count > 0)
                        {
                            MsgFlow msgFlow = new MsgFlow();
                            msgFlow.State = (int)MsgState.NO;
                            msgFlow.ID = auditRoute.MsgID;
                            bool b = BasicsEngine.ProcessMsgFlow(msgFlow);
                            BasicsEngine.UpdAuditFormNilToInvalid(auditRoute.MsgID);
                        }
                        else if (auditFlows.FindAll(a => a.AuditState == (int)MsgState.Yes).Count == AuditorArray.Length)
                        {
                            MsgFlow msgFlow = new MsgFlow();
                            msgFlow.State = (int)MsgState.Yes;
                            msgFlow.ID = auditRoute.MsgID;

                            bool b = BasicsEngine.ProcessMsgFlow(msgFlow);

                            //进入抄送环节
                            PushCopies(CopiesArray, auditRoute.MsgID);
                        }
                    }
                }
                else
                {
                    List<AuditFlow> auditFlowList = new List<AuditFlow>();
                    for (int i = 0; i < AuditorArray.Length; i++)
                    {
                        AuditFlow flow = new AuditFlow();
                        flow.AuditorJID = AuditorArray[i];
                        flow.MsgID = auditRoute.MsgID;
                        flow.AuditState = (int)AuditState.Nil;
                        auditFlowList.Add(flow);
                    }

                    bool b = BasicsEngine.AddAuditFlow(auditFlowList);
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("会签过程出错：" + ex.Message);
            }
        }
    }
}