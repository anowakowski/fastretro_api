namespace Fastretro.API.Models
{
    public class LastRetroBoardModel
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }        
        public bool IsFinished { get; set; }        
    }
}