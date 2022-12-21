using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Round
    {
        public int RoundId { get; set; }
        public decimal Size { get; set; }
        public string MaterialGroup { get; set; } = null!;
        public bool? Nps { get; set; }
        public decimal? Npssize { get; set; }
    }
}
