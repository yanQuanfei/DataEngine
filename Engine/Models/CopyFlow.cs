using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEngine.Models
{
    /// <summary>
    /// 抄送流
    /// </summary>
    public class CopyFlow
    {
        public int ID { get; set; }
        /// <summary>
        /// 抄送人账号
        /// </summary>
        public string CopyJID { get; set; }
        /// <summary>
        /// 信息ID
        /// </summary>
        public int MsgID { get; set; }
        /// <summary>
        /// 抄送评价
        /// </summary>
        public string CopyOpinion { get; set; }


    }
}
