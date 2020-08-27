namespace Fastretro.API.Data.Domain
{
    public class MergetRetroBoardCard : Entity
    {
        public string RetroBoardFirebaseDocId { get; set; }
        public string RetroBoardCardFirebaseDocId { get; set; }
        public RetroBoardCard retroBoardCard { get; set; }
        public string Text { get; set; }
    }
}
