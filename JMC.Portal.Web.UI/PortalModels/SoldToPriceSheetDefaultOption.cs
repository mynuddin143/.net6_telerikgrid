using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SoldToPriceSheetDefaultOption
    {
        public long SapsoldToId { get; set; }
        public bool FlgsoldToPrice { get; set; }
        public bool FlgShipToPrice { get; set; }
        public string DefaultShiptos { get; set; } = null!;
        public string DefaultUsers { get; set; } = null!;
        public string OtherEmailId { get; set; } = null!;
        public string PriceSheetType { get; set; } = null!;
        public string PriceSheetView { get; set; } = null!;
    }
}
