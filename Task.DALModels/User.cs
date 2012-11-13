using System;
using System.Collections.Generic;
using Task.DALModels.Interfaces;

namespace Task.DALModels
{
    public class User : IBaseModel
    {
        public User()
        {
            Roles = new List<Role>();
        }

        public virtual int Id { get; set; }

        public virtual string Login { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual string PasswordSalt { get; set; }

        public virtual bool IsActivated { get; set; }

        public virtual DateTime? CreatedDate { get; set; }

        public virtual DateTime? LastLoginDate { get; set; }

        public virtual bool IsLockedOut { get; set; }

        public virtual DateTime? LastLockedOutDate { get; set; }

        public virtual IList<Role> Roles { get; set; }
    }
}
