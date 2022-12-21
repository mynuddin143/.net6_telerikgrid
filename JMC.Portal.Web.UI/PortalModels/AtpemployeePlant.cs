using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class AtpemployeePlant
    {
        public long UserId { get; set; }
        public long LocationId { get; set; }

        public virtual Plant Location { get; set; } = null!;
        public virtual Employee User { get; set; } = null!;
    }
}
