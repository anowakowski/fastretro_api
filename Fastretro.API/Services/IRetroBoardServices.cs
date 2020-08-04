using Fastretro.API.Models;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IRetroBoardServices
    {
        Task SetRetroBoard(RetroBoardModel model);
    }
}