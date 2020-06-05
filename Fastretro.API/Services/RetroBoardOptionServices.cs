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

        public RetroBoardOptionServices(IRepository<RetroBoardOptions> retroBoardOptionsRepository)
        {
            this.retroBoardOptionsRepository = retroBoardOptionsRepository;
        }

        public Task SetRetroBoardOptions(RetroBoardOptionsModel model)
        {
            throw new NotImplementedException();
        }
    }
}
