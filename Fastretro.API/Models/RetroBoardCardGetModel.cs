using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class RetroBoardCardGetModel
    {
        public int RetroBoardCardApiId { get; set; }
        public string RetroBoardFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
        public string Text { get; set; }
        public bool IsMerged { get; set; }
        public IList<string> MergedContent { get; set; }
    }
}
