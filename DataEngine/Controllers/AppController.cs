using System.Collections.Generic;
using Admin.Models;
using AdminManage.BLL;
using AdminManage.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Swashbuckle.AspNetCore.Annotations;

namespace DataEngine.Controllers
{
    [Route("api/[controller]")]
    [SwaggerTag("APP应用")]
    public class AppController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            IOperation<App> operation =new OperationApp();

            operation.GetData();
            
            
            return new string[] { "124567890" };
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