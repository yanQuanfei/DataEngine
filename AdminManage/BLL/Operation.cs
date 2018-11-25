using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Admin.Interface;
using Admin.Models;
using AdminManage.Interface;
using Dapper;
using DAL;
using Tool;

namespace AdminManage.BLL
{
    #region 操作APP

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
                        @"INSERT INTO AppResources(WebUrl,ResourceName,ImageUrl)VALUES(@WebUrl,@ResourceName,@ImageUrl)";
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
                    int result = conn.Execute(sqlCommandText, new {ID = ID});
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

    #endregion

    #region 操作角色

    public class OperationRole : IOperation<Role>
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
                    Role role = conn.Query<Role>(sqlCommandText, new {ID = id}).FirstOrDefault();
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
                    int result = conn.Execute(sqlCommandText, new {ID = ID});
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

    #endregion

    #region 操作用户

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
                    int result = conn.Execute(sqlCommandText, new {ID = ID});
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
                    User user = conn.Query<User>(sqlCommandText, new {ID = id}).FirstOrDefault();
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

    #endregion
}