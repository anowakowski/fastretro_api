using Fastretro.API.Data.Domain;
using Fastretro.API.Models;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IRetroBoardServices
    {
        Task SetRetroBoard(RetroBoardModel model);
        Task SetRetroBoardCard(RetroBoardCardModel model);
        Task<RetroBoardGetModel> GetRetroBoard(string retroBoardFirebaseDocId); 
    }
}