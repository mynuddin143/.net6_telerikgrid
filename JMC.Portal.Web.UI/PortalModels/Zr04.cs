using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Zr04
    {
        public long Zr04id { get; set; }
        public long SapsoldtoId { get; set; }
        public long SapmaterialGroupId { get; set; }
        public decimal Rate { get; set; }
        public string Currency { get; set; } = null!;
        public long Per { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual SapcharacteristicOption SapmaterialGroup { get; set; } = null!;
        public virtual SapsoldTo Sapsoldto { get; set; } = null!;
    }
}
