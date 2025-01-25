
using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Ecommerce.Persistence.Repositories.Base
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly AppDbContext _dbContext;
        private DbSet<Entity> _entities { get; set; }
        public GenericRepository(AppDbContext context)
        {
            _dbContext = context;
            _entities = _dbContext.Set<Entity>();
        }
        public async Task<Entity?> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<List<Entity>> GetAllAsNoTracking()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public IQueryable<Entity> GetAllAsTracking()
        {
            return _entities.AsQueryable();
        }


        public async Task AddRangeAsync(ICollection<Entity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public async Task AddAsync(Entity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task UpdateRangeAsync<TProperty>(Func<Entity, TProperty> propertyExpression, Func<Entity, TProperty> valueExpression)
        {
            await _entities.ExecuteUpdateAsync(x => x.SetProperty(propertyExpression, valueExpression));
        }

        public async Task UpdateRangeAsync(
            Expression<Func<Entity, bool>> predicate, Expression<Func<SetPropertyCalls<Entity>, SetPropertyCalls<Entity>>> setPropertyCalls)
        {
            var query = _entities.Where(predicate);
            await query.ExecuteUpdateAsync(setPropertyCalls);
        }
        public async Task DeleteRangeAsync(Expression<Func<Entity, bool>> predicate)
        {
            await _entities.Where(predicate).ExecuteDeleteAsync();
        }

        public void Delete(Entity entity)
        {
            _entities.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
