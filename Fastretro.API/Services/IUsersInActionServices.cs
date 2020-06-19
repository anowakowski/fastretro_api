using System.Collections.Generic;
using System.Threading.Tasks;
using Fastretro.API.Data.Domain;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IUsersInActionServices
    {
        Task SetUserInAction(UsersInActionModel model);
        Task<IEnumerable<UsersInAction>> GetUserInActionForRetroBoardCard(string teamFirebaseDocId, string workspaceFirebaseDocId, string retroBoardCardFirebaseDocId, string retroBoardActionCardFirebaseDocId);
        Task<IEnumerable<GetUsersInActionModel>> GetUsersInActionForTeam(string teamFirebaseDocId, string workspaceFirebaseDocId);
    }
}