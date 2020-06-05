using Fastretro.API.Models;
using System.Threading.Tasks;

namespace Fastretro.API.Services
{
    public interface IRetroBoardOptionServices
    {
        public Task SetRetroBoardOptions(RetroBoardOptionsModel model);
    }
}