﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Fastretro.API.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected DbContext DbContext;
        protected DbSet<TEntity> DbSet;

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbContext.AddAsync(entity);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        public async Task<int> GetMax(Expression<Func<TEntity, int>> predicate)
        {
            return await DbSet.MaxAsync(predicate);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await DbSet.SingleAsync(x => x.Id == id);
        }

        public void Delete(TEntity entity)
        {
            DbContext.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbContext.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbContext.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbContext.UpdateRange(entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TType>> SelectAsync<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await DbSet.Where(where).Select(select).ToListAsync();
        }         

        public async Task<TEntity> FirstOrDefaultWithIncludedEntityAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> includeEntity)
        {
            var query = DbSet.Include(includeEntity);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAsyncWithIncludedEntityAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> includeEntity)
        {
            var query = DbSet.Include(includeEntity);

            return await query.Where(predicate).ToListAsync();
        }
        public async Task<TEntity> FirstOrDefaulAsyncWithIncludedEntities(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            var query = DbSet.IncludeMultiple(includeEntities);

            return await query.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<TEntity>> FindAsyncWithIncludedEntities(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeEntities)
        {
            var query = DbSet.IncludeMultiple(includeEntities);

            return await query.Where(predicate).ToListAsync();
        }
    }
}
