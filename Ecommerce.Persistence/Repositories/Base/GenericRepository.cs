
using Ecommerce.Application.Contracts.Persistence.Base;
using Ecommerce.Doman.Entities.Base;
using Ecommerce.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ecommerce.Persistence.Repositories.Base
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseEntity
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

        public IQueryable<Entity> GetAllAsNoTracking()
        {
            return _entities.AsNoTracking().AsQueryable();
        }

        public IQueryable<Entity> GetAllAsTracking()
        {
            return _entities.AsQueryable();
        }


        public async Task AddRangeAsync(ICollection<Entity> entities)
        {
            await _entities.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(Entity entity)
        {
            await _entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync<TProperty>(Func<Entity, TProperty> propertyExpression, Func<Entity, TProperty> valueExpression)
        {
            await _entities.ExecuteUpdateAsync(x => x.SetProperty(propertyExpression, valueExpression));
        }

        public async Task UpdateAsync(Entity entity)
        {
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(Expression<Func<Entity, bool>> predicate)
        {
            await _entities.Where(predicate).ExecuteDeleteAsync();
        }

        public async Task DeleteAsync(Entity entity)
        {
            _entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
