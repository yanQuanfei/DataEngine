using System;
using MySql.Data.MySqlClient;

namespace Engine
{
    public class DapperContext
    {
        //连接字符串
        const string connectionString = "Server=localhost;port=3306;Database=Test;Uid=root;Pwd=Root123.;SslMode=None;";
        public static MySqlConnection Connection()
        {

            var mysql = new MySqlConnection(connectionString);
            mysql.Open();
            return mysql;

        }
    }
}
