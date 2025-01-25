using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.ProductSearchQuery
{
    public class GetProductSearchQueryHandler(IProductRepository productRepository) : ResponseHandler, IRequestHandler<GetProductSearchQuery, Response<PaginatedProductSearchResult<ProductSummaryDTO>>>
    {
        public async Task<Response<PaginatedProductSearchResult<ProductSummaryDTO>>> Handle(GetProductSearchQuery request, CancellationToken cancellationToken)
        {
            var data = await productRepository.GetAllItemsDetailsAsync(new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 47, 45, 58 });
            //var result = await productRepository.GetProductsSearch(request);

            //return Success(result);
            return Success(new PaginatedProductSearchResult<ProductSummaryDTO>(data, 1, 20, 20, 10, 600));
        }
    }
}
