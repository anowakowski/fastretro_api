namespace Fastretro.API.Data.Domain
{
    public class userWaitingToApproveWorkspaceJoin : Entity
    {
        public string WorkspceWithRequiredAccessFirebaseId { get; set; }
        public string CreatorUserFirebaseId { get; set; }
        public string UserWantToJoinFirebaseId { get; set; }
        public bool RequestIsApprove { get; set; }
        public bool IsApprovalByCreator {get; set; }
        public string LastModifyDate { get; set; }            
    }
}