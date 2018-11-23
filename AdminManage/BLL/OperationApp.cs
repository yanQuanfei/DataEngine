using System.Collections.Generic;
using System.Data;
using System.Linq;
using Admin.Interface;
using Admin.Models;
using Dapper;
using DAL;


namespace Admin.BLL
{
    public class OperationApp:IOperation
    {
        public List<User> GetData()
        {
            //分页
            using (IDbConnection conn = DapperContext.MsSqlConnection())
            
            {
//                int pageIndex = 0;
//                int pageSize = 2;
                //string sqlCommandText = string.Format(@"SELECT * FROM USERS  LIMIT {0},{1} ", pageIndex * pageSize, pageSize);
                string sqlCommandText = string.Format(@"SELECT * FROM employeesTree ");
                List<User> users = conn.Query<User>(sqlCommandText).ToList();
                return users;
            }

           
        }

        public Data GetData(int id)
        {
            throw new System.NotImplementedException();
        }

        public Data AddData(Data data)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdData(Data data)
        {
            throw new System.NotImplementedException();
        }

        public bool DelData(Data data)
        {
            throw new System.NotImplementedException();
        }
    }

   
}