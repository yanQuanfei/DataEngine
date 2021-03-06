﻿using System;
using System.Data.SqlClient;
//using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

using Tool;

namespace DAL
{
    

    public class DapperContext
    {
        private static string mssql = DataBaseConfig.GetDataBaseStr("mssql");


        private static string mysql = DataBaseConfig.GetDataBaseStr("mysql");
       

        public static string msSql
        {
            get { return mssql;}
            set { mssql = DataBaseConfig.GetDataBaseStr("mssql"); }
        }
        public static string mySql
        {
            get { return mysql;}
            set { mysql = DataBaseConfig.GetDataBaseStr("mysql"); }
        }
      
        public static MySqlConnection MySqlConnection()
        {
            //连接字符串
             string mysqlconnectionString = mySql; 
            var mysql = new MySqlConnection(mysqlconnectionString);
            mysql.Open();
            return mysql;

        }
        public static SqlConnection MsSqlConnection()
        {
          
           string mssqlconnectionString = msSql;
           

            var connection = new SqlConnection(mssqlconnectionString);
            connection.Open();
            return connection;
        }

         
    }
}
