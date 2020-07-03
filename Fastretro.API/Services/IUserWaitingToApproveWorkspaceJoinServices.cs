using System.Threading.Tasks;
using Fastretro.API.Data.Domain;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IUserWaitingToApproveWorkspaceJoinServices
    {
        Task SetWaitUserToWantToJoinToWorkspace(UserWaitingToApproveWorkspaceJoinModel model);
        Task SetApproveUserWantToJoinToWorkspace(UserWaitingToApproveWorkspaceJoinModel model);
        Task<UserWaitingToApproveWorkspaceJoin> GetUserWaitingToApproveWorkspaceJoin(string userWantToJoinFirebaseId, string creatorUserFirebaseId, string workspceWithRequiredAccessFirebaseId, int userWaitingToApproveWorkspaceJoinId);
        Task SetWaitUserToWantToJoinToWorkspaceByEntity(UserWaitingToApproveWorkspaceJoin entity);
    }
}