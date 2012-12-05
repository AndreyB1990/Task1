using System;
using FizzWare.NBuilder;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Task.DALModels;
using Task.Infrastructure.Ninject;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.DataAccess.UnitTests.Repositories.BaseImplementation
{
    [TestFixture]
    public class FixtureBase
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
            Mockery = new MockRepository();
            SessionProvider = Mockery.DynamicMock<ISessionProvider>();
            SessionFactory = Mockery.DynamicMock<ISessionFactory>();
            Session = Mockery.DynamicMock<ISession>();
            CreateCriteria = Mockery.DynamicMock<ICriteria>();
            var news = Builder<News>.CreateListOfSize(10)
                               .All()
                                    .With(x => x.Date = DateTime.Now)
                               .Random(5)
                                    .With(x => x.Date = new DateTime(2010, 1, 1))
                               .Build();
            using (Mockery.Record())
            {
                Expect.Call(Session.CreateCriteria(typeof(News))).Return(CreateCriteria);
                Expect.Call(SessionFactory.GetCurrentSession()).Return(Session);
                Expect.Call(SessionProvider.GetSessionFactory()).Return(SessionFactory);
                Expect.Call(SessionProvider.GetSession()).Return(Session);
                Expect.Call(CreateCriteria.List<News>()).Return(news);
                //Expect.Call(CreateCriteria.List<News>()).Return(news);
                //Expect.Call(CreateCriteria.UniqueResult()).Return(news);
            }
        }

        protected virtual void AfterEachTest()
        {
        }

        protected virtual void CreateInitialData()
        {
        }
    }
}
