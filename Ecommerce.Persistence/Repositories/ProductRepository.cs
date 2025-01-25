using AutoMapper;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Application.Features.Products;
using Ecommerce.Application.Features.Products.Queries.ProductSearchQuery;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Ecommerce.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        private ProductSummaryDTO Cast(DbDataReader reader)
        {
            decimal? PriceAfterDiscount = reader.IsDBNull(reader.GetOrdinal("PriceAfterDiscount"))
                ? null : reader.GetDecimal(reader.GetOrdinal("PriceAfterDiscount"));

            byte? Rate = reader.IsDBNull(reader.GetOrdinal("Rate"))
                ? null : reader.GetByte(reader.GetOrdinal("Rate"));


            return new ProductSummaryDTO()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Stars = reader.GetDecimal(reader.GetOrdinal("Stars")),
                Reviews = reader.GetInt32(reader.GetOrdinal("Reviews")),
                ImageURL = reader.GetString(reader.GetOrdinal("ImageURL")),
                ProductItemId = reader.GetInt32(reader.GetOrdinal("ProductItemId")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                StockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity")),
                PriceAfterDiscount = PriceAfterDiscount,
                Rate = Rate,
            };
        }

        public async Task<PaginatedProductSearchResult<ProductSummaryDTO>> GetProductsSearch(GetProductSearchQuery Query)
        {
            // Define the input parameters
            var queryParameters = new[]
            {
                new SqlParameter("@SearchQuery", Query.SearchQuery ?? (object)DBNull.Value),
                new SqlParameter("@BrandId", Query.BrandId ?? (object)DBNull.Value),
                new SqlParameter("@CategoryId", Query.CategoryId ?? (object)DBNull.Value),
                new SqlParameter("@Condition", Query.Condition ?? (object)DBNull.Value),
                new SqlParameter("@Stars", Query.Stars ?? (object)DBNull.Value),
                new SqlParameter("@maxPrice", Query.MaxPrice ?? (object)DBNull.Value),
                new SqlParameter("@minPrice", Query.MinPrice ?? (object)DBNull.Value),
                new SqlParameter("@OrderBy", Query.OrderBy),
                new SqlParameter("@OrderDirection", Query.OrderDirection),
                new SqlParameter("@PageNumber", Query.PageNumber),
                new SqlParameter("@PageSize", Query.PageSize)
            };

            var data = new List<ProductSummaryDTO>();
            int totalCount = 0;
            decimal minPrice = 0;
            decimal maxPrice = 0;

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[SP_ProductsSearch2]";
                command.CommandType = CommandType.StoredProcedure;
                foreach (var param in queryParameters)
                {
                    command.Parameters.Add(param);
                }

                try
                {
                    _context.Database.OpenConnection();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            totalCount = reader.GetInt32(reader.GetOrdinal("totalCount"));
                            minPrice = reader.GetDecimal(reader.GetOrdinal("minPrice"));
                            maxPrice = reader.GetDecimal(reader.GetOrdinal("maxPrice"));

                            data.Add(Cast(reader));
                        }

                        while (await reader.ReadAsync())
                        {
                            data.Add(Cast(reader));
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    _context.Database.CloseConnection();
                }

                var result = new PaginatedProductSearchResult<ProductSummaryDTO>(data, Query.PageNumber, Query.PageSize, totalCount, minPrice, maxPrice);

                return result;
            }
        }

        public async Task<Product?> GetProductDetails(int productId)
        {
            var productDetails = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.CountryOfOrigin)
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.Discounts.Where(d => !d.IsDisabled && d.EndDate > DateTime.UtcNow))
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.Images)
                .Include(p => p.ProductItems)
                    .ThenInclude(pi => pi.ProductVariations)
                        .ThenInclude(pv => pv.VariationOption)
                            .ThenInclude(vo => vo.Variation)
                .FirstOrDefaultAsync(p => p.Id == productId);


            return productDetails;
        }

        public async Task<IList<ProductSummaryDTO>> GetAllItemsDetailsAsync(ICollection<int> ids)
        {
            DataTable tvpIds = new DataTable();
            tvpIds.Columns.Add("Id", typeof(int));

            foreach (int id in ids)
            {
                tvpIds.Rows.Add(id);
            }

            var idsParameter = new SqlParameter("@Ids", SqlDbType.Structured)
            {
                TypeName = "dbo.IdList", // Matches the TVP in SQL Server
                Value = tvpIds
            };

            // Execute the stored procedure
            var productResults = await _context.ProductDetailsResults
                .FromSqlRaw("EXEC dbo.SP_ProductsByIds @Ids", idsParameter)
                .ToListAsync();

            return productResults;
        }
    }
}
