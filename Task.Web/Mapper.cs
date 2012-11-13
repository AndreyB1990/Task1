using System.Collections.Generic;
using AutoMapper;
using Task.DALModels;
using Task.Infrastructure.Helpers;
using Task.Web.Models;

namespace Task.Web
{
    public static class CreateMappings
    {
        /// <summary>
        /// Creates mappings for AutoMapper
        /// </summary>
        public static void Create()
        {
            Mapper.CreateMap<Girl, GirlModel>().ForMember(v => v.Age, opt => opt.MapFrom(GirlMethods.GetAge))
                .ForMember(v => v.Factor, opt => opt.MapFrom(GirlMethods.GetFactor));
        }
    }
    public static class GirlMapper
    {
        /// <summary>
        /// Converts Girl class to GirlModel class
        /// </summary>
        /// <param name="girl"></param>
        /// <returns></returns>
        public static GirlModel ConvertToGirlModel(this Girl girl)
        {
            return Mapper.Map<Girl, GirlModel>(girl);
        }

        /// <summary>
        /// Converts IEnumerable(Girl) to IEnumerable(GirlModel) class
        /// </summary>
        /// <param name="girls"></param>
        /// <returns></returns>
        public static IEnumerable<GirlModel> ConvertToGirlModels(this IEnumerable<Girl> girls)
        {
            return Mapper.Map<IEnumerable<Girl>, IEnumerable<GirlModel>>(girls);
        }
    }
}
