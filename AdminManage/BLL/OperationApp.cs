using Admin.Interface;
using Admin.Models;
using DAL;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Tool;

namespace Admin.BLL
{
    public class OperationApp : IOperation<App>
    {
        public List<App> GetData()
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM AppResources");
                    List<App> apps = conn.Query<App>(sqlCommandText).ToList();
                    return apps;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取App列表报错：" + ex.Message);
                return null;
            }
        }

        public App GetData(int id)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM AppResources  WHERE ID=@ID");
                    App app = conn.Query<App>(sqlCommandText, new {ID = id}).FirstOrDefault();
                    return app;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取App详情报错：" + ex.Message);
                return null;
            }
          
        }

        public App AddData(App data)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                   
                    string sqlCommandText =
                        @"INSERT INTO AppResources(WebUrl,ResourceName,ImageUrl,)VALUES(@WebUrl,@ResourceName,@ImageUrl)";
                    sqlCommandText += "SELECT CAST(SCOPE_IDENTITY() as int)";
                    int result = conn.Query<int>(sqlCommandText, data).FirstOrDefault();

                    if (result > 0)
                    {
                        data.ID = result;
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("添加App报错：" + ex.Message);
                return null;
            }
        }

        public bool UpdData(List<App> data)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"UPDATE AppResources SET WebUrl=@WebUrl,ResourceName=@ResourceName,ImageUrl=@ImageUrl WHERE ID=@ID";
                    int result = conn.Execute(sqlCommandText,data);
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("修改App报错：" + ex.Message);
                return false;
            }
        }

        public bool DelData(int ID)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"DELETE FROM [AppResources] WHERE ID=@ID";
                    int result = conn.Execute(sqlCommandText, new { ID = ID });
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("删除App报错：" + ex.Message);
                return false;
            }
        }
    }
}