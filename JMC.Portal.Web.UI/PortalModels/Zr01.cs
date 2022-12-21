using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Zr01
    {
        public long Zr01id { get; set; }
        public long SapsoldtoId { get; set; }
        public long SappricingGroupId { get; set; }
        public decimal Rate { get; set; }
        public string Currency { get; set; } = null!;
        public long Per { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual SapcharacteristicOption SappricingGroup { get; set; } = null!;
        public virtual SapshipTo Sapsoldto { get; set; } = null!;
    }
}
