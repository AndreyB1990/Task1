using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using Rhino.Mocks;
using Task.DALModels;
using Task.Infrastructure;
using Task.Infrastructure.UnitOfWork;
using Task.Services.Interfaces;
using Task.Web.Controllers;
using Task.Web.UnitTests.Controllers.BaseImplementation;

namespace Task.Web.UnitTests.Controllers
{
    [TestFixture]
    public class NewsControllerTests : ControllerFixtureBase
    {

        [Test]
        public void List_getValidNumberAndReferencesOfNews()
        {
            var news = Builder<News>.CreateListOfSize(100)
                       .Build();
            for (int page = 1; page < 10; page++)
            {
                Mockery = new MockRepository();
                var newsService = Mockery.DynamicMock<INewsService>();
                UnitOfWorkFactory = Mockery.DynamicMock<IUnitOfWorkFactory>();
                UnitOfWork = Mockery.DynamicMock<IUnitOfWork>();
                using (Mockery.Record())
                {
                    Expect.Call(UnitOfWorkFactory.Create()).Return(UnitOfWork);
                    Expect.Call(newsService.GetAll()).Return(news);
                    Expect.Call(newsService.Get(Arg<Int32>.Is.Equal((page - 1) * Constants.NEWS_PAGER_LINKS_PER_PAGE), Arg<Int32>.Is.Equal(Constants.NEWS_PAGER_LINKS_PER_PAGE))).
                    Return(news.Skip((page - 1) * Constants.NEWS_PAGER_LINKS_PER_PAGE).Take(Constants.NEWS_PAGER_LINKS_PER_PAGE).ToList());
                }
                IEnumerable<News> resultNews;
                using (Mockery.Playback())
                {
                    var controller = new NewsController(newsService, UnitOfWorkFactory);
                    var result = controller.List(page);
                    resultNews = (IEnumerable<News>)result.Data;
                }
                var enumerable = resultNews as News[] ?? resultNews.ToArray();
                Assert.AreEqual(news.Skip((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE).
                    Take(Constants.GIRLS_PAGER_LINKS_PER_PAGE).Count(), enumerable.Count());
                if (enumerable.Count() != 0)
                    Assert.AreEqual(news.Skip((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE).
                        Take(Constants.GIRLS_PAGER_LINKS_PER_PAGE).First().Id, enumerable.First().Id);
            }
        }
    }
}
