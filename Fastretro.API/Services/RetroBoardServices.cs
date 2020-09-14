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
        private readonly IRepository<RetroBoardActionCard> retroBoardActionCardRepository;
        private readonly IUnitOfWork unitOfWork;

        public RetroBoardServices(
            IRepository<RetroBoard> retroBoardRepository,
            IRepository<RetroBoardCard> retroBoardCardRepository,
            IRepository<MergedRetroBoardCard> mergedRetroBoardCardRepository,
            IRepository<RetroBoardCardMergedGroup> retroBoardCardMergetGroupRepository,
            IRepository<RetroBoardActionCard> retroBoardActionCardRepository,
            IUnitOfWork unitOfWork)
        {
            this.retroBoardRepository = retroBoardRepository;
            this.retroBoardCardRepository = retroBoardCardRepository;
            this.mergedRetroBoardCardRepository = mergedRetroBoardCardRepository;
            this.retroBoardCardMergetGroupRepository = retroBoardCardMergetGroupRepository;
            this.retroBoardActionCardRepository = retroBoardActionCardRepository;
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
            var findedRetroBoardCards = await this.retroBoardCardRepository.FindAsyncWithIncludedEntities(
                rbc => 
                    rbc.RetroBoardFirebaseDocId == retroBoardFirebaseDocId && 
                    rbc.IsHidenMergedChild == false,
                include => include.MergetRetroBoardCards,
                include => include.RetroBoardCardMergedGroup);

            var retroBoardCards = new List<RetroBoardCardGetModel>();

            foreach(var findedRetroBoardCard in findedRetroBoardCards)
            {

                var rbcToAdd = new RetroBoardCardGetModel
                {
                    RetroBoardCardApiId = findedRetroBoardCard.Id,
                    RetroBoardCardFirebaseDocId = findedRetroBoardCard.RetroBoardCardFirebaseDocId,
                    RetroBoardFirebaseDocId = findedRetroBoardCard.RetroBoardFirebaseDocId,
                    Text = findedRetroBoardCard.Text,
                    IsMerged = findedRetroBoardCard.IsMerged,
                    MergedContent = new List<string>()
                };

                if (findedRetroBoardCard.IsMerged)
                {
                    rbcToAdd.MergedContent = new List<string>();

                    var retroBoardMergedGroup = findedRetroBoardCard.RetroBoardCardMergedGroup;
                    var mergedCards = await this.retroBoardCardRepository.FindAsyncWithIncludedEntityAsync(
                        rbc => rbc.RetroBoardCardMergedGroup.Id == retroBoardMergedGroup.Id,
                        include => include.RetroBoardCardMergedGroup);


                    foreach (var mc in mergedCards)
                    {
                        if (mc.IsHidenMergedChild)
                        {
                            rbcToAdd.MergedContent.Add(mc.Text);
                        }
                    }
                }

                retroBoardCards.Add(rbcToAdd);
            }

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
                Text = model.Text,
                UserFirebaseDocId = model.UserFirebaseDocId
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
                await this.retroBoardCardRepository.FirstOrDefaulAsyncWithIncludedEntities(
                    rbc => rbc.RetroBoardCardFirebaseDocId == model.RetroBoardCardToMergeFromFirebaseDocId,
                    include => include.RetroBoardCardMergedGroup,
                    include => include.MergetRetroBoardCards);

            var findedRetroBoardCardToMergeToCurrent =
                await this.retroBoardCardRepository.FirstOrDefaulAsyncWithIncludedEntities(
                    rbc => rbc.RetroBoardCardFirebaseDocId == model.RetroBoardCardToMergeToCurrentFirebaseDocId,
                    include => include.RetroBoardCardMergedGroup,
                    include => include.MergetRetroBoardCards);

            var retrunModel = new RetroBoardCardMergedContentGetModel();

            await MergeCardsWithMergeRules(findedRetroBoardCardToMergeFrom, findedRetroBoardCardToMergeToCurrent, retrunModel);

            return retrunModel;
        }

        public async Task SetRetroBoardMergedFirebaseDocId(RetroBoardCardMergedSetCardFirebaseIdModel model)
        {
            var findedRetroBoardCard = await this.retroBoardCardRepository.FirstOrDefaultAsync(rbc => rbc.Id == model.RetroBoardCardApiId);

            findedRetroBoardCard.RetroBoardCardFirebaseDocId = model.RetroBoardCardFirebaseDocId;

            this.retroBoardCardRepository.Update(findedRetroBoardCard);
            await this.unitOfWork.CompleteAsync();
        }

        public async Task<RetroBoardCardUnMergedContentGetModel> SetRetroBoardCardUnmerged(RetroBoardCardUnMergedContentModel model)
        {
            var findedRetroBoardCardMergedParent = await this.retroBoardCardRepository.FirstOrDefaulAsyncWithIncludedEntities(
                rbc =>
                    rbc.Id == model.RetroBoardCardApiId && rbc.IsShowMergedParent,
                include => include.MergetRetroBoardCards,
                include => include.RetroBoardCardMergedGroup);

            var mergedChildCardsToMakeVisible = (await this.mergedRetroBoardCardRepository.FindAsyncWithIncludedEntities(
                mrbc => mrbc.RetroBoardCardMergedGroup.Id == findedRetroBoardCardMergedParent.RetroBoardCardMergedGroup.Id,
                include => include.RetroBoardCardMergedGroup,
                include => include.RetroBoardCard)).ToList();

            var unMergedRetroBoardCardsModel = new RetroBoardCardUnMergedContentGetModel
            {
                ChildRetroBoardCards = new List<RetroBoardCardAfterUnmergeGetModel>()
            };

            foreach (var mctmv in mergedChildCardsToMakeVisible)
            {
                if (mctmv.RetroBoardCard.IsHidenMergedChild)
                {
                    mctmv.RetroBoardCard.IsMerged = false;
                    mctmv.RetroBoardCard.IsHidenMergedChild = false;
                    unMergedRetroBoardCardsModel.ChildRetroBoardCards.Add(new RetroBoardCardAfterUnmergeGetModel
                    {
                        IsMerged = mctmv.RetroBoardCard.IsMerged,
                        RetroBoardCardApiId = mctmv.RetroBoardCard.Id,
                        RetroBoardFirebaseDocId = mctmv.RetroBoardCard.RetroBoardFirebaseDocId,
                        UserFirebaseDocId = mctmv.RetroBoardCard.UserFirebaseDocId
                    });
                }
            }

            this.retroBoardCardRepository.UpdateRange(mergedChildCardsToMakeVisible.Select(rc => rc.RetroBoardCard));
            
            this.mergedRetroBoardCardRepository.DeleteRange(mergedChildCardsToMakeVisible);
            await this.unitOfWork.CompleteAsync();

            this.retroBoardCardMergetGroupRepository.Delete(findedRetroBoardCardMergedParent.RetroBoardCardMergedGroup);
            await this.unitOfWork.CompleteAsync();

            this.retroBoardCardRepository.Delete(findedRetroBoardCardMergedParent);
            await this.unitOfWork.CompleteAsync();

            return unMergedRetroBoardCardsModel;
        }

        public async Task RemoveRetroBoardCard(RetroBoardCardRemoveModel model)
        {
            var findedRetroBoardCardToRemove = await this.retroBoardCardRepository.FirstOrDefaultAsync(rbc => rbc.Id == model.RetroBoardCardApiId);

            this.retroBoardCardRepository.Delete(findedRetroBoardCardToRemove);
            await this.unitOfWork.CompleteAsync();
        }

        public async Task<RetroBoardActionCardGetAfrerAddModel> SetRetroBoardAction(RetroBoardActionCardModel model)
        {
            var retroBoardActionToSave = new RetroBoardActionCard
            {
                RetroBoardActionCardFirebaseDocId = model.RetroBoardActionCardFirebaseDocId,
                RetroBoardCardFirebaseDocId = model.RetroBoardCardFirebaseDocId,
                RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                Text = model.Text
            };

            await this.retroBoardActionCardRepository.AddAsync(retroBoardActionToSave);
            await this.unitOfWork.CompleteAsync();

            var retroBoardActionModelToGet = new RetroBoardActionCardGetAfrerAddModel
            {
                RetroBoardApiDocId = retroBoardActionToSave.Id,
                RetroBoardActionCardFirebaseDocId = model.RetroBoardActionCardFirebaseDocId
            };

            return retroBoardActionModelToGet;
        }

        public async Task<RetroBoardActionCardGetReturnModel> GetRetroBoardAction(int retroBoardActionCardId)
        {
            var retroBoardActionCard = 
                await this.retroBoardActionCardRepository.FirstOrDefaultAsync(ac => ac.Id == retroBoardActionCardId);

            var modelToReturn = new RetroBoardActionCardGetReturnModel {
                RetroBoardActionCardFirebaseDocId = retroBoardActionCard.RetroBoardActionCardFirebaseDocId,
                RetroBoardApiDocId = retroBoardActionCard.Id,
                Text = retroBoardActionCard.Text
            };

            return modelToReturn;    
        }

        private async Task MergeCardsWithMergeRules(RetroBoardCard findedRetroBoardCardToMergeFrom, RetroBoardCard findedRetroBoardCardToMergeToCurrent, RetroBoardCardMergedContentGetModel retrunModel)
        {
            if (!findedRetroBoardCardToMergeFrom.IsMerged && !findedRetroBoardCardToMergeToCurrent.IsMerged)
            {
                RetroBoardCardMergedGroup mergedGroup = await CreatMergedGroupForRetroBoardCard();

                await AddedExistingRetroBoardCardsToMergedGropu(findedRetroBoardCardToMergeFrom, findedRetroBoardCardToMergeToCurrent, mergedGroup);
                await UpdateExistingCardWithMergedInfo(findedRetroBoardCardToMergeFrom, findedRetroBoardCardToMergeToCurrent, mergedGroup);

                RetroBoardCard newParentMergedCard = await CreateNewParentRetroBoardCard(findedRetroBoardCardToMergeFrom, mergedGroup);
                await AddParentCardToMergedGroup(mergedGroup, newParentMergedCard);

                retrunModel.MergedGroupId = mergedGroup.Id;
                retrunModel.RetroBoardCardApiId = newParentMergedCard.Id;
            }
            else if (!findedRetroBoardCardToMergeFrom.IsMerged && findedRetroBoardCardToMergeToCurrent.IsMerged && findedRetroBoardCardToMergeToCurrent.IsShowMergedParent)
            {
                var mergedGroup = findedRetroBoardCardToMergeToCurrent.RetroBoardCardMergedGroup;

                var mergedRetroBoardCardFrom = PrepareMergedRetroBoardCard(findedRetroBoardCardToMergeFrom, mergedGroup);
                await this.mergedRetroBoardCardRepository.AddAsync(mergedRetroBoardCardFrom);

                this.PrepareToUpdateWithMergedInfo(findedRetroBoardCardToMergeFrom, mergedGroup);

                this.retroBoardCardRepository.Update(findedRetroBoardCardToMergeFrom);
                await this.unitOfWork.CompleteAsync();

                retrunModel.MergedGroupId = mergedGroup.Id;
                retrunModel.RetroBoardCardApiId = findedRetroBoardCardToMergeToCurrent.Id;
            }
            else if (findedRetroBoardCardToMergeFrom.IsMerged && findedRetroBoardCardToMergeFrom.IsShowMergedParent && !findedRetroBoardCardToMergeToCurrent.IsMerged)
            {
                var mergedGroup = findedRetroBoardCardToMergeFrom.RetroBoardCardMergedGroup;

                var mergedRetroBoardCardFrom = PrepareMergedRetroBoardCard(findedRetroBoardCardToMergeToCurrent, mergedGroup);
                await this.mergedRetroBoardCardRepository.AddAsync(mergedRetroBoardCardFrom);

                this.PrepareToUpdateWithMergedInfo(findedRetroBoardCardToMergeToCurrent, mergedGroup);

                this.retroBoardCardRepository.Update(findedRetroBoardCardToMergeToCurrent);
                await this.unitOfWork.CompleteAsync();

                retrunModel.MergedGroupId = mergedGroup.Id;
                retrunModel.RetroBoardCardApiId = findedRetroBoardCardToMergeFrom.Id;
            }
            else if (
                findedRetroBoardCardToMergeFrom.IsMerged &&
                findedRetroBoardCardToMergeFrom.IsShowMergedParent &&
                findedRetroBoardCardToMergeToCurrent.IsMerged &&
                findedRetroBoardCardToMergeToCurrent.IsShowMergedParent)
            {
                RetroBoardCardMergedGroup mergedGroupFrom = await MergeCardForMergedToMergedProcess(findedRetroBoardCardToMergeFrom, findedRetroBoardCardToMergeToCurrent);

                retrunModel.MergedGroupId = mergedGroupFrom.Id;
                retrunModel.RetroBoardCardApiId = findedRetroBoardCardToMergeFrom.Id;
            }
            else
            {
                // can't merge
            }
        }

        private async Task<RetroBoardCardMergedGroup> MergeCardForMergedToMergedProcess(RetroBoardCard findedRetroBoardCardToMergeFrom, RetroBoardCard findedRetroBoardCardToMergeToCurrent)
        {
            var mergedGroupFrom = findedRetroBoardCardToMergeFrom.RetroBoardCardMergedGroup;

            var currentMergedCards = (await this.mergedRetroBoardCardRepository.FindAsyncWithIncludedEntities(
                mrbc => mrbc.RetroBoardCardMergedGroup.Id == findedRetroBoardCardToMergeToCurrent.RetroBoardCardMergedGroup.Id,
                include => include.RetroBoardCardMergedGroup,
                include => include.RetroBoardCard))
             .ToList();

            foreach (var cmc in currentMergedCards)
            {
                cmc.RetroBoardCardMergedGroup = mergedGroupFrom;
            }

            this.mergedRetroBoardCardRepository.UpdateRange(currentMergedCards);

            var currentMergedCradsToUpdate = currentMergedCards.Where(x => x.RetroBoardCard.IsHidenMergedChild);

            foreach (var cmctu in currentMergedCradsToUpdate)
            {
                cmctu.RetroBoardCard.RetroBoardCardMergedGroup = mergedGroupFrom;
            }

            this.retroBoardCardRepository.UpdateRange(currentMergedCradsToUpdate.Select(x => x.RetroBoardCard));

            var currentParentToDelete = currentMergedCards.FirstOrDefault(x => x.RetroBoardCard.IsShowMergedParent);

            this.mergedRetroBoardCardRepository.Delete(currentParentToDelete);

            this.retroBoardCardRepository.Delete(findedRetroBoardCardToMergeToCurrent);
            await this.unitOfWork.CompleteAsync();
            return mergedGroupFrom;
        }

        private async Task AddParentCardToMergedGroup(RetroBoardCardMergedGroup mergedGroup, RetroBoardCard newParentMergedCard)
        {
            var mergedParentRetroBoardCardTo = new MergedRetroBoardCard
            {
                RetroBoardCardMergedGroup = mergedGroup,
                RetroBoardCard = newParentMergedCard,
                RetroBoardCardId = newParentMergedCard.Id
            };

            await this.mergedRetroBoardCardRepository.AddAsync(mergedParentRetroBoardCardTo);
            await this.unitOfWork.CompleteAsync();
        }

        private async Task<RetroBoardCard> CreateNewParentRetroBoardCard(RetroBoardCard findedRetroBoardCardToMergeFrom, RetroBoardCardMergedGroup mergedGroup)
        {
            var newParentMergedCard = new RetroBoardCard
            {
                IsHidenMergedChild = false,
                IsShowMergedParent = true,
                IsMerged = true,
                RetroBoardCardFirebaseDocId = string.Empty,
                Text = string.Empty,
                RetroBoardFirebaseDocId = findedRetroBoardCardToMergeFrom.RetroBoardFirebaseDocId,
                RetroBoardCardMergedGroup = mergedGroup
            };

            await this.retroBoardCardRepository.AddAsync(newParentMergedCard);
            await this.unitOfWork.CompleteAsync();
            return newParentMergedCard;
        }

        private async Task UpdateExistingCardWithMergedInfo(RetroBoardCard findedRetroBoardCardToMergeFrom, RetroBoardCard findedRetroBoardCardToMergeToCurrent, RetroBoardCardMergedGroup mergedGroup)
        {
            this.PrepareToUpdateWithMergedInfo(findedRetroBoardCardToMergeFrom, mergedGroup);
            this.PrepareToUpdateWithMergedInfo(findedRetroBoardCardToMergeToCurrent, mergedGroup);

            this.retroBoardCardRepository.Update(findedRetroBoardCardToMergeFrom);
            this.retroBoardCardRepository.Update(findedRetroBoardCardToMergeToCurrent);

            await this.unitOfWork.CompleteAsync();
        }

        private void PrepareToUpdateWithMergedInfo(RetroBoardCard retroBoardCard, RetroBoardCardMergedGroup retroBoadCardMergedGroup)
        {
            retroBoardCard.IsHidenMergedChild = true;
            retroBoardCard.IsMerged = true;
            retroBoardCard.RetroBoardCardMergedGroup = retroBoadCardMergedGroup;
        }

        private async Task AddedExistingRetroBoardCardsToMergedGropu(RetroBoardCard findedRetroBoardCardToMergeFrom, RetroBoardCard findedRetroBoardCardToMergeToCurrent, RetroBoardCardMergedGroup mergedGroup)
        {
            var mergedRetroBoardCardFrom = PrepareMergedRetroBoardCard(findedRetroBoardCardToMergeFrom, mergedGroup);
            var mergedRetroBoardCardTo = PrepareMergedRetroBoardCard(findedRetroBoardCardToMergeToCurrent, mergedGroup);

            await this.mergedRetroBoardCardRepository.AddAsync(mergedRetroBoardCardFrom);
            await this.mergedRetroBoardCardRepository.AddAsync(mergedRetroBoardCardTo);

            await this.unitOfWork.CompleteAsync();
        }

        private static MergedRetroBoardCard PrepareMergedRetroBoardCard(RetroBoardCard findedRetroBoardCardToMergeFrom, RetroBoardCardMergedGroup mergedGroup)
        {
            return new MergedRetroBoardCard
            {
                RetroBoardCardMergedGroup = mergedGroup,
                RetroBoardCard = findedRetroBoardCardToMergeFrom,
                RetroBoardCardId = findedRetroBoardCardToMergeFrom.Id
            };
        }

        private async Task<RetroBoardCardMergedGroup> CreatMergedGroupForRetroBoardCard()
        {
            var mergedGroup = new RetroBoardCardMergedGroup
            {
                CreateDate = DateTime.Now
            };

            await this.retroBoardCardMergetGroupRepository.AddAsync(mergedGroup);
            await this.unitOfWork.CompleteAsync();
            return mergedGroup;
        }
    }
}
