using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapmaterialPlant
    {
        public long SapmaterialPlantId { get; set; }
        public long SapmaterialId { get; set; }
        public long PlantId { get; set; }
        public decimal? StandardPrice { get; set; }
        public decimal? PriceUnit { get; set; }
        public string? PriceControlIndicator { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
    }
}
