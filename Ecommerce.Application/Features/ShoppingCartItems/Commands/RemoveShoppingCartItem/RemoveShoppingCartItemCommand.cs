using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.RemoveShoppingCartItem
{
    public class RemoveShoppingCartItemCommand : IRequest<Response<bool>>
    {
        public int ProductItemId { get; set; }
    }
}
