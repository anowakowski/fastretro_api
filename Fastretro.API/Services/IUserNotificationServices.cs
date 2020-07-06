using System.Collections.Generic;
using System.Threading.Tasks;
using Fastretro.API.Data.Domain;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IUserNotificationServices
    {
        Task SetUserNotification(UserNotificationModel model);
        Task SetUserNotificationAsRead(UserNotificationAsReadModel model);
        Task<IEnumerable<UserNotificationWorkspaceWithRequiredAccess>> GetUserNotification(string CreatorUserFirebaseId);
        Task<IEnumerable<UserNotificationWorkspaceWithRequiredAccess>> GetAllWaitingWorkspaceRequests(string userWantToJoinFirebaseId);
        Task SetUserNotificationForuserWaitingToApproveWorkspaceJoin(UserNotificationForUserWaitingToApproveWorkspaceJoinModel model);
    }
}