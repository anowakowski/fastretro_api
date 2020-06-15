using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fastretro.API.Data;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public class UsersInActionServices : IUsersInActionServices
    {
        private readonly IRepository<UsersInAction> repository;
        private readonly IUnitOfWork unitOfWork;
        public UsersInActionServices(IRepository<UsersInAction> repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public async Task SetUserInAction(UsersInActionModel model)
        {
            if (model.UserFirebaseDocIds.Count() == 0)
            {
                await DeleteAllUsersInActionsIfAnyNotExists(model);
            }
            else
            {
                await SetUsersInAction(model);
            }
        }

        private async Task DeleteAllUsersInActionsIfAnyNotExists(UsersInActionModel model)
        {
            bool isExistingUserInAction = await IsExistingUsersInAction(model.TeamFirebaseDocId, model.WorkspaceFirebaseDocId, model.RetroBoardActionCardFirebaseDocId, model.RetroBoardActionCardFirebaseDocId);

            if (isExistingUserInAction)
            {
                await DeleteUsersInAction(model);
            }
        }

        private async Task<bool> IsExistingUsersInAction(string teamFirebaseDocId, string workspaceFirebaseDocId, string retroBoardCardFirebaseDocId, string retroBoardActionCardFirebaseDocId)
        {
            return await this.repository.AnyAsync(uia =>
                    uia.RetroBoardActionCardFirebaseDocId == retroBoardActionCardFirebaseDocId &&
                    uia.RetroBoardActionCardFirebaseDocId == retroBoardActionCardFirebaseDocId &&
                    uia.TeamFirebaseDocId == teamFirebaseDocId &&
                    uia.WorkspaceFirebaseDocId == workspaceFirebaseDocId);
        }

        private async Task DeleteUsersInAction(UsersInActionModel model)
        {
            var findedExistingUserInAction =
                (await this.repository.FindAsync(uia =>
                    uia.RetroBoardActionCardFirebaseDocId == model.RetroBoardActionCardFirebaseDocId &&
                    uia.RetroBoardActionCardFirebaseDocId == model.RetroBoardActionCardFirebaseDocId &&
                    uia.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                    uia.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId)).ToList();

            if (findedExistingUserInAction.Count() > 0)
            {
                foreach (var usrInActionToDelete in findedExistingUserInAction)
                {
                    this.repository.Delete(usrInActionToDelete);
                    await this.unitOfWork.CompleteAsync();
                }
            }
        }

        private async Task SetUsersInAction(UsersInActionModel model)
        {
            await this.DeleteUsersInAction(model);
            await SetNewUsersInActionIfExists(model);
        }

        private async Task SetNewUsersInActionIfExists(UsersInActionModel model)
        {
            foreach (var userFirebaseDocId in model.UserFirebaseDocIds)
            {
                var isExistingUserInAction =
                    await this.repository.AnyAsync(uia =>
                        uia.UserFirebaseDocId == userFirebaseDocId &&
                        uia.RetroBoardActionCardFirebaseDocId == model.RetroBoardActionCardFirebaseDocId &&
                        uia.RetroBoardActionCardFirebaseDocId == model.RetroBoardActionCardFirebaseDocId &&
                        uia.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                        uia.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId);

                if (!isExistingUserInAction)
                {
                    UsersInAction usersInAction = new UsersInAction
                    {
                        RetroBoardActionCardFirebaseDocId = model.RetroBoardActionCardFirebaseDocId,
                        TeamFirebaseDocId = model.TeamFirebaseDocId,
                        RetroBoardCardFirebaseDocId = model.RetroBoardCardFirebaseDocId,
                        UserFirebaseDocId = userFirebaseDocId,
                        WorkspaceFirebaseDocId = model.WorkspaceFirebaseDocId
                    };

                    await this.repository.AddAsync(usersInAction);
                    await this.unitOfWork.CompleteAsync();
                }
            }
        }

        public async Task<IEnumerable<UsersInAction>> GetUserInAction(string teamFirebaseDocId, string workspaceFirebaseDocId, string retroBoardCardFirebaseDocId, string retroBoardActionCardFirebaseDocId)
        {
            bool isExistingUserInAction = await IsExistingUsersInAction(teamFirebaseDocId, workspaceFirebaseDocId, retroBoardCardFirebaseDocId, retroBoardActionCardFirebaseDocId);

            var findedExistingUserInAction =
                (await this.repository.FindAsync(uia =>
                    uia.RetroBoardActionCardFirebaseDocId == retroBoardActionCardFirebaseDocId &&
                    uia.RetroBoardActionCardFirebaseDocId == retroBoardActionCardFirebaseDocId &&
                    uia.TeamFirebaseDocId == teamFirebaseDocId &&
                    uia.WorkspaceFirebaseDocId == workspaceFirebaseDocId)).ToList();

            return findedExistingUserInAction;        

        }
    }
}