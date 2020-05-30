using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class CurrentUserVote : Entity
    {
        public string UserId { get; set; }
        public string RetroBoardId { get; set; }
        public string RetroBoardCardId { get; set; }
    }
}
