using System.Threading.Tasks;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IUserInActionServices
    {
        Task SetUserInAction(UsersInActionModel model);
    }
}