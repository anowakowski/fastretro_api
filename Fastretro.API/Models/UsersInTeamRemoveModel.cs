namespace Fastretro.API.Models
{
    public class UsersInTeamRemoveModel
    {
        public string UserFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }
    }
}
