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
        public async Task SetRetroBoardStatus(RetroBoardStatusModel model)
        {
                var findedLastRetroBoard = (await this.repository.FindAsync(lrb => lrb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId)).ToList();

                var findedLastOpenedRetroBoard = findedLastRetroBoard.FirstOrDefault(lrb => !lrb.IsFinished);
                var findedLastFinishedRetroBoard = findedLastRetroBoard.FirstOrDefault(lrb => lrb.IsFinished);

                await AddedNonStartedNewRBIfNotExists(model, findedLastRetroBoard);

                // update if started to opened isStarted: true 
                // add new if finished isStardet: true isFinished: true

                //await updateIfNonStartedRule(model, findedLastOpenedRetroBoard);
                //await updateIfFinishedRule(model, findedLastFinishedRetroBoard);
        }

        private async Task AddedNonStartedNewRBIfNotExists(RetroBoardStatusModel model, List<RetroBoardStatus> findedLastRetroBoard)
        {
            if (findedLastRetroBoard.Any(lrb => lrb.WorkspaceFirebaseDocId != model.WorkspaceFirebaseDocId))
            {
                RetroBoardStatus retroBoardStatus = new RetroBoardStatus
                {
                    RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                    WorkspaceFirebaseDocId = model.WorkspaceFirebaseDocId,
                    TeamFirebaseDocId = model.TeamFirebaseDocId,
                    IsFinished = model.IsFinished,
                    IsStarted = model.IsStarted
                };

                await this.repository.AddAsync(retroBoardStatus);
                await this.unitOfWork.CompleteAsync();
            }
        }

        private async Task updateIfNonStartedRule(RetroBoardStatusModel model, RetroBoardStatus findedLastOpenedRetroBoard)
        {
            if (findedLastOpenedRetroBoard != null)
            {
                if (model.RetroBoardFirebaseDocId != findedLastOpenedRetroBoard.RetroBoardFirebaseDocId &&
                    !findedLastOpenedRetroBoard.IsFinished &&
                    !findedLastOpenedRetroBoard.IsStarted)
                {
                    findedLastOpenedRetroBoard.RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId;

                    this.repository.Update(findedLastOpenedRetroBoard);

                    await this.unitOfWork.CompleteAsync();
                }
            }
        }

        private async Task updateIfFinishedRule(RetroBoardStatusModel model, RetroBoardStatus findedLastFinishedRetroBoard)
        {
            if (model.IsFinished && findedLastFinishedRetroBoard != null)
            {
                if (model.RetroBoardFirebaseDocId != findedLastFinishedRetroBoard.RetroBoardFirebaseDocId)
                {
                    findedLastFinishedRetroBoard.RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId;

                    this.repository.Update(findedLastFinishedRetroBoard);

                    await this.unitOfWork.CompleteAsync();
                }
            }
        }
    }
}