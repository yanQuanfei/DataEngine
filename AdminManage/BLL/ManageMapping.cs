using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Admin.Interface;
using Admin.Models;
using Dapper;
using DAL;
using Tool;

namespace AdminManage.BLL
{
    #region 管理角色APP关系映射

    /// <summary>
    /// 管理角色到 App 的关系
    /// </summary>
    class RoleToAppMapping : IMapping<RoleApp>
    {
        public List<RoleApp> GetMappingsByFirst(RoleApp mapping)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM RoleResourcesMapping where RoleID=@RoleID");
                    List<RoleApp> roleApps = conn.Query<RoleApp>(sqlCommandText, mapping).ToList();
                    return roleApps;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取角色 APP 列表报错：" + ex.Message);
                return null;
            }
        }

        public List<RoleApp> GetMappingsBySecond(RoleApp mapping)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        string.Format(@"SELECT * FROM RoleResourcesMapping where ResourcesID=@ResourcesID");
                    List<RoleApp> roleApps = conn.Query<RoleApp>(sqlCommandText, mapping).ToList();
                    return roleApps;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取角色 APP 列表报错：" + ex.Message);
                return null;
            }
        }

        public bool AddMapping(List<RoleApp> mappings)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"INSERT INTO RoleResourcesMapping(RoleID,ResourcesID)VALUES(@RoleID,@ResourcesID)";
                    int result = conn.Query<int>(sqlCommandText, mappings).FirstOrDefault();

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
                Log.ToFile("添加角色 APP映射报错：" + ex.Message);
                return false;
            }
        }

        public bool DelMapping(List<RoleApp> mappings)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"DELETE FROM [RoleResourcesMapping] WHERE ID=@ID";
                    int result = conn.Execute(sqlCommandText, mappings);
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
                Log.ToFile("删除角色App映射报错：" + ex.Message);
                return false;
            }
        }
    }

    #endregion

    #region 管理用户到角色的关系

    /// <summary>
    /// 管理用户到角色的关系
    /// </summary>
    class UserToRoleMapping : IMapping<UserRole>
    {
        public List<UserRole> GetMappingsByFirst(UserRole mapping)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM UserResourcesMapping where UserJID=@UserJID");
                    List<UserRole> userRoles = conn.Query<UserRole>(sqlCommandText, mapping).ToList();
                    return userRoles;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取用户角色列表报错：" + ex.Message);
                return null;
            }
        }

        public List<UserRole> GetMappingsBySecond(UserRole mapping)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM UserResourcesMapping where RoleID=@RoleID");
                    List<UserRole> userRoles = conn.Query<UserRole>(sqlCommandText, mapping).ToList();
                    return userRoles;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取用户角色列表报错：" + ex.Message);
                return null;
            }
        }

        public bool AddMapping(List<UserRole> mappings)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"INSERT INTO UserResourcesMapping(UserJID,RoleID)VALUES(@UserJID,@RoleID)";
                    int result = conn.Query<int>(sqlCommandText, mappings).FirstOrDefault();

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
                Log.ToFile("添加用户角色映射报错：" + ex.Message);
                return false;
            }
        }

        public bool DelMapping(List<UserRole> mappings)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"DELETE FROM [UserResourcesMapping] WHERE ID=@ID";
                    int result = conn.Execute(sqlCommandText, mappings);
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
                Log.ToFile("删除用户角色映射报错：" + ex.Message);
                return false;
            }
        }
    }

    #endregion

    #region 管理用户到APP的关系

    /// <summary>
    /// 管理用户到 APP 的关系
    /// </summary>
    class UserToAppMapping : IMapping<UserApp>
    {
        public List<UserApp> GetMappingsByFirst(UserApp mapping)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM UserResourcesMapping where UserJID=@UserJID");
                    List<UserApp> userApps = conn.Query<UserApp>(sqlCommandText, mapping).ToList();
                    return userApps;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取用户app列表报错：" + ex.Message);
                return null;
            }
        }

        public List<UserApp> GetMappingsBySecond(UserApp mapping)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText = string.Format(@"SELECT * FROM UserResourcesMapping where ResourcesID=@ResourcesID");
                    List<UserApp> userApps = conn.Query<UserApp>(sqlCommandText, mapping).ToList();
                    return userApps;
                }
            }
            catch (Exception ex)
            {
                Log.ToFile("获取用户app列表报错：" + ex.Message);
                return null;
            }
        }

        public bool AddMapping(List<UserApp> mappings)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"INSERT INTO UserResourcesMapping(UserJID,ResourcesID)VALUES(@UserJID,@ResourcesID)";
                    int result = conn.Query<int>(sqlCommandText, mappings).FirstOrDefault();

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
                Log.ToFile("添加用户app映射报错：" + ex.Message);
                return false;
            }
        }

        public bool DelMapping(List<UserApp> mappings)
        {
            try
            {
                using (IDbConnection conn = DapperContext.MsSqlConnection())
                {
                    string sqlCommandText =
                        @"DELETE FROM [UserResourcesMapping] WHERE ID=@ID";
                    int result = conn.Execute(sqlCommandText, mappings);
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
                Log.ToFile("删除用户app映射报错：" + ex.Message);
                return false;
            }
        }
    }

    #endregion
   
}