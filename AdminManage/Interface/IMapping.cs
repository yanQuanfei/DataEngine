using System.Collections.Generic;
using Admin.Models;

namespace Admin.Interface
{
    public interface IMapping
    {
        /// <summary>
        /// 添加映射
        /// </summary>
        
        /// <returns></returns>
        bool AddMapping(Mapping mapping);
        /// <summary>
        /// 移除映射
        /// </summary>
      
        /// <returns></returns>
        bool  DelMapping(Mapping mapping);
    }
}