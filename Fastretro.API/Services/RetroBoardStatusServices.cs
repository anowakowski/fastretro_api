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
                var currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

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
    }
}