using System;
using System.Security.Cryptography;
using System.Web.Security;

namespace Task.Infrastructure.Helpers
{
    public static class PasswordMethods
    {
        /// <summary>
        /// Create salt
        /// </summary>
        /// <returns>String</returns>
        public static string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Create password hash using salt
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns>String</returns>
        public static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            return hashedPwd;
        }
    }
}
