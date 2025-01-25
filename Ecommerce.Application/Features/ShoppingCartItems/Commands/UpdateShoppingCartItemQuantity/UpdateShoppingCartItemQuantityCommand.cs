using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.UpdateShoppingCartItemQuantity
{
    public class UpdateShoppingCartItemQuantityCommand : IRequest<Response<bool>>
    {
        public int ProductItemId { get; set; }
        public int NewQuantity { get; set; }
    }
}
