using AutoMapper;
using Ecommerce.Application.Features.ProductItems;
using Ecommerce.Application.Features.ShoppingCartItems;
using Ecommerce.Application.Features.ShoppingCarts;

namespace Ecommerce.Application.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {

            CreateMap<ShoppingCartResult, ShoppingCartItemDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ShoppingCartItemId))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
           .ForMember(dest => dest.ProductItemDetails, opt => opt.Ignore()); // Handled separately


            CreateMap<ShoppingCartResult, ProductItemDetailsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductItemId))
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.Variations, opt => opt.Ignore()); // Handled separately

            CreateMap<ShoppingCartResult, VariationDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.VariationName))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.VariationValue));
        }
    }
}
