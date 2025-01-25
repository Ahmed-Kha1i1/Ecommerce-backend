using AutoMapper;
using Ecommerce.Application.Features.ProductItems;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Profiles
{
    public class ProductItemProfile : Profile
    {
        public ProductItemProfile()
        {
            CreateMap<ProductItem, ProductItemDTO>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.DiscountRate, opt => opt.MapFrom(src => src.Discounts.Select(d => d.Rate).FirstOrDefault()))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images
                .Select(i => new ImageDTO { ImageName = i.ImageName, IsDefault = i.IsDefault }).ToList()))
                .ForMember(dest => dest.Variations, opt => opt.MapFrom(src => src.ProductVariations
                .ToDictionary(pv => pv.VariationOption.Variation.Name, pv => pv.VariationOption.Value).ToList()));


            CreateMap<ProductItemDetailsResult, ProductItemDetailsDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName))
                .ForMember(dest => dest.Variations, opt => opt.Ignore()); // Handled separately

            CreateMap<ProductItemDetailsResult, VariationDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.VariationName))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.VariationValue));
        }
    }
}
