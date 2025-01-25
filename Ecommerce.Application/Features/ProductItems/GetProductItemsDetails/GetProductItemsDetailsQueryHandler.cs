using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.ProductItems.GetProductItemsDetails
{
    public class GetProductItemsDetailsQueryHandler : ResponseHandler, IRequestHandler<GetProductItemsDetailsQuery, Response<ICollection<ProductItemDetailsDTO>>>
    {
        private readonly IProductItemRepository _productItemRepository;

        public GetProductItemsDetailsQueryHandler(IProductItemRepository productItemRepository)
        {
            _productItemRepository = productItemRepository;
        }

        public async Task<Response<ICollection<ProductItemDetailsDTO>>> Handle(GetProductItemsDetailsQuery request, CancellationToken cancellationToken)
        {
            var itemsDetails = await _productItemRepository.GetAllItemsDetailsAsync(request.ProductItemIds);
            return Success(itemsDetails);
        }
    }
}
