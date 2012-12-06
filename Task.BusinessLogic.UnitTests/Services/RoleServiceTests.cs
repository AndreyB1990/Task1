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
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _roleRepository = Mockery.DynamicMock<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin("A")).Return(users[0]);
                Expect.Call(_userRepository.GetUserByLogin("B")).Return(users[1]);
                Expect.Call(_roleRepository.GetRoleByRoleName("User")).Return(roles[0]);
                Expect.Call(_roleRepository.GetRoleByRoleName("Admin")).Return(roles[1]);
            }
            _roleService = new RoleService(_roleRepository, _userRepository);
            _roleService.AddUsersToRoles(new string[] {"A", "B"}, new string[] {"User", "Admin"});
            Assert.AreEqual(users[0].Roles.Count, 2);
            Assert.AreEqual(users[1].Roles.Count, 2);
        }

        [Test]
        public void IsUserInRole_returnsFalse()
        {
            var role = Builder<Role>.CreateNew().Build();
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _roleRepository = Mockery.DynamicMock<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            _roleService = new RoleService(_roleRepository, _userRepository);
            var validate = _roleService.IsUserInRole(user.Login, role.RoleName);
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void IsUserInRole_returnsTrue()
        {
            var role = Builder<Role>.CreateNew().Build();
            var user = Builder<User>.CreateNew().With(x => x.Roles = new List<Role> { role }).Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _roleRepository = Mockery.DynamicMock<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            _roleService = new RoleService(_roleRepository, _userRepository);
            var validate = _roleService.IsUserInRole(user.Login, role.RoleName);
            Assert.AreEqual(true, validate);
        }

        [Test]
        public void UserInRoleExists_returnsFalse()
        {
            var role = Builder<Role>.CreateNew().Build();
            var user = Builder<User>.CreateNew().Build();
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _roleRepository = Mockery.DynamicMock<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            _roleService = new RoleService(_roleRepository, _userRepository);
            var validate = _roleService.UserInRoleExists(role.RoleName);
            Assert.AreEqual(false, validate);
        }

        [Test]
        public void UserInRoleExists_returnsTrue()
        {
            var role = Builder<Role>.CreateNew().Build();
            var user = Builder<User>.CreateNew().With(x => x.Roles = new List<Role> { role }).Build();
            role.Users.Add(user);
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _roleRepository = Mockery.DynamicMock<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin(null)).IgnoreArguments().Return(user);
                Expect.Call(_roleRepository.GetRoleByRoleName(null)).IgnoreArguments().Return(role);
            }
            _roleService = new RoleService(_roleRepository, _userRepository);
            var validate = _roleService.UserInRoleExists(role.RoleName);
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
            _userRepository = Mockery.DynamicMock<IUserRepository>();
            _roleRepository = Mockery.DynamicMock<IRoleRepository>();
            using (Mockery.Record())
            {
                Expect.Call(_userRepository.GetUserByLogin("A")).Return(users[0]);
                Expect.Call(_userRepository.GetUserByLogin("B")).Return(users[1]);
                Expect.Call(_roleRepository.GetRoleByRoleName("User")).Return(roles[0]);
                Expect.Call(_roleRepository.GetRoleByRoleName("Admin")).Return(roles[1]);
            }
            _roleService = new RoleService(_roleRepository, _userRepository);
            _roleService.RemoveUsersFromRoles(new string[] { "A", "B" }, new string[] { "User", "Admin" });
            Assert.AreEqual(users[0].Roles.Count, 0);
            Assert.AreEqual(users[1].Roles.Count, 0);
        }
    }
}
