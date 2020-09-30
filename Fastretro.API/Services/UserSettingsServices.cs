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

        public Task<UserSettings> GetUserSettings(UserSettingsModel model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserSettings(UserSettingsModel model)
        {
            throw new NotImplementedException();
        }
    }
}
