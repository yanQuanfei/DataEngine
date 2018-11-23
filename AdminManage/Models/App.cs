namespace Admin.Models
{
    public  class App:Data
    {
      
        /// <summary>
        /// App名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        ///  App_Url
        /// </summary>
        public string WebUrl { get; set; }
        /// <summary>
        /// ICON_Url
        /// </summary>
        public string ImageUrl { get; set; }
    }
}