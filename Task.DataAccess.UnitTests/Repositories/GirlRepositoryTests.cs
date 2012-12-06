﻿using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using Rhino.Mocks;
using Task.DALModels;
using Task.DataAccess.UnitTests.Repositories.BaseImplementation;
using Task.Repositories;
using Task.Repositories.Interfaces;

namespace Task.DataAccess.UnitTests.Repositories
{
    [TestFixture]
    public class GirlRepositoryTests : RepositoryFixtureBase
    {
        private IGirlRepository _girlRepository;

        [Test]
        public void GetBeautifulGirls_returnsValidNumberOfBeautifulGirls()
        {
            var girls = Builder<Girl>.CreateListOfSize(10)
                               .All()
                                    .With(x => x.Height = 175).With(x => x.Weight = 55).With(x => x.BirthDate = new DateTime(1990,2,2))
                               .Random(3)
                                    .With(x => x.Height = 180).With(x => x.Weight = 105).With(x => x.BirthDate = new DateTime(1990, 2, 2))
                               .Random(2)
                                    .With(x => x.Height = 180).With(x => x.Weight = 55).With(x => x.BirthDate = new DateTime(1990, 2, 2))
                               .Build();
            using (Mockery.Record())
            {
                Expect.Call(Session.CreateCriteria(typeof(Girl))).Return(CreateCriteria);
                Expect.Call(SessionFactory.GetCurrentSession()).Return(Session);
                Expect.Call(SessionProvider.GetSessionFactory()).Return(SessionFactory);
                Expect.Call(SessionProvider.GetSession()).Return(Session);
                Expect.Call(CreateCriteria.List<Girl>()).Return(girls);
            }
            _girlRepository = new GirlRepository(SessionProvider);
            var count = _girlRepository.GetBeautifulGirls().Count();
            Assert.AreEqual(count, 5);
        }

        [Test]
        public void GetLatestNews_returnsEmptyListIfNumberOfBeautifulGirlsIsNull()
        {
            var girls = Builder<Girl>.CreateListOfSize(10)
                                .All()
                                     .With(x => x.Height = 180).With(x => x.Weight = 105).With(x => x.BirthDate = new DateTime(1990, 2, 2))
                                .Build();
            using (Mockery.Record())
            {
                Expect.Call(Session.CreateCriteria(typeof(Girl))).Return(CreateCriteria);
                Expect.Call(SessionFactory.GetCurrentSession()).Return(Session);
                Expect.Call(SessionProvider.GetSessionFactory()).Return(SessionFactory);
                Expect.Call(SessionProvider.GetSession()).Return(Session);
                Expect.Call(CreateCriteria.List<Girl>()).Return(girls);
            }
            _girlRepository = new GirlRepository(SessionProvider);
            var beautiful = _girlRepository.GetBeautifulGirls();
            Assert.IsNotNull(beautiful);
            Assert.IsEmpty(beautiful);
        }
    }
}
