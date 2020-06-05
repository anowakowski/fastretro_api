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
    public class RetroBoardOptionServices : IRetroBoardOptionServices
    {
        private readonly IRepository<RetroBoardOptions> retroBoardOptionsRepository;
        private readonly IUnitOfWork unitOfWork;

        public RetroBoardOptionServices(IRepository<RetroBoardOptions> retroBoardOptionsRepository, IUnitOfWork unitOfWork)
        {
            this.retroBoardOptionsRepository = retroBoardOptionsRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<RetroBoardOptions> GetRetroBoardOptions(string retroBoardId)
        {
            return await this.retroBoardOptionsRepository.FirstOrDefaultAsync(rb => rb.RetroBoardFirebaseDocId == retroBoardId);
        }

        public async Task SetRetroBoardOptions(RetroBoardOptionsModel model)
        {
            var isExistngRetroBoardOptionsForRetroBoardId =
                await this.retroBoardOptionsRepository.AnyAsync(rb => rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId);

            if (!isExistngRetroBoardOptionsForRetroBoardId)
            {
                RetroBoardOptions retroBoardOptionsToSave = new RetroBoardOptions
                {
                    RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId,
                    MaxVouteCount = model.MaxVouteCount,
                    ShouldBlurRetroBoardCardText = model.ShouldBlurRetroBoardCardText
                };

                await this.retroBoardOptionsRepository.AddAsync(retroBoardOptionsToSave);

                await unitOfWork.CompleteAsync();
            } 
            else
            {
                var findedRetroBoardOptions = await this.retroBoardOptionsRepository.FirstOrDefaultAsync(rb => rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId);

                findedRetroBoardOptions.MaxVouteCount = model.MaxVouteCount;
                findedRetroBoardOptions.ShouldBlurRetroBoardCardText = model.ShouldBlurRetroBoardCardText;

                this.retroBoardOptionsRepository.Update(findedRetroBoardOptions);

                await this.unitOfWork.CompleteAsync();
            }
        }
    }
}
