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
        public List<object> GetData()
        {
           
            using (IDbConnection conn = DapperContext.MsSqlConnection())
            
            {
//                int pageIndex = 0;
//                int pageSize = 2;
                //string sqlCommandText = string.Format(@"SELECT * FROM USERS  LIMIT {0},{1} ", pageIndex * pageSize, pageSize);
                string sqlCommandText = string.Format(@"SELECT * FROM employeesTree ");
                List<User> users = conn.Query<User>(sqlCommandText).ToList();
                return (List<object>())users;
            }

           
        }

        object IOperation.GetData(int id)
        {
            return GetData(id);
        }

        public object AddData(object data)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdData(object data)
        {
            
        }

        public bool DelData(int ID)
        {
            throw new System.NotImplementedException();
        }

      
    }

   
}