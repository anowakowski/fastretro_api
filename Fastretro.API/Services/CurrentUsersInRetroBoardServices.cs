using Fastretro.API.Data;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public class CurrentUsersInRetroBoardServices : ICurrentUsersInRetroBoardServices
    {
        private readonly IRepository<FirebaseUserData> firebaseUserDataRepository;
        private readonly IRepository<CurrentUserInRetroBoard> currentUserInRetroBoardRepository;
        private readonly IUnitOfWork unitOfWork;

        public CurrentUsersInRetroBoardServices(
            IRepository<FirebaseUserData> firebaseUserDataRepository,
            IRepository<CurrentUserInRetroBoard> currentUserInRetroBoardRepository,
            IUnitOfWork unitOfWork)
        {
            this.firebaseUserDataRepository = firebaseUserDataRepository;
            this.currentUserInRetroBoardRepository = currentUserInRetroBoardRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<FirebaseUserData>> GetCurrentUsersInRetroBoard(string retroBoardId)
        {
           return await this.firebaseUserDataRepository.FindAsync(x => x.CurrentUserInRetroBoard.RetroBoardId == retroBoardId);
        }

        public async Task SetUpCurrentUserInRetroBoard(string docUserId, string retroBoardId)
        {
            if (await this.firebaseUserDataRepository.AnyAsync(x => x.FirebaseUserDocId == docUserId && x.CurrentUserInRetroBoard.RetroBoardId == retroBoardId))
            {
                await UpdateUserIfCurrentUserDataExist(docUserId, retroBoardId);
            } 
            else
            {
                await AddNewUserIfNotExistInCurrentRetroBoard(docUserId, retroBoardId);
            }
        }

        private async Task AddNewUserIfNotExistInCurrentRetroBoard(string docUserId, string retroBoardId)
        {
            var firebaseUserData = new FirebaseUserData
            {
                FirebaseUserDocId = docUserId,
                DateOfExistingCheck = DateTime.Now.ToString()
            };

            if (await this.currentUserInRetroBoardRepository.AnyAsync(x => x.RetroBoardId == retroBoardId))
            {
                var findedCurrentUserInRetroBoard = await this.currentUserInRetroBoardRepository.FirstOrDefaultWithIncludedEntityAsync(x => x.RetroBoardId == retroBoardId, x => x.firebaseUsersData);
                findedCurrentUserInRetroBoard.firebaseUsersData.Add(firebaseUserData);

                this.currentUserInRetroBoardRepository.Update(findedCurrentUserInRetroBoard);

                await this.unitOfWork.CompleteAsync();
            } 
            else
            {
                var firebaseUsersData = new List<FirebaseUserData> { firebaseUserData };

                await this.firebaseUserDataRepository.AddAsync(firebaseUserData);

                var newCurrentUserInRetroBoard = new CurrentUserInRetroBoard
                {
                    firebaseUsersData = firebaseUsersData,
                    RetroBoardId = retroBoardId
                };

                await this.currentUserInRetroBoardRepository.AddAsync(newCurrentUserInRetroBoard);

                await this.unitOfWork.CompleteAsync();
            }
        }

        private async Task UpdateUserIfCurrentUserDataExist(string docUserId, string retroBoardId)
        {
            var fbUserData = await this.firebaseUserDataRepository.FirstOrDefaultAsync(x => x.FirebaseUserDocId == docUserId && x.CurrentUserInRetroBoard.RetroBoardId == retroBoardId);
            fbUserData.DateOfExistingCheck = DateTime.Now.ToString();
            this.firebaseUserDataRepository.Update(fbUserData);

            await this.unitOfWork.CompleteAsync();
        }
    }
}
