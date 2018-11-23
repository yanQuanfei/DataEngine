namespace Admin.Models
{
    public class User:Data
    {
        /// <summary>
        /// 员工号
        /// </summary>
        public int eid { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string ename { get; set; }
        
        /// <summary>
        /// 员工职位
        /// </summary>
        public string position { get; set; }
        
        /// <summary>
        /// 树状路径
        /// </summary>
        public string path { get; set; }
        
        /// <summary>
        /// 员工登陆账号
        /// </summary>
        public string UserJID { get; set; }
    }
}