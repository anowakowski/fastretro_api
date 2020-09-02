using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class RetroBoardCardAfterUnmergeGetModel
    {
        public int RetroBoardCardApiId { get; set; }
        public string RetroBoardFirebaseDocId { get; set; }
        public string UserFirebaseDocId { get; set; }
        public bool IsMerged { get; set; }
    }
}
