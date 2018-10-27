using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataEngine.Controllers
{
    /// <summary>
    /// 查看还有多少未处理的消息
    /// </summary>
    [Route("api/[controller]")]
    public class QueueController : Controller
    {
        // GET: api/Queue
        [HttpGet]
        public IEnumerable<string> Get()
        {

            string ProcessQueueText ="处理队列还有" +Program.ProcessQueue.Count.ToString()+"条未处理";
            string FilterQueueText = "过滤队列还有" + Program.FilterQueue.Count.ToString()+ "条未处理";

            return new string[] { FilterQueueText, ProcessQueueText };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
