using System.Collections.Generic;
using System.Threading.Tasks;
using Fastretro.API.Data.Domain;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IUsersInActionServices
    {
        Task SetUserInAction(UsersInActionModel model);
        Task<IEnumerable<UsersInAction>>  GetUserInAction(string teamFirebaseDocId, string workspaceFirebaseDocId, string retroBoardCardFirebaseDocId, string retroBoardActionCardFirebaseDocId);
    }
}