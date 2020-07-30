namespace Fastretro.API.Models
{
    public class UserWaitingToApproveWorkspaceJoinModel
    {
        public string WorkspceWithRequiredAccessFirebaseId { get; set; }
        public string CreatorUserFirebaseId { get; set; }
        public string UserWantToJoinFirebaseId { get; set; }
        public bool RequestIsApprove { get; set; }      
    }
}