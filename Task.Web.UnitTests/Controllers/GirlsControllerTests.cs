using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FizzWare.NBuilder;
using NUnit.Framework;
using Rhino.Mocks;
using Task.DALModels;
using Task.Infrastructure;
using Task.Infrastructure.UnitOfWork;
using Task.Services.Interfaces;
using Task.Web.Controllers;
using Task.Web.Models;
using Task.Web.UnitTests.Controllers.BaseImplementation;

namespace Task.Web.UnitTests.Controllers
{
    [TestFixture]
    public class GirlsControllerTests : ControllerFixtureBase
    {
        [Test]
        public void List_getValidNumberAndReferencesOfBeautifulGirls()
        {
            const int countOfBeautifulGirls= 50;
            var girls = Builder<Girl>.CreateListOfSize(2 * countOfBeautifulGirls)
                       .All()
                            .With(x => x.Height = 175).With(x => x.Weight = 55).With(x => x.BirthDate = new DateTime(1990, 2, 2))
                       .TheLast(countOfBeautifulGirls)
                            .With(x => x.Height = 180).With(x => x.Weight = 105).With(x => x.BirthDate = new DateTime(1990, 2, 2))
                       .Build();
            for (int page = 1; page < 10; page++)
            {
                Mockery = new MockRepository();
                var girlService = Mockery.Stub<IGirlService>();
                UnitOfWorkFactory = Mockery.DynamicMock<IUnitOfWorkFactory>();
                UnitOfWork = Mockery.DynamicMock<IUnitOfWork>();
                using (Mockery.Record())
                {
                    Expect.Call(UnitOfWorkFactory.Create()).Return(UnitOfWork);
                    Expect.Call(girlService.GetBeautifulGirls()).Return(girls.Take(countOfBeautifulGirls).ToList());
                }
                IEnumerable<GirlModel> resultGirls;
                using (Mockery.Playback())
                {
                    var controller = new GirlsController(girlService, UnitOfWorkFactory);
                    var result = controller.List(page, true);
                    resultGirls = (IEnumerable<GirlModel>) result.Data;
                }
                var girlModels = resultGirls as GirlModel[] ?? resultGirls.ToArray();
                Assert.AreEqual(girls.Take(countOfBeautifulGirls).Skip((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE).
                    Take(Constants.GIRLS_PAGER_LINKS_PER_PAGE).Count(), girlModels.Count());
                if (girlModels.Count() != 0)
                    Assert.AreEqual(girls.Take(countOfBeautifulGirls).Skip((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE).
                        Take(Constants.GIRLS_PAGER_LINKS_PER_PAGE).First().Id, girlModels.First().Id);
            }
        }

        [Test]
        public void List_getValidNumberAndReferencesOfGirls()
        {
            const int countOfBeautifulGirls = 50;
            var girls = Builder<Girl>.CreateListOfSize(2 * countOfBeautifulGirls)
                       .All()
                            .With(x => x.Height = 175).With(x => x.Weight = 55).With(x => x.BirthDate = new DateTime(1990, 2, 2))
                       .TheLast(countOfBeautifulGirls)
                            .With(x => x.Height = 180).With(x => x.Weight = 105).With(x => x.BirthDate = new DateTime(1990, 2, 2))
                       .Build();
            for (int page = 1; page < 10; page++)
            {
                Mockery = new MockRepository();
                var girlService = Mockery.DynamicMock<IGirlService>();
                UnitOfWorkFactory = Mockery.DynamicMock<IUnitOfWorkFactory>();
                UnitOfWork = Mockery.DynamicMock<IUnitOfWork>();
                using (Mockery.Record())
                {
                    Expect.Call(UnitOfWorkFactory.Create()).Return(UnitOfWork);
                    Expect.Call(girlService.Get(Arg<Int32>.Is.Equal((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE), Arg<Int32>.Is.Equal(Constants.GIRLS_PAGER_LINKS_PER_PAGE))).
                    Return(girls.Skip((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE).Take(Constants.GIRLS_PAGER_LINKS_PER_PAGE).ToList());
                }
                IEnumerable<GirlModel> resultGirls;
                using (Mockery.Playback())
                {
                    var controller = new GirlsController(girlService, UnitOfWorkFactory);
                    var result = controller.List(page, false);
                    resultGirls = (IEnumerable<GirlModel>)result.Data;
                }
                var girlModels = resultGirls as GirlModel[] ?? resultGirls.ToArray();
                Assert.AreEqual(girls.Skip((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE).
                    Take(Constants.GIRLS_PAGER_LINKS_PER_PAGE).Count(), girlModels.Count());
                if (girlModels.Count() != 0)
                    Assert.AreEqual(girls.Skip((page - 1) * Constants.GIRLS_PAGER_LINKS_PER_PAGE).
                        Take(Constants.GIRLS_PAGER_LINKS_PER_PAGE).First().Id, girlModels.First().Id);
            }
        }
    }
}
