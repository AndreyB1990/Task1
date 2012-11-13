using System;
using System.Linq;
using System.Linq.Expressions;

namespace Task.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T: class, new()
    {
        /// <summary>
        /// Gets all objects from database
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets list of objects from database using predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the total number of objects in database
        /// </summary>
        /// <returns>Int</returns>
        int Count();

        /// <summary>
        /// Gets "take" objects after skipping "skip" objects from database
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IQueryable<T> Get(int skip, int take);

        /// <summary>
        /// Gets "take" objects after skipping "skip" objects from dataabase using predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate, int skip, int take);

        /// <summary>
        /// Adds object to database
        /// </summary>
        /// <param name="obj"></param>
        void Add(T obj);

        /// <summary>
        /// Deletes object from database
        /// </summary>
        /// <param name="obj"></param>
        void Delete(T obj);

        /// <summary>
        /// Saves object to database
        /// </summary>
        /// <param name="entity"></param>
        void Save(T entity);
    }
}
