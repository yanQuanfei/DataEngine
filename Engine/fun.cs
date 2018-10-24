using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using static Engine.Model;

namespace Engine
{
    public class fun
    {
        public List<Users> GetUsers()
        {
            //分页数据
            List<Users> users = new List<Users>();
            using (IDbConnection connection = DapperContext.Connection())
            {

                users = connection.Query<Users>("select * from test").AsList<Users>();
            }
            return users;

        }
    }
}
