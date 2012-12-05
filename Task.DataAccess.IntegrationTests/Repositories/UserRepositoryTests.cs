using System;
using NUnit.Framework;
using Task.DALModels;
using Task.DataAccess.IntegrationTests.Repositories.BaseImplementation;
using Task.Infrastructure.Helpers;
using Task.Infrastructure.Ninject;
using Task.Repositories.Interfaces;

namespace Task.DataAccess.IntegrationTests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests : FixtureBase
    {
        private IUserRepository _userRepository;

        [Test]
        public void TestMethod1()
        {
        }

        protected override void CreateInitialData()
        {
            _userRepository = Locator.GetService<IUserRepository>();
            var passwordSalt = PasswordMethods.CreateSalt();
            var users = new[]
                           {
                               new User
                                   {
                                       Login = "Test1",
                                       Email = "test1@test.com",
                                       CreatedDate = DateTime.Now,
                                       PasswordSalt = passwordSalt,
                                       Password = PasswordMethods.CreatePasswordHash("testtest", passwordSalt),
                                       IsActivated = true,
                                       IsLockedOut = false,
                                       LastLockedOutDate = null,
                                       LastLoginDate = null,
                                   },
                               new User
                                   {
                                       Login = "Test2",
                                       Email = "test2@test.com",
                                       CreatedDate = DateTime.Now,
                                       PasswordSalt = passwordSalt,
                                       Password = PasswordMethods.CreatePasswordHash("testtest", passwordSalt),
                                       IsActivated = true,
                                       IsLockedOut = false,
                                       LastLockedOutDate = null,
                                       LastLoginDate = null,
                                   }
                           };
            foreach (var obj in users)
            {
                _userRepository.Add(obj);
            }
        }

        protected override void AfterEachTest()
        {
            base.AfterEachTest();
            _userRepository.DeleteAll();
        }
    }
}
