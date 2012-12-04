using System;
using System.Linq;
using NUnit.Framework;
using Task.DALModels;
using Task.DataAccess.UnitTests.Repositories.BaseImplementation;
using Task.Infrastructure.Helpers;
using Task.Infrastructure.Ninject;
using Task.Infrastructure.UnitOfWork;
using Task.Repositories.Interfaces;

namespace Task.DataAccess.UnitTests.Repositories
{
    [TestFixture]
    public class GirlRepositoryTests : FixtureBase
    {
        private IGirlRepository _girlRepository;

        [Test]
        public void GetBeautifulGirls_returnsValidNumberOfBeautifulGirls()
        {
            var count = _girlRepository.GetBeautifulGirls().Count();
            Assert.AreEqual(count, 2);
        }

        [Test]
        public void GetBeautifulGirls_returnsBeautifulGirlsInRightOrder()
        {
            var ids = _girlRepository.GetBeautifulGirls().Select(x => x.Id).ToArray();
            var expIds =
                _girlRepository.GetBeautifulGirls().OrderBy(x => GirlMethods.GetAge(x)).Select(x => x.Id).ToArray();

            Assert.AreEqual(ids[0], expIds[0]);
            Assert.AreEqual(ids[1], expIds[1]);
        }

        [Test]
        public void GetAll_returnsGirlsInRightOrder()
        {
            var ids = _girlRepository.GetAll().Select(x => x.Id).ToArray();
            var expIds =
                _girlRepository.GetAll().OrderBy(x => GirlMethods.GetAge(x)).Select(x => x.Id).ToArray();
            Assert.AreEqual(ids[0], expIds[0]);
            Assert.AreEqual(ids[1], expIds[1]);
            Assert.AreEqual(ids[2], expIds[2]);
        }

        [Test]
        [ExpectedException(typeof(NHibernate.PropertyValueException))]
        public void GetLatestNews_nameCannotBeNull_ThrowsException()
        {
            using (Locator.GetService<IUnitOfWorkFactory>().Create())
            {
                _girlRepository.Add(new Girl
                {
                    BirthDate = new DateTime(1990, 4, 4),
                    Height = 180,
                    Weight = 56
                });
            }
        }

        [Test]
        [ExpectedException(typeof(NHibernate.PropertyValueException))]
        public void GetLatestNews_heightCannotBeNull_ThrowsException()
        {
            using (Locator.GetService<IUnitOfWorkFactory>().Create())
            {
                _girlRepository.Add(new Girl
                {
                    Name = "Alena",
                    BirthDate = new DateTime(1990, 4, 4),
                    Weight = 56
                });
            }
        }

        [Test]
        [ExpectedException(typeof(NHibernate.PropertyValueException))]
        public void GetLatestNews_weightCannotBeNull_ThrowsException()
        {
            using (Locator.GetService<IUnitOfWorkFactory>().Create())
            {
                _girlRepository.Add(new Girl
                {
                    Name = "Alena",
                    BirthDate = new DateTime(1990, 4, 4),
                    Height = 180
                });
            }
        }

        protected override void CreateInitialData()
        {
            _girlRepository = Locator.GetService<IGirlRepository>();
            var girls = new[]
                            {
                                new Girl
                                    {
                                        Name = "Alena",
                                        BirthDate = new DateTime(1990, 4, 4),
                                        Height = 180,
                                        Weight = 56
                                    },
                                new Girl
                                    {
                                        Name = "Alla",
                                        BirthDate = new DateTime(1992, 7, 7),
                                        Height = 170,
                                        Weight = 50
                                    },
                                new Girl
                                    {
                                        Name = "Marina",
                                        BirthDate = new DateTime(1992, 7, 7),
                                        Height = 150,
                                        Weight = 80
                                    }
                            };
            foreach (var obj in girls)
            {
                _girlRepository.Add(obj);
            }
        }

        protected override void AfterEachTest()
        {
            base.AfterEachTest();
            _girlRepository.DeleteAll();
        }
    }
}
