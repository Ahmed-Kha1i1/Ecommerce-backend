using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Application.Features.OrderLines;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IOrderLineRepository : IGenericRepository<OrderLine>
    {
        Task<List<OrderLineDTO>> GetOrderLinesPerOrder(int orderId);
        Task<OrderLine?> GetByIdIncludeItemAsync(int id);
        Task<PaginatedResult<OrderLineDetailsDTO>> GetPaginatedOrderLines(int CustomerId, int PageNumber, int PageSize, bool isCanceled);
        Task DeleteRangePerOrderAsync(int OrderId);
        Task<decimal> GetOrderPriceAsync(int orderId);
    }
}
