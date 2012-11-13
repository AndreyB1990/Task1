using System;

namespace Task.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit all changes of transaction
        /// </summary>
        void Commit();
    }
}
