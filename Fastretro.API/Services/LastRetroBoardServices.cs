using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fastretro.API.Data;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public class LastRetroBoardServices : ILastRetroBoardServices
    {
        private readonly IRepository<LastRetroBoard> repository;
        private readonly IUnitOfWork unitOfWork;

        public LastRetroBoardServices(IRepository<LastRetroBoard> repository, IUnitOfWork unitOfWork) 
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task SetLastRetroBoardId(LastRetroBoardModel model)
        {
            if (await this.repository.AnyAsync(lrb =>lrb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId))
            {
                var findedLastRetroBoard = (await this.repository.FindAsync(lrb => lrb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId)).ToList();

                var findedLastOpenedRetroBoard = findedLastRetroBoard.FirstOrDefault(lrb => !lrb.IsFinished);
                var findedLastFinishedRetroBoard = findedLastRetroBoard.FirstOrDefault(lrb => lrb.IsFinished);

                await AddedIfNotExists(model, findedLastRetroBoard);
                await updateIfFinished(model, findedLastFinishedRetroBoard);
                await updateIfOpened(model, findedLastOpenedRetroBoard);
            }
        }

        private async Task AddedIfNotExists(LastRetroBoardModel model, List<LastRetroBoard> findedLastRetroBoard)
        {
            if (!findedLastRetroBoard.Any())
            {
                LastRetroBoard lastRetroBoard = new LastRetroBoard
                {
                    RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                    WorkspaceFirebaseDocId = model.WorkspaceFirebaseDocId,
                    TeamFirebaseDocId = model.TeamFirebaseDocId,
                    IsFinished = model.IsFinished
                };

                await this.repository.AnyAsync(lastRetroBoard);

                await this.unitOfWork.CompleteAsync();
            }
        }

        private async Task updateIfOpened(LastRetroBoardModel model, LastRetroBoard findedLastOpenedRetroBoard)
        {
            if (!model.IsFinished && findedLastOpenedRetroBoard != null)
            {
                if (model.RetroBoardFirebaseDocId != findedLastOpenedRetroBoard.RetroBoardFirebaseDocId)
                {
                    findedLastOpenedRetroBoard.RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId;

                    this.repository.Update(findedLastOpenedRetroBoard);

                    await this.unitOfWork.CompleteAsync();
                }
            }
        }

        private async Task updateIfFinished(LastRetroBoardModel model, LastRetroBoard findedLastFinishedRetroBoard)
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