using DataEngine.Queue;
using FluentScheduler;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using DataEngine.Models;

namespace DataEngine
{
    public class Program
    {
        private static Queue<MsgFlow> FilterMsgQueue;
        private static Queue<AuditFlow> ProcessMsgQueue;

        public static void Main(string[] args)
        {
            FilterMsgQueue = new Queue<MsgFlow>();
            ProcessMsgQueue = new Queue<AuditFlow>();

            PollingQueue queue = new PollingQueue();
            JobManager.Initialize(queue.Start());

            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 过滤队列
        /// </summary>
        public static Queue<MsgFlow> FilterQueue
        {
            get { return FilterMsgQueue; }
            set { FilterMsgQueue = value; }
        }

        /// <summary>
        /// 处理队列
        /// </summary>
        public static Queue<AuditFlow> ProcessQueue
        {
            get { return ProcessMsgQueue; }
            set { ProcessMsgQueue = value; }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}