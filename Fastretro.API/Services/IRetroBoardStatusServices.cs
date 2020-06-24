using System.Collections.Generic;
using System.Threading.Tasks;
using Fastretro.API.Data.Domain;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IRetroBoardStatusServices
    {
        Task SetNewRetroBoardStatus(RetroBoardStatusModel model);

        Task<IEnumerable<RetroBoardStatus>> GetLastRetroBoardForWorkspace(string workspaceId);
    }
}