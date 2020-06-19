using System.Collections.Generic;

namespace Fastretro.API.Models
{
    public class GetUsersInActionModel
    {
        public string UserFirebaseDocId { get; set; }
        public string RetroBoardActionCardFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
    }
}