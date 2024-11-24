using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class ProductVariationRepository : GenericRepository<ProductVariation>, IProductVariationRepository
    {
        public ProductVariationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
