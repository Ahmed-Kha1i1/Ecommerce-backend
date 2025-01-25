using AutoMapper;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Application.Features.ProductItems;
using Ecommerce.Application.Features.ShoppingCartItems;
using Ecommerce.Application.Features.ShoppingCarts;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories
{
    public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ShoppingCartRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> ClearCartAsync(int CustomerId)
        {
            var result = await _context.Database.ExecuteSqlRawAsync(
       "EXEC SP_ClearCartByCustomerId @CustomerId = {0}", CustomerId);

            // Return true if any rows were affected
            return result > 0;
        }

        public async Task<ShoppingCart?> GetByCustomerIdAsync(int CustomerId)
        {
            return await _context.ShoppingCarts.AsNoTracking().FirstOrDefaultAsync(sh => sh.CustomerId == CustomerId);
        }

        public async Task<ShoppingCartDTO?> GetCartItemsAsync(int customerId)
        {
            // Retrieve the shopping cart for the customer
            var cart = await _context.ShoppingCarts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (cart == null)
                return null;

            var customerParameter = new SqlParameter("@CustomerId", customerId);

            var cartResults = await _context.ShoppingCartResults
                .FromSqlRaw("EXEC dbo.SP_GetShoppingCartDetails @CustomerId", customerParameter)
                .ToListAsync();

            // Group the results by ShoppingCartItemId to assemble the DTOs
            var groupedResults = cartResults
                .GroupBy(r => new
                {
                    r.ShoppingCartItemId,
                    r.Quantity,
                    r.CreatedDate,
                    r.ProductItemId,
                    r.ProductId,
                    r.Title,
                    r.Price,
                    r.DiscountRate,
                    r.StockQuantity,
                    r.ImageName,
                    r.IsDefault
                })
                .ToList();

            var items = new List<ShoppingCartItemDTO>();

            foreach (var group in groupedResults)
            {
                var first = group.First();

                var item = _mapper.Map<ShoppingCartItemDTO>(first);
                var productItemDetails = _mapper.Map<ProductItemDetailsDTO>(first);

                productItemDetails.Variations = group
                   .Where(r => !string.IsNullOrEmpty(r.VariationName) && !string.IsNullOrEmpty(r.VariationValue))
                   .Select(r => _mapper.Map<VariationDTO>(r))
                   .ToList();
                item.ProductItemDetails = productItemDetails;
                items.Add(item);
            }

            ShoppingCartDTO? cartDTO = new ShoppingCartDTO
            {
                Id = cart.Id,
                CustomerId = cart.CustomerId,
                Items = items
            };
            return cartDTO;
        }


        public async Task<ShoppingCart?> GetIncludingItems(int CustomerId)
        {
            return await _context.ShoppingCarts.Include(sh => sh.Items)
                .FirstOrDefaultAsync(sh => sh.CustomerId == CustomerId);
        }
    }
}
