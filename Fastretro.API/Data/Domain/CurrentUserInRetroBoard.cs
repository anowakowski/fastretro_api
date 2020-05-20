using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Data.Domain
{
    public class CurrentUserInRetroBoard : Entity 
    {
       public string RetroBoardId { get; set; }
       public ICollection<FirebaseUser> firebaseUsers { get; set; }
    }
}
