using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Task.Repositories.Mappings;
using Task.Repositories.NHibernate.Interfaces;
using Configuration = NHibernate.Cfg.Configuration;

namespace Task.Repositories.NHibernate
{
    public class FluentInitializer : INHibernateInitializer
    {
        /// <summary>
        /// Gets configuration for FluentNHibernate
        /// </summary>
        /// <returns></returns>
        public Configuration GetConfiguration()
        {
            var config = Fluently.Configure().
                Database(
                    MsSqlConfiguration
                        .MsSql2008
                        .ConnectionString(ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                .Mappings(mappingConfiguration => mappingConfiguration.FluentMappings.Conventions.AddFromAssemblyOf<FluentInitializer>())
                .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "thread_static"))
                .BuildConfiguration();
            return config;
        }
    }
}
