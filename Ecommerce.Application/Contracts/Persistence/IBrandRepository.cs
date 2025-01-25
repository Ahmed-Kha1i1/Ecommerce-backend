using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Application.Features.Brands.Queries.BrandsSearchQuery;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        public Task<IReadOnlyCollection<GetBrandsSearchQueryResponse>> GetProductsSearchBrands(GetBrandsSearchQuery Query);
    }
}
