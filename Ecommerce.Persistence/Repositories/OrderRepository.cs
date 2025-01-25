using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order?> FindOpenedOrderAsync(int customerId)
        {
            //this function has logical error in enOrderStatus.Placed
            return await _context.Orders.FirstOrDefaultAsync(order => order.CustomerId == customerId && order.Status == enOrderStatus.Placed);
        }

        public async Task<Order?> GetOrderIncludeAddress(int OrderId)
        {
            return await _context.Orders.Include(o => o.OrderAddress).ThenInclude(address => address.Country)
                .FirstOrDefaultAsync(order => order.Id == OrderId);
        }

        public async Task<bool> UpdateOrderPriceAsync(int orderId)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
            "EXEC SP_UpdateOrderPriceAndStatus @OrderId = {0}", orderId);

            return result > 0;
        }
    }
}
