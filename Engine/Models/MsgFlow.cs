using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEngine.Models
{
    /// <summary>
    /// 信息流
    /// </summary>
    public class MsgFlow
    {
        public int ID { get; set; }
        /// <summary>
        /// 发起人
        /// </summary>
        public string Initiator { get; set; }
        /// <summary>
        /// 发起人账号
        /// </summary>
        public string UserJID { get; set; }
        /// <summary>
        /// 类别号
        /// </summary>
        public int Classify { get; set; }
        /// <summary>
        /// 记录ID
        /// </summary>
        public int RecordID { get; set; }
        /// <summary>
        /// 状态码
        /// 0 未审批
        /// 1 批准
        /// 2 拒绝
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 发起时间
        /// </summary>
        public string LaunchTime { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public string AuditTime { get; set; }
        /// <summary>
        /// 适用规则ID
        /// </summary>
        public int RulesID { get; set; }
        /// <summary>
        /// 路线ID
        /// </summary>
        public int RouteID { get; set; }


    }
    /// <summary>
    /// 消息状态
    /// </summary>
    public enum MsgState
    {
        /// <summary>
        /// 未审核
        /// </summary>
        Nil = 0,
        /// <summary>
        /// 批准
        /// </summary>
        Yes = 1,
        /// <summary>
        /// 拒绝
        /// </summary>
        NO = 2

    }

}
