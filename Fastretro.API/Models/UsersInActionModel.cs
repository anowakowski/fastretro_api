using System.Collections.Generic;

namespace Fastretro.API.Models
{
    public class UsersInActionModel
    {
        public IEnumerable<string> UserFirebaseDocIds { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
        public string RetroBoardActionCardFirebaseDocId { get; set; }
    }
}