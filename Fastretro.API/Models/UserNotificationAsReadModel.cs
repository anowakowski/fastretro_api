namespace Fastretro.API.Models
{
    public class UserNotificationAsReadModel
    {
        public string WorkspceWithRequiredAccessFirebaseId { get; set; }
        public string CreatorUserFirebaseId { get; set; }
        public string UserWantToJoinFirebaseId { get; set; }      
        public int UserWaitingToApproveWorkspaceJoinId { get; set; }
    }
}