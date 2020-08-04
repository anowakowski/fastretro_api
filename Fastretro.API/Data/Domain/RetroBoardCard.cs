using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class RetroBoardCard : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
        public string Text { get; set; }
    }
}
