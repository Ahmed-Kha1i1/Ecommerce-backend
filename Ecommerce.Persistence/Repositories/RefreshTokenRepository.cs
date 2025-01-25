using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Ecommerce.Persistence.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        private readonly int _refreshTokenLifetime;
        public RefreshTokenRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _refreshTokenLifetime = configuration.GetValue<int>("RefreshTokenLifetime");
        }

        public async Task<RefreshToken?> GetActiveRefreshToken(int customerId)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.RevokedOn == null && r.ExpiresOn > DateTime.UtcNow && r.CustomerId == customerId);
        }

        public RefreshToken GenerateRefreshToken(int CustomerId)
        {
            var randomNumber = new byte[32];
            string Token;
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                Token = Convert.ToBase64String(randomNumber);
            }

            return new RefreshToken
            {
                Token = Token,
                CreatedOn = DateTime.UtcNow,
                ExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenLifetime),
                CustomerId = CustomerId
            };
        }

        public async Task<RefreshToken?> GetWithCustomerAsync(string token)
        {
            return await _context.RefreshTokens.Include(r => r.Customer).FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task<RefreshToken?> Get(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        }
    }
}
