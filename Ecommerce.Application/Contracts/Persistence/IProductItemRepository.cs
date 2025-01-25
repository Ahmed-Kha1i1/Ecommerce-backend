using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Application.Features.ProductItems;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface IProductItemRepository : IGenericRepository<ProductItem>
    {
        Task<ICollection<ProductItemDetailsDTO>> GetAllItemsDetailsAsync(ICollection<int> ids);
    }
}
