using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Application.Features.Categories;
using Ecommerce.Application.Features.Categories.Queries.CategoryHierarchyQuery;
using Ecommerce.Doman.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IList<Category>> GetAllMainCategories()
        {
            var result = _context.Categories.AsNoTracking().Where(c => c.ParentCategoryId == null);

            return await result.ToListAsync();
        }

        public async Task<GetCategoryHierarchyQueryResponse?> GetCategoryHierarchy(int categoryId)
        {
            var Category = await _context.Categories
                .AsNoTracking()
                .Include(c => c.ChildCategories)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (Category == null)
                return null;

            var ParentHierarchy = await _context.CategoryOverviewResults
                .FromSqlRaw("exec dbo.[SP_GetRecusiveCategoryParents] @CategoryId", new SqlParameter("@CategoryId", categoryId)).ToListAsync();

            var Children = Category.ChildCategories
                .Select(child => new CategoryOverviewDTO
                {
                    Id = child.Id,
                    Name = child.Name
                }).ToList();

            return new GetCategoryHierarchyQueryResponse
            {
                Id = Category.Id,
                Name = Category.Name,
                ParentHierarchy = ParentHierarchy,
                Children = Children
            };
        }


    }
}
