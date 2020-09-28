using Fastretro.API.Data.Domain;
using Fastretro.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IUsersInTeamServices
    {
        Task SetUserInTeam(UsersInTeamModel model);
        Task RemoveUserInTeam(IEnumerable<UsersInTeamRemoveModel> model);
        Task<IEnumerable<UsersInTeam>> GetUsersInTeam(UsersInTeamToGetModel model);
    }
}