using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.UpdateShoppingCartItemQuantity
{
    public class UpdateShoppingCartItemQuantityCommandHandler : ResponseHandler, IRequestHandler<UpdateShoppingCartItemQuantityCommand, Response<bool>>
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateShoppingCartItemQuantityCommandHandler(IShoppingCartItemRepository shoppingCartItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(UpdateShoppingCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            var shoppingCartItem = await _shoppingCartItemRepository.GetWithProductItemAsync(userId, request.ProductItemId);
            if (shoppingCartItem == null)
            {
                return NotFound<bool>($"ShoppingCartItem not found.");
            }

            int availableStock = shoppingCartItem.ProductItem.StockQuantity;
            if (request.NewQuantity > availableStock)
            {
                return BadRequest<bool>($"Requested quantity exceeds available stock. Available stock: {availableStock}");
            }

            // Update quantity
            shoppingCartItem.Quantity = request.NewQuantity;

            await _shoppingCartItemRepository.SaveChangesAsync();
            return Success<bool>(true, "Shopping cart item quantity updated successfully.");
        }
    }
}
