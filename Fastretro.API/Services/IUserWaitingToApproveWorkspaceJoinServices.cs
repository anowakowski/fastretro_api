using System.Threading.Tasks;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IUserWaitingToApproveWorkspaceJoinServices
    {
        Task SetWaitUserToWantToJoinToWorkspace(UserWaitingToApproveWorkspaceJoinModel model);
        Task SetApproveUserWantToJoinToWorkspace(UserWaitingToApproveWorkspaceJoinModel model);
    }
}