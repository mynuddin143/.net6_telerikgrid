using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapcustomerGroupUser
    {
        public long SapcustomerGroupUserId { get; set; }
        public long SapcustomerGroupId { get; set; }
        public long UserId { get; set; }

        public virtual SapcustomerGroup SapcustomerGroup { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
