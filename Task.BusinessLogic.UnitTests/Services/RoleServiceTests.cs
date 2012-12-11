using System.Collections.Generic;
using FizzWare.NBuilder;
using NUnit.Framework;
using Rhino.Mocks;
using Task.BusinessLogic.UnitTests.Services.BaseImplementation;
using Task.DALModels;
using Task.Repositories.Interfaces;
using Task.Services;
using Task.Services.Interfaces;

namespace Task.BusinessLogic.UnitTests.Services
{
    [TestFixture]
    public class RoleServiceTests : ServiceFixtureBase
    {
        private IRoleService _roleService;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        [Test]
        public void AddUsersToRoles()
        {
            var users = Builder<User>.CreateListOfSize(2).
                TheFirst(1).With(x => x.Login = "A").
                TheLast(1).With(x => x.Login = "B").Build();
            var roles = Builder<Role>.CreateListOfSize(2).
                TheFirst(1).With(x => x.RoleName = "User").
                TheLast(1).With( x => x.RoleName = "Admin").Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin("A")).Return(users[0]);
                Expect.Call(_userRepository.GetUserByLogin("B")).Return(users[1]);
                Expect.Call(_roleRepository.GetRoleByRoleName("User")).Return(roles[0]);
                Expect.Call(_roleRepository.GetRoleByRoleName("Admin")).Return(roles[1]);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                _roleService.AddUsersToRoles(new[] {"A", "B"}, new[] {"User", "Admin"});
            }
            Assert.AreEqual(users[0].Roles.Count, 2);
            Assert.AreEqual(users[1].Roles.Count, 2);
        }

        [Test]
        public void AddUsersToRoles_methodAddUserToRoleIsCalled()
        {
            var users = Builder<User>.CreateListOfSize(2).
                TheFirst(1).With(x => x.Login = "A").
                TheLast(1).With(x => x.Login = "B").Build();
            var roles = Builder<Role>.CreateListOfSize(2).
                TheFirst(1).With(x => x.RoleName = "User").
                TheLast(1).With(x => x.RoleName = "Admin").Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            var mockRoleService = Mockery.PartialMock<RoleService>(_roleRepository, _userRepository);
            var t = users[0];
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin("A")).Return(users[0]);
                Expect.Call(_userRepository.GetUserByLogin("B")).Return(users[1]);
                Expect.Call(_roleRepository.GetRoleByRoleName("User")).Return(roles[0]);
                Expect.Call(_roleRepository.GetRoleByRoleName("Admin")).Return(roles[1]);
                Expect.Call(() => mockRoleService.AddRoleToUser(Arg<User>.Is.Equal(users[0]), Arg<Role>.Is.Equal(roles[0]))).Repeat.Once();
                Expect.Call(() => mockRoleService.AddRoleToUser(Arg<User>.Is.Equal(users[0]), Arg<Role>.Is.Equal(roles[1]))).Repeat.Once();
                Expect.Call(() => mockRoleService.AddRoleToUser(Arg<User>.Is.Equal(users[1]), Arg<Role>.Is.Equal(roles[0]))).Repeat.Once();
                Expect.Call(() => mockRoleService.AddRoleToUser(Arg<User>.Is.Equal(users[1]), Arg<Role>.Is.Equal(roles[1]))).Repeat.Once();
            }

            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                mockRoleService.AddUsersToRoles(new[] { "A", "B" }, new[] { "User", "Admin" });
            }
        }

        [Test]
        public void AddRoleToUser_addRoleToUserIfUserNotContainsRole()
        {
            var user = Builder<User>.CreateNew().Build();
            var role = Builder<Role>.CreateNew().Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            _roleService = new RoleService(_roleRepository, _userRepository);
            _roleService.AddRoleToUser(user, role);
            Assert.AreEqual(true, user.Roles.Contains(role));
        }

        [Test]
        public void AddRoleToUser_notAddRoleToUserIfUserContainsRole()
        {
            var role = Builder<Role>.CreateNew().Build();
            var user = Builder<User>.CreateNew().With(x => x.Roles = new List<Role> { role }).Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            _roleService = new RoleService(_roleRepository, _userRepository);
            _roleService.AddRoleToUser(user, role);
            Assert.AreEqual(true, user.Roles.Contains(role));
            user.Roles.Remove(role);
            Assert.AreEqual(false, user.Roles.Contains(role));
        }

        [Test]
        public void DeleteRole_returnsTrueIfRoleExists()
        {
            bool validate;
            var role = Builder<Role>.CreateNew().Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
                Expect.Call(() => _roleRepository.Delete(Arg<Role>.Is.Anything)).Repeat.Once();
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.DeleteRole(role.RoleName);
            }
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void DeleteRole_returnsFalseIfRoleNotExists()
        {
            bool validate;
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(null);
                DoNotExpect.Call(() => _roleRepository.Delete(Arg<Role>.Is.Anything));
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.DeleteRole("UnknownRole");
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void RoleExists_returnsTrueIfRoleExists()
        {
            bool validate;
            var role = Builder<Role>.CreateNew().Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.RoleExists(role.RoleName);
            }
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void RoleExists_returnsFalseIfRoleNotExists()
        {
            bool validate;
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(null);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.RoleExists("UnknownRole");
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void UserInRoleExists_returnsTrueIfUserInRoleExists()
        {
            bool validate;
            var user = Builder<User>.CreateNew().Build();
            var role = Builder<Role>.CreateNew().With(x => x.Users = new List<User> { user }).Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.UserInRoleExists(role.RoleName);
            }
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void UserInRoleExists_returnsFalseIfUserInRoleNotExists()
        {
            bool validate;
            var role = Builder<Role>.CreateNew().With(x => x.Users = new List<User>()).Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.UserInRoleExists(role.RoleName);
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void UserInRoleExists_returnsFalseIfRoleNotExists()
        {
            bool validate;
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(null);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.UserInRoleExists("UnknownRole");
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void IsUserInRole_returnsFalseIfUserNotInRole()
        {
            bool validate;
            var role = Builder<Role>.CreateNew().Build();
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.IsUserInRole(user.Login, role.RoleName);
            }
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void IsUserInRole_returnsTrueIfUserInRole()
        {
            bool validate;
            var role = Builder<Role>.CreateNew().Build();
            var user = Builder<User>.CreateNew().With(x => x.Roles = new List<Role> { role }).Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                validate = _roleService.IsUserInRole(user.Login, role.RoleName);
            }
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void RemoveUsersFromRoles()
        {
            var roles = Builder<Role>.CreateListOfSize(2).
                TheFirst(1).With(x => x.RoleName = "User").
                TheLast(1).With(x => x.RoleName = "Admin").Build();
            var users = Builder<User>.CreateListOfSize(2).
                TheFirst(1).With(x => x.Login = "A").With(x => x.Roles =  roles).
                TheLast(1).With(x => x.Login = "B").With(x => x.Roles = roles).Build();
            _userRepository = Mockery.Stub<IUserRepository>();
            _roleRepository = Mockery.Stub<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin("A")).Return(users[0]);
                Expect.Call(_userRepository.GetUserByLogin("B")).Return(users[1]);
                Expect.Call(_roleRepository.GetRoleByRoleName("User")).Return(roles[0]);
                Expect.Call(_roleRepository.GetRoleByRoleName("Admin")).Return(roles[1]);
            }
            using (Mockery.Playback())
            {
                _roleService = new RoleService(_roleRepository, _userRepository);
                _roleService.RemoveUsersFromRoles(new[] {"A", "B"}, new[] {"User", "Admin"});
            }
            Assert.AreEqual(users[0].Roles.Count, 0);
            Assert.AreEqual(users[1].Roles.Count, 0);
        }
    }
}
