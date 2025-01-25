using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.AddMultipleShoppingCartItems
{
    public class AddMultipleShoppingCartItemsCommand : IRequest<Response<bool>>
    {
        public List<ProductItemDto> ProductItems { get; set; }

        public class ProductItemDto
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public DateTime CreatedDate { get; set; }
        }
    }
}
