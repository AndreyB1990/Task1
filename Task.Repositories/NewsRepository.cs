using System;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using Task.DALModels;
using Task.Infrastructure;
using Task.Repositories.BaseImplementation;
using Task.Repositories.Interfaces;
using Task.Repositories.NHibernate;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.Repositories
{
    public class NewsRepository : NHibernateRepository<News>, INewsRepository
    {
        /// <summary>
        /// Base constructor, which initializes NHibernateRepository(News)
        /// </summary>
        /// <param name="sessionProvider"></param>
        public NewsRepository(ISessionProvider sessionProvider):base(sessionProvider)
        {
        }

        /// <summary>
        /// Gets list of news objects from database using predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override IQueryable<News> Get(System.Linq.Expressions.Expression<Func<News, bool>> predicate)
        {
            ICriteria criteriaQuery = Session.CreateCriteria(typeof(News));
            criteriaQuery.Add(predicate);
            criteriaQuery.AddOrder(Order.Desc("Date"));
            return criteriaQuery.List<News>().AsQueryable();
        }
        
        /// <summary>
        /// Gets all news objects
        /// </summary>
        /// <returns></returns>
        public override IQueryable<News> GetAll()
        {
            ICriteria criteriaQuery = Session.CreateCriteria(typeof(News));
            criteriaQuery.AddOrder(Order.Desc("Date"));
            return criteriaQuery.List<News>().AsQueryable();
        }

        /// <summary>
        /// Gets all news objects which was published at no more than Constants.TIME_OF_NEWS days ago
        /// </summary>
        /// <returns></returns>
        public IQueryable<News> GetLatestNews()
        {
            //ICriteria criteriaQuery = Session.CreateCriteria(typeof(News));
            var newsTime = DateTime.Now - Constants.TIME_OF_NEWS;
            //criteriaQuery.Add(Restrictions.Gt("Date", newsTime));
            //criteriaQuery.AddOrder(Order.Desc("Date"));
            //if (criteriaQuery.List<News>() == null)
            //    return new EnumerableQuery<News>(new News[]{});
            //return criteriaQuery.List<News>().AsQueryable();
            return GetAll().Where(x => x.Date.CompareTo(newsTime) == 1).OrderBy(x => x.Date);
        }
    }
}

