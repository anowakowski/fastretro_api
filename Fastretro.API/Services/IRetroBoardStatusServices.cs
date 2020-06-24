using System.Threading.Tasks;
using Fastretro.API.Models;

namespace Fastretro.API.Services
{
    public interface IRetroBoardStatusServices
    {
        Task SetRetroBoardStatus(RetroBoardStatusModel model);
    }
}