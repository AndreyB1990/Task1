using System;
using System.Data;
using NHibernate;
using NHibernate.Context;
using Task.Infrastructure.Logging;
using Task.Infrastructure.UnitOfWork;

namespace Task.Repositories.UnitOfWork
{
    class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="session"></param>
        /// <param name="isolationLevel"></param>
        public NHibernateUnitOfWork(ISession session, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (session == null)
            {
                Logger.Log("Error with NHibernate session");
                throw new InvalidOperationException("Error with NHibernate session");
            }
            CurrentSessionContext.Bind(session);
            _session = session;
            _transaction = session.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            if (!_transaction.WasCommitted && !_transaction.WasRolledBack)
            {
                _transaction.Rollback();
            }
            _transaction.Dispose();
            _transaction = null;

            CurrentSessionContext.Unbind(_session.SessionFactory);
            _session.Dispose();
        }

        /// <summary>
        /// Commit all changes of transaction
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
        }
    }
}
