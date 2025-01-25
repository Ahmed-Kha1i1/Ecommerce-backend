using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> Get(string Email, string PasswordHash);
        Task AddCustomerToExistsUser(int UserId);
    }
}
