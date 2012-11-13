using NHibernate;
using NHibernate.Context;
using Task.Infrastructure.Logging;
using Task.Repositories.NHibernate.Interfaces;
using Configuration = NHibernate.Cfg.Configuration;

namespace Task.Repositories.NHibernate
{
    class SessionProvider : ISessionProvider
    {
        /// <summary>
        /// Lock object for Singleton pattern
        /// </summary>
        private static readonly object LockObject = new object();
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;
        private readonly INHibernateInitializer _nHibernateInitializer;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="nHibernateInitializer"></param>
        public SessionProvider(INHibernateInitializer nHibernateInitializer)
        {
            _nHibernateInitializer = nHibernateInitializer;
        }

        /// <summary>
        /// Get the <see cref="ISessionFactory" /> that created <c>ISession</c> instance.
        /// </summary>
        public ISessionFactory GetSessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    lock (LockObject)
                    {
                        if (_sessionFactory == null)
                            _sessionFactory = Configuration.BuildSessionFactory();
                    }
                }

                return _sessionFactory;
            }
        }

        /// <summary>
        /// Gets current session
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            if (!CurrentSessionContext.HasBind(GetSessionFactory))
            {
                Logger.Log("Error with NHibernate session");
                CurrentSessionContext.Bind(GetSessionFactory.OpenSession());
            }
            return GetSessionFactory.GetCurrentSession();
        }

        /// <summary>
        /// Instructs NHibernate to build a SessionFactory.
        /// </summary>
        /// <returns></returns>
        ISessionFactory ISessionProvider.GetSessionFactory()
        {
            if (!CurrentSessionContext.HasBind(GetSessionFactory))
            {
                Logger.Log("Error with NHibernate session");
                CurrentSessionContext.Bind(GetSessionFactory.OpenSession());
            }
            return GetSessionFactory;
        }
        
        /// <summary>
        /// Property. Verifies the configuration and populates the NHibernate Configuration instance.
        /// </summary>
        private Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    lock (LockObject)
                    {
                        if (_configuration == null)
                            _configuration = _nHibernateInitializer.GetConfiguration();
                    }
                }

                return _configuration;
            }
        }
    }
}
