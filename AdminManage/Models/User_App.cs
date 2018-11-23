using System.Collections.Generic;

namespace Admin.Models
{
    public class UserApp:Mapping
    {
        /// <summary>
        /// 员工
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// 资源组
        /// </summary>
        public List<App>apps { get; set; }
    }
    
    public class AppUser:Mapping
    {
        /// <summary>
        /// 资源
        /// </summary>
       public App app { get; set; }
        /// <summary>
        /// 用户组
        /// </summary>
       public List<User> users { get; set; }
    }
    
}