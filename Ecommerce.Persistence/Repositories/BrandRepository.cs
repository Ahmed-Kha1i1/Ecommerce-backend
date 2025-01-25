using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Application.Features.Brands.Queries.BrandsSearchQuery;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce.Persistence.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly AppDbContext _context;
        public BrandRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<GetBrandsSearchQueryResponse>> GetProductsSearchBrands(GetBrandsSearchQuery Query)
        {
            // Define the input parameters
            var queryParameters = new[]
            {
                new SqlParameter("@SearchQuery", Query.SearchQuery ?? (object)DBNull.Value),
                new SqlParameter("@CategoryId", Query.CategoryId ?? (object)DBNull.Value),
                new SqlParameter("@Condition", Query.Condition ?? (object)DBNull.Value),
                new SqlParameter("@Stars", Query.Stars ?? (object)DBNull.Value),
                new SqlParameter("@maxPrice", Query.MaxPrice ?? (object)DBNull.Value),
                new SqlParameter("@minPrice", Query.MinPrice ?? (object)DBNull.Value),
            };

            var data = new List<GetBrandsSearchQueryResponse>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "[dbo].[SP_ProductsSearchBrands]";
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
                            data.Add(new GetBrandsSearchQueryResponse()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            });
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

                return data;
            }
        }
    }
}