using System.Collections.Generic;

namespace AdminManage.Interface
{
    public interface IOperation<T>
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        List<T> GetData();


        /// <summary>
        /// 获取一条数据的详细信息
        /// </summary>
        /// <param name="id">消息 ID</param>
        /// <returns></returns>
        T GetData(int id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T AddData(T data);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        bool UpdData(List<T> datas);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        bool DelData(int ID);
    }
}