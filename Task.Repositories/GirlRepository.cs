using System.Linq;
using NHibernate;
using Task.DALModels;
using Task.Infrastructure;
using Task.Infrastructure.Helpers;
using Task.Repositories.BaseImplementation;
using Task.Repositories.Interfaces;
using Task.Repositories.NHibernate.Interfaces;

namespace Task.Repositories
{
    class GirlRepository : NHibernateRepository<Girl>, IGirlRepository
    {
        /// <summary>
        /// Base constructor, which initializes NHibernateRepository(Girl)
        /// </summary>
        /// <param name="sessionProvider"></param>
        public GirlRepository(ISessionProvider sessionProvider)
            : base(sessionProvider)
        {
        }

        /// <summary>
        /// Returns girl items from database, that have Factor less than 22 and greater then 17
        /// </summary>
        /// <returns></returns>
        public IQueryable<Girl> GetBeautifulGirls()
        {
            var beautifulGirls =
                (from girl in GetAll()
                 let factor = CalculateFactor(girl)
                 where Constants.MIN_VALUE_FACTOR_GIRL < factor
                  && factor < Constants.MAX_VALUE_FACTOR_GIRL
                 select girl).ToList().OrderBy(GirlMethods.GetAge).AsQueryable();
            return beautifulGirls;
            //ICriteria criteriaQuery = Session.CreateCriteria(typeof(Girl));
            //IProjection multiplyProjection = new SqlFunctionProjection(new VarArgsSQLFunction("(", "*", ")"), NHibernateUtil.Double,
            //    Projections.Property<Girl>(girl => girl.Height), Projections.Property<Girl>(girl => girl.Height));
            //IProjection divideProjection = new SqlFunctionProjection(new VarArgsSQLFunction("(", "/", ")"), NHibernateUtil.Double,
            //    multiplyProjection, Projections.Constant(Constants.ONE_METR_IN_SM * Constants.ONE_METR_IN_SM));
            //criteriaQuery.Add(Restrictions.Between(new SqlFunctionProjection(new VarArgsSQLFunction("(", "/", ")"), NHibernateUtil.Double,
            //    Projections.Property<Girl>(girl => girl.Weight), divideProjection), Constants.MIN_VALUE_FACTOR_GIRL, Constants.MAX_VALUE_FACTOR_GIRL));
            //return criteriaQuery.List<Girl>().OrderBy(GirlHelperMethods.GetAge).AsQueryable();
        }

        private static double CalculateFactor(Girl girl)
        {
            var heightInMeteres = (girl.Height / Constants.ONE_METR_IN_SM);
            var heightDoubled = heightInMeteres * heightInMeteres;
            return (double) (girl.Weight / heightDoubled);
        }


        /// <summary>
        /// Gets all girl items
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Girl> GetAll()
        {
            ICriteria criteriaQuery = Session.CreateCriteria(typeof(Girl));
            return criteriaQuery.List<Girl>().OrderBy(GirlMethods.GetAge).AsQueryable();
        }
    }
}
