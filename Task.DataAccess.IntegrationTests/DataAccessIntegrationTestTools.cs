using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using NHibernate.Cfg;
using Task.Repositories.Mappings;
using Task.Repositories.NHibernate.Interfaces;
using Task.Repositories.NinjectModules;
using System.Data.SQLite;

namespace Task.DataAccess.IntegrationTests
{
    public static class DataAccessIntegrationTestTools
    {
        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://google.com/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });

            return httpContext;
        }
    }

    class FakeNHibernateInitializer : INHibernateInitializer
    {
        private readonly Assembly _assemblyContainingMapping;

        public FakeNHibernateInitializer(Assembly assemblyContainingMapping)
        {
            _assemblyContainingMapping = assemblyContainingMapping;
        }
        public Configuration GetConfiguration()
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(() =>
                          SQLiteConfiguration.Standard.UsingFile("Test.db"))
                              //.InMemory().ShowSql())
                .Mappings(mappingConfiguration => mappingConfiguration
                                                      .FluentMappings
                                                      .AddFromAssembly(_assemblyContainingMapping))
                .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "thread_static"));
            SessionSource sessionSource = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration);//cfg.ToProperties(), AutoMap.AssemblyOf<UserMap>(t => t.Namespace == "Task.Repositories.Mappings"));//configuration);
            sessionSource.BuildSchema();
            //sessionSource.Configuration.SetProperty("dialect", "NHibernate.Dialect.SQLiteDialect");
            return sessionSource.Configuration;
            
        }
    }
    //public class PersistenceModel : AutoPersistenceModel
    //{
    //    public RoommatePersistenceModel()
    //    {
    //        AddEntityAssembly(AutoMap.AssemblyOf<UserMap>(t => t.Namespace == "MyNamespace.Mappings")));
    //        AddEntityAssembly(typeof (UserMap).Assembly);

    //    }
    //}

    public class FakeRepositoryModule : RepositoryModule
    {
        public override void Load()
        {
            base.Load();
            Rebind<INHibernateInitializer>().To<FakeNHibernateInitializer>().WithConstructorArgument("assemblyContainingMapping", typeof(UserMap).Assembly);
        }
    }
}
