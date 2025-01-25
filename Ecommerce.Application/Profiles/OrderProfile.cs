using AutoMapper;
using Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, GetOrderDetailsQueryResponse>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderAddress, opt => opt.MapFrom(src => src.OrderAddress))
                .ForMember(dest => dest.Lines, opt => opt.Ignore());
        }
    }
}
