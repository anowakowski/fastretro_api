using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class GetFreshCurrentUserDataModel
    {
        public string UserId { get; set; }
        public string RetroBoardId { get; set; }
    }
}
