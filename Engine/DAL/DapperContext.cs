using System;
using System.Data.SqlClient;
//using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace Engine
{

    public class SqlContext
    {
        public  string mysqlconnectionString { get; set; }
        public  string mssqlconnectionString { get; set; }
    }




    public class DapperContext
    {

        public static SqlContext Sql;
//        public DapperContext(IOptions<SqlContext> context)
//        {
//            Sql = context.Value;
//           
//        }

        public static MySqlConnection MySqlConnection()
        {
            //连接字符串
            const string mysqlconnectionString = "Server=localhost;port=3306;Database=Test;Uid=root;Pwd=Root123.;SslMode=None;";
            var mysql = new MySqlConnection(mysqlconnectionString);
            mysql.Open();
            return mysql;

        }
        public static SqlConnection MsSqlConnection()
        {
           // string mssqlconnectionString = "server=127.0.0.1;database=sysoffice;User=sa;password=<YourStrong!Passw0rd>;Connect Timeout=1000000";
            string mssqlconnectionString = "server=192.168.2.85;database=SysOffice;User=tbobjects;password=tbobjects2013;Connect Timeout=1000000";
           

            var connection = new SqlConnection(mssqlconnectionString);
            connection.Open();
            return connection;
        }


    }
}
