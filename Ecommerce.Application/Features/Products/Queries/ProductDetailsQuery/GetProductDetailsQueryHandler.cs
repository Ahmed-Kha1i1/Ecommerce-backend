using AutoMapper;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.ProductDetailsQuery
{
    public class GetProductDetailsQueryHandler(IProductRepository productRepository, IMapper mapper) : ResponseHandler, IRequestHandler<GetProductDetailsQuery, Response<GetProductDetailsQueryReponse>>
    {
        public async Task<Response<GetProductDetailsQueryReponse>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var productDetails = await productRepository.GetProductDetails(request.Id);

            if (productDetails == null)
            {
                return BadRequest<GetProductDetailsQueryReponse>($"Product with ID {request.Id} not found.");
            }

            var productResponse = mapper.Map<GetProductDetailsQueryReponse>(productDetails);

            return Success(productResponse);
        }
    }
}
