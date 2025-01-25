using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Application.Features.Categories.Queries.CategoryHierarchyQuery;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IList<Category>> GetAllMainCategories();
        Task<GetCategoryHierarchyQueryResponse?> GetCategoryHierarchy(int categoryId);
    }
}
