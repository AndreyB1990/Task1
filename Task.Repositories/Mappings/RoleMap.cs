using FluentNHibernate.Mapping;
using Task.DALModels;

namespace Task.Repositories.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        /// <summary>
        /// Base constructor, which creates nhibernate mappings for class 'Role'
        /// </summary>
        public RoleMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.RoleName);
            HasManyToMany(x => x.Users)
                .Cascade.All()
                .Inverse()
                .Table("UserRole");
            //.ParentKeyColumn("RoleID")
            //.ChildKeyColumn("UserID");
        }
    }
}
