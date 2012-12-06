using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.DataAccess.UnitTests.Repositories.BaseImplementation
{
    [TestFixture]
    public class RepositoryFixtureBase
    {
        protected MockRepository Mockery;
        protected ISessionProvider SessionProvider;
        protected ISession Session;
        protected ISessionFactory SessionFactory;
        protected ICriteria CreateCriteria;

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
            SessionProvider = Mockery.DynamicMock<ISessionProvider>();
            SessionFactory = Mockery.DynamicMock<ISessionFactory>();
            Session = Mockery.DynamicMock<ISession>();
            CreateCriteria = Mockery.DynamicMock<ICriteria>();
        }

        protected virtual void AfterEachTest()
        {
        }
    }
}
