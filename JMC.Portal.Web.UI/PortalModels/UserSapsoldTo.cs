using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class UserSapsoldTo
    {
        public long UserId { get; set; }
        public long SapsoldToId { get; set; }

        public virtual SapsoldTo SapsoldTo { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
