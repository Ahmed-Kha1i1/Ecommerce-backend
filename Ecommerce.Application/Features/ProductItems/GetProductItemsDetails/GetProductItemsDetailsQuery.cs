using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.ProductItems.GetProductItemsDetails
{
    public class GetProductItemsDetailsQuery : IRequest<Response<ICollection<ProductItemDetailsDTO>>>
    {
        public ICollection<int> ProductItemIds { get; set; }

        public GetProductItemsDetailsQuery(ICollection<int> productItemIds)
        {
            ProductItemIds = productItemIds;
        }
    }
}
