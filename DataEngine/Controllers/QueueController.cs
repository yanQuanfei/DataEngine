﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataEngine.Controllers
{
    /// <summary>
    /// 查看还有多少未处理的消息
    /// </summary>
    [Route("api/[controller]")]
    [SwaggerTag("队列情况")]
    public class QueueController : Controller
    {
        // GET: api/Queue
        /// <summary>
        /// 获取队列的未处理情况
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Summary="获取队列未处理")]
        public IEnumerable<string> Get()
        {

            string ProcessQueueText ="处理队列还有" +Program.ProcessQueue.Count.ToString()+"条未处理";
            string FilterQueueText = "过滤队列还有" + Program.FilterQueue.Count.ToString()+ "条未处理";

            return new string[] { FilterQueueText, ProcessQueueText };
        }

       
    }
}
