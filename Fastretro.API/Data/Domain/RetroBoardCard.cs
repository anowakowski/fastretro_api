﻿using System.Collections.Generic;

namespace Fastretro.API.Data.Domain
{
    public class RetroBoardCard : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
        public string Text { get; set; }
        public bool isMerged { get; set; }
        public ICollection<MergedRetroBoardCard> MergetRetroBoardCards { get; set; }
    }
}
