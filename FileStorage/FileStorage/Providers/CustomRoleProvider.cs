using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;
using FileStorage.DAL;

namespace FileStorage
{
    public class CustomRoleProvider : RoleProvider
    {
        //DI с UserRepository??
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };

            var ur = new UserRepository();
            var user = ur.SearchUserByLogin(username);

            if (user != null) //или newUser?? 
            {
                if (user.Role != null) // или roleID != 0?? 
                {
                    roles = new string[] { user.Role.Name };
                }
            }

            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;

            var ur = new UserRepository();
            var user = ur.SearchUserByLogin(username);

            if (user.Role.Name == roleName)
            {
                outputResult = true;
            }

            return outputResult;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}