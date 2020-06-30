namespace Fastretro.API.Models
{
    public class UserNotificationModel
    {
        public string NotyficationType { get; set; }
        public string WorkspceWithRequiredAccessFirebaseId { get; set; }
        public string CreatorUserFirebaseId { get; set; }
        public string UserWantToJoinFirebaseId { get; set; }        
    }
}