using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.RemoveShoppingCartItem
{
    public class RemoveShoppingCartItemCommandHandler : ResponseHandler, IRequestHandler<RemoveShoppingCartItemCommand, Response<bool>>
    {
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RemoveShoppingCartItemCommandHandler(IShoppingCartItemRepository shoppingCartItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(RemoveShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            // Retrieve the ShoppingCartItem
            var shoppingCartItem = await _shoppingCartItemRepository.GetAsync(userId, request.ProductItemId);
            if (shoppingCartItem == null)
            {
                return NotFound<bool>($"ShoppingCartItem not found.");
            }

            // Remove the ShoppingCartItem
            _shoppingCartItemRepository.Delete(shoppingCartItem);
            await _shoppingCartItemRepository.SaveChangesAsync();
            return Success<bool>(true, "Shopping cart item removed successfully.");
        }
    }
}
