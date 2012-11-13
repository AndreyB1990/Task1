using System.Collections.Generic;
using System.Linq;
using Task.DALModels.Interfaces;
using Task.Repositories.Interfaces;
using Task.Services.Interfaces;

namespace Task.Services.BaseImplementation
{
    internal abstract class BaseService<T> : IBaseService<T> where T : class, IBaseModel, new()
    {
        /// <summary>
        /// Data context
        /// </summary>
        protected readonly IBaseRepository<T> Context;

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="context"></param>
        protected BaseService(IBaseRepository<T> context)
        {
            Context = context;
        }

        /// <summary>
        /// Adds object to context
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Add(T obj)
        {
            Context.Add(obj);
        }

        /// <summary>
        /// Deletes object from context
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Delete(T obj)
        {
            Context.Delete(obj);
        }

        /// <summary>
        /// Updates object in context
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Update(T obj)
        {
            Context.Save(obj);
        }

        /// <summary>
        /// Gets single object from context by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(int id)
        {
            return Context.Get(x => x.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Gets "take" objects after skipping "skip" objects
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get(int skip, int take)
        {
            return Context.Get(skip, take);
        }

        /// <summary>
        /// Gets all objects from context
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return Context.GetAll();
        }
    }
}
