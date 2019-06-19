using MyShop.Models.MyShopModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.Entity;

namespace MyShop.Providers
{
    public class HelpdeskRoleProvider : RoleProvider
    {

        public override string[] GetRolesForUser(string username)
        {
            string[] role = new string[] { };
            using (DBContext _db = new DBContext())
            {
                try
                {
                    // Get user
                    User user = _db.Users.Include(c => c.Credential).FirstOrDefault(c=>c.UserEmail==username);
                    if (user != null)
                    {
                        //// получаем роль
                        //UserRole userRole = _db.UserRoles..ViewSingle(user.UserRoleId);

                        if (user.Credential != null)
                        {                     
                            role=   user.Credential.Select(c=>c.NameCredential).ToArray();
                        }
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            }
            return role;
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Находим пользователя
            using (DBContext _db = new DBContext())
            {
                try
                {
                    // Получаем пользователя
                    User user = _db.Users.FirstOrDefault(c => c.UserEmail == username);
                    if (user != null)
                    {
                        // получаем роль
                        UserRole userRole = user.UserRole;

                        //сравниваем
                        if (userRole != null && userRole.UserRoleName == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
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