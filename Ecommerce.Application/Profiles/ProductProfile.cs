using AutoMapper;
using Ecommerce.Application.Features.Products;
using Ecommerce.Application.Features.Products.Queries.ProductDetailsQuery;
using Ecommerce.Doman.Entities;
using System.Data;

namespace Ecommerce.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<IDataReader, ProductSummaryDTO>();
            CreateMap<Product, GetProductDetailsQueryReponse>()
                .ForMember(dest => dest.Condition, opt => opt.MapFrom(src => src.Condition.ToString()))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.CountryOfOrigin.Name));
        }
    }
}
