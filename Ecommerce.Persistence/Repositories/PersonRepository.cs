using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {
        }
    }
}
