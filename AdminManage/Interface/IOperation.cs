using System.Collections.Generic;
using Admin.Models;

namespace Admin.Interface
{
    public interface IOperation
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        List<object> GetData();
      

        /// <summary>
        /// 获取一条数据的详细信息
        /// </summary>
        /// <param name="id">消息 ID</param>
        /// <returns></returns>
        object GetData(int id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        object AddData(object data);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdData(object data);

        /// <summary>
        /// 删除数据
        /// </summary>
       
        /// <returns></returns>
        bool DelData(int ID);
    }
}