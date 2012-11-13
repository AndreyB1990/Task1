using System.Collections.Generic;
using Task.DALModels.Interfaces;

namespace Task.Services.Interfaces
{
    public interface IBaseService<T> where T : class, IBaseModel, new()
    {
        /// <summary>
        /// Adds object to context
        /// </summary>
        /// <param name="obj"></param>
        void Add(T obj);

        /// <summary>
        /// Deletes object from context
        /// </summary>
        /// <param name="obj"></param>
        void Delete(T obj);

        /// <summary>
        /// Updates object in context
        /// </summary>
        /// <param name="obj"></param>
        void Update(T obj);

        /// <summary>
        /// Gets single object from context by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);
        
        /// <summary>
        /// Gets "take" objects after skipping "skip" objects from context
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        IEnumerable<T> Get(int skip, int take);

        /// <summary>
        /// Gets all objects from context
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
    }
}
