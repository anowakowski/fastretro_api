namespace Fastretro.API.Data.Domain
{
    public class UserNotification : Entity
    {
        public string NotyficationType { get; set; }
        public string CreatonDate { get; set; }
        public bool IsRead { get; set; }
    }
}