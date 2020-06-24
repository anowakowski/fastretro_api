namespace Fastretro.API.Models
{
    public class RetroBoardStatusModel
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string TeamFirebaseDocId { get; set; }
        public string WorkspaceFirebaseDocId { get; set; }        
        public bool IsFinished { get; set; }
        public bool IsStarted { get; set; }                
    }
}