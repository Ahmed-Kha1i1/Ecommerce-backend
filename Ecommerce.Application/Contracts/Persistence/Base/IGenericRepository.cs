using Ecommerce.Doman.Entities.Base;
using System.Linq.Expressions;

namespace Ecommerce.Application.Contracts.Persistence.Base
{
    public interface IGenericRepository<Entity> where Entity : BaseEntity
    {
        Task<Entity?> GetByIdAsync(int id);
        IQueryable<Entity> GetAllAsNoTracking();
        IQueryable<Entity> GetAllAsTracking();
        Task AddRangeAsync(ICollection<Entity> entities);
        Task AddAsync(Entity entity);
        Task UpdateRangeAsync<TProperty>(Func<Entity, TProperty> propertyExpression, Func<Entity, TProperty> valueExpression);
        Task UpdateAsync(Entity entity);
        Task DeleteRangeAsync(Expression<Func<Entity, bool>> predicate);
        Task DeleteAsync(Entity entity);
        Task SaveChangesAsync();
    }
}
