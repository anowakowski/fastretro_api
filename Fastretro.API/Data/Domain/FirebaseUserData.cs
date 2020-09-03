using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class FirebaseUserData : Entity
    {
        public string FirebaseUserDocId { get; set; }
        public CurrentUserInRetroBoard CurrentUserInRetroBoard { get; set; }
        public string DateOfExistingCheck { get; set; }
        public string ChosenAvatarName { get; set; }
        public string DisplayName { get; set; }
    }
}
