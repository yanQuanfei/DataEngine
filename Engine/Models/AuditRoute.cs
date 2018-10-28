using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEngine.Models
{

    /// <summary>
    /// 路线表
    /// </summary>
    public class AuditRoute
    {
        public int ID { get; set; }
        /// <summary>
        /// 信息ID
        /// </summary>
        public int MsgID { get; set; }

        /// <summary>
        /// 规则ID
        /// </summary>
        public int RulesID { get; set; }
        /// <summary>
        /// 审核方式
        /// 1：依次审核
        /// 2：会签
        /// 3：或签
        /// 4：一次审核（就是一个人审完就行）
        /// </summary>
        public int AuditMethod { get; set; }
        /// <summary>
        /// 审核人
        /// 格式
        /// [{1:"123@123.com"},{2:"123@123.com"}]
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 抄送人
        /// [{1:"123@123.com"},{2:"123@123.com"}]
        /// </summary>
        public string Copies { get; set; }



    }

    enum AuditMethod
    {
        /// <summary>
        /// 依次审核
        /// </summary>
        yici = 1,
        /// <summary>
        /// 会签
        /// </summary>
        hui = 2,
        /// <summary>
        /// 或签
        /// </summary>
        huo = 3

    }

}
