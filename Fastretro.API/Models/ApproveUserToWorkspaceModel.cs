namespace Fastretro.API.Models
{
    public class ApproveUserToWorkspaceModel
    {
        public string WorkspceWithRequiredAccessFirebaseId { get; set; }
        public string CreatorUserFirebaseId { get; set; }
        public string UserWantToJoinFirebaseId { get; set; }
        public bool RequestIsApprove { get; set; }      
    }
}