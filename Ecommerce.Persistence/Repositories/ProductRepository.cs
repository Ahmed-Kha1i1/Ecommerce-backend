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
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        public ProductRepository(AppDbContext context, IDbContextFactory<AppDbContext> dbContextFactory) : base(context)
        {
            _context = context;
            this.dbContextFactory = dbContextFactory;
        }

        private ProductSummaryDTO Cast(DbDataReader reader)
        {
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
                Rate = Rate,
            };
        }

        public async Task<PaginatedProductSearchResult<ProductSummaryDTO>> GetProductsSearch(GetProductSearchQuery Query)
        {
            var productsTask = GetFilteredProducts(Query);
            var metadataTask = GetPagedProductsMetadata(Query);

            await Task.WhenAll(productsTask, metadataTask);

            var products = await productsTask;
            var (totalCount, minPrice, maxPrice) = await metadataTask;

            var result = new PaginatedProductSearchResult<ProductSummaryDTO>(products, Query.PageNumber, Query.PageSize, totalCount, minPrice ?? 0, maxPrice ?? 0);

            return result;
        }


        private async Task<List<ProductSummaryDTO>> GetFilteredProducts(GetProductSearchQuery Query)
        {
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

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[SP_FilteringProducts]";
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
                        while (await reader.ReadAsync())
                        {
                            data.Add(Cast(reader));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log it, rethrow it, etc.)
                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }

            return data;
        }

        private async Task<(int totalCount, decimal? minPrice, decimal? maxPrice)> GetPagedProductsMetadata(GetProductSearchQuery Query)
        {
            var queryParameters = new[]
           {
                new SqlParameter("@SearchQuery", Query.SearchQuery ?? (object)DBNull.Value),
                new SqlParameter("@BrandId", Query.BrandId ?? (object)DBNull.Value),
                new SqlParameter("@CategoryId", Query.CategoryId ?? (object)DBNull.Value),
                new SqlParameter("@Condition", Query.Condition ?? (object)DBNull.Value),
                new SqlParameter("@Stars", Query.Stars ?? (object)DBNull.Value),
                new SqlParameter("@maxPrice", Query.MaxPrice ?? (object)DBNull.Value),
                new SqlParameter("@minPrice", Query.MinPrice ?? (object)DBNull.Value),
            };

            int totalCount = 0;
            decimal? minPrice = null;
            decimal? maxPrice = null;

            using (AppDbContext context = await dbContextFactory.CreateDbContextAsync())
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[SP_GetPagedProductsMetadata]";
                command.CommandType = CommandType.StoredProcedure;
                foreach (var param in queryParameters)
                {
                    command.Parameters.Add(param);
                }

                try
                {
                    context.Database.OpenConnection();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {

                            totalCount = reader.GetInt32(reader.GetOrdinal("totalCount"));
                            minPrice = reader.IsDBNull(reader.GetOrdinal("minPrice"))
                    ? null : reader.GetDecimal(reader.GetOrdinal("minPrice"));
                            maxPrice = reader.IsDBNull(reader.GetOrdinal("maxPrice"))
                    ? null : reader.GetDecimal(reader.GetOrdinal("maxPrice"));
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    context.Database.CloseConnection();
                }


                return (totalCount, minPrice, maxPrice);
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
