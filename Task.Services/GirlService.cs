using System.Collections.Generic;
using Task.DALModels;
using Task.Repositories.Interfaces;
using Task.Services.BaseImplementation;
using Task.Services.Interfaces;

namespace Task.Services
{
    class GirlService : BaseService<Girl>, IGirlService
    {
        /// <summary>
        /// Data context
        /// </summary>
        private readonly IGirlRepository _context;

        /// <summary>
        /// Base constructor, which initializes BaseService(Girl)
        /// </summary>
        /// <param name="context"></param>
        public GirlService(IGirlRepository context) : base(context)
        {
            _context = (IGirlRepository)Context;
        }

        /// <summary>
        /// Return all girl objects 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Girl> GetAll()
        {
            return _context.GetAll();
        }

        /// <summary>
        /// Return girl objects, that have Factor less than 17 and greater then 22
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Girl> GetBeautifulGirls()
        {
            return _context.GetBeautifulGirls();
        }

    }
}
