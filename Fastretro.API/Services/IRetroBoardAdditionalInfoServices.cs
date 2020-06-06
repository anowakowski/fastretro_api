using Fastretro.API.Models;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IRetroBoardAdditionalInfoServices
    {
        Task SetRetroBoardAdditionalInfo(RetroBoardAdditionalInfoModel model);
    }
}