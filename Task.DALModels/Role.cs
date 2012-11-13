using System.Collections.Generic;
using Task.DALModels.Interfaces;

namespace Task.DALModels
{
    public class Role : IBaseModel
    {
        public Role()
        {
            Users = new List<User>();
        }

        public virtual int Id { get; set; }

        public virtual string RoleName { get; set; }

        public virtual IList<User> Users { get; set; }
    }
}
