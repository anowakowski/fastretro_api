using Fastretro.API.Data.Domain;
using Fastretro.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IRetroBoardServices
    {
        Task SetRetroBoard(RetroBoardModel model);
        Task<RetroBoardCard> SetRetroBoardCard(RetroBoardCardModel model);
        Task<RetroBoardGetModel> GetRetroBoard(string retroBoardFirebaseDocId);
        Task<IEnumerable<RetroBoardCardGetModel>> GetRetroBoardCards(string retroBoardFirebaseDocId);
        Task UpdateRetroBoardCardtext(RetroBoardCardModel model);
        Task UpdateRetroBoardCardFirebaseDocId(RetroBoardCardModelAfterSaveForAddFirebaseDocId model);
        Task<RetroBoardCardMergedContentGetModel> SetRetroBoardCardMergetContent(RetroBoardCardMergedContentModel model);
        Task SetRetroBoardMergedFirebaseDocId(RetroBoardCardMergedSetCardFirebaseIdModel model);
        Task<RetroBoardCardUnMergedContentGetModel> SetRetroBoardCardUnmerged(RetroBoardCardUnMergedContentModel model);
        Task RemoveRetroBoardCard(RetroBoardCardRemoveModel model);
        Task<RetroBoardActionCardGetAfrerAddModel> SetRetroBoardAction(RetroBoardActionCardModel model);
        Task<RetroBoardActionCardGetReturnModel> GetRetroBoardAction(int retroBoardActionCardId);
        Task<IEnumerable<RetroBoardActionCardGetReturnModel>> GetRetroBoardActionsForCard(string retroBoardCardFirebaseId);
        Task UpdateRetroBoardAction(RetroBoardActionCardUpdateModel model);
        Task RemoveRetroBoardActionCard(RetroBoardActionCardRemoveModel model);
    }
}