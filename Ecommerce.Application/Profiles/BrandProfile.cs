using AutoMapper;
using Ecommerce.Application.Features.Brands;
using Ecommerce.Application.Features.Brands.Queries.BrandsSearchQuery;
using Ecommerce.Doman.Entities;
using System.Data;

namespace Ecommerce.Application.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<IDataReader, GetBrandsSearchQueryResponse>();
            CreateMap<Brand, BrandOverviewDTO>();
        }
    }
}
