﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fastretro.API.Data.Domain;
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

        public async Task<TEntity> GetById(int id)
        {
            return await DbSet.SingleAsync(x => x.Id == id);
        }

        public void Delete(TEntity entity)
        {
            DbContext.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            DbContext.Update(entity);
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

        public async Task<TEntity> FirstOrDefaultWithIncludedEntityAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> includeEntity)
        {
            var query = DbSet.Include(includeEntity);

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}