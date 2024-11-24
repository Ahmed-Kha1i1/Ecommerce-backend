using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext context) : base(context)
        {
        }
    }
}
