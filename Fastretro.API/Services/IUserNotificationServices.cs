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
        Task<GetAllNotificationTypeModel> GetUserNotification(string creatorUserFirebaseId);
        Task<IEnumerable<UserNotificationWorkspaceWithRequiredAccess>> GetAllWaitingWorkspaceRequests(string userWantToJoinFirebaseId);
        Task SetUserNotificationForuserWaitingToApproveWorkspaceJoin(UserNotificationForUserWaitingToApproveWorkspaceJoinModel model);
    }
}