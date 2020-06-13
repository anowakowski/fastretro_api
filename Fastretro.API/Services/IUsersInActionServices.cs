using System.Threading.Tasks;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IUsersInActionServices
    {
        Task SetUserInAction(UsersInActionModel model);
    }
}