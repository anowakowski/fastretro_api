using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class RetroBoardAdditionalInfoModel : RetroBoardAdditionalInfoBaseModel
    {
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }
    }
}
