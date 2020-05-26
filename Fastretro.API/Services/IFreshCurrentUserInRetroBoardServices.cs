using Fastretro.API.Models;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IFreshCurrentUserInRetroBoardServices
    {
        Task SetUpFreshListOfCurrentUsers(GetFreshCurrentUserDataModel model);
    }
}