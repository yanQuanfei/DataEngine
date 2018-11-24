using System.Collections.Generic;

namespace Admin.Models
{




    public class RoleApp
    {
        /// <summary>
        /// 角色
        /// </summary>
         public Role role { get; set; }
        /// <summary>
        /// 资源组
        /// </summary>
        public List<App> apps{ get; set; }
    }

    public class AppRole
    {
        /// <summary>
        /// 资源
        /// </summary>
        public App app{ get; set; }
        /// <summary>
        /// 角色组
        /// </summary>
        public List<Role>roles{ get; set; }
    }
}