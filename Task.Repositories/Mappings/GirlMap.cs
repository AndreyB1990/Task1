using FluentNHibernate.Mapping;
using Task.DALModels;

namespace Task.Repositories.Mappings
{
    public class GirlMap : ClassMap<Girl>
    {
        /// <summary>
        /// Base constructor, which creates nhibernate mappings for class 'Girl'
        /// </summary>
        public GirlMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.BirthDate);
            Map(x => x.Weight);
            Map(x => x.Height);
        }
    }
}
