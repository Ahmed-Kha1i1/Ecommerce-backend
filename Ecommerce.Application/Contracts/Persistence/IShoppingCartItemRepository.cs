using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IShoppingCartItemRepository : IGenericRepository<ShoppingCartItem>
    {
        Task<ShoppingCartItem?> GetWithProductItemAsync(int customerId, int ProductItemId);
        Task<ShoppingCartItem?> GetAsync(int customerId, int ProductItemId);
    }
}
