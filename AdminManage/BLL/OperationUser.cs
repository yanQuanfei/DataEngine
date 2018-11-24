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
    public class OperationUser : IOperation<User>
    {
        public User AddData(User data)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {

                    string sqlCommandText =
                        @"INSERT INTO employeesTree(eid,ename,position,path,UserJID)VALUES(@eid,@ename,@position,@path,@UserJID)";
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
                Log.ToFile("添加User报错：" + ex.Message);
                return null;
            }
        }

        public bool DelData(int ID)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"DELETE FROM [employeesTree] WHERE ID=@ID";
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
                Log.ToFile("删除User报错：" + ex.Message);
                return false;
            }
        }

        public List<User> GetData()
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM employeesTree");
                    List<User> users = conn.Query<User>(sqlCommandText).ToList();
                    return users;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取User列表报错：" + ex.Message);
                return null;
            }
        }

        public User GetData(int id)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM employeesTree  WHERE ID=@ID");
                    User user = conn.Query<User>(sqlCommandText, new { ID = id }).FirstOrDefault();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取User详情报错：" + ex.Message);
                return null;
            }
        }

        public bool UpdData(List<User> data)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"UPDATE employeesTree SET eid=@eid,ename=@ename,position=@position,path=@path,UserJID=UserJID WHERE ID=@ID";
                    int result = conn.Execute(sqlCommandText, data);
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
                Log.ToFile("修改User报错：" + ex.Message);
                return false;
            }
        }
    }
}