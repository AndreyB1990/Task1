using System.Linq;
using Task.DALModels;
using Task.Repositories.Interfaces;
using Task.Services.Interfaces;

namespace Task.Services
{
    class RoleService : IRoleService
    {
        /// <summary>
        /// Data context
        /// </summary>
        private readonly IRoleRepository _rolesContext;

        /// <summary>
        /// Instance of IUserRepository interface
        /// </summary>
        private readonly IUserRepository _usersContext;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="rolesContext"></param>
        /// <param name="usersContext"></param>
        public RoleService(IRoleRepository rolesContext, IUserRepository usersContext)
        {
            _rolesContext = rolesContext;
            _usersContext = usersContext;
        }

        /// <summary>
        /// Add users with usernames to roles with rolenames
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var username in usernames)
            {
                var user = _usersContext.GetUserByLogin(username);
                if (user == null) continue;
                foreach (var role in roleNames.Select(rolename => _rolesContext.GetRoleByRoleName(rolename)).Where(role => role != null))
                {
                    AddRoleToUser(user, role);
                }
            }
        }

        /// <summary>
        /// Adds role to user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        public virtual void AddRoleToUser(User user, Role role)
        {
            if (!user.Roles.Contains(role))
            {
                user.Roles.Add(role);
                AddUserToRole(role, user);
            }
        }

        /// <summary>
        /// Adds user to role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        public void AddUserToRole(Role role, User user)
        {
            if (!role.Users.Contains(user))
            {
                role.Users.Add(user);
                AddRoleToUser(user, role);
            }
        }

        /// <summary>
        /// Creates role which name is "rolename"
        /// </summary>
        /// <param name="roleName"></param>
        public void CreateRole(string roleName)
        {
            if (RoleExists(roleName)) return;
            var role = new Role { RoleName = roleName };
            _rolesContext.Add(role);
        }

        /// <summary>
        /// Deletes role which name is "rolename"
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>Boolen</returns>
        public bool DeleteRole(string roleName)
        {
            var role = _rolesContext.GetRoleByRoleName(roleName);
            if (role == null)
                return false;
            _rolesContext.Delete(role);
            return true;
        }

        /// <summary>
        /// Checks, if role which name is "rolename" exists in context
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>Boolen</returns>
        public bool RoleExists(string roleName)
        {
            var role = _rolesContext.GetRoleByRoleName(roleName);
            if (role == null)
                return false;
            return true;
        }

        /// <summary>
        /// Checks, if any user exists in role which name is "rolename"
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>Boolen</returns>
        public bool UserInRoleExists(string roleName)
        {
            var role = _rolesContext.GetRoleByRoleName(roleName);
            if (role == null)
                return false;
            if (!role.Users.Any())
                return false;
            return true;
        }

        /// <summary>
        /// Checks, if user which name is "username" exists in role which name is "rolename"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="rolename"></param>
        /// <returns>Boolen</returns>
        public bool IsUserInRole(string username, string rolename)
        {
            var role = _rolesContext.GetRoleByRoleName(rolename);
            if (role == null)
                return false;
            var user = _usersContext.GetUserByLogin(username);
            return user != null && user.Roles.Contains(role);
        }

        /// <summary>
        /// Removes users with usernames to roles from rolenames
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="roleNames"></param>
        public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (var username in usernames)
            {
                var user = _usersContext.GetUserByLogin(username);
                if (user == null) continue;
                foreach (var role in roleNames.Select(roleName => _rolesContext.GetRoleByRoleName(roleName)).Where(role => role != null))
                {
                    user.Roles.Remove(role);
                }
            }
        }
    }
}