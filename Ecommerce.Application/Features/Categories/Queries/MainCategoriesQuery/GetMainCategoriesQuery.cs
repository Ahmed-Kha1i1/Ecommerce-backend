using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Categories.Queries.MainCategoriesQuery
{
    public class GetMainCategoriesQuery : IRequest<Response<IList<CategoryDto>>>
    {
    }
}
