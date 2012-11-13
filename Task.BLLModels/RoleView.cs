using System.Collections.Generic;

namespace Task.BLLModels
{
    public class RoleView
    {
        public RoleView()
        {
            Users = new List<UserView>();
        }

        public int Id { get; set; }

        public string RoleName { get; set; }

        public List<UserView> Users { get; set; }
    }
}
