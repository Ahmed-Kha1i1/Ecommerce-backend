using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Application.Features.Products;
using Ecommerce.Application.Features.Products.Queries.ProductSearchQuery;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PaginatedProductSearchResult<ProductSummaryDTO>> GetProductsSearch(GetProductSearchQuery Query);
        Task<IList<ProductSummaryDTO>> GetAllItemsDetailsAsync(ICollection<int> ids);
        Task<Product?> GetProductDetails(int productId);
    }
}
