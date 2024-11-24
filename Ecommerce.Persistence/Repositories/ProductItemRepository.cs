using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class ProductItemRepository : GenericRepository<ProductItem>, IProductItemRepository
    {
        public ProductItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
