using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsoldToSapshipTo
    {
        public long SapsoldToId { get; set; }
        public long SapshipToId { get; set; }

        public virtual SapshipTo SapshipTo { get; set; } = null!;
        public virtual SapsoldTo SapsoldTo { get; set; } = null!;
    }
}
