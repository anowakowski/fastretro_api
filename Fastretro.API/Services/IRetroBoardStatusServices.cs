using System.Collections.Generic;
using System.Threading.Tasks;
using Fastretro.API.Data.Domain;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IRetroBoardStatusServices
    {
        Task SetNewRetroBoardStatus(RetroBoardStatusModel model);
        Task<RetroBoardStatusForDashboard> GetLastRetroBoardForWorkspace(string workspaceId)
        Task SetRetroBoardAsStarted(RetroBoardStatusForSetRBAsStartedModel model); 
        Task SetRetroBoardAsFinished(RetroBoardStatusForSetRBAsFinishedModel model);
        Task SetRetroBoardAsOpened(RetroBoardStatusForSetRBAsOpenedModel model);
    }
}