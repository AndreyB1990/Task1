using Task.DALModels;

namespace Task.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Gets user by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        User GetUserByLogin(string login);

        /// <summary>
        /// Gets user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User GetUserByEmail(string email);
    }
}
