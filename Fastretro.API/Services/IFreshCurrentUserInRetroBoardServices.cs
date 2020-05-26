using Fastretro.API.Data.Domain;
using Fastretro.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IFreshCurrentUserInRetroBoardServices
    {
        Task<IEnumerable<FirebaseUserData>> SetUpFreshListOfCurrentUsers(GetFreshCurrentUserDataModel model);
    }
}