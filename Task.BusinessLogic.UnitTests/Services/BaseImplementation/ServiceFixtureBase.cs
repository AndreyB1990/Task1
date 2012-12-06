using NUnit.Framework;
using Rhino.Mocks;

namespace Task.BusinessLogic.UnitTests.Services.BaseImplementation
{
    [TestFixture]
    public class ServiceFixtureBase
    {
        protected MockRepository Mockery;

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
        }

        protected virtual void AfterEachTest()
        {
        }
    }
}
