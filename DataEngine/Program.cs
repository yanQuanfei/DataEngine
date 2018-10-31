using DataEngine.Queue;
using FluentScheduler;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using DataEngine.Models;
using Microsoft.Extensions.Configuration;

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

             //   BuildWebHost(args).Run();

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


        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
            return WebHost.CreateDefaultBuilder(args).UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
        }

  

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}