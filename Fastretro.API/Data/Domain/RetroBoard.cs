using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class RetroBoard : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string RetroBoardName { get; set; }
        public string SprintNumber { get; set; }
    }
}
