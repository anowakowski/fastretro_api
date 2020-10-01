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
    public class UserSettingsServices : IUserSettingsServices
    {
        private readonly IRepository<UserSettings> repository;
        private readonly IUnitOfWork unitOfWork;

        public UserSettingsServices(IRepository<UserSettings> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task AddNewUserSettings(UserSettingsModel model)
        {
            var userSettings = new UserSettings
            {
                UserFirebaseDocId = model.UserFirebaseDocId,
                ChosenImageBackgroundName = model.ChosenImageBackgroundName
            };

            await this.repository.AddAsync(userSettings);
            await this.unitOfWork.CompleteAsync();
        }

        public async Task<UserSettings> GetUserSettings(string firebaseDocId)
        {
            var userSettings = await this.repository.FirstOrDefaultAsync(us => us.UserFirebaseDocId == firebaseDocId);

            return userSettings;
        }

        public async Task UpdateUserSettings(UserSettingsModel model)
        {
            var userSettings = await this.repository.FirstOrDefaultAsync(us => us.UserFirebaseDocId == model.UserFirebaseDocId);

            if (userSettings != null)
            {
                userSettings.ChosenImageBackgroundName = model.ChosenImageBackgroundName;
                this.repository.Update(userSettings);
                await this.unitOfWork.CompleteAsync();
            }
        }
    }
}
