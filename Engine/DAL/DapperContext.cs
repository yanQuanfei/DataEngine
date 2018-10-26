using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace Engine
{
    public class DapperContext
    {
        //连接字符串
        const string connectionString = "Server=localhost;port=3306;Database=Test;Uid=root;Pwd=Root123.;SslMode=None;";
        public static MySqlConnection MySqlConnection()
        {

            var mysql = new MySqlConnection(connectionString);
            mysql.Open();
            return mysql;

        }
        public static SqlConnection MsSqlConnection()
        {
//            string sqlconnectionString = "server=127.0.0.1;database=sysoffice;User=sa;password=<YourStrong!Passw0rd>;Connect Timeout=1000000";
            string sqlconnectionString = "server=192.168.2.85;database=SysOffice;User=tbobjects;password=tbobjects2013;Connect Timeout=1000000";
            var connection = new SqlConnection(sqlconnectionString);
            connection.Open();
            return connection;
        }


    }
}
