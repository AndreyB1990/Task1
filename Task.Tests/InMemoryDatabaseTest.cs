using System;
using System.Reflection;
using System.Web;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using NHibernate;

namespace Task.Tests
{

    public class InMemoryDatabaseTest : IDisposable
    {
        protected ISession Session { get; set; }

        public InMemoryDatabaseTest(Assembly assemblyContainingMapping)
        {
            FluentConfiguration configuration = Fluently.Configure()
              .Database(() =>
                SQLiteConfiguration.Standard
                 .InMemory())
                 .Mappings(mappingConfiguration => mappingConfiguration
                 .FluentMappings
                 .AddFromAssembly(assemblyContainingMapping)); 
            SessionSource sessionSource = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration);
            sessionSource.BuildSchema();
            Session = sessionSource.CreateSession();
            HttpContext.Current = MoqHelper.FakeHttpContext();
            //HttpContext.Current.Items["thread_static"] = Session;
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}
