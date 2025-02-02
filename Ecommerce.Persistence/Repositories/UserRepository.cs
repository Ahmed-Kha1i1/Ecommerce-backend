using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<User?> FindByEmailIncludeDeletedAsync(string email)
        {
            return await _context.Users.IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
