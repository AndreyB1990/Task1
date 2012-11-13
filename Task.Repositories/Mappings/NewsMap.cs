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
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title);
            Map(x => x.Date);
            Map(x => x.ShortDescription);
            Map(x => x.Description);
        }
    }
}
