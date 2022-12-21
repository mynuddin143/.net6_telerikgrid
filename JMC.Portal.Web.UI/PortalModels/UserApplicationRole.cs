using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class UserApplicationRole
    {
        public long UserId { get; set; }
        public long ApplicationRoleId { get; set; }

        public virtual ApplicationRole ApplicationRole { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
