using NUnit.Framework;
using Rhino.Mocks;

namespace Task.Infrastructure.UnitTests.BaseImplementation
{
    [TestFixture]
    public class InfrastructureFixtureBase
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
