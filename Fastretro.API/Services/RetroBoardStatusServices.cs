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
    public class RetroBoardStatusServices : IRetroBoardStatusServices
    {
        private readonly IRepository<RetroBoardStatus> repository;
        private readonly IUnitOfWork unitOfWork;

        public RetroBoardStatusServices(IRepository<RetroBoardStatus> repository, IUnitOfWork unitOfWork) 
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RetroBoardStatus>> GetLastRetroBoardForWorkspace(string workspaceId)
        {
            var findedLastRetroBoards = await this.repository.FindAsync(lrb => lrb.WorkspaceFirebaseDocId == workspaceId);

            return findedLastRetroBoards.ToList();
        }

        public async Task SetNewRetroBoardStatus(RetroBoardStatusModel model)
        {
            string currentDate = GetCurrentDate();

            RetroBoardStatus retroBoardStatus = new RetroBoardStatus
            {
                RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                WorkspaceFirebaseDocId = model.WorkspaceFirebaseDocId,
                TeamFirebaseDocId = model.TeamFirebaseDocId,
                IsFinished = model.IsFinished,
                IsStarted = model.IsStarted,
                LastModifyDate = currentDate
            };

            await this.repository.AddAsync(retroBoardStatus);
            await this.unitOfWork.CompleteAsync();
        }

        public async Task SetRetroBoardAsStarted(RetroBoardStatusForSetRBAsStartedModel model) 
        {
            var findedRetroBoardStatus = await this.repository.FirstOrDefaultAsync(rb => rb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId && rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId);
            
            if (findedRetroBoardStatus != null)
            {
                findedRetroBoardStatus.IsStarted = true;
                findedRetroBoardStatus.LastModifyDate = GetCurrentDate();

                this.repository.Update(findedRetroBoardStatus);
                await this.unitOfWork.CompleteAsync();
            }
        }

        public async Task SetRetroBoardAsFinished(RetroBoardStatusForSetRBAsFinishedModel model) 
        {
            var findedRetroBoardStatus = await this.repository.FirstOrDefaultAsync(rb => rb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId && rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId);
            
            if (findedRetroBoardStatus != null && findedRetroBoardStatus.IsStarted)
            {
                findedRetroBoardStatus.IsFinished = true;
                findedRetroBoardStatus.LastModifyDate = GetCurrentDate();

                this.repository.Update(findedRetroBoardStatus);
                await this.unitOfWork.CompleteAsync();
            }
        }        

        private static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public async Task SetRetroBoardAsOpened(RetroBoardStatusForSetRBAsOpenedModel model)
        {
            var findedRetroBoardStatus = await this.repository.FirstOrDefaultAsync(rb => rb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId && rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId);
            
            if (findedRetroBoardStatus != null && findedRetroBoardStatus.IsStarted)
            {
                findedRetroBoardStatus.IsFinished = false;
                findedRetroBoardStatus.LastModifyDate = GetCurrentDate();

                this.repository.Update(findedRetroBoardStatus);
                await this.unitOfWork.CompleteAsync();
            }
        }
    }
}