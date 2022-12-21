using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapbundlingOption
    {
        public long SapbundlingOptionId { get; set; }
        public long DivisionId { get; set; }
        public decimal LengthLow { get; set; }
        public decimal LengthHigh { get; set; }
        public int Bundling1 { get; set; }
        public int Bundling2 { get; set; }
        public string? Plant { get; set; }

        public virtual Division Division { get; set; } = null!;
    }
}
