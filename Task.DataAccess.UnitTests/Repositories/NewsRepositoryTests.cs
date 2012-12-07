using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using Rhino.Mocks;
using Task.DALModels;
using Task.DataAccess.UnitTests.Repositories.BaseImplementation;
using Task.Infrastructure;
using Task.Repositories;
using Task.Repositories.Interfaces;

namespace Task.DataAccess.UnitTests.Repositories
{
    [TestFixture]
    public class NewsRepositoryTests : RepositoryFixtureBase
    {
        private INewsRepository _newsRepository;

        [Test]
        public void GetLatestNews_returnsValidNumberOfLatestNews()
        {
            IQueryable<News> latestNews;
            var news = Builder<News>.CreateListOfSize(10)
                               .All()
                                    .With(x => x.Date = DateTime.Now)
                               .Random(5)
                                    .With(x => x.Date = new DateTime(2010, 1, 1))
                               .Build();
            using (Mockery.Record())
            {
                Expect.Call(Session.CreateCriteria(typeof(News))).Return(CreateCriteria);
                Expect.Call(SessionProvider.GetSession()).Return(Session);
                Expect.Call(CreateCriteria.List<News>()).Return(news);
            }
            var newsTime = DateTime.Now - Constants.TIME_OF_NEWS;
            using (Mockery.Playback())
            {
                _newsRepository = new NewsRepository(SessionProvider);
                latestNews = _newsRepository.GetLatestNews();
            }
            Assert.AreEqual(latestNews.Count(), 5);
            Assert.GreaterOrEqual(latestNews.First().Date, newsTime);
        }

        [Test]
        public void GetLatestNews_returnsEmptyListIfNumberOfLatestNewsIsNull()
        {
            IQueryable<News> latestNews;
            var news = Builder<News>.CreateListOfSize(10)
                               .All()
                                    .With(x => x.Date = new DateTime(2010, 1, 1))
                               .Build();
            using (Mockery.Record())
            {
                Expect.Call(Session.CreateCriteria(typeof(News))).Return(CreateCriteria);
                Expect.Call(SessionProvider.GetSession()).Return(Session);
                Expect.Call(CreateCriteria.List<News>()).Return(news);
            }
            using (Mockery.Playback())
            {
                _newsRepository = new NewsRepository(SessionProvider);
                latestNews = _newsRepository.GetLatestNews();
            }
            Assert.IsNotNull(latestNews);
            Assert.IsEmpty(latestNews);
        }

        //[Test]
        //[ExpectedException(typeof(NHibernate.PropertyValueException))]
        //public void GetLatestNews_shortDescriptionCannotBeNull_ThrowsException()
        //{
        //    //using (Locator.GetService<IUnitOfWorkFactory>().Create())
        //    //{
        //        _newsRepository.Add(new News { Date = DateTime.Now, Title = "News_with_Exception" });
        //    //}
        //}

        //[Test]
        //public void GetLatestNews_descriptionCanBeNull_DoesNotThrowsException()
        //{
        //    //using (Locator.GetService<IUnitOfWorkFactory>().Create())
        //    //{
        //        Assert.DoesNotThrow(() => _newsRepository.Add(new News { ShortDescription = "Short Description", Date = DateTime.Now, Title = "News_with_Exception" }));
        //    //}
        //}
    }
}
