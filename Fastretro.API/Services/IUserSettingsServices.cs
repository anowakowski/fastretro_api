using Fastretro.API.Data.Domain;
using Fastretro.API.Models;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IUserSettingsServices
    {
        Task AddNewUserSettings(UserSettingsModel model);
        Task UpdateUserSettings(UserSettingsModel model);
        Task<UserSettings> GetUserSettings(UserSettingsModel model);
    }
}