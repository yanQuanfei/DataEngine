using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Admin.Interface;
using Admin.Models;
using Dapper;
using DAL;
using Tool;

namespace Admin.BLL
{
    public class OperationRole:IOperation<Role>
    {
        public List<Role> GetData()
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM APPRole");
                    List<Role> roles = conn.Query<Role>(sqlCommandText).ToList();
                    return roles;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取Role列表报错：" + ex.Message);
                return null;
            }
        }

        public Role GetData(int id)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM APPRole  WHERE ID=@ID");
                    Role role = conn.Query<Role>(sqlCommandText, new { ID = id }).FirstOrDefault();
                    return role;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取Role详情报错：" + ex.Message);
                return null;
            }
        }

        public Role AddData(Role data)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {

                    string sqlCommandText =
                        @"INSERT INTO APPRole(Name)VALUES(@Name)";
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
                Log.ToFile("添加Role报错：" + ex.Message);
                return null;
            }
        }

        public bool UpdData(List<Role> datas)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"UPDATE AppResources SET WebUrl=@WebUrl,ResourceName=@ResourceName,ImageUrl=@ImageUrl WHERE ID=@ID";
                    int result = conn.Execute(sqlCommandText, datas);
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
                Log.ToFile("修改Role报错：" + ex.Message);
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
                        @"DELETE FROM [APPRole] WHERE ID=@ID";
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
                Log.ToFile("删除Role报错：" + ex.Message);
                return false;
            }
        }
    }
}