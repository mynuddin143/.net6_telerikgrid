using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Rolling
    {
        public long RollingId { get; set; }
        public long QuoteMaterialId { get; set; }
        public long PlantId { get; set; }
        public DateTime Date { get; set; }
        public decimal PlannedQuantity { get; set; }
        public decimal AllocatedQuantity { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual QuoteMaterial QuoteMaterial { get; set; } = null!;
    }
}
