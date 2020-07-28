namespace Fastretro.API.Data.Domain
{
    public class UserNotificationWorkspaceWithRequiredAccessResponse : Entity
    {
        public string WorkspceWithRequiredAccessFirebaseId { get; set; }
        public string UserJoinedToWorkspaceFirebaseId { get; set; }
        public string WorkspaceName { get; set; }
        public int UserWaitingToApproveWorkspaceJoinId { get; set; }
        public UserNotification UserNotification { get; set; }
        public UserWaitingToApproveWorkspaceJoin UserWaitingToApproveWorkspaceJoin { get; set; }
    }
}