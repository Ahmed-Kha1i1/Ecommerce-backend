using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.ProductsDetailsQuery
{
    public class GetProductsDetailsQuery : IRequest<Response<IList<ProductSummaryDTO>>>
    {
        public ICollection<int> Ids { get; set; }

        public GetProductsDetailsQuery(ICollection<int> ids)
        {
            Ids = ids;
        }
    }
}
