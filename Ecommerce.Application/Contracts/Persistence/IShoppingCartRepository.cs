using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Application.Features.ShoppingCarts;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        Task<ShoppingCart?> GetByCustomerIdAsync(int CustomerId);

        Task<ShoppingCartDTO?> GetCartItemsAsync(int CustomerId);

        Task<ShoppingCart?> GetIncludingItems(int CustomerId);
        Task<bool> ClearCartAsync(int CustomerId);
    }
}
