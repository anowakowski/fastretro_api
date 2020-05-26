using Fastretro.API.Data.Domain;
using Fastretro.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface ICurrentUserVoteServices
    {
        Task AddUserVote(CurrentUserVoteModel model);
        Task<IEnumerable<CurrentUserVote>> GetCurrentUserVoteInRetroBoard(CurrentUserVoteModel model)
    }
}