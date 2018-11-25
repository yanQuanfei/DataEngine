using System.Collections.Generic;
using Admin.Models;

namespace Admin.Interface
{
    public  interface IMapping<T>
    {

        /// <summary>
        /// 获取映射关系,根据第一个参数
        /// </summary>
        /// <returns></returns>
        List<T> GetMappingsByFirst(T Mapping);
        /// <summary>
        /// 获取映射关系，根据第二参数
        /// </summary>
        /// <param name="Mapping"></param>
        /// <returns></returns>
        List<T> GetMappingsBySecond(T Mapping);
        /// <summary>
        /// 添加映射
        /// </summary>
        
        /// <returns></returns>
        bool AddMapping(List<T> Mappings);
        /// <summary>
        /// 移除映射
        /// </summary>
      
        /// <returns></returns>
        bool  DelMapping(List<T> Mappings);
    }
}