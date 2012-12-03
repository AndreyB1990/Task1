using Ninject.Modules;
using Task.Infrastructure.UnitOfWork;
using Task.Repositories.Interfaces;
using Task.Repositories.NHibernate;
using Task.Repositories.NHibernate.Interfaces;
using Task.Repositories.UnitOfWork;

namespace Task.Repositories.NinjectModules
{
    public class RepositoryModule : NinjectModule
    {
        /// <summary>
        /// Registers dependencies in Repositories library
        /// </summary>
        public override void Load()
        {
            Bind<IGirlRepository>().To<GirlRepository>();
            Bind<INewsRepository>().To<NewsRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IRoleRepository>().To<RoleRepository>();
            Bind<INHibernateInitializer>().To<FluentInitializer>();
            Bind<ISessionProvider>().To<SessionProvider>();
            Bind<IUnitOfWork>().To<NHibernateUnitOfWork>();
            Bind<IUnitOfWorkFactory>().To<NHibernateUnitOfWorkFactory>();
        }
    }
}
