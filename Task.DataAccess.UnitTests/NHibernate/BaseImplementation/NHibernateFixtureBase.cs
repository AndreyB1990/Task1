using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;
using Rhino.Mocks;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.DataAccess.UnitTests.NHibernate.BaseImplementation
{
    [TestFixture]
    public class NHibernateFixtureBase
    {
        protected MockRepository Mockery;
        protected INHibernateInitializer NHibernateInitializer;
        protected ISessionFactory SessionFactory;
        protected Configuration NhCongiguration;

        #region Additional test attributes

        [SetUp]
        public void Initialize()
        {
            BeforeEachTest();
        }

        [TearDown]
        public void Cleanup()
        {
            AfterEachTest();
        }

        #endregion

        protected virtual void BeforeEachTest()
        {
            Mockery = new MockRepository();
            NHibernateInitializer = Mockery.DynamicMock<INHibernateInitializer>();
            SessionFactory = Mockery.DynamicMock<ISessionFactory>();
            NhCongiguration = Mockery.DynamicMock<Configuration>();
        }

        protected virtual void AfterEachTest()
        {
        }
    }
}
