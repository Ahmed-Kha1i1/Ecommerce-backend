using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories
{
    public class ShoppingCartItemRepository : GenericRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        private readonly AppDbContext _appDbContext;
        public ShoppingCartItemRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<ShoppingCartItem?> GetWithProductItemAsync(int customerId, int ProductItemId)
        {
            return await _appDbContext.ShoppingCartItems.Include(x => x.ProductItem)
                .FirstOrDefaultAsync(x => x.ShoppingCart.CustomerId == customerId && x.ProductItemId == ProductItemId);
        }

        public async Task<ShoppingCartItem?> GetAsync(int customerId, int ProductItemId)
        {
            return await _appDbContext.ShoppingCartItems
                .FirstOrDefaultAsync(x => x.ShoppingCart.CustomerId == customerId && x.ProductItemId == ProductItemId);
        }
    }
}
