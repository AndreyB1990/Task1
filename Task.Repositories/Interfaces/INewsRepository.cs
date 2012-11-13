using System.Linq;
using Task.DALModels;

namespace Task.Repositories.Interfaces
{
    public interface INewsRepository : IBaseRepository<News>
    {
        /// <summary>
        /// Gets all news objects which was published at no more than Constants.TIME_OF_NEWS days ago
        /// </summary>
        /// <returns></returns>
        IQueryable<News> GetLatestNews();
    }
}
