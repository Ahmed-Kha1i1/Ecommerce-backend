using Ecommerce.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.Extentions
{
    public static class QueryableExtentions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> sourse, int PageNumber, int PageSize) where T : class
        {
            //int count = await sourse.AsNoTracking().CountAsync();
            var result = await sourse.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();

            return new PaginatedResult<T>(result, PageNumber, PageSize, 1425354);
        }
    }
}
