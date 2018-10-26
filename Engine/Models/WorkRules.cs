using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEngine.Models
{
    /// <summary>
    /// 工作规则
    /// </summary>
    class WorkRules
    {
        public int ID { get; set; }
        /// <summary>
        /// 申请类别
        /// </summary>
        public int ClassifyID { get; set; }

        /// <summary>
        /// 审批规则
        /// </summary>
        public string AuditorRules { get; set; }
        /// <summary>
        /// 抄送规则
        /// </summary>
        public string CopyRules { get; set; }

        /// <summary>
        /// 规则条件
        /// 为空的话，用默认两字代替
        /// </summary>
        public string Premise { get; set; }
        /// <summary>
        /// 优先级
        /// 0：代表默认
        /// 数字越大代表优先级越高
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 审核方式
        /// 1：依次审核
        /// 2：会签
        /// 3：或签
        /// 4：一次审核（就是一个人审完就行）
        /// </summary>
        public int AuditMethod { get; set; }




    }
}
