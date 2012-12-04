using NUnit.Framework;
using Task.Infrastructure.Ninject;

namespace Task.DataAccess.UnitTests.Repositories.BaseImplementation
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
