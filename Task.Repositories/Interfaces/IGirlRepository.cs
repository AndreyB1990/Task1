using System.Linq;
using Task.DALModels;

namespace Task.Repositories.Interfaces
{
    public interface IGirlRepository : IBaseRepository<Girl>
    {
        /// <summary>
        /// Returns girl objects from database, that have Factor less than 17 and greater then 22
        /// </summary>
        /// <returns></returns>
        IQueryable<Girl> GetBeautifulGirls();
    }
}
