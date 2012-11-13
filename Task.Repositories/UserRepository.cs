using System.Linq;
using Task.DALModels;
using Task.Repositories.BaseImplementation;
using Task.Repositories.Interfaces;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.Repositories
{
    class UserRepository : NHibernateRepository<User>, IUserRepository
    {
        /// <summary>
        /// Base constructor, which initializes NHibernateRepository(User)
        /// </summary>
        /// <param name="sessionProvider"></param>
        public UserRepository(ISessionProvider sessionProvider):base(sessionProvider)
        {
        }

        /// <summary>
        /// Gets user by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public User GetUserByLogin(string login)
        {
            return Get(u => u.Login == login).SingleOrDefault();
        }

        /// <summary>
        /// Gets user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUserByEmail(string email)
        {
            return Get(u => u.Email == email).SingleOrDefault();
        }
    }
}
