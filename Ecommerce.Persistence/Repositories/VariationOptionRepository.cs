using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class VariationOptionRepository : GenericRepository<VariationOption>, IVariationOptionRepository
    {
        public VariationOptionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
