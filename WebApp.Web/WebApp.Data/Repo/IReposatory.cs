using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Data.Repo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> FindByIdAsync(int id);


        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);


        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
