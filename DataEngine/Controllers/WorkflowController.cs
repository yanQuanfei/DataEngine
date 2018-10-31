﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataEngine.Models;
using Engine;
using Engine.Tool;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace DataEngine.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]

    [SwaggerTag("工作流")]
    public class WorkflowController : ControllerBase
    {
//        // GET api/values
//        [HttpGet]
//        public ActionResult<IEnumerable<string>> Get()
//        {
//                        MsgFlow msg = new MsgFlow();
//                        msg.Initiator = "闫全飞";
//                        msg.UserJID = "1042";
//                        msg.RecordID = 51;
//                        msg.Classify = 0;
//                        msg.LaunchTime = DateTime.Now.ToString();
//                        msg.State = (int)MsgState.Nil;
//
//             // msg.ID = BasicsEngine.AddMsgFlow("闫全飞", "1024", 1, 1);
//
////            string[] arr= new string []{ "auto_group21_netrole@im.sino-med.net", "auto_group22_netrole@im.sino-med.net" };
////            string aq = "auto_group22_netrole@im.sino-med.net";
////            string a = BasicsEngine.GetUserForRole(arr);
//
//           // AdvancedEngine.FilterMsgToAuditRoute(msg);
//
//            //string json= Newtonsoft.Json.JsonConvert.SerializeObject(msg) ;
//
//
//            //Program.FilterQueue.Enqueue(msg);
//            //           
//            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue111111111111");
//            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue222222222222");
//            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue333333333333");
//
////            object job=  BasicsEngine.GetJobject(51,0);
////            string json = Newtonsoft.Json.JsonConvert.SerializeObject(job);
////
////            string str = ClassifyConfig.GetClassifyStr(0);
//
//
//
//           ;
//           
//            return new string[] {  AdvancedEngine.GetAuditoArr(
//                "{\r\n\"type\":\"3\",\r\n\"level\":\"3\",\r\n\"name\":[\"auto_group21_netrole@im.sino-med.net\", \"auto_group22_netrole@im.sino-med.net\"]\r\n}")};
//        }
//
//        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult<string> Get(int id)
//        {
//           
//                //AuditFlow auditFlow = new AuditFlow();
//                //auditFlow.AuditState = (int)AuditState.Yes;
//                //auditFlow.AuditOpinion = "同意";
//                //auditFlow.ID = 5;
//                //auditFlow.MsgID = 54;
//                //auditFlow.AuditorJID = "2222@com";
//
//                //bool b = BasicsEngine.UpdAuditFlow(auditFlow);
//                //Program.ProcessQueue.Enqueue(auditFlow);
//
//                return "";
//           
//
//        }
//
//        // POST api/values
        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(Summary = "添加消息")]
        public Response PostAddMsg([ SwaggerParameter("{\"Initiator\":\"闫全飞\",\"UserJID\":\"1042\",\"Classify\":0,\"RecordID\":51}", Required = true)]dynamic m)
        {
            var result = new Response();
            MsgFlow msg = new MsgFlow();
            msg.Initiator =m["Initiator"];
            msg.UserJID = m["UserJID"];
            msg.RecordID = m["RecordID"];
            msg.Classify = m["Classify"];
            msg.LaunchTime = DateTime.Now.ToString();
            msg.State = (int)MsgState.Nil;

            if (ClassifyConfig.GetClassifyStr(msg.Classify)!=null)
            {
                msg.ID = BasicsEngine.AddMsgFlow(msg.Initiator, msg.UserJID, msg.Classify, msg.RecordID);

                if (msg.ID > 0)
                {
                    Program.FilterQueue.Enqueue(msg);

                }
                else
                {
                    result.Code = 500;
                    result.Message = "添加失败";
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                    Log.ToFile("添加失败，消息json：" + json);
                }
            }
            else
            {
                result.Code = 500;
                result.Message = "类型不存在";
            }

            return result;

        }

        [AllowAnonymous]
        [HttpPut]
        [SwaggerOperation(Summary = "审批")]
        public Response PutAudit([ SwaggerParameter("{\"ID\":10,\"AuditState\":2,\"AuditOpinion\":\"不同意\"}", Required = true)]dynamic audit)
        {
            var result = new Response();
            AuditFlow auditFlow = new AuditFlow();

            int id = audit["ID"];
            AuditState state = audit["AuditState"];
            string Opinion = audit["AuditOpinion"];
            
            auditFlow = BasicsEngine.UpdAuditFlow(id,state,Opinion);

            if (auditFlow != null)
            {
                Program.ProcessQueue.Enqueue(auditFlow);
            }
            else
            {
                result.Code = 500;
                result.Message = "审核失败";
            }
            return result;

        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "撤回消息")]
        public Response DelMsg([ SwaggerParameter("", Required = true)]int id)
        {
            var result = new Response();
            if (id > 0)
            {
               bool b= BasicsEngine.DelMsg(id);
                if (!b)
                {
                    result.Code = 500;
                    result.Message = "删除失败，该消息状态已不允许删除";
                }
              
            }
            else
            {
                result.Code = 500;
                result.Message = "参数错误";
            }

            return result;
        }
    }
}