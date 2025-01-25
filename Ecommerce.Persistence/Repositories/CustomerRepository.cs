using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task AddCustomerToExistsUser(int UserId)
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC AddCustomerToExistsUser @Id = {0}", UserId);
        }
        public async Task<Customer?> Get(string Email, string PasswordHash)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Email == Email && c.PasswordHash == PasswordHash);
        }
    }
}
