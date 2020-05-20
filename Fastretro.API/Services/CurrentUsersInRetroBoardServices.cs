using Fastretro.API.Data.Domain;
using Fastretro.API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public class CurrentUsersInRetroBoardServices : ICurrentUsersInRetroBoardServices
    {
        private readonly IRepository<FirebaseUserData> firebaseUserDataRepository;

        public CurrentUsersInRetroBoardServices(IRepository<FirebaseUserData> firebaseUserDataRepository)
        {
            this.firebaseUserDataRepository = firebaseUserDataRepository;
        }
        public async Task<IEnumerable<FirebaseUserData>> GetCurrentUsersInRetroBoard(string retroBoardId, string firebaseUserDocId)
        {
           return await this.firebaseUserDataRepository.FindAsync(x => x.FirebaseUserDocId == firebaseUserDocId);
        }
    }
}
