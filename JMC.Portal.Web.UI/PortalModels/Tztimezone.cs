using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Tztimezone
    {
        public int? ZoneId { get; set; }
        public string Abbreviation { get; set; } = null!;
        public decimal TimeStart { get; set; }
        public int GmtOffset { get; set; }
        public string Dst { get; set; } = null!;

        public virtual Tzzone? Zone { get; set; }
    }
}
