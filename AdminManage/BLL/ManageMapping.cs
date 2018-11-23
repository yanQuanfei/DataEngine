using Admin.Interface;
using Admin.Models;

namespace Admin.BLL
{
    /// <summary>
    /// 管理角色到 App 的关系
    /// </summary>
      class RoleToAppMapping : IMapping
    {
        public bool AddMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }

        public bool DelMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 管理 app 到角色的关系
    /// </summary>
     class AppToRoleMapping : IMapping
    {
        public bool AddMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }

        public bool DelMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }
    }
    
    /// <summary>
    /// 管理用户到角色的关系
    /// </summary>
     class UserToRoleMapping : IMapping
    {
        public bool AddMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }

        public bool DelMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 管理角色到用户的关系
    /// </summary>
     class RoleToUserMapping : IMapping
    {
        public bool AddMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }

        public bool DelMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 管理用户到 APP 的关系
    /// </summary>
     class UserToAppMapping : IMapping
    {
        public bool AddMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }

        public bool DelMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 管理 APP 到用户的关系
    /// </summary>
     class AppToUserMapping : IMapping
    {
        public bool AddMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }

        public bool DelMapping(Mapping mapping)
        {
            throw new System.NotImplementedException();
        }
    }
    
}