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
            var isExistingUserInAction = 
                await this.repository.AnyAsync(uia =>
                    uia.UserFirebaseDocId == model.UserFirebaseDocId &&
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
                    UserFirebaseDocId = model.UserFirebaseDocId,
                    WorkspaceFirebaseDocId = model.WorkspaceFirebaseDocId
                };

                await this.repository.AddAsync(usersInAction);
                await this.unitOfWork.CompleteAsync();
            }        
        }
    }
}