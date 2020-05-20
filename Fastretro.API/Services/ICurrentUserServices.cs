using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface ICurrentUserServices
    {
        Task<string> GetCurrentUsersInRetroBoard(string retroBoardId);
    }
}