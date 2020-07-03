namespace Fastretro.API.Data.Domain
{
    public class UserNotificationWorkspaceWithRequiredAccess : Entity
    {
        public string WorkspceWithRequiredAccessFirebaseId { get; set; }
        public string CreatorUserFirebaseId { get; set; }
        public string UserWantToJoinFirebaseId { get; set; }
        public string WorkspaceName { get; set; }
        public string Email { get; set; }     
        public string DisplayName { get; set; }
        public int UserWaitingToApproveWorkspaceJoinId { get; set; }
        public UserNotification UserNotification { get; set; }     
        public UserWaitingToApproveWorkspaceJoin UserWaitingToApproveWorkspaceJoin { get; set; }
    }
}