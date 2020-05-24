using Fastretro.API.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface ICurrentUsersInRetroBoardServices
    {
        Task<IEnumerable<FirebaseUserData>> GetCurrentUsersInRetroBoard(string retroBoardId);
        Task SetUpCurrentUserInRetroBoard(string docUserId, string retroBoardId);
    }
}