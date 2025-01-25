using AutoMapper;
using Ecommerce.Application.Features.Countries.Queries;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}

