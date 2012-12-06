using FizzWare.NBuilder;
using NUnit.Framework;
using Rhino.Mocks;
using Task.BusinessLogic.UnitTests.Services.BaseImplementation;
using Task.DALModels;
using Task.Infrastructure.Helpers.Providers.Interfaces;
using Task.Infrastructure.Models;
using Task.Repositories.Interfaces;
using Task.Services;
using Task.Services.Interfaces;

namespace Task.BusinessLogic.UnitTests.Services
{
    [TestFixture]
    public class UserServiceTests : ServiceFixtureBase
    {
        private IUserService _userService;
        private IUserRepository _userRepository;
        private IPasswordMethodsProvider _passwordMethodsProvider;

        [Test]
        public void RegisterUser_returnsSuccessRegisterStatusIfValidUser()
        {
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _passwordMethodsProvider = Mockery.DynamicMock<IPasswordMethodsProvider>();
            using (Mockery.Record())
            {
                Expect.Call(_passwordMethodsProvider.CreateSalt()).Return("CreateSalt");
                Expect.Call(_passwordMethodsProvider.CreatePasswordHash(null, null)).IgnoreArguments().Return("CreatePasswordHash");
            }
            _userService = new UserService(_userRepository, _passwordMethodsProvider);
            RegisterStatus createStatus;
            _userService.RegisterUser(user.Login, user.Email, user.Password, out createStatus);
            Assert.AreEqual(createStatus, RegisterStatus.Success);
        }

        [Test]
        public void ValidateUser_returnsTrueIfValidUser()
        {
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _passwordMethodsProvider = Mockery.DynamicMock<IPasswordMethodsProvider>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
                Expect.Call(_passwordMethodsProvider.CreatePasswordHash(null, null)).IgnoreArguments().Return(user.Password);
            }
            _userService = new UserService(_userRepository, _passwordMethodsProvider);
            var validate = _userService.ValidateUser(user.Login, user.Password);
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void ChangePassword_returnsTrueIfValidArguments()
        {
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
            }
            _userService = new UserService(_userRepository, _passwordMethodsProvider);
            var validate = _userService.ChangePassword(user.Login, user.Password, "newPassword");
            Assert.AreEqual(true, validate);
            Assert.AreEqual(user.Password, "newPassword");
        }
    }
}
