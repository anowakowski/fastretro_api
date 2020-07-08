using Fastretro.API.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fastretro.API.Models
{
    public class GetAllNotificationTypeModel
    {
        public IEnumerable<UserNotificationWorkspaceWithRequiredAccess> UserNotificationWorkspaceWithRequiredAccesses { get; set; }
        public IEnumerable<UserNotificationWorkspaceWithRequiredAccessResponse> UserNotificationWorkspaceWithRequiredAccessResponses { get; set; }
    }
}
