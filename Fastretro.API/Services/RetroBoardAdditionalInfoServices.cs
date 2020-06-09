using Fastretro.API.Data;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public class RetroBoardAdditionalInfoServices : IRetroBoardAdditionalInfoServices
    {
        private readonly IRepository<RetroBoardAdditionalInfo> repository;
        private readonly IUnitOfWork unitOfWork;

        public RetroBoardAdditionalInfoServices(IRepository<RetroBoardAdditionalInfo> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task SetRetroBoardAdditionalInfo(RetroBoardAdditionalInfoModel model)
        {
            if (await this.repository.AnyAsync(
                    rb => rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId &&
                    rb.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                    rb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId))
            {
                // update if additional info exist 
            } 
            else
            {
                var newMaxIndexCount = 0;
                if (await repository.AnyAsync(x => x.Id != null))
                {
                    var maxRetroBoardAdditionalInfo = await repository.GetMax(rb => rb.RetroBoardIndexCount);
                    newMaxIndexCount = maxRetroBoardAdditionalInfo + 1;
                }

                var newRetroBoardAdditionalInfoToSave = new RetroBoardAdditionalInfo
                {
                    RetroBoardIndexCount = newMaxIndexCount,
                    RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                    TeamFirebaseDocId = model.TeamFirebaseDocId,
                    WorkspaceFirebaseDocId = model.WorkspaceFirebaseDocId,
                    RetroBoardActionCount = 0,
                };

                await this.repository.AddAsync(newRetroBoardAdditionalInfoToSave);
                await this.unitOfWork.CompleteAsync();
            }
        }

        public Task<object> GetRetroBoardAdditionalInfo(string retroBoardId)
        {
            throw new NotImplementedException();
        }

        public async Task<RetroBoardAdditionalInfoPreviousRetroBoardModel> GetRetroBoardAdditionalInfoPreviousRbId(string retroBoardId, string teamDocId, string workspaceDocId)
        {
            var findedCurrentRetroBoardAdditionalInfo = await this.repository.FirstOrDefaultAsync(rb =>
                rb.RetroBoardFirebaseDocId == retroBoardId &&
                rb.TeamFirebaseDocId == teamDocId &&
                rb.WorkspaceFirebaseDocId == workspaceDocId);

            var previousRetroBoardDocId = string.Empty;
            var shouldShowAction = false;

            if (findedCurrentRetroBoardAdditionalInfo != null)
            {
                var findedAllRetroBoardsAdditionalInfoInCurrentRb = (await this.repository.FindAsync(rb =>
                    rb.TeamFirebaseDocId == findedCurrentRetroBoardAdditionalInfo.TeamFirebaseDocId &&
                    rb.WorkspaceFirebaseDocId == findedCurrentRetroBoardAdditionalInfo.WorkspaceFirebaseDocId &&
                    rb.RetroBoardIndexCount <= findedCurrentRetroBoardAdditionalInfo.RetroBoardIndexCount)).ToList();

                var orderedfindedAllRetroBoardsAdditionalInfoInCurrentRb =
                    findedAllRetroBoardsAdditionalInfoInCurrentRb.OrderByDescending(x => x.RetroBoardIndexCount).ToList();

                var firstToRemoveFromList = orderedfindedAllRetroBoardsAdditionalInfoInCurrentRb.First();

                if (firstToRemoveFromList.RetroBoardFirebaseDocId == findedCurrentRetroBoardAdditionalInfo.RetroBoardFirebaseDocId)
                {
                    orderedfindedAllRetroBoardsAdditionalInfoInCurrentRb.Remove(firstToRemoveFromList);
                    var previousRetroBoardAdditonalInfo = orderedfindedAllRetroBoardsAdditionalInfoInCurrentRb.FirstOrDefault();

                    if (previousRetroBoardAdditonalInfo != null)
                    {
                        previousRetroBoardDocId = previousRetroBoardAdditonalInfo.RetroBoardFirebaseDocId;
                        shouldShowAction = previousRetroBoardAdditonalInfo.RetroBoardActionCount > 0;
                    }
                }
            }

            

            RetroBoardAdditionalInfoPreviousRetroBoardModel modelToReturn = new RetroBoardAdditionalInfoPreviousRetroBoardModel
            {
                PreviousRetroBoardDocId = previousRetroBoardDocId,
                ShouldShowPreviousActionsButton = shouldShowAction
            };

            return modelToReturn;
        }

        public async Task SetRetroBoardAdditionalInfoRetroBoardActionCount(RetroBoardAdditionalInfoRetroBoardActionCountModel model)
        {
            var findedCurrentRetroBoardAdditionalInfo = 
                await this.repository.FirstOrDefaultAsync(rb =>
                    rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId &&
                    rb.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                    rb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId);

            if (findedCurrentRetroBoardAdditionalInfo != null)
            {
                findedCurrentRetroBoardAdditionalInfo.RetroBoardActionCount = model.ActionsCount;

                this.repository.Update(findedCurrentRetroBoardAdditionalInfo);

                await this.unitOfWork.CompleteAsync();
            }
        }
    }
}
