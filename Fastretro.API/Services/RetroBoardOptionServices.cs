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

        public async Task SetRetroBoardOptions(RetroBoardOptionsModel model)
        {
            var isExistngRetroBoardOptionsForRetroBoardId =
                await this.retroBoardOptionsRepository.AnyAsync(rb => rb.RetroBoardFirebaseDocId == model.RetroBoardFirebaseDocId);

            if (!isExistngRetroBoardOptionsForRetroBoardId)
            {
                RetroBoardOptions retroBoardOptionsToSave = new RetroBoardOptions
                {
                    RetroBoardFirebaseDocId = model.RetroBoardFirebaseDocId
                };

                await this.retroBoardOptionsRepository.AddAsync(retroBoardOptionsToSave);

                await unitOfWork.CompleteAsync();
            }
        }
    }
}
