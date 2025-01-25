using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetActiveRefreshToken(int CustomerId);
        Task<RefreshToken?> Get(string token);
        Task<RefreshToken?> GetWithCustomerAsync(string token);
        RefreshToken GenerateRefreshToken(int CustomerId);
    }
}
