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
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.BirthDate).Not.Nullable();
            Map(x => x.Weight).Not.Nullable();
            Map(x => x.Height).Not.Nullable();
        }
    }
}
