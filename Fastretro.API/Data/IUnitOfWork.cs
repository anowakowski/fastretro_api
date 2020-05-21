using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
