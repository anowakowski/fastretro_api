using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class RetroBoardAdditionalInfo : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }
        public int RetroBoardIndexCount { get; set; }
        public int RetroBoardActionCount { get; set; }
    }
}
