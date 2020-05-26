using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext DbContext;

        public UnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task CompleteAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
