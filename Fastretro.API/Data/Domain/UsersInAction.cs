namespace Fastretro.API.Data.Domain
{
    public class UsersInAction : Entity
    {
        public string UserFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
        public string RetroBoardActionCardFirebaseDocId { get; set; }
    }
}