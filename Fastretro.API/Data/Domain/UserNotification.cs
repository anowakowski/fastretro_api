namespace Fastretro.API.Data.Domain
{
    public class UserNotification : Entity
    {
        public string NotyficationType { get; set; }
        public string WorkspceWithRequiredAccessFirebaseId { get; set; }
        public string CreatorUserFirebaseId { get; set; }
        public string UserWantToJoinFirebaseId { get; set; }
    }
}