﻿using Ninject.Modules;
using Task.Infrastructure.Helpers.Providers;
using Task.Infrastructure.Helpers.Providers.Interfaces;
using Task.Services.Interfaces;

namespace Task.Services.NinjectModules
{
    public class ServiceModule : NinjectModule
    {
        /// <summary>
        /// Registers dependencies in Services library
        /// </summary>
        public override void Load()
        {
            Bind<IGirlService>().To<GirlService>();
            Bind<INewsService>().To<NewsService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IUserService>().To<UserService>();
            Bind<IPasswordMethodsProvider>().To<PasswordMethodsProvider>();
        }
    }
}
