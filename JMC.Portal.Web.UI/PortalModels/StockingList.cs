using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class StockingList
    {
        public long SapmaterialId { get; set; }
        public bool? Active { get; set; }

        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
    }
}
