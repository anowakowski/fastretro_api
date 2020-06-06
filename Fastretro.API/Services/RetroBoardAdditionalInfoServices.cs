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
    public class RetroBoardAdditionalInfoServices : IRetroBoardAdditionalInfoServices
    {
        private readonly IRepository<RetroBoardAdditionalInfo> repository;
        private readonly IUnitOfWork unitOfWork;

        public RetroBoardAdditionalInfoServices(IRepository<RetroBoardAdditionalInfo> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public async Task SetRetroBoardAdditionalInfo(RetroBoardAdditionalInfoModel model)
        {
            if (await this.repository.AnyAsync(
                    rb => rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId &&
                    rb.TeamFirebaseDocId == model.TeamFirebaseDocId &&
                    rb.WorkspaceFirebaseDocId == model.WorkspaceFirebaseDocId))
            {
                // update if additional info exist 
            } 
            else
            {
                var maxRetroBoardAdditionalInfo = await repository.GetMax(rb => rb.RetroBoardIndexCount);
                var newMaxIndexCount = maxRetroBoardAdditionalInfo++;
                
                var newRetroBoardAdditionalInfoToSave = new RetroBoardAdditionalInfo
                {
                    RetroBoardIndexCount = newMaxIndexCount,
                    RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                    TeamFirebaseDocId = model.TeamFirebaseDocId,
                    WorkspaceFirebaseDocId = model.WorkspaceFirebaseDocId
                };

                await this.repository.AddAsync(newRetroBoardAdditionalInfoToSave);
                await this.unitOfWork.CompleteAsync();
            }
            

        }
    }
}
