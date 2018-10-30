using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataEngine.Models;
using Engine;
using Engine.Tool;
using FluentScheduler;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataEngine.Queue
{
    /// <summary>
    /// 队列轮询定时器
    /// </summary>
    public class PollingQueue : Registry
    {
        /// <summary>
        /// 定时器启动
        /// </summary>
        /// <returns></returns>
        public Registry Start()
        {
            Registry registry = new Registry();
            // Schedule an IJob to run at an interval
            // 立即执行每两秒一次的计划任务。（指定一个时间间隔运行，根据自己需求，可以是秒、分、时、天、月、年等。）
            registry.Schedule<FilterMsg>().ToRunNow().AndEvery(5).Seconds();
            registry.Schedule<ProcessMsg>().ToRunNow().AndEvery(5).Seconds();

            //// Schedule an IJob to run once, delayed by a specific time interval
            //// 延迟一个指定时间间隔执行一次计划任务。（当然，这个间隔依然可以是秒、分、时、天、月、年等。）
            //registry.Schedule<MyJob>().ToRunOnceIn(5).Seconds();

            return registry;
        }


        /// <summary>
        /// 过滤队列出列
        /// </summary>
        public class FilterMsg : IJob
        {
            public void Execute()
            {
               
                try
                {
                    ////取出最先加进去的元素，并删除，充分体现队列的先进先出的特性
                    ///如队列中无元素，则会引发异常
                    if (Program.FilterQueue != null && Program.FilterQueue.Count > 0)
                    {

                        MsgFlow msg = Program.FilterQueue.Dequeue();

                        string msgJson = Newtonsoft.Json.JsonConvert.SerializeObject(msg);

                      bool b=  AdvancedEngine.FilterMsgToAuditRoute(msg);
                        if (b)
                        {

                            AuditFlow auditFlow = new AuditFlow();
                            auditFlow.MsgID = msg.ID;
                            //主要是来源不一样
                            //过滤来的，只有 ID
                            //API来的有整个审批流
                            Program.ProcessQueue.Enqueue(auditFlow);
                            Log.ToFile("过滤队列消息：" + msgJson);
                        }
                        else
                        {
                            Log.ToFile("过滤队列消息：" + msgJson);
                            Log.ToFile("过滤队列循环,创建路线图失败,未加入处理队列");
                        }

                    }
                }
                catch (Exception ex)
                {
                    Log.ToFile("过滤队列出现错误" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 处理队列出列
        /// </summary>
        public class ProcessMsg : IJob
        {
            public void Execute()
            {

                try
                {
                    ////取出最先加进去的元素，并删除，充分体现队列的先进先出的特性
                    ///如队列中无元素，则会引发异常
                    if (Program.ProcessQueue != null && Program.ProcessQueue.Count > 0)
                    {
                       
                        AuditFlow auditFlow = Program.ProcessQueue.Dequeue();

                        AdvancedEngine.ProcessMsgToAuditAndCopy(auditFlow);
                       
                        string msgJson = Newtonsoft.Json.JsonConvert.SerializeObject(auditFlow);
                        Log.ToFile("处理队列：" + msgJson);
                    
                     
                    }
                }
                catch (Exception ex)
                {
                    Log.ToFile("处理队列出现错误" + ex.Message);
                }
            }
        }


    }
}
