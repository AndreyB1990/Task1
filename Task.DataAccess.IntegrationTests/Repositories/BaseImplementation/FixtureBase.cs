using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Task.Infrastructure.Ninject;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.DataAccess.IntegrationTests.Repositories.BaseImplementation
{
    [TestFixture]
    public class FixtureBase
    {
        #region Additional test attributes

        [SetUp]
        public void Initialize()
        {
            BeforeEachTest();
            CreateInitialData();
        }

        [TearDown]
        public void Cleanup()
        {
            AfterEachTest();
        }

        #endregion

        protected virtual void BeforeEachTest()
        {
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
