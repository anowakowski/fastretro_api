using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class Entity : IEntity
    {
        public int Id { get; private set; }
    }
}
