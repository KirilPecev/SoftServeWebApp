using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Domain.Models.Abstract;

namespace WebApp.Data.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected DbSet<TEntity> dbSet;

        public Repository(WebAppDbContext dbContext)
        {
            this.dbSet = dbContext.Set<TEntity>();
        }

        public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity) => await dbSet.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await dbSet.AddRangeAsync(entities);

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) => await dbSet.AnyAsync(predicate);

        public async Task<TEntity> FindByIdAsync(int id) => id != 0 ? await dbSet.FindAsync(id) : null;

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await dbSet.FirstOrDefaultAsync(predicate);

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
