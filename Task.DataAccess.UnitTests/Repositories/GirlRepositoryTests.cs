using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DALModels;
using Task.DataAccess.UnitTests.Repositories.BaseImplementation;
using Task.Infrastructure.Ninject;
using Task.Repositories.Interfaces;

namespace Task.DataAccess.UnitTests.Repositories
{
    [TestClass]
    public class GirlRepositoryTests : FixtureBase
    {
        private IGirlRepository _girlRepository;

        [TestMethod]
        public void GetBeautifulGirls_returnsValidNumberOfBeautifulGirls_ReturnsTrue()
        {
            var count = _girlRepository.GetBeautifulGirls().Count();
            Assert.AreEqual(count, 2);
        }

        [TestMethod]
        public void GetBeautifulGirls_returnsBeautifulGirlsInRightOrder_ReturnsTrue()
        {
            var ids = _girlRepository.GetBeautifulGirls().Select(x => x.Id).ToArray();
            int[] expIds = {2, 1};
            Assert.AreEqual(ids, expIds);
        }

        protected override void CreateInitialData()
        {
            _girlRepository = Locator.GetService<IGirlRepository>();
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
                                    },
                                new Girl
                                    {
                                        Id = 3,
                                        Name = "Marina",
                                        BirthDate = new DateTime(1992, 7, 7),
                                        Height = 150,
                                        Weight = 80
                                    }
                            };
            foreach (var girl in girls)
            {
                _girlRepository.Add(girl);
            }
        }
    }
}
