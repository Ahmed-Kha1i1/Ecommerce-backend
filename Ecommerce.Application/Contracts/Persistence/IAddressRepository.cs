using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId);
        Task<Address?> GetDefaultAddressByCustomerIdAsync(int customerId);
        Task<int?> GetDefaultAddressIdByCustomerIdAsync(int customerId);
        Task<Address?> GetByIdIncludeCountryAsync(int Id);
        Task<bool> HasDefaultAddress(int customerId);
    }
}
