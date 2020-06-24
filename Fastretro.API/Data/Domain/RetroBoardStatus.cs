namespace Fastretro.API.Data.Domain
{
    public class RetroBoardStatus : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }        
        public bool IsFinished { get; set; }
        public bool IsStarted { get; set; }
        public string LastModifyDate { get; set; }
    }
}