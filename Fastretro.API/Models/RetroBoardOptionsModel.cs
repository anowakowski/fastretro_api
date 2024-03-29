﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class RetroBoardOptionsModel
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public bool ShouldBlurRetroBoardCardText { get; set; }
        public int MaxVouteCount { get; set; }
        public bool ShouldHideVoutCountInRetroBoardCard { get; set; }
    }
}
