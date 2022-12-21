using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsoldToPlantExclusion
    {
        public long SapshipToId { get; set; }
        public long PlantId { get; set; }
        public bool? ViewStockRollings { get; set; }
        public bool? CanOrder { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual SapsoldTo SapshipTo { get; set; } = null!;
    }
}
