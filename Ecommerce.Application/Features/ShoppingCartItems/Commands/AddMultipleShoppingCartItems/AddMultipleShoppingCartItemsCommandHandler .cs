using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.AddMultipleShoppingCartItems
{
    public class AddMultipleShoppingCartItemsCommandHandler : ResponseHandler, IRequestHandler<AddMultipleShoppingCartItemsCommand, Response<bool>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddMultipleShoppingCartItemsCommandHandler(
            IShoppingCartRepository shoppingCartRepository,
            IProductItemRepository productItemRepository,
            IShoppingCartItemRepository shoppingCartItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productItemRepository = productItemRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(AddMultipleShoppingCartItemsCommand request, CancellationToken cancellationToken)
        {
            int? userId = _httpContextAccessor.GetUserId();
            if (userId is null)
            {
                return Unauthorized<bool>("User is not authenticated.");
            }

            var shoppingCart = await _shoppingCartRepository.GetIncludingItems(userId.Value);
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    CustomerId = userId.Value
                };
                await _shoppingCartRepository.AddAsync(shoppingCart);
                await _shoppingCartRepository.SaveChangesAsync();
            }


            var productItemIds = request.ProductItems.Select(pi => pi.Id).ToList();
            var productItems = await _productItemRepository.GetAllItemsDetailsAsync(productItemIds);

            var itemsToUpdate = new List<ShoppingCartItem>();
            var itemsToAdd = new List<ShoppingCartItem>();

            foreach (var item in request.ProductItems)
            {
                var productItem = productItems.FirstOrDefault(pi => pi.Id == item.Id);
                if (productItem == null)
                    continue;

                var existingItem = shoppingCart.Items.FirstOrDefault(ci => ci.ProductItemId == item.Id);

                if (existingItem != null)
                {
                    int desiredTotalQuantity = existingItem.Quantity + item.Quantity;
                    int finalQuantity = Math.Min(desiredTotalQuantity, productItem.StockQuantity);
                    if (finalQuantity != existingItem.Quantity)
                    {
                        existingItem.Quantity = finalQuantity;

                        itemsToUpdate.Add(existingItem);
                    }

                }
                else
                {
                    if (item.Quantity > productItem.StockQuantity)
                        continue;

                    var newCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = shoppingCart.Id,
                        ProductItemId = item.Id,
                        Quantity = item.Quantity,
                        CreatedDate = item.CreatedDate,
                    };
                    itemsToAdd.Add(newCartItem);
                }
            }

            // Add new items
            await _shoppingCartItemRepository.AddRangeAsync(itemsToAdd);
            await _shoppingCartItemRepository.SaveChangesAsync();
            return Success(true, "Shopping cart items added or updated successfully.");
        }
    }
}


