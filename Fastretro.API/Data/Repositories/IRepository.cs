﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetById(int id);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
