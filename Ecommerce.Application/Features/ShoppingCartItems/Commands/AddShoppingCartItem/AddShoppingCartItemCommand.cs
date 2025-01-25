using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.AddShoppingCartItem
{
    public class AddShoppingCartItemCommand : IRequest<Response<int?>>
    {
        public int ProductItemId { get; set; }
        public int Quantity { get; set; }

    }
}
