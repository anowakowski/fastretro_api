﻿using Fastretro.API.Data;
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
        public async Task<IEnumerable<FirebaseUserData>> GetCurrentUsersInRetroBoard(string retroBoardId, string firebaseUserDocId)
        {
           return await this.firebaseUserDataRepository.FindAsync(x => x.FirebaseUserDocId == firebaseUserDocId);
        }

        public async Task SetUpCurrentUserInRetroBoard(string docUserId, string retroBoardId)
        {
            await updateIfCurrentUserDataExist(docUserId, retroBoardId);
            await addNewIfNotExistInCurrentRetroBoard(docUserId, retroBoardId);
        }

        private async Task addNewIfNotExistInCurrentRetroBoard(string docUserId, string retroBoardId)
        {
            if (await this.firebaseUserDataRepository.AnyAsync(x => x.FirebaseUserDocId != docUserId && x.CurrentUserInRetroBoard.RetroBoardId == retroBoardId))
            {
                var firebaseUserData = new FirebaseUserData
                {
                    FirebaseUserDocId = docUserId,
                    DateOfExistingCheck = DateTime.Now.ToString()
                };

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

        private async Task updateIfCurrentUserDataExist(string docUserId, string retroBoardId)
        {
            if (await this.firebaseUserDataRepository.AnyAsync(x => x.FirebaseUserDocId == docUserId && x.CurrentUserInRetroBoard.RetroBoardId == retroBoardId))
            {
                var fbUserData = await this.firebaseUserDataRepository.FirstOrDefaultAsync(x => x.FirebaseUserDocId == docUserId && x.CurrentUserInRetroBoard.RetroBoardId == retroBoardId));
                fbUserData.DateOfExistingCheck = DateTime.Now.ToString();
                this.firebaseUserDataRepository.Update(fbUserData);

                await this.unitOfWork.CompleteAsync();
            }
        }
    }
}
