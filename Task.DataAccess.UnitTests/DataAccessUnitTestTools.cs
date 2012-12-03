using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using NHibernate;
using NHibernate.Cfg;
using Ninject;
using Ninject.Modules;
using Rhino.Mocks;
using Task.Repositories.Mappings;
using Task.Repositories.NHibernate;
using Task.Repositories.NHibernate.Interfaces;
using Task.Repositories.NinjectModules;
using NHibernate.ByteCode.Castle;
using System.Data.SQLite;

namespace Task.DataAccess.UnitTests
{
    public static class DataAccessUnitTestTools
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

        //private ISession Session { get; set; }

        public FakeNHibernateInitializer(Assembly assemblyContainingMapping)
        {
            _assemblyContainingMapping = assemblyContainingMapping;
        }
        public Configuration GetConfiguration()
        {
            //var cfg = new SQLiteConfiguration()
            //    .InMemory()
            //    .ShowSql()
            //    .Raw("connection.release_mode", "on_close")
            //    .Raw("proxyfactory.factory_class",
            //         "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");
            FluentConfiguration configuration = Fluently.Configure()
                .Database(() =>
                          SQLiteConfiguration.Standard
                              .InMemory())//.Raw("proxyfactory.factory_class","NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle"))
                .Mappings(mappingConfiguration => mappingConfiguration
                                                      .FluentMappings
                                                      .AddFromAssembly(_assemblyContainingMapping))
                .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "thread_static"));
            SessionSource sessionSource = new SingleConnectionSessionSourceForSQLiteInMemoryTesting(configuration);//cfg.ToProperties(), AutoMap.AssemblyOf<UserMap>(t => t.Namespace == "Task.Repositories.Mappings"));//configuration);
            sessionSource.BuildSchema();
            //Session = sessionSource.CreateSession();
            //HttpContext.Current = DataAccessUnitTestTools.FakeHttpContext();
            //HttpContext.Current.Items["thread_static"] = Session;
            return sessionSource.Configuration;//configuration.BuildConfiguration();
            
        }
    }
    //public class RoommatePersistenceModel : AutoPersistenceModel
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
