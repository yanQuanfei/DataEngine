using System.Collections.Generic;

namespace Admin.Models
{
    public class UserRole : Mapping
    {
        /// <summary>
        /// 用户
        /// </summary>
        public string UserJID { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int RoleID { get; set; }
    }
}