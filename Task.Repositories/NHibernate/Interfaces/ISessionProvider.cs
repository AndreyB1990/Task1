using NHibernate;

namespace Task.Repositories.NHibernate.Interfaces
{
    public interface ISessionProvider
    {
        /// <summary>
        /// Gets current session
        /// </summary>
        /// <returns></returns>
        ISession GetSession();

        /// <summary>
        /// Instructs NHibernate to build a SessionFactory.
        /// </summary>
        /// <returns></returns>
        ISessionFactory GetSessionFactory();
    }
}
