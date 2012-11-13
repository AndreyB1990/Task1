using System;
using System.Collections.Generic;

namespace Task.BLLModels
{
    public class UserView
    {
        public UserView()
        {
            Roles = new List<RoleView>();
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public bool IsActivated { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime? LastLockedOutDate { get; set; }

        public List<RoleView> Roles { get; set; }
    }
}
