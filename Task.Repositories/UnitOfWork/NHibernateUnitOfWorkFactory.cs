using System.Data;
using Task.Infrastructure.UnitOfWork;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.Repositories.UnitOfWork
{
    class NHibernateUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ISessionProvider _sessionFactory;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="sessionProvider"></param>
        public NHibernateUnitOfWorkFactory(ISessionProvider sessionProvider)
        {
            _sessionFactory = sessionProvider;
        }

        /// <summary>
        /// Creates an instance of IUnitOfWork interface using IsolationLevel
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public IUnitOfWork Create(IsolationLevel isolationLevel)
        {
            return new NHibernateUnitOfWork(_sessionFactory.GetSessionFactory().OpenSession(), isolationLevel);
        }

        /// <summary>
        /// Creates an instance of IUnitOfWork interface
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork Create()
        {
            return Create(IsolationLevel.ReadCommitted);
        }
    }
}
