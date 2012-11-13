using Task.DALModels;
using Task.Infrastructure.Models;

namespace Task.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="status"></param>
        /// <returns>User</returns>
        User RegisterUser(string username, string email, string password, out RegisterStatus status);

        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Boolen</returns>
        bool ValidateUser(string username, string password);

        /// <summary>
        /// Checks user login
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolen</returns>
        bool CheckUserLogin(string username);

        /// <summary>
        /// Checks user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Boolen</returns>
        bool CheckUserEmail(string email);

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolen</returns>
        bool DeleteUser(string username);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user"></param>
        void UpdateUser(User user);

        /// <summary>
        /// Lock user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolen</returns>
        bool LockUser(string username);

        /// <summary>
        /// Unlocks user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolen</returns>
        bool UnlockUser(string username);

        /// <summary>
        /// Changes password for user which name is "username"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>Boolen</returns>
        bool ChangePassword(string username, string oldPassword, string newPassword);
    }
}
