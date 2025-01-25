using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly AppDbContext _context;
        public AddressRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Address?> GetByIdIncludeCountryAsync(int Id)
        {
            return await _context.Addresses.Include(a => a.Country).FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId)
        {
            return await _context.Addresses.Include(a => a.Country)
                .Where(A => A.CustomerId == customerId).OrderByDescending(a => a.IsDefault).ThenByDescending(a => a.CreatedDate).ToListAsync();
        }

        public async Task<Address?> GetDefaultAddressByCustomerIdAsync(int customerId)
        {
            return await _context.Addresses.Include(a => a.Country)
               .FirstOrDefaultAsync(a => a.CustomerId == customerId && a.IsDefault);
        }

        public async Task<bool> HasDefaultAddress(int customerId)
        {
            return await _context.Addresses
            .AnyAsync(a => a.CustomerId == customerId && a.IsDefault);
        }

        public async Task<int?> GetDefaultAddressIdByCustomerIdAsync(int customerId)
        {
            var defaultAddress = await _context.Addresses
                .Where(address => address.CustomerId == customerId && address.IsDefault)
                .Select(address => (int?)address.Id)
                .FirstOrDefaultAsync();

            return defaultAddress;
        }
    }
}
