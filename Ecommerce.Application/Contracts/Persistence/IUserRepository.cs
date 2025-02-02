using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        Task<bool> IsEmailExists(string email);
        Task<User?> FindByEmailIncludeDeletedAsync(string email);
    }
}
