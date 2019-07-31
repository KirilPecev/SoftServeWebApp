using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Domain.Models.Abstract;

namespace WebApp.Data.Repo
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly WebAppDbContext dbContext;
        protected DbSet<TEntity> dbSet;

        public Repository(WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }

            TEntity entity = await dbSet.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }

        public async Task<TEntity> FindByIdAsyncAbsolute(int id)
        {
            if (id == 0)
                return null;

            TEntity entity = await dbSet.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity query = await dbSet.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(predicate);
            return query;
        }

        // doesn't take into account if the Entity is Deleted
        public async Task<TEntity> FirstOrDefaultAsyncAbsolute(Expression<Func<TEntity, bool>> predicate)
        {
            TEntity query = await dbSet.FirstOrDefaultAsync(predicate);
            return query;
        }

        // takes all without the deleted
        public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] include)
        {
            return await include.Aggregate(dbSet.Where(x => x.IsDeleted == false).AsQueryable(), (query, path) => query.Include(path)).ToListAsync();
        }

        // doesn't take into account if the Entity is Deleted
        public async Task<IEnumerable<TEntity>> GetAllAsyncAbsolute(params string[] include)
        {
            return await include.Aggregate(dbSet.AsQueryable(), (query, path) => query.Include(path)).ToListAsync();
        }

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
