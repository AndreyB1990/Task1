using Task.DALModels;

namespace Task.Services.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// Adds users with usernames to roles with rolenames
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="rolenames"></param>
        void AddUsersToRoles(string[] usernames, string[] rolenames);

        /// <summary>
        /// Adds role to user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        void AddRoleToUser(User user, Role role);

        /// <summary>
        /// Adds user to role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="user"></param>
        void AddUserToRole(Role role, User user);

        /// <summary>
        /// Creates role which name is "rolename"
        /// </summary>
        /// <param name="rolename"></param>
        void CreateRole(string rolename);

        /// <summary>
        /// Deletes role which name is "rolename"
        /// </summary>
        /// <param name="rolename"></param>
        /// <returns>Boolen</returns>
        bool DeleteRole(string rolename);

        /// <summary>
        /// Checks, if role which name is "rolename" exists in context
        /// </summary>
        /// <param name="rolename"></param>
        /// <returns>Boolen</returns>
        bool RoleExists(string rolename);

        /// <summary>
        /// Checks, if any user exists in role which name is "rolename"
        /// </summary>
        /// <param name="rolename"></param>
        /// <returns>Boolen</returns>
        bool UserInRoleExists(string rolename);

        /// <summary>
        /// Checks, if user which name is "username" exists in role which name is "rolename"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="rolename"></param>
        /// <returns>Boolen</returns>
        bool IsUserInRole(string username, string rolename);

        /// <summary>
        /// Removes users with usernames from roles with rolenames
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="rolenames"></param>
        void RemoveUsersFromRoles(string[] usernames, string[] rolenames);
    }
}
