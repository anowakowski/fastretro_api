﻿using Fastretro.API.Data.Domain;
using Fastretro.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IRetroBoardServices
    {
        Task SetRetroBoard(RetroBoardModel model);
        Task SetRetroBoardCard(RetroBoardCardModel model);
        Task<RetroBoardGetModel> GetRetroBoard(string retroBoardFirebaseDocId);
        Task<IEnumerable<RetroBoardCardGetModel>> GetRetroBoardCards(string retroBoardFirebaseDocId);
        Task UpdateRetroBoardCard(RetroBoardCardModel model);
    }
}