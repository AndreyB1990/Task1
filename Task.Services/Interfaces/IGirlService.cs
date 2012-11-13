using System.Collections.Generic;
using Task.DALModels;

namespace Task.Services.Interfaces
{
    public interface IGirlService : IBaseService<Girl>
    {
        /// <summary>
        /// Returns girl objects from context, that have Factor less than 17 and greater then 22
        /// </summary>
        /// <returns></returns>
        IEnumerable<Girl> GetBeautifulGirls();
    }
}
