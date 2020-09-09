using System.Collections.Generic;

namespace Fastretro.API.Data.Domain
{
    public class RetroBoardActionCardModel
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
        public string RetroBoardActionCardFirebaseDocId { get; set; }
        public string Text { get; set; }
    }
}
