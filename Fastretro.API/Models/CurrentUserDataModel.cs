using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class CurrentUserDataModel
    {
        public string UserId { get; set; }
        public string RetroBoardId { get; set; }
        public string ChosenAvatarUrl { get; set; }
        public string DisplayName { get; set; }
    }
}
