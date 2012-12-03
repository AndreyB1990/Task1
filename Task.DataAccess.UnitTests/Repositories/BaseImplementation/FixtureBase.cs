using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Modules;
using Task.Infrastructure.Ninject;
using Task.Repositories.Interfaces;
using Task.Repositories.NHibernate.Interfaces;
using Task.Repositories.NinjectModules;

namespace Task.DataAccess.UnitTests.Repositories.BaseImplementation
{
    [TestClass]
    public class FixtureBase
    {
        #region Additional test attributes

        [TestInitialize()]
        public void Initialize()
        {
            BeforeEachTest();
            CreateInitialData();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            AfterEachTest();
        }

        #endregion

        protected virtual void BeforeEachTest()
        {
            //var fakeRepositoryModule = new RepositoryModule();//.Rebind<INHibernateInitializer>().To<FakeNHibernateInitializer>();
            //fakeRepositoryModule.Load();
            //fakeRepositoryModule.Unbind<INHibernateInitializer>();
            //fakeRepositoryModule.Kernel.Rebind<INHibernateInitializer>().To<FakeNHibernateInitializer>();
            Locator.Init(new FakeRepositoryModule());
        }

        protected virtual void AfterEachTest()
        {
        }

        protected virtual void CreateInitialData()
        {
        }
    }
}
