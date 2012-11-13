using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using Task.Repositories.Interfaces;
using Task.Repositories.NHibernate;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.Repositories.BaseImplementation
{
    abstract class NHibernateRepository<T> : IBaseRepository<T> where T : class, new()
    {
        /// <summary>
        /// Object which creates a database connection and open a <c>ISession</c> on it
        /// </summary>
        private readonly ISessionProvider _sessionProvider;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="sessionProvider"></param>
        protected NHibernateRepository(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        /// <summary>
        /// Current session
        /// </summary>
        protected ISession Session
        {
            get { return _sessionProvider.GetSession(); }
        }

        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            ICriteria criteriaQuery = Session.CreateCriteria(typeof(T));
            return criteriaQuery.List<T>().AsQueryable();
        }

        /// <summary>
        /// Gets list of objects from database using predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            ICriteria criteriaQuery = Session.CreateCriteria(typeof(T));
            criteriaQuery.Add(predicate);
            return criteriaQuery.List<T>().AsQueryable();
        }

        /// <summary>
        /// Returns the total number of objects in database
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return GetAll().Count();
        }

        /// <summary>
        /// Gets "take" objects after skipping "skip" objects from database
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Get(int skip, int take)
        {
            return GetAll().Skip(skip).Take(take);
        }

        /// <summary>
        /// Gets "take" objects after skipping "skip" objects from dataabase using predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return Get(predicate).Skip(skip).Take(take);
        }

        /// <summary>
        /// Adds object to database
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Add(T obj)
        {
            Session.Save(obj);
        }

        /// <summary>
        /// Deletes object from database
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Delete(T obj)
        {
            Session.Delete(obj);
            Session.Flush();
        }

        /// <summary>
        /// Saves object to database
        /// </summary>
        /// <param name="entity"></param>
        public void Save(T entity)
        {
            Session.SaveOrUpdate(entity);
            Session.Flush();
        }
    }
}
