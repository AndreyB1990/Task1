using Task.DALModels;

namespace Task.Repositories.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        /// <summary>
        /// Get rolenames for user which name is "username"
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        string[] GetRolesForUser(string username);

        /// <summary>
        /// Get usernames for role which name is "rolename"
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        string[] GetUsersInRole(string roleName);

        /// <summary>
        /// Get role which name is "rolename"
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>Role</returns>
        Role GetRoleByRoleName(string roleName);

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        string[] GetAllRoles();
    }
}
