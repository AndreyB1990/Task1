using System.Collections.Generic;
using Task.DALModels;
using Task.Repositories.Interfaces;
using Task.Services.BaseImplementation;
using Task.Services.Interfaces;

namespace Task.Services
{
    class NewsService : BaseService<News>, INewsService
    {
        /// <summary>
        /// Data context
        /// </summary>
        private readonly INewsRepository _context;

        /// <summary>
        /// Base constructor, which initializes BaseService(News)
        /// </summary>
        /// <param name="context"></param>
        public NewsService(INewsRepository context) : base(context)
        {
            _context = (INewsRepository)Context;
        }

        /// <summary>
        /// Gets all news objects
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<News> GetAll()
        {
            var t = _context.GetAll();
            return _context.GetAll();
        }

        /// <summary>
        /// Gets news objects which was published at no more than Constants.TIME_OF_NEWS days ago
        /// </summary>
        /// <returns></returns>
        public IEnumerable<News> GetLatestNews()
        {
            return _context.GetLatestNews();
        }

    }
}
