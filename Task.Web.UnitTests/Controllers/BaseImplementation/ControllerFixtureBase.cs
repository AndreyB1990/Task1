using NUnit.Framework;
using Rhino.Mocks;
using Task.Infrastructure.UnitOfWork;
using Task.Services.Interfaces;

namespace Task.Web.UnitTests.Controllers.BaseImplementation
{
    [TestFixture]
    public class ControllerFixtureBase
    {
        protected MockRepository Mockery;
        protected IUnitOfWorkFactory UnitOfWorkFactory;
        protected IUnitOfWork UnitOfWork;

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
            CreateMappings.Create();
            Mockery = new MockRepository();
            UnitOfWorkFactory = Mockery.DynamicMock<IUnitOfWorkFactory>();
            UnitOfWork = Mockery.DynamicMock<IUnitOfWork>();
        }

        protected virtual void AfterEachTest()
        {
        }
    }
}
