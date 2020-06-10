using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class UsersInTeam : Entity
    {
        public string UserFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }
    }
}
