namespace Task.Infrastructure.Models
{
    public enum RegisterStatus : short
    {
        DuplicateUserName = 0,

        DuplicateEmail = 1,

        InvalidPassword = 2,

        InvalidEmail = 3,

        InvalidUserName = 4,

        ProviderError = 5,

        UserRejected = 6,

        Success = 7
    }
}
