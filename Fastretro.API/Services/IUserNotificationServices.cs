using System.Threading.Tasks;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IUserNotificationServices
    {
        Task SetUserNotification(UserNotificationModel model);
    }
}