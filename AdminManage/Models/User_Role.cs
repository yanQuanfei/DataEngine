using System.Collections.Generic;

namespace Admin.Models
{
    public class UserRole:Mapping
    {
        /// <summary>
        /// 用户
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// 角色组
        /// </summary>
        public List<Role> roles { get; set; }
    }

    public class RoleUser:Mapping
    {
        /// <summary>
        /// 角色
        /// </summary>
        public Role role { get; set; }
        /// <summary>
        /// 用户组
        /// </summary>
        public List<User> users { get; set; }
    }
    
}