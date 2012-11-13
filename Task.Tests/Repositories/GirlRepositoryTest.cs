using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DALModels;
using Task.Infrastructure.Ninject;
using Task.Repositories.Interfaces;
using Task.Repositories.Mappings;
using Task.Repositories.NinjectModules;

namespace Task.Tests.Repositories
{
    [TestClass]
    public class GirlRepositoryTest : InMemoryDatabaseTest
    {
        private readonly IGirlRepository _girlRepository;

        public GirlRepositoryTest()
            : base(typeof(GirlMap).Assembly)
        {
            Locator.Init(new RepositoryModule());
            
            _girlRepository = Locator.GetService<IGirlRepository>();
        }

        [TestMethod]
        public void GirlRepositoryGetBeautifulGirls1()
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
                                        Weight = 50
                                    }
                            };
            _girlRepository.Add(girls[0]);
            _girlRepository.Add(girls[1]);
            Assert.AreEqual(_girlRepository.GetBeautifulGirls().Count(), 2);
        }

        [TestMethod]
        public void GirlRepositoryGetBeautifulGirls2()
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
            _girlRepository.Add(girls[0]);
            _girlRepository.Add(girls[1]);
            Assert.AreEqual(_girlRepository.GetBeautifulGirls().Count(), 1);
        }
    }
}
