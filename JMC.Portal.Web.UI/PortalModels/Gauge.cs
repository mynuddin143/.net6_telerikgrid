using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Gauge
    {
        public int GaugeId { get; set; }
        public decimal Gauge1 { get; set; }
        public string MaterialGroup { get; set; } = null!;
        public bool? Nps { get; set; }
        public string? Npsdescription { get; set; }
    }
}
