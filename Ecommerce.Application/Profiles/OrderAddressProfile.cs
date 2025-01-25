using AutoMapper;
using Ecommerce.Application.Features.Orders.Queries;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Profiles
{
    public class OrderAddressProfile : Profile
    {
        public OrderAddressProfile()
        {
            CreateMap<OrderAddress, OrderAddressDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.Address2))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
        }
    }
}
