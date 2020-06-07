using Fastretro.API.Models;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IRetroBoardAdditionalInfoServices
    {
        Task SetRetroBoardAdditionalInfo(RetroBoardAdditionalInfoModel model);
        Task<object> GetRetroBoardAdditionalInfo(string retroBoardId);
        Task<string> GetRetroBoardAdditionalInfoPreviousRbId(string retroBoardId, string teamDocId, string workspaceDocId);
        Task SetRetroBoardAdditionalInfoRetroBoardActionCount(RetroBoardAdditionalInfoRetroBoardActionCountModel model);
    }
}