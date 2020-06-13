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
                var isExistingUserInAction =
                    await this.repository.AnyAsync(uia =>
                        uia.RetroBoardActionCardFirebaseDocId == model.RetroBoardActionCardFirebaseDocId &&
                        uia.RetroBoardActionCardFirebaseDocId == model.RetroBoardActionCardFirebaseDocId &&
                        uia.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                        uia.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId);

                if (isExistingUserInAction)
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
            }
            else
            {
                await setNewUserInAction(model);
            }

        }

        private async Task setNewUserInAction(UsersInActionModel model)
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
    }
}