using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataEngine.Models;
using Engine;
using Engine.Tool;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DataEngine.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
     

    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
                        MsgFlow msg = new MsgFlow();
                        msg.Initiator = "闫全飞";
                        msg.UserJID = "1042";
                        msg.RecordID = 51;
                        msg.Classify = 0;
                        msg.LaunchTime = DateTime.Now.ToString();
                        msg.State = (int)MsgState.Nil;

             // msg.ID = BasicsEngine.AddMsgFlow("闫全飞", "1024", 1, 1);

//            string[] arr= new string []{ "auto_group21_netrole@im.sino-med.net", "auto_group22_netrole@im.sino-med.net" };
//            string aq = "auto_group22_netrole@im.sino-med.net";
//            string a = BasicsEngine.GetUserForRole(arr);

           // AdvancedEngine.FilterMsgToAuditRoute(msg);

            //string json= Newtonsoft.Json.JsonConvert.SerializeObject(msg) ;


            //Program.FilterQueue.Enqueue(msg);
            //           
            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue111111111111");
            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue222222222222");
            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue333333333333");

//            object job=  BasicsEngine.GetJobject(51,0);
//            string json = Newtonsoft.Json.JsonConvert.SerializeObject(job);
//
//            string str = ClassifyConfig.GetClassifyStr(0);



           ;
           
            return new string[] {  AdvancedEngine.GetAuditoArr(
                "{\r\n\"type\":\"3\",\r\n\"level\":\"3\",\r\n\"name\":[\"auto_group21_netrole@im.sino-med.net\", \"auto_group22_netrole@im.sino-med.net\"]\r\n}")};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
           
                //AuditFlow auditFlow = new AuditFlow();
                //auditFlow.AuditState = (int)AuditState.Yes;
                //auditFlow.AuditOpinion = "同意";
                //auditFlow.ID = 5;
                //auditFlow.MsgID = 54;
                //auditFlow.AuditorJID = "2222@com";

                //bool b = BasicsEngine.UpdAuditFlow(auditFlow);
                //Program.ProcessQueue.Enqueue(auditFlow);

                return "";
           

        }

        // POST api/values
        [AllowAnonymous]
        [HttpPost]
        public void Post(dynamic m)
        {

            MsgFlow msg = new MsgFlow();
            msg.Initiator =m["Initiator"];
            msg.UserJID = m["UserJID"];
            msg.RecordID = m["RecordID"];
            msg.Classify = m["RecordID"];
            msg.LaunchTime = DateTime.Now.ToString();
            msg.State = (int)MsgState.Nil;

            msg.ID = BasicsEngine.AddMsgFlow(msg.Initiator, msg.UserJID,msg.Classify ,msg.RecordID);

            string json= Newtonsoft.Json.JsonConvert.SerializeObject(msg) ;


            Program.FilterQueue.Enqueue(msg);
        }

        [AllowAnonymous]
        [HttpPut]
        public void Put(dynamic audit)
        {

            AuditFlow auditFlow = new AuditFlow();
            //auditFlow.AuditState = audit["AuditState"];
            //auditFlow.AuditOpinion = audit["AuditOpinion"];
            //auditFlow.ID = audit["ID"];
            //auditFlow.MsgID = audit["MsgID"];
            //auditFlow.AuditorJID = audit["AuditorJID"];

            //audit["ID"], audit["AuditState"], audit["AuditOpinion"]
            int id = audit["ID"];
            AuditState state = audit["AuditState"];
            string Opinion = audit["AuditOpinion"];



            auditFlow = BasicsEngine.UpdAuditFlow(id,state,Opinion);
            Program.ProcessQueue.Enqueue(auditFlow);

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
