using AutoMapper;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Application.Features.ProductItems;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce.Persistence.Repositories
{
    public class ProductItemRepository : GenericRepository<ProductItem>, IProductItemRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductItemRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<ProductItemDetailsDTO>> GetAllItemsDetailsAsync(ICollection<int> ids)
        {
            List<ProductItemDetailsDTO> items = new List<ProductItemDetailsDTO>();
            DataTable tvpIds = new DataTable();
            tvpIds.Columns.Add("Id", typeof(int));

            foreach (int id in ids)
            {
                tvpIds.Rows.Add(id);
            }

            var idsParameter = new SqlParameter("@Ids", SqlDbType.Structured)
            {
                TypeName = "dbo.IdList", // Must match the TVP name in SQL Server
                Value = tvpIds
            };


            var itemsResults = await _context.ProductItemDetailsResults
                .FromSqlRaw("EXEC dbo.SP_GetProductItemsDetails @Ids", idsParameter)
                .ToListAsync();

            var groupedResults = itemsResults
                .GroupBy(r => new
                {
                    r.Id,
                    r.ProductId,
                    r.Title,
                    r.Price,
                    r.DiscountRate,
                    r.StockQuantity,
                    r.ImageName,
                    r.IsDefault
                })
                .ToList();

            foreach (var group in groupedResults)
            {
                var first = group.First();

                var productItemDetails = _mapper.Map<ProductItemDetailsDTO>(first);

                productItemDetails.Variations = group
                   .Where(r => !string.IsNullOrEmpty(r.VariationName) && !string.IsNullOrEmpty(r.VariationValue))
                   .Select(r => _mapper.Map<VariationDTO>(r))
                   .ToList();


                items.Add(productItemDetails);
            }

            return items;
        }

    }
}
