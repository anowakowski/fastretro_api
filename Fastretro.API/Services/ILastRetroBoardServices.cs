using System.Threading.Tasks;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface ILastRetroBoardServices
    {
        Task SetLastRetroBoardId(LastRetroBoardModel model);
    }
}