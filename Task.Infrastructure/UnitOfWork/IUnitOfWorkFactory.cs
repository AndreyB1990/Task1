using System.Data;

namespace Task.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates an instance of IUnitOfWork interface using IsolationLevel
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        IUnitOfWork Create(IsolationLevel isolationLevel);

        /// <summary>
        /// Creates an instance of IUnitOfWork interface
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Create();
    }
}
