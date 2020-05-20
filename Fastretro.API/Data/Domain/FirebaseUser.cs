using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class FirebaseUser : Entity
    {
        public string FirebaseUserDocId { get; set; }
        public CurrentUserInRetroBoard CurrentUserInRetroBoard { get; set; }
    }
}
