using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataEngine.Models;
using Engine;
using Engine.Tool;
using Microsoft.AspNetCore.Mvc;

namespace DataEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            //
            //            AuditRoute auditRoute = new AuditRoute();
            //            auditRoute.AuditMethod = 1;
            //            auditRoute.MsgID = 23;
            //            auditRoute.RulesID = 1;
            //            auditRoute.Auditor = "[{1:123@com}]";
            //            auditRoute.Copies = "[{1:123@com}]";
            //          
            //            MsgFlow msg = new MsgFlow();
            //            msg.Initiator = "闫全飞";
            //            msg.UserJID = "1042";
            //            msg.RecordID = 1;
            //            msg.Classify = 1;
            //            msg.LaunchTime = DateTime.Now.ToString();
            //            msg.State = (int)MsgState.Nil;
            //
            //            msg.ID = BasicsEngine.AddMsgFlow("闫全飞", "1024", 1, 1);
            //
            //          string json= Newtonsoft.Json.JsonConvert.SerializeObject(msg) ;
            //
            //
            //            Program.FilterQueue.Enqueue(msg);
            //           
            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue111111111111");
            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue222222222222");
            ////            Program.ProcessQueue.Enqueue("ProcessMsgQueue333333333333");


            string str = ClassifyConfig.GetClassifyStr(0);
           
            return new string[] { str };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
