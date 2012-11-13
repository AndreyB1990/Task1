using FluentNHibernate.Mapping;
using Task.DALModels;

namespace Task.Repositories.Mappings
{
    public class UserMap : ClassMap<User>
    {
        /// <summary>
        /// Base constructor, which creates nhibernate mappings for class 'User'
        /// </summary>
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Login);
            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.PasswordSalt);
            Map(x => x.IsActivated);
            Map(x => x.CreatedDate);
            Map(x => x.LastLoginDate);
            Map(x => x.IsLockedOut);
            Map(x => x.LastLockedOutDate);
            HasManyToMany(x => x.Roles)
                .Cascade.All()
                .Table("UserRole");
            //.ParentKeyColumn("UserID")
            //.ChildKeyColumn("RoleID");
        }
    }
}
