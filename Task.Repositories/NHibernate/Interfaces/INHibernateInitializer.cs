using NHibernate.Cfg;

namespace Task.Repositories.NHibernate.Interfaces
{
    public interface INHibernateInitializer
    {
        /// <summary>
        /// Gets configuration for NHibernate
        /// </summary>
        /// <returns></returns>
        Configuration GetConfiguration();
    }
}
