using System.Linq;
using Task.DALModels;
using Task.Repositories.BaseImplementation;
using Task.Repositories.Interfaces;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.Repositories
{
    class RoleRepository : NHibernateRepository<Role>, IRoleRepository
    {
        /// <summary>
        /// Instance of IUserRepository interface
        /// </summary>
        public IUserRepository UsersRepository { get; set; }

        /// <summary>
        /// Base constructor, which initializes NHibernateRepository(Role)
        /// </summary>
        /// <param name="usersRepository"></param>
        /// <param name="sessionProvider"></param>
        public RoleRepository(IUserRepository usersRepository, ISessionProvider sessionProvider) :base(sessionProvider)
        {
            UsersRepository = usersRepository;
        }

        /// <summary>
        /// Get rolenames for user which name is "username"
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string[] GetRolesForUser(string username)
        {
            return UsersRepository.GetUserByLogin(username).Roles.Select(r => r.RoleName).ToArray();
        }

        /// <summary>
        /// Get usernames for role which name is "rolename"
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public string[] GetUsersInRole(string roleName)
        {
            return GetRoleByRoleName(roleName).Users.Select(u => u.Login).ToArray();
        }

        /// <summary>
        /// Get role which name is "rolename"
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public Role GetRoleByRoleName(string roleName)
        {
            return Get(u => u.RoleName == roleName).SingleOrDefault();
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns></returns>
        public string[] GetAllRoles()
        {
            return GetAll().Select(r => r.RoleName).ToArray();
        }
    }
}
