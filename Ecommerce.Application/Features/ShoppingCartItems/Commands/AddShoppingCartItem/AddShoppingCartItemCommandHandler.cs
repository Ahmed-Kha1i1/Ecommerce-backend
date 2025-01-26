using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.AddShoppingCartItem
{
    public class AddShoppingCartItemCommandHandler : ResponseHandler, IRequestHandler<AddShoppingCartItemCommand, Response<int?>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddShoppingCartItemCommandHandler(
            IShoppingCartRepository shoppingCartRepository,
            IProductItemRepository productItemRepository,
            IShoppingCartItemRepository shoppingCartItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productItemRepository = productItemRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<int?>> Handle(AddShoppingCartItemCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            // Check if ProductItem exists and has sufficient stock
            var productItem = await _productItemRepository.GetByIdAsync(request.ProductItemId);
            if (productItem == null)
            {
                return NotFound<int?>($"ProductItem with ID {request.ProductItemId} not found.");
            }

            // Retrieve or create ShoppingCart for the customer
            var shoppingCart = await _shoppingCartRepository.GetByCustomerIdAsync(userId);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    CustomerId = userId
                };
                await _shoppingCartRepository.AddAsync(shoppingCart);
                await _shoppingCartRepository.SaveChangesAsync();

            }

            // Check if the item already exists in the cart

            var existingItem = await _shoppingCartItemRepository.GetAsync(userId, request.ProductItemId);
            if (existingItem != null)
            {
                if (existingItem.Quantity == productItem.StockQuantity)
                {
                    return BadRequest<int?>(
                        $"You’ve requested more of ProductItem ID {productItem.Id} than the available stock of {productItem.StockQuantity}. Please adjust your quantity accordingly.");
                }

                int desiredTotalQuantity = existingItem.Quantity + request.Quantity;
                int finalQuantity = Math.Min(desiredTotalQuantity, productItem.StockQuantity);


                existingItem.Quantity = finalQuantity;

                await _shoppingCartItemRepository.SaveChangesAsync();
                return Success<int?>(existingItem.Id, "Shopping cart item quantity updated successfully.");
            }
            else
            {
                // Add new item
                if (request.Quantity > productItem.StockQuantity)
                {
                    return BadRequest<int?>(
                        $"Requested quantity exceeds available stock. Available stock for ProductItem ID {request.ProductItemId}: {productItem.StockQuantity}.");
                }

                // Add new item
                var cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = shoppingCart.Id,
                    ProductItemId = request.ProductItemId,
                    Quantity = request.Quantity,
                    CreatedDate = DateTime.UtcNow
                };
                await _shoppingCartItemRepository.AddAsync(cartItem);
                await _shoppingCartItemRepository.SaveChangesAsync();
                return Success<int?>(cartItem.Id, "Shopping cart item added successfully.");
            }
        }
    }
}
