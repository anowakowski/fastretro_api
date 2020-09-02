namespace Fastretro.API.Data.Domain
{
    public class MergedRetroBoardCard : Entity
    {
        public RetroBoardCardMergedGroup RetroBoardCardMergedGroup { get; set; }
        public int RetroBoardCardId { get; set; }
        public RetroBoardCard RetroBoardCard { get; set; }
    }
}
