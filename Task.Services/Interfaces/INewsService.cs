using System.Collections.Generic;
using Task.DALModels;

namespace Task.Services.Interfaces
{
    public interface INewsService : IBaseService<News>
    {
        /// <summary>
        /// Gets all news which was published at no more than Constants.TIME_OF_NEWS days ago
        /// </summary>
        /// <returns></returns>
        IEnumerable<News> GetLatestNews();
    }
}
