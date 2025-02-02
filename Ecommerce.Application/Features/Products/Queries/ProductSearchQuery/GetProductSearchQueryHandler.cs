using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.ProductSearchQuery
{
    public class GetProductSearchQueryHandler(IProductRepository productRepository) : ResponseHandler, IRequestHandler<GetProductSearchQuery, Response<PaginatedProductSearchResult<ProductSummaryDTO>>>
    {
        public async Task<Response<PaginatedProductSearchResult<ProductSummaryDTO>>> Handle(GetProductSearchQuery request, CancellationToken cancellationToken)
        {
            var result = await productRepository.GetProductsSearch(request);

            return Success(result);
        }
    }
}
