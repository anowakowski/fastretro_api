using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class RetroBoardGetModel
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string RetroBoardName { get; set; }
        public string SprintNumber { get; set; }
    }
}
