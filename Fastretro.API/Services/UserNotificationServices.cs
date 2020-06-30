using System.Threading.Tasks;
using Fastretro.API.Data;
using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public class UserNotificationServices : IUserNotificationServices
    {
        private readonly IRepository<UserNotification> repository;
        private readonly IUnitOfWork unitOfWork;

        public UserNotificationServices(IRepository<UserNotification> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public async Task SetUserNotification(UserNotificationModel model)
        {
            var userNotification = new UserNotification 
            {
                UserWantToJoinFirebaseId = model.UserWantToJoinFirebaseId,
                CreatorUserFirebaseId = model.CreatorUserFirebaseId,
                WorkspceWithRequiredAccessFirebaseId = model.WorkspceWithRequiredAccessFirebaseId,
                NotyficationType = "WorkspaceWithRequiredAccess"
            };

            await this.repository.AddAsync(userNotification);
            await this.unitOfWork.CompleteAsync();
        }
    }
}