using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class RetroBoardCardMergedContentModel
    {
        public string RetroBoardCardToMergeFromFirebaseDocId { get; set; }
        public string RetroBoardCardToMergeToCurrentFirebaseDocId { get; set; }
    }
}
