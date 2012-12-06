namespace Task.Infrastructure.Helpers.Providers.Interfaces
{
    public interface IPasswordMethodsProvider
    {
        string CreateSalt();
        string CreatePasswordHash(string pwd, string salt);
    }
}
