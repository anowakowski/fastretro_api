using System.Collections.Generic;

namespace Fastretro.API.Data.Domain
{
    public class RetroBoardCard : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
        public string Text { get; set; }
        public string UserFirebaseDocId { get; set; }
        public bool IsMerged { get; set; }
        public bool IsHidenMergedChild { get; set; }
        public bool IsShowMergedParent { get; set; }
        public RetroBoardCardMergedGroup RetroBoardCardMergedGroup { get; set; }
        public ICollection<MergedRetroBoardCard> MergetRetroBoardCards { get; set; }
    }
}
