using Fastretro.API.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class RetroBoardCardUnMergedContentGetModel
    {
        public IList<RetroBoardCardAfterUnmergeGetModel> ChildRetroBoardCards{ get; set; }
    }
}
