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
    public class RetroBoardServices : IRetroBoardServices
    {
        private readonly IRepository<RetroBoard> retroBoardRepository;
        private readonly IRepository<RetroBoardCard> retroBoardCardRepository;
        private readonly IUnitOfWork unitOfWork;

        public RetroBoardServices(
            IRepository<RetroBoard> retroBoardRepository,
            IRepository<RetroBoardCard> retroBoardCardRepository,
            IUnitOfWork unitOfWork)
        {
            this.retroBoardRepository = retroBoardRepository;
            this.retroBoardCardRepository = retroBoardCardRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<RetroBoardGetModel> GetRetroBoard(string retroBoardFirebaseDocId)
        {
            var findedRetroBoard = await this.retroBoardRepository.FirstOrDefaultAsync(rb => rb.RetroBoardFirebaseDocId == retroBoardFirebaseDocId);

            if (findedRetroBoard != null)
            {
                var retroBoardModel = new RetroBoardGetModel
                {
                    RetroBoardFirebaseDocId = findedRetroBoard.RetroBoardFirebaseDocId,
                    RetroBoardName = findedRetroBoard.RetroBoardName,
                    SprintNumber = findedRetroBoard.SprintNumber
                };

                return retroBoardModel;
            }

            return null;
        }

        public async Task<IEnumerable<RetroBoardCardGetModel>> GetRetroBoardCards(string retroBoardFirebaseDocId)
        {
            var findedRetroBoardCards = await this.retroBoardCardRepository.FindAsync(rbc => rbc.RetroBoardCardFirebaseDocId == retroBoardFirebaseDocId);

            var retroBoardCards = new List<RetroBoardCardGetModel>();

            findedRetroBoardCards
                .ToList()
                .ForEach(rbc =>
                {
                    var rbcToAdd = new RetroBoardCardGetModel
                    {
                        RetroBoardCardFirebaseDocId = rbc.RetroBoardCardFirebaseDocId,
                        RetroBoardFirebaseDocId = rbc.RetroBoardFirebaseDocId,
                        Text = rbc.Text
                    };

                    retroBoardCards.Add(rbcToAdd);
                });

            return retroBoardCards;
        }

        public async Task SetRetroBoard(RetroBoardModel model)
        {
            var retroBoard = new RetroBoard
            {
                RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                RetroBoardName = model.RetroBoardName,
                SprintNumber = model.SprintNumber
            };

            await this.retroBoardRepository.AddAsync(retroBoard);
            await this.unitOfWork.CompleteAsync();
        }

        public async Task SetRetroBoardCard(RetroBoardCardModel model)
        {
            var retroBoardCard = new RetroBoardCard
            {
                RetroBoardCardFirebaseDocId = model.RetroBoardCardFirebaseDocId,
                RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                Text = model.Text
            };

            await this.retroBoardCardRepository.AddAsync(retroBoardCard);
            await this.unitOfWork.CompleteAsync();
        }

        public async Task UpdateRetroBoardCard(RetroBoardCardModel model)
        {
            var findedRetroBoardCardToUpdate =
                await this.retroBoardCardRepository.FirstOrDefaultAsync(rbc =>
                    rbc.RetroBoardCardFirebaseDocId == model.RetroBoardCardFirebaseDocId &&
                    rbc.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId);

            if (findedRetroBoardCardToUpdate != null)
            {
                findedRetroBoardCardToUpdate.Text = model.Text;
                this.retroBoardCardRepository.Update(findedRetroBoardCardToUpdate);
                await this.unitOfWork.CompleteAsync();
            }
        }
    }
}
