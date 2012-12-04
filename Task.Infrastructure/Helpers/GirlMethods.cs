using System;
using Task.DALModels;

namespace Task.Infrastructure.Helpers
{
    public static class GirlMethods
    {
        /// <summary>
        /// Calculates the age of girl using her date of birth
        /// </summary>
        /// <param name="girl"></param>
        /// <returns>Int</returns>
        public static int GetAge(Girl girl)
        {
            TimeSpan span = DateTime.Now - girl.BirthDate;
            var relative = new DateTime(span.Ticks);
            return relative.Year;
        }

        /// <summary>
        /// Calculates the factor of girl by formila weight/(height/100)^2
        /// </summary>
        /// <param name="girl"></param>
        /// <returns>Double</returns>
        public static double GetFactor(Girl girl)
        {
            if (girl.Height != null)
                return (double) (girl.Weight / (Math.Pow((double) girl.Height, 2) / (Constants.ONE_METR_IN_SM * Constants.ONE_METR_IN_SM)));
            return 0;
        }
    }
}
