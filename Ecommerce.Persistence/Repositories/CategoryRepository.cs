using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
