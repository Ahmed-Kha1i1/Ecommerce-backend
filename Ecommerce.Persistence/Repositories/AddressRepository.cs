using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;

namespace Ecommerce.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context)
        {
        }
    }
}
