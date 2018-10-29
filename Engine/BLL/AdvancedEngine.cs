using DataEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Engine.Tool;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
                object job = BasicsEngine.GetJobject(msg.RecordID, msg.Classify);
               


                AuditRoute auditRoute = new AuditRoute();
                auditRoute.AuditMethod = 2;
                auditRoute.MsgID = msg.ID;
                auditRoute.RulesID = 1;
                auditRoute.Auditor = "[\"1111@com\",\"2222@com\",\"3333@com\"]";
                auditRoute.Copies = "[\"1111@com\",\"2222@com\"]";


                bool b = BasicsEngine.AddAuditRoute(auditRoute);

                return b;
            }
            catch (Exception ex)
            {
                Log.ToFile("过滤生成路线图报错ex：" + ex.Message);
                return false;

            }
            
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
