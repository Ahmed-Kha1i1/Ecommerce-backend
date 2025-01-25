using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Ecommerce.Application.Contracts.Persistence.Base
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity?> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsNoTracking();
        IQueryable<Entity> GetAllAsTracking();
        Task AddRangeAsync(ICollection<Entity> entities);
        Task AddAsync(Entity entity);
        Task UpdateRangeAsync<TProperty>(Func<Entity, TProperty> propertyExpression, Func<Entity, TProperty> valueExpression);
        Task UpdateRangeAsync(Expression<Func<Entity, bool>> predicate, Expression<Func<SetPropertyCalls<Entity>, SetPropertyCalls<Entity>>> setPropertyCalls);
        Task DeleteRangeAsync(Expression<Func<Entity, bool>> predicate);
        void Delete(Entity entity);
        Task SaveChangesAsync();

    }
}
