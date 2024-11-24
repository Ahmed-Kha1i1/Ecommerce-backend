using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class VariationRepository : GenericRepository<Variation>, IVariationRepository
    {
        public VariationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
