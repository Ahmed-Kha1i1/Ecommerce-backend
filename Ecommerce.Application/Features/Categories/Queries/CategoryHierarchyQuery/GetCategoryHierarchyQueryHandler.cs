using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Queries.CategoryHierarchyQuery
{
    public class GetCategoryHierarchyQueryHandler(ICategoryRepository categoryRepository) : ResponseHandler, IRequestHandler<GetCategoryHierarchyQuery, Response<GetCategoryHierarchyQueryResponse>>
    {
        public async Task<Response<GetCategoryHierarchyQueryResponse>> Handle(GetCategoryHierarchyQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetCategoryHierarchy(request.CategoryId);

            if (category == null)
            {
                return BadRequest<GetCategoryHierarchyQueryResponse>($"Category with ID {request.CategoryId} not found.");
            }

            return Success(category);
        }
    }
}
