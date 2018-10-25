using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEngine.Models
{
    /// <summary>
    /// 审核流
    /// </summary>
    class AuditFlow
    {
        /// <summary>
        /// 审核人账号
        /// </summary>
        public string AuditorJID { get; set; }
        /// <summary>
        /// 信息流ID
        /// </summary>
        public int MsgID { get; set; }
        /// <summary>
        /// 审核状态
        /// 0：未审核
        /// 1：同意
        /// 2：拒绝
        /// </summary>
        public int AuditState { get; set; }
        /// <summary>
        /// 审核意见
        /// 默认空时填写
        /// 同意
        /// 拒绝
        /// </summary>
        public string AuditOpinion { get; set; }

    }
}
