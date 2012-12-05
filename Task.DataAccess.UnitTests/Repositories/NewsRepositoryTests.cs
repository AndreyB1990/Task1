using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using Rhino.Mocks;
using Task.DALModels;
using Task.DataAccess.UnitTests.Repositories.BaseImplementation;
using Task.Infrastructure.Ninject;
using Task.Infrastructure.UnitOfWork;
using Task.Repositories;
using Task.Repositories.Interfaces;

namespace Task.DataAccess.UnitTests.Repositories
{
    [TestFixture]
    public class NewsRepositoryTests : FixtureBase
    {
        private INewsRepository _newsRepository;

        [Test]
        public void GetLatestNews_returnsValidNumberOfLatestNews()
        {
            //var news = Builder<News>.CreateListOfSize(10)
            //                   .All()
            //                        .With(x => x.Date = DateTime.Now)
            //                   .Random(5)
            //                        .With(x => x.Date = new DateTime(2010, 1, 1))
            //                   .Build();
            //using (Mockery.Record())
            //{
                //Expect.Call(_newsRepository.GetAll()).Return(news.AsQueryable());
                //Expect.Call(CreateCriteria.List<News>()).Return(news);
            //}
            var count = _newsRepository.GetLatestNews().Count();
            Assert.AreEqual(count, 5);
        }

        [Test]
        [ExpectedException(typeof(NHibernate.PropertyValueException))]
        public void GetLatestNews_shortDescriptionCannotBeNull_ThrowsException()
        {
            using (Locator.GetService<IUnitOfWorkFactory>().Create())
            {
                _newsRepository.Add(new News { Date = DateTime.Now, Title = "News_with_Exception" });
            }
        }

        [Test]
        public void GetLatestNews_descriptionCanBeNull_DoesNotThrowsException()
        {
            using (Locator.GetService<IUnitOfWorkFactory>().Create())
            {
                Assert.DoesNotThrow(() => _newsRepository.Add(new News { ShortDescription = "Short Description", Date = DateTime.Now, Title = "News_with_Exception" }));
            }
        }

        protected override void CreateInitialData()
        {
            //_newsRepository = Locator.GetService<INewsRepository>();
            //_newsRepository = Mockery.DynamicMock<NewsRepository>(SessionProvider);
            _newsRepository = new NewsRepository(SessionProvider);
            
            //var news = new[]
            //               {
            //                   new News
            //                       {
            //                           Date = new DateTime(2010,1,1),
            //                           Title = "News 1",
            //                           ShortDescription = "News 1"
            //                       },
            //                   new News
            //                       {
            //                           Date = new DateTime(2012,10,10),
            //                           Title = "News 2",
            //                           ShortDescription = "News 2"
            //                       },
            //                   new News
            //                       {
            //                           Date = DateTime.Now,
            //                           Title = "News 3",
            //                           ShortDescription = "News 3"
            //                       },
            //                   new News
            //                       {
            //                           Date = DateTime.Now,
            //                           Title = "News 4",
            //                           ShortDescription = "News 4"
            //                       }
            //               };
            //foreach (var obj in news)
            //{
            //    _newsRepository.Add(obj);
            //}
        }

        //protected override void AfterEachTest()
        //{
        //    base.AfterEachTest();
        //    _newsRepository.DeleteAll();
        //}
    }
}
