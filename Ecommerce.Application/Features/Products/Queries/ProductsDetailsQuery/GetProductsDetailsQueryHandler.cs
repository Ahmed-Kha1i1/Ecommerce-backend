using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.ProductsDetailsQuery
{
    public class GetProductsDetailsQueryHandler(IProductRepository productRepository) : ResponseHandler, IRequestHandler<GetProductsDetailsQuery, Response<IList<ProductSummaryDTO>>>
    {
        public async Task<Response<IList<ProductSummaryDTO>>> Handle(GetProductsDetailsQuery request, CancellationToken cancellationToken)
        {
            var itemsDetails = await productRepository.GetAllItemsDetailsAsync(request.Ids);
            return Success(itemsDetails);
        }
    }
}
