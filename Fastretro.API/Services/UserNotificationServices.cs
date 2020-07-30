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
        private readonly IRepository<UserNotification> userNotificatonRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<UserNotificationWorkspaceWithRequiredAccess> UserNotificationWorkspaceWithRequiredAccessRepository;
        private readonly IRepository<UserNotificationWorkspaceWithRequiredAccessResponse> userNotificationWorkspaceWithRequiredAccessResponseRepository;
        private readonly IRepository<UserWaitingToApproveWorkspaceJoin> userWaitingToApproveWorkspaceJoinRepository;

        public UserNotificationServices(
            IRepository<UserNotification> userNotificatonRepository,
            IRepository<UserNotificationWorkspaceWithRequiredAccess> UserNotificationWorkspaceWithRequiredAccessRepository,
            IRepository<UserNotificationWorkspaceWithRequiredAccessResponse> UserNotificationWorkspaceWithRequiredAccessResponseRepository,
            IRepository<UserWaitingToApproveWorkspaceJoin> userWaitingToApproveWorkspaceJoinRepository,
            IUnitOfWork unitOfWork)
        {
            this.UserNotificationWorkspaceWithRequiredAccessRepository = UserNotificationWorkspaceWithRequiredAccessRepository;
            userNotificationWorkspaceWithRequiredAccessResponseRepository = UserNotificationWorkspaceWithRequiredAccessResponseRepository;
            this.userWaitingToApproveWorkspaceJoinRepository = userWaitingToApproveWorkspaceJoinRepository;
            this.userNotificatonRepository = userNotificatonRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<GetAllNotificationTypeModel> GetUserNotification(string userFirebaseId)
        {
            var userNotificationWorkspaceWithRequiredAccess =
                await this.UserNotificationWorkspaceWithRequiredAccessRepository.FindAsyncWithIncludedEntityAsync(un => un.CreatorUserFirebaseId == userFirebaseId, include => include.UserNotification);

            var userNotificationWorkspaceWithRequiredAccessResponse = 
                await this.userNotificationWorkspaceWithRequiredAccessResponseRepository.FindAsyncWithIncludedEntities(
                        un => un.UserJoinedToWorkspaceFirebaseId == userFirebaseId, include => include.UserNotification, include => include.UserWaitingToApproveWorkspaceJoin);

            var allUserNotificationModel = new GetAllNotificationTypeModel
            {
                UserNotificationWorkspaceWithRequiredAccesses = userNotificationWorkspaceWithRequiredAccess,
                UserNotificationWorkspaceWithRequiredAccessResponses = userNotificationWorkspaceWithRequiredAccessResponse
            };

            return allUserNotificationModel;
        }

        public async Task SetUserNotification(UserNotificationModel model)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            UserNotification userNotification = PrepareUserNotification(currentDate, model.UserNotificationFirebaseDocId);

            await this.userNotificatonRepository.AddAsync(userNotification);

            UserWaitingToApproveWorkspaceJoin userWaitingToApproveWorkspaceJoin = PrepareUserWaitingToApproveWorkspaceJoin(model, currentDate);

            await this.userWaitingToApproveWorkspaceJoinRepository.AddAsync(userWaitingToApproveWorkspaceJoin);

            UserNotificationWorkspaceWithRequiredAccess userNotificationWorkspaceWithRequiredAccess = PrepareUserNotificationWorkspaceWithRequiredAccess(model, userNotification, userWaitingToApproveWorkspaceJoin);

            await this.UserNotificationWorkspaceWithRequiredAccessRepository.AddAsync(userNotificationWorkspaceWithRequiredAccess);

            await this.unitOfWork.CompleteAsync();
        }

        private static UserNotificationWorkspaceWithRequiredAccess PrepareUserNotificationWorkspaceWithRequiredAccess(UserNotificationModel model, UserNotification userNotification, UserWaitingToApproveWorkspaceJoin userWaitingToApproveWorkspaceJoin)
        {
            return new UserNotificationWorkspaceWithRequiredAccess
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
        }

        private static UserWaitingToApproveWorkspaceJoin PrepareUserWaitingToApproveWorkspaceJoin(UserNotificationModel model, string currentDate)
        {
            return new UserWaitingToApproveWorkspaceJoin
            {
                LastModifyDate = currentDate,
                CreatorUserFirebaseId = model.CreatorUserFirebaseId,
                UserWantToJoinFirebaseId = model.UserWantToJoinFirebaseId,
                WorkspceWithRequiredAccessFirebaseId = model.WorkspceWithRequiredAccessFirebaseId,
                RequestIsApprove = false
            };
        }

        private static UserNotification PrepareUserNotification(string currentDate, string userNotificationFirebaseDocId)
        {
            return new UserNotification
            {
                NotyficationType = "WorkspaceWithRequiredAccess",
                CreatonDate = currentDate,
                IsRead = false,
                UserNotificationFirebaseDocId = userNotificationFirebaseDocId
            };
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

        public async Task SetUserNotificationAsReadForWorkspaceWithRequiredAccessResponse(UserNotificationAsReadForWorkspaceResonseAccessModel model)
        {
            var findedUsrNotification = await this.userNotificatonRepository.FirstOrDefaultAsync(un => un.Id == model.UserNotificationId);

            if (findedUsrNotification != null)
            {
                findedUsrNotification.IsRead = true;
                this.userNotificatonRepository.Update(findedUsrNotification);

                await this.unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<UserNotificationWorkspaceWithRequiredAccess>> GetAllWaitingWorkspaceRequests(string userWantToJoinFirebaseId)
        {
            var findedUserNotificationWorkspaceWithRequiredAccess =
                (await this.UserNotificationWorkspaceWithRequiredAccessRepository
                        .FindAsyncWithIncludedEntities(unw => 
                            unw.UserWantToJoinFirebaseId == userWantToJoinFirebaseId,
                            include => include.UserNotification, include => include.UserWaitingToApproveWorkspaceJoin))
                        .ToList();

            var filteredFindedUserNotificationWorkspaceWithRequiredAccess =
                    findedUserNotificationWorkspaceWithRequiredAccess.Where(funr => !funr.UserWaitingToApproveWorkspaceJoin.IsApprovalByCreator);

            return filteredFindedUserNotificationWorkspaceWithRequiredAccess;
        }

        public async Task SetUserNotificationForuserWaitingToApproveWorkspaceJoin(UserNotificationForUserWaitingToApproveWorkspaceJoinModel model)
        {
            var findedUserNotificationWorkspaceWithRequiredAccess =
                await this.UserNotificationWorkspaceWithRequiredAccessRepository.FirstOrDefaulAsyncWithIncludedEntities(
                    uwa => uwa.UserWaitingToApproveWorkspaceJoinId == model.UserWaitingToApproveWorkspaceJoinId,
                    include => include.UserNotification, include => include.UserWaitingToApproveWorkspaceJoin);

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await SetUserNotificationWorkspaceWithRequiredAccessResponse(model, findedUserNotificationWorkspaceWithRequiredAccess, currentDate);
        }

        private async Task SetUserNotificationWorkspaceWithRequiredAccessResponse(UserNotificationForUserWaitingToApproveWorkspaceJoinModel model, UserNotificationWorkspaceWithRequiredAccess findedUserNotificationWorkspaceWithRequiredAccess, string currentDate)
        {
            var userNotification = new UserNotification
            {
                CreatonDate = currentDate,
                IsRead = false,
                NotyficationType = "WorkspaceWithRequiredAccessResponse",
                UserNotificationFirebaseDocId = model.UserNotificationDocId
            };

            await this.userNotificatonRepository.AddAsync(userNotification);

            var userNotificationWorkspaceWithRequiredAccessResponse = new UserNotificationWorkspaceWithRequiredAccessResponse
            {
                UserJoinedToWorkspaceFirebaseId = findedUserNotificationWorkspaceWithRequiredAccess.UserWantToJoinFirebaseId,
                WorkspaceName = findedUserNotificationWorkspaceWithRequiredAccess.WorkspaceName,
                WorkspceWithRequiredAccessFirebaseId = findedUserNotificationWorkspaceWithRequiredAccess.WorkspceWithRequiredAccessFirebaseId,
                UserNotification = userNotification,
                UserWaitingToApproveWorkspaceJoinId = model.UserWaitingToApproveWorkspaceJoinId,
                UserWaitingToApproveWorkspaceJoin = findedUserNotificationWorkspaceWithRequiredAccess.UserWaitingToApproveWorkspaceJoin,
            };

            await this.userNotificationWorkspaceWithRequiredAccessResponseRepository.AddAsync(userNotificationWorkspaceWithRequiredAccessResponse);

            await this.unitOfWork.CompleteAsync();
        }
    }
}