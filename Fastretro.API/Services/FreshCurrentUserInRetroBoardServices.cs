using Fastretro.API.Data;using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public class FreshCurrentUserInRetroBoardServices : IFreshCurrentUserInRetroBoardServices
    {
        private readonly IRepository<FirebaseUserData> firebaseUserDataRepository;
        private readonly IRepository<CurrentUserInRetroBoard> currentUserInRetroBoardRepository;
        private readonly IUnitOfWork unitOfWork;

        public FreshCurrentUserInRetroBoardServices(
            IRepository<FirebaseUserData> firebaseUserDataRepository,
            IRepository<CurrentUserInRetroBoard> currentUserInRetroBoardRepository,
            IUnitOfWork unitOfWork)
        {
            this.firebaseUserDataRepository = firebaseUserDataRepository;
            this.currentUserInRetroBoardRepository = currentUserInRetroBoardRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<FirebaseUserData>> SetUpFreshListOfCurrentUsers(GetFreshCurrentUserDataModel model)
        {
            await updateCurrentUserTick(model);

            await removeExpiredUsers(model);

            return await this.firebaseUserDataRepository.FindAsync(x => x.CurrentUserInRetroBoard.RetroBoardId == model.RetroBoardId);
        }

        private async Task removeExpiredUsers(GetFreshCurrentUserDataModel model)
        {
            var findedCurrentUserInRetroBoard =
                await this.currentUserInRetroBoardRepository.FirstOrDefaultWithIncludedEntityAsync(x => x.RetroBoardId == model.RetroBoardId, x => x.firebaseUsersData);

            var findedFirebaseUsersData = findedCurrentUserInRetroBoard.firebaseUsersData;

            foreach (var userData in findedFirebaseUsersData)
            {
                var currentDate = DateTime.Now;
                var userDateToCheck = DateTime.Parse(userData.DateOfExistingCheck);

                TimeSpan dateDiffSpan = (currentDate - userDateToCheck);
                var diffInSec = dateDiffSpan.TotalSeconds;

                if (diffInSec >= 15)
                {
                    //remove
                    this.firebaseUserDataRepository.Delete(userData);
                    await this.unitOfWork.CompleteAsync();
                }
            }
        }

        private async Task updateCurrentUserTick(GetFreshCurrentUserDataModel model)
        {
            var fbUserData =
                await this.firebaseUserDataRepository.FirstOrDefaultAsync(x => x.FirebaseUserDocId == model.UserId && x.CurrentUserInRetroBoard.RetroBoardId == model.RetroBoardId);

            if (fbUserData != null)
            {
                fbUserData.DateOfExistingCheck = DateTime.Now.ToString();
                this.firebaseUserDataRepository.Update(fbUserData);

                await this.unitOfWork.CompleteAsync();
            }
        }
    }
}
