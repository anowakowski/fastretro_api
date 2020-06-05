using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class RetroBoardOptions : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public bool ShouldBlurRetroBoardCardText { get; set; }
        public int maxVouteCount { get; set; }
    }
}
