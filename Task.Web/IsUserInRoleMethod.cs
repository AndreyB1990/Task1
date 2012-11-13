using Task.Infrastructure.Ninject;
using Task.Infrastructure.UnitOfWork;
using Task.Services.Interfaces;

namespace Task.Web
{
    public static class IsUserInRoleMethod
    {
        public static bool IsUserInRole(string username, string roleName)
        {
            using (Locator.GetService<IUnitOfWorkFactory>().Create())
            {
                return Locator.GetService<IRoleService>().IsUserInRole(username, roleName);
            }
        }
    }
}