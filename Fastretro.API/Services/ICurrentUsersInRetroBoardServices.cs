using Fastretro.API.Data.Domain;
using Fastretro.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface ICurrentUsersInRetroBoardServices
    {
        Task<IEnumerable<FirebaseUserData>> GetCurrentUsersInRetroBoard(string retroBoardId);
        Task SetUpCurrentUserInRetroBoard(CurrentUserDataModel currentUserDataModel);

    }
}