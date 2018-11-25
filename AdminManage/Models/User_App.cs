using System.Collections.Generic;

namespace Admin.Models
{
    public class UserApp : Mapping
    {
        /// <summary>
        /// 员工
        /// </summary>
        public string UserJID { get; set; }

        /// <summary>
        /// 资源组
        /// </summary>
        public int ResourcesID { get; set; }
    }
}