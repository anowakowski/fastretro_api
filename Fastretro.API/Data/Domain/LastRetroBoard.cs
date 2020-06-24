namespace Fastretro.API.Data.Domain
{
    public class LastRetroBoard : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }        
        public bool IsFinished { get; set; }
    }
}