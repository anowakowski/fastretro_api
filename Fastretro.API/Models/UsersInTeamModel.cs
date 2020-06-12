using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class UsersInTeamModel
    {
        public string UserFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }
        public string ChosenAvatarUrl { get; set; }
        public string DisplayName { get; set; }
    }
}
