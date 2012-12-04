using FluentNHibernate.Mapping;
using Task.DALModels;

namespace Task.Repositories.Mappings
{
    public class NewsMap : ClassMap<News>
    {
        /// <summary>
        /// Base constructor, which creates nhibernate mappings for class 'News'
        /// </summary>
        public NewsMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Date).Not.Nullable();
            Map(x => x.ShortDescription).Not.Nullable();
            Map(x => x.Description).Nullable();
        }
    }
}
