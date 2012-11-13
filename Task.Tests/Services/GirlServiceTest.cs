using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DALModels;
using Task.Infrastructure.Ninject;
using Task.Repositories.Mappings;
using Task.Repositories.NinjectModules;
using Task.Services.Interfaces;
using Task.Services.NinjectModules;
using Task.Web;
namespace Task.Tests.Services
{
    [TestClass]
    public class GirlServiceTest : InMemoryDatabaseTest
    {
        private readonly IGirlService _girlService;

        public GirlServiceTest()
            : base(typeof(GirlMap).Assembly)
        {
            Locator.Init(new ServiceModule(), new RepositoryModule());
            CreateMappings.Create();
            _girlService = Locator.GetService<IGirlService>();
        }

        [TestMethod]
        public void GirlServiceGetBeautifulGirls1()
        {
            var girls = new[]
                            {
                                new Girl
                                    {
                                        Id = 1,
                                        Name = "Alena",
                                        BirthDate = new DateTime(1990, 4, 4),
                                        Height = 180,
                                        Weight = 56
                                    },
                                new Girl
                                    {
                                        Id = 2,
                                        Name = "Alla",
                                        BirthDate = new DateTime(1992, 7, 7),
                                        Height = 170,
                                        Weight = 47
                                    }
                            };
            _girlService.Add(girls[0]);
            _girlService.Add(girls[1]);
            IEnumerable<Girl> beautifulGirls = _girlService.GetBeautifulGirls();
            Assert.AreEqual(beautifulGirls.Count(), 1);
        }
    }
}
