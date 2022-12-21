using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Zr00
    {
        public long ConditionId { get; set; }
        public long SapsoldtoId { get; set; }
        public long SapmaterialPricingGroupId { get; set; }
        public decimal Rate { get; set; }
        public string Currency { get; set; } = null!;
        public long Per { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual SapcharacteristicOption SapmaterialPricingGroup { get; set; } = null!;
        public virtual SapshipTo Sapsoldto { get; set; } = null!;
    }
}
