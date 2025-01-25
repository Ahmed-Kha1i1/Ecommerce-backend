using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Queries.CategoryHierarchyQuery
{
    public class GetCategoryHierarchyQuery : IRequest<Response<GetCategoryHierarchyQueryResponse>>
    {
        public int CategoryId { get; set; }
        public GetCategoryHierarchyQuery(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
}
