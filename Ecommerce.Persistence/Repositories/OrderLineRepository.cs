using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Application.Features.OrderLines;
using Ecommerce.Application.Features.ProductItems;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce.Persistence.Repositories
{
    public class OrderLineRepository : GenericRepository<OrderLine>, IOrderLineRepository
    {
        private readonly AppDbContext _context;
        public OrderLineRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteRangePerOrderAsync(int OrderId)
        {
            await UpdateRangeAsync(ol => ol.OrderId == OrderId,
                setter => setter.SetProperty(ol => ol.IsDeleted, true).SetProperty(ol => ol.DateDeleted, DateTime.UtcNow));
        }

        public async Task<OrderLine?> GetByIdIncludeItemAsync(int id)
        {
            return await _context.OrderLines.Include(l => l.ProductItem).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<OrderLineDTO>> GetOrderLinesPerOrder(int orderId)
        {
            return await _context.OrderLines
                .IgnoreQueryFilters()
                .Where(O => O.OrderId == orderId).Select(
                line => new OrderLineDTO
                {
                    Id = line.Id,
                    ImageName = line.ProductItem.Images
                    .Where(image => image.IsDefault)
                    .Select(image => image.ImageName)
                    .FirstOrDefault(),
                    Title = line.ProductItem.Product.Name,
                    Quantity = line.Quantity,
                    Price = line.Price,
                    IsCanceled = line.IsDeleted,
                    ProductId = line.ProductItem.ProductId,
                    Variations = line.ProductItem.ProductVariations.Select(v => new VariationDTO
                    {
                        Name = v.VariationOption.Variation.Name,
                        Value = v.VariationOption.Value,

                    }).ToList(),
                }).ToListAsync();
        }

        public async Task<decimal> GetOrderPriceAsync(int orderId)
        {
            return await _context.OrderLines.SumAsync(ol => ol.Price * ol.Quantity);
        }

        public async Task<PaginatedResult<OrderLineDetailsDTO>> GetPaginatedOrderLines(int CustomerId, int PageNumber, int PageSize, bool isCanceled)
        {
            var query = _context.OrderLines.Where(O => O.Order.CustomerId == CustomerId);

            if (isCanceled)
            {
                query = query.IgnoreQueryFilters().Where(o => o.IsDeleted);
            }
            else
            {
                query = query.Where(o => !o.IsDeleted);
            }

            var totalItems = await query.CountAsync();



            var queryParameters = new[]
           {
                new SqlParameter("@CustomerId", CustomerId),
                new SqlParameter("@PageNumber", PageNumber),
                new SqlParameter("@PageSize", PageSize),
                new SqlParameter("@IsCanceled", isCanceled),
            };

            var itemsResults = await _context.OrderLineDetailsResults
                .FromSqlRaw("EXEC dbo.SP_GetPaginatedOrderLines @CustomerId, @PageNumber, @PageSize, @IsCanceled", queryParameters)
                .ToListAsync();

            var orderLines = itemsResults.Select(item => new OrderLineDetailsDTO
            {
                Id = item.Id,
                ImageName = item.ImageName,
                Title = item.Title,
                Quantity = item.Quantity,
                Price = item.Price,
                IsCanceled = item.IsCanceled,
                ProductId = item.ProductId,
                OrderId = item.OrderId,
                Variations = item.Str_Variations?.Split(", ").Select(v =>
                {
                    var parts = v.Split(':');
                    return new VariationDTO
                    {
                        Name = parts[0],
                        Value = parts.Length > 1 ? parts[1] : string.Empty
                    };
                }).ToList() ?? new List<VariationDTO>()
            }).ToList();

            var paginatedResult = new PaginatedResult<OrderLineDetailsDTO>(orderLines, PageNumber, PageSize, totalItems);

            return paginatedResult;
        }

    }
}
