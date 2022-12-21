using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DealsBySoldToShipTo
    {
        public long? SapsoldToId { get; set; }
        public long? SapshipToId { get; set; }
        public long DealId { get; set; }
        public long Id { get; set; }

        public virtual DealsDetail Deal { get; set; } = null!;
        public virtual SapshipTo? SapshipTo { get; set; }
        public virtual SapsoldTo? SapsoldTo { get; set; }
    }
}
