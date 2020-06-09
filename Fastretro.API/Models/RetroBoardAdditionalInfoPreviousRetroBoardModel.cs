using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class RetroBoardAdditionalInfoPreviousRetroBoardModel
    {
        public string PreviousRetroBoardDocId { get; set; }
        public bool ShouldShowPreviousActionsButton { get; set; }
    }
}
