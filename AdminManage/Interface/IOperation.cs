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
        List<Data> GetData();

        /// <summary>
        /// 获取一条数据的详细信息
        /// </summary>
        /// <param name="id">消息 ID</param>
        /// <returns></returns>
        Data GetData(int id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Data AddData(Data data);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdData(Data data);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool DelData(Data data);
    }
}