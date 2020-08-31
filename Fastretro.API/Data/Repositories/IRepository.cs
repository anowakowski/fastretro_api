using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fastretro.API.Data.Domain;

namespace Fastretro.API.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TType>> SelectAsync<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class;

        Task AddAsync(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> GetMax(Expression<Func<TEntity, int>> predicate);

        Task<TEntity> GetById(int id);

        void Delete(TEntity entity);
        
        void DeleteRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultWithIncludedEntityAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> includeEntity);
        Task<IEnumerable<TEntity>> FindAsyncWithIncludedEntityAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> includeEntity);
        Task<TEntity> FirstOrDefaulAsyncWithIncludedEntities(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeEntities);
        Task<IEnumerable<TEntity>> FindAsyncWithIncludedEntities(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeEntities);
    }
}
