using System;
using Task.DALModels;
using Task.Infrastructure.Helpers.Providers.Interfaces;
using Task.Infrastructure.Models;
using Task.Repositories.Interfaces;
using Task.Services.Interfaces;

namespace Task.Services
{
    class UserService : IUserService
    {
        /// <summary>
        /// Data context
        /// </summary>
        private readonly IUserRepository _context;

        private readonly IPasswordMethodsProvider _passwordMethodsProvider;

        public UserService(IUserRepository context, IPasswordMethodsProvider passwordMethodsProvider)
        {
            _context = context;
            _passwordMethodsProvider = passwordMethodsProvider;
        }

        /// <summary>
        /// Base constructor, which initializes BaseService(News)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public User RegisterUser(string username, string email, string password, out RegisterStatus status)
        {
            var passwordSalt = _passwordMethodsProvider.CreateSalt();
            var newUser = new User
            {
                Login = username,
                CreatedDate = DateTime.Now,
                PasswordSalt = passwordSalt,
                Password = _passwordMethodsProvider.CreatePasswordHash(password, passwordSalt),
                Email = email,
                IsActivated = true,
                IsLockedOut = false,
                LastLockedOutDate = null,
                LastLoginDate = null,
            };
            _context.Add(newUser);
            status = RegisterStatus.Success;
            return newUser;
        }

        /// <summary>
        /// Validates user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Boolen</returns>
        public bool ValidateUser(string username, string password)
        {
            var user = _context.GetUserByLogin(username);
            if (user == null)
                return false;
            if (user.Password == _passwordMethodsProvider.CreatePasswordHash(password, user.PasswordSalt))
                return true;
            return false;
        }

        /// <summary>
        /// Checks user login
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolen</returns>
        public bool CheckUserLogin(string username)
        {
            var user = _context.GetUserByLogin(username);
            if (user == null)
                return true;
            return false;
        }

        /// <summary>
        /// Checks user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Boolen</returns>
        public bool CheckUserEmail(string email)
        {
            var user = _context.GetUserByEmail(email);
            if (user == null)
                return true;
            return false;
        }
        
        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolen</returns>
        public bool DeleteUser(string username)
        {
            var user = _context.GetUserByLogin(username);
            if (user == null)
                return false;
            _context.Delete(user);
            return true;
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user)
        {
            _context.Save(user);
        }

        /// <summary>
        /// Lock user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolen</returns>
        public bool LockUser(string username)
        {
            var user = _context.GetUserByLogin(username);
            if (user == null)
                return false;
            user.IsLockedOut = false;
            return true;
        }

        /// <summary>
        /// Unlocks user
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolen</returns>
        public bool UnlockUser(string username)
        {
            var user = _context.GetUserByLogin(username);
            if (user == null)
                return false;
            user.IsLockedOut = true;
            return true;
        }

        /// <summary>
        /// Changes password for user which name is "username"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns>Boolen</returns>
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = _context.GetUserByLogin(username);
            if (user.Password != oldPassword)
                return false;
            user.Password = newPassword;
            return true;
        }
    }
}
