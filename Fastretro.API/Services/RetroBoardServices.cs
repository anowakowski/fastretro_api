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
        private readonly IRepository<MergedRetroBoardCard> mergedRetroBoardCardRepository;
        private readonly IRepository<RetroBoardCardMergedGroup> retroBoardCardMergetGroupRepository;
        private readonly IUnitOfWork unitOfWork;

        public RetroBoardServices(
            IRepository<RetroBoard> retroBoardRepository,
            IRepository<RetroBoardCard> retroBoardCardRepository,
            IRepository<MergedRetroBoardCard> mergedRetroBoardCardRepository,
            IRepository<RetroBoardCardMergedGroup> retroBoardCardMergetGroupRepository,
            IUnitOfWork unitOfWork)
        {
            this.retroBoardRepository = retroBoardRepository;
            this.retroBoardCardRepository = retroBoardCardRepository;
            this.mergedRetroBoardCardRepository = mergedRetroBoardCardRepository;
            this.retroBoardCardMergetGroupRepository = retroBoardCardMergetGroupRepository;
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
            var findedRetroBoardCards = await this.retroBoardCardRepository.FindAsync(rbc => rbc.RetroBoardFirebaseDocId == retroBoardFirebaseDocId);

            var retroBoardCards = new List<RetroBoardCardGetModel>();

            findedRetroBoardCards
                .ToList()
                .ForEach(rbc =>
                {
                    var rbcToAdd = new RetroBoardCardGetModel
                    {
                        RetroBoardCardApiId = rbc.Id,
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

        public async Task<RetroBoardCard> SetRetroBoardCard(RetroBoardCardModel model)
        {
            var retroBoardCard = new RetroBoardCard
            {
                RetroBoardCardFirebaseDocId = model.RetroBoardCardFirebaseDocId,
                RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                Text = model.Text
            };

            await this.retroBoardCardRepository.AddAsync(retroBoardCard);
            await this.unitOfWork.CompleteAsync();

            return retroBoardCard;
        }

        public async Task UpdateRetroBoardCardtext(RetroBoardCardModel model)
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

        public async Task UpdateRetroBoardCardFirebaseDocId(RetroBoardCardModelAfterSaveForAddFirebaseDocId model)
        {
            var findedRetroBoardCardToUpdate =
                await this.retroBoardCardRepository.FirstOrDefaultAsync(rbc =>
                    rbc.Id == model.RetroBoardCardApiId &&
                    rbc.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId);

            if (findedRetroBoardCardToUpdate != null)
            {
                findedRetroBoardCardToUpdate.RetroBoardCardFirebaseDocId= model.RetroBoardCardFirebaseDocId;
                this.retroBoardCardRepository.Update(findedRetroBoardCardToUpdate);
                await this.unitOfWork.CompleteAsync();
            }
        }

        public async Task<RetroBoardCardMergedContentGetModel> SetRetroBoardCardMergetContent(RetroBoardCardMergedContentModel model)
        {
            var findedRetroBoardCardToMergeFrom = 
                await this.retroBoardCardRepository.FirstOrDefaultAsync(x => x.RetroBoardCardFirebaseDocId == model.RetroBoardCardToMergeFromFirebaseDocId);
            var findedRetroBoardCardToMergeToCurrent = 
                await this.retroBoardCardRepository.FirstOrDefaultAsync(x => x.RetroBoardCardFirebaseDocId == model.RetroBoardCardToMergeToCurrentFirebaseDocId);

            var retrunModel = new RetroBoardCardMergedContentGetModel();

            if (!findedRetroBoardCardToMergeFrom.IsMerged && !findedRetroBoardCardToMergeToCurrent.IsMerged)
            {
                var mergedGroup = new RetroBoardCardMergedGroup
                {
                    CreateDate = DateTime.Now
                };

                await this.retroBoardCardMergetGroupRepository.AddAsync(mergedGroup);
                await this.unitOfWork.CompleteAsync();

                var mergedRetroBoardCardFrom = new MergedRetroBoardCard
                {
                    RetroBoardCardMergedGroup = mergedGroup,
                    RetroBoardCard = findedRetroBoardCardToMergeFrom,
                    RetroBoardCardId = findedRetroBoardCardToMergeFrom.Id
                };

                var mergedRetroBoardCardTo = new MergedRetroBoardCard
                {
                    RetroBoardCardMergedGroup = mergedGroup,
                    RetroBoardCard = findedRetroBoardCardToMergeToCurrent,
                    RetroBoardCardId = findedRetroBoardCardToMergeToCurrent.Id
                };

                await this.mergedRetroBoardCardRepository.AddAsync(mergedRetroBoardCardFrom);
                await this.mergedRetroBoardCardRepository.AddAsync(mergedRetroBoardCardTo);

                await this.unitOfWork.CompleteAsync();

                findedRetroBoardCardToMergeFrom.IsHidenMergedChild = true;
                findedRetroBoardCardToMergeFrom.IsMerged = true;

                findedRetroBoardCardToMergeToCurrent.IsHidenMergedChild = true;
                findedRetroBoardCardToMergeToCurrent.IsMerged = true;

                this.retroBoardCardRepository.Update(findedRetroBoardCardToMergeFrom);
                this.retroBoardCardRepository.Update(findedRetroBoardCardToMergeToCurrent);

                await this.unitOfWork.CompleteAsync();

                var newParentMergedCard = new RetroBoardCard
                {
                    IsHidenMergedChild = false,
                    IsShowMergedParent = true,
                    IsMerged = true,
                    RetroBoardCardFirebaseDocId = string.Empty,
                    Text = string.Empty,
                    RetroBoardFirebaseDocId = findedRetroBoardCardToMergeFrom.RetroBoardFirebaseDocId
                };

                await this.retroBoardCardRepository.AddAsync(newParentMergedCard);
                await this.unitOfWork.CompleteAsync();

                retrunModel.MergedGroupId = mergedGroup.Id;
                retrunModel.RetroBoardCardApiId = newParentMergedCard.Id;
            }
            else if (findedRetroBoardCardToMergeFrom.IsMerged)
            {
                var findedMergedRetroBoardCard = await this.mergedRetroBoardCardRepository.FirstOrDefaultAsync(mrbc => mrbc.Id == findedRetroBoardCardToMergeFrom.Id);
                var mergetGroupId = findedMergedRetroBoardCard.RetroBoardCardMergedGroup.Id;
            }

            return retrunModel;
        }
    }
}
