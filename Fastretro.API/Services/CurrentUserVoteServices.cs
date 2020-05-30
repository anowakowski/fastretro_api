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
    public class CurrentUserVoteServices : ICurrentUserVoteServices
    {
        private readonly IRepository<CurrentUserVote> currentUserVoteRepository;
        private readonly IUnitOfWork unitOfWork;

        public CurrentUserVoteServices(IRepository<CurrentUserVote> currentUserVoteRepository, IUnitOfWork unitOfWork)
        {
            this.currentUserVoteRepository = currentUserVoteRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task AddUserVote(CurrentUserVoteModel model)
        {
            CurrentUserVote currentUserVote = new CurrentUserVote
            {
                UserId = model.UserId,
                RetroBoardCardId = model.RetroBoardCardId,
                RetroBoardId = model.RetroBoardId
            };

            await this.currentUserVoteRepository.AddAsync(currentUserVote);

            await this.unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<CurrentUserVote>> GetCurrentUserVoteInRetroBoard(string retroBoardId)
        {
            var findedCurrentUserVotes =
                await this.currentUserVoteRepository.FindAsync(
                    x => x.RetroBoardId == retroBoardId);

            return findedCurrentUserVotes;
        }

        public async Task<int> GetUserVoteCount(string retroBoardId, string userId)
        {
            var findedUserVoteCount =
                await this.currentUserVoteRepository.FindAsync(x => x.RetroBoardId == retroBoardId && x.UserId == userId);

            return findedUserVoteCount.ToList().Count();
        }

        public async Task RemoveUserVote(CurrentUserVoteModel model)
        {
            var findedCurrentUserVoteToRemove =
               await this.currentUserVoteRepository.FirstOrDefaultAsync(x => x.RetroBoardId == model.RetroBoardId && x.RetroBoardCardId == model.RetroBoardCardId && x.UserId == model.UserId);

            this.currentUserVoteRepository.Delete(findedCurrentUserVoteToRemove);

            await unitOfWork.CompleteAsync();
        }
    }
}
