using AutoMapper;
using CoreWebAPI.Model;

namespace CoreWebAPI.Core
{
    public  class PopulationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PopulationProfile"/> class.
        /// </summary>
        public PopulationProfile()
        {
            CreateMap<Actual, Response>().ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Population, opt => opt.MapFrom(src => src.ActualPopulation));

            CreateMap<int, Response>().ForMember(dest => dest.Population, opt => opt.MapFrom(src => src));
        }
    }
}
