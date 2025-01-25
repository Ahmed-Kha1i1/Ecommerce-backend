using AutoMapper;
using Ecommerce.Application.Features.Categories;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryOverviewDTO>();

        }
    }
}
