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
            RegisterStatus createStatus;
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                _userService.RegisterUser(user.Login, user.Email, user.Password, out createStatus);
            }
            Assert.AreEqual(createStatus, RegisterStatus.Success);
        }

        [Test]
        public void ValidateUser_returnsTrueIfValidUser()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _passwordMethodsProvider = Mockery.DynamicMock<IPasswordMethodsProvider>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
                Expect.Call(_passwordMethodsProvider.CreatePasswordHash(null, null)).IgnoreArguments().Return(user.Password);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.ValidateUser(user.Login, user.Password);
            }
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void CheckUserLogin_returnsFalseIfUserNotExists()
        {
            bool validate;
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(null);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.CheckUserLogin("UnknownUser");
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void CheckUserLogin_returnsTrueIfUserExists()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.CheckUserLogin(user.Login);
            }
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void CheckUserEmail_returnsFalseIfUserNotExists()
        {
            bool validate;
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByEmail(null)).IgnoreArguments().Return(null);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.CheckUserEmail("UnknownEmail");
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void CheckUserEmail_returnsTrueIfUserExists()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByEmail(null)).IgnoreArguments().Return(user);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.CheckUserEmail(user.Email);
            }
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void DeleteUser_returnsTrueIfUserExists()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
                Expect.Call(() => _userRepository.Delete(Arg<User>.Is.Anything)).Repeat.Once();
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.DeleteUser(user.Login);
            }
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void DeleteUser_returnsFalseIfUserNotExists()
        {
            bool validate;
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(null);
                DoNotExpect.Call(() => _userRepository.Delete(Arg<User>.Is.Anything));
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.DeleteUser("UnknownUser");
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void LockUser_returnsTrueIfUserExists()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.LockUser(user.Login);
            }
            Assert.AreEqual(true, validate);
            Assert.AreEqual(true, user.IsLockedOut);
        }

        [Test]
        public void LockUser_returnsFalseIfUserNotExists()
        {
            bool validate;
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(null);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.LockUser("UnknownUser");
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void UnlockUser_returnsTrueIfUserExists()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.UnlockUser(user.Login);
            }
            Assert.AreEqual(true, validate);
            Assert.AreEqual(false, user.IsLockedOut);
        }

        [Test]
        public void UnlockUser_returnsFalseIfUserNotExists()
        {
            bool validate;
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(null);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.UnlockUser("UnknownUser");
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void ChangePassword_returnsTrueIfValidArguments()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.ChangePassword(user.Login, user.Password, "newPassword");
            }
            Assert.AreEqual(true, validate);
            Assert.AreEqual(user.Password, "newPassword");
        }

        [Test]
        public void ChangePassword_returnsFalseIfUnvalidArguments()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
            }
            using (Mockery.Playback())
            {
                _userService = new UserService(_userRepository, _passwordMethodsProvider);
                validate = _userService.ChangePassword(user.Login, "UnvalidPassword", "newPassword");
            }
            Assert.AreEqual(false, validate);
            Assert.AreNotEqual(user.Password, "newPassword");
        }
    }
}
