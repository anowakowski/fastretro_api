using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fastretro.API.Data;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public class UserNotificationServices : IUserNotificationServices
    {
        private readonly IUserWaitingToApproveWorkspaceJoinServices userWaitingToApproveWorkspaceJoinServices;
        private readonly IRepository<UserNotification> userNotificatonRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<UserNotificationWorkspaceWithRequiredAccess> UserNotificationWorkspaceWithRequiredAccessRepository;

        public UserNotificationServices(
            IUserWaitingToApproveWorkspaceJoinServices userWaitingToApproveWorkspaceJoinServices,
            IRepository<UserNotification> userNotificatonRepository,
            IRepository<UserNotificationWorkspaceWithRequiredAccess> UserNotificationWorkspaceWithRequiredAccessRepository,
            IUnitOfWork unitOfWork)
        {
            this.UserNotificationWorkspaceWithRequiredAccessRepository = UserNotificationWorkspaceWithRequiredAccessRepository;
            this.userWaitingToApproveWorkspaceJoinServices = userWaitingToApproveWorkspaceJoinServices;
            this.userNotificatonRepository = userNotificatonRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserNotificationWorkspaceWithRequiredAccess>> GetUserNotification(string creatorUserFirebaseId)
        {
            var userNotificationWorkspaceWithRequiredAccess =
                await this.UserNotificationWorkspaceWithRequiredAccessRepository.FindAsyncWithIncludedEntityAsync(un => un.CreatorUserFirebaseId == creatorUserFirebaseId, include => include.UserNotification);

            var orderedUserNotification = userNotificationWorkspaceWithRequiredAccess.ToList().OrderBy(un => un.UserNotification.IsRead).ToList();

            return userNotificationWorkspaceWithRequiredAccess;
        }

        public async Task SetUserNotification(UserNotificationModel model)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var userNotification = new UserNotification
            {
                NotyficationType = "WorkspaceWithRequiredAccess",
                CreatonDate = currentDate,
                IsRead = false
            };

            await this.userNotificatonRepository.AddAsync(userNotification);

            var userWaitingToApproveWorkspaceJoin = new UserWaitingToApproveWorkspaceJoin
            {
                LastModifyDate = currentDate,
                CreatorUserFirebaseId = model.CreatorUserFirebaseId,
                UserWantToJoinFirebaseId = model.UserWantToJoinFirebaseId,
                WorkspceWithRequiredAccessFirebaseId = model.WorkspceWithRequiredAccessFirebaseId,
                RequestIsApprove = false
            };

            await this.userWaitingToApproveWorkspaceJoinServices.SetWaitUserToWantToJoinToWorkspaceByEntity(userWaitingToApproveWorkspaceJoin);

            var userNotificationWorkspaceWithRequiredAccess = new UserNotificationWorkspaceWithRequiredAccess
            {
                UserWantToJoinFirebaseId = model.UserWantToJoinFirebaseId,
                CreatorUserFirebaseId = model.CreatorUserFirebaseId,
                WorkspceWithRequiredAccessFirebaseId = model.WorkspceWithRequiredAccessFirebaseId,
                WorkspaceName = model.WorkspaceName,
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserNotification = userNotification,
                UserWaitingToApproveWorkspaceJoin = userWaitingToApproveWorkspaceJoin
            };

            await this.UserNotificationWorkspaceWithRequiredAccessRepository.AddAsync(userNotificationWorkspaceWithRequiredAccess);

            await this.unitOfWork.CompleteAsync();
        }

        public async Task SetUserNotificationAsRead(UserNotificationAsReadModel model)
        {
            var userNotificationWorkspaceWithRequiredAccess =
                await this.UserNotificationWorkspaceWithRequiredAccessRepository.FirstOrDefaulAsyncWithIncludedEntities(
                    uwa => uwa.CreatorUserFirebaseId == model.CreatorUserFirebaseId &&
                    uwa.UserWantToJoinFirebaseId == model.UserWantToJoinFirebaseId &&
                    uwa.WorkspceWithRequiredAccessFirebaseId == model.WorkspceWithRequiredAccessFirebaseId &&
                    uwa.UserWaitingToApproveWorkspaceJoinId == model.UserWaitingToApproveWorkspaceJoinId,
                    include => include.UserNotification, include => include.UserWaitingToApproveWorkspaceJoin);

            if (userNotificationWorkspaceWithRequiredAccess != null)
            {
                var findedUserNotification = userNotificationWorkspaceWithRequiredAccess.UserNotification;
                findedUserNotification.IsRead = true;

                this.UserNotificationWorkspaceWithRequiredAccessRepository.Update(userNotificationWorkspaceWithRequiredAccess);

                await this.unitOfWork.CompleteAsync();
            }
        }
    }
}