using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class UserSettings : Entity
    {
        public string UserFirebaseDocId { get; set; }
        public string ChosenImageBackgroundName { get; set; }
    }
}
