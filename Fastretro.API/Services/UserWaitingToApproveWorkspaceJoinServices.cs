using System;
using System.Threading.Tasks;
using Fastretro.API.Data;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public class UserWaitingToApproveWorkspaceJoinServices : IUserWaitingToApproveWorkspaceJoinServices
    {
        private readonly IRepository<userWaitingToApproveWorkspaceJoin> userWaitingToApproveWorkspaceJoinRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserWaitingToApproveWorkspaceJoinServices(
            IRepository<userWaitingToApproveWorkspaceJoin> userWaitingToApproveWorkspaceJoinRepository,
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.userWaitingToApproveWorkspaceJoinRepository = userWaitingToApproveWorkspaceJoinRepository;

        }
        public async Task SetApproveUserWantToJoinToWorkspace(UserWaitingToApproveWorkspaceJoinModel model)
        {
            var findedUserWaitingToApproveWorkspaceJoin = 
                await this.userWaitingToApproveWorkspaceJoinRepository.FirstOrDefaultAsync(
                    uwa => uwa.CreatorUserFirebaseId == model.CreatorUserFirebaseId &&
                    uwa.UserWantToJoinFirebaseId == model.UserWantToJoinFirebaseId &&
                    uwa.WorkspceWithRequiredAccessFirebaseId == model.WorkspceWithRequiredAccessFirebaseId
                );
            if (findedUserWaitingToApproveWorkspaceJoin != null) 
            {
                var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                findedUserWaitingToApproveWorkspaceJoin.RequestIsApprove = model.RequestIsApprove;
                findedUserWaitingToApproveWorkspaceJoin.LastModifyDate = currentDate;
                this.userWaitingToApproveWorkspaceJoinRepository.Update(findedUserWaitingToApproveWorkspaceJoin);
                await this.unitOfWork.CompleteAsync();
            }
        }

        public async Task SetWaitUserToWantToJoinToWorkspace(UserWaitingToApproveWorkspaceJoinModel model)
        {
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var entity = new userWaitingToApproveWorkspaceJoin
            {
                CreatorUserFirebaseId = model.CreatorUserFirebaseId,
                UserWantToJoinFirebaseId = model.UserWantToJoinFirebaseId,
                WorkspceWithRequiredAccessFirebaseId = model.WorkspceWithRequiredAccessFirebaseId,
                LastModifyDate = currentDate,
                RequestIsApprove = false
            };

            await this.userWaitingToApproveWorkspaceJoinRepository.AddAsync(entity);
            await this.unitOfWork.CompleteAsync();
        }
    }
}