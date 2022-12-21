using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Mill
    {
        public long MillId { get; set; }
        public string Name { get; set; } = null!;
        public long PlantId { get; set; }
        public bool? Active { get; set; }
        public decimal? MinLength { get; set; }
        public decimal? MaxLength { get; set; }
        public decimal? MinSize { get; set; }
        public decimal? MaxSize { get; set; }
        public decimal? MinGauge { get; set; }
        public decimal? MaxGauge { get; set; }
        public string? WorkCenter { get; set; }

        public virtual Plant Plant { get; set; } = null!;
    }
}
