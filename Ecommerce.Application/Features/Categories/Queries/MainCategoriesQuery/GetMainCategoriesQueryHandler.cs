using AutoMapper;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;


namespace Ecommerce.Application.Features.Categories.Queries.MainCategoriesQuery
{
    public class GetMainCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : ResponseHandler, IRequestHandler<GetMainCategoriesQuery, Response<IList<CategoryDto>>>
    {
        public async Task<Response<IList<CategoryDto>>> Handle(GetMainCategoriesQuery request, CancellationToken cancellationToken)
        {
            var mainCategories = await categoryRepository.GetAllMainCategories();

            var mapperResult = mapper.Map<IList<CategoryDto>>(mainCategories);

            return Success(mapperResult);
        }
    }
}
