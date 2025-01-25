using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> FindOpenedOrderAsync(int customerId);
        Task<Order?> GetOrderIncludeAddress(int OrderId);
        Task<bool> UpdateOrderPriceAsync(int orderId);
    }
}
