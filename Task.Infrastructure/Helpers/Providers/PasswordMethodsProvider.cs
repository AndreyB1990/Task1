using Task.Infrastructure.Helpers.Providers.Interfaces;

namespace Task.Infrastructure.Helpers.Providers
{
    public class PasswordMethodsProvider : IPasswordMethodsProvider
    {
        public virtual string CreateSalt()
        {
            return PasswordMethods.CreateSalt();
        }

        public virtual string CreatePasswordHash(string pwd, string salt)
        {
            return PasswordMethods.CreatePasswordHash(pwd, salt);
        }
    }
}
