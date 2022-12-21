using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DealsMaterialPricingGroup
    {
        public long? DealId { get; set; }
        public long? Zr00 { get; set; }
        public decimal Discount { get; set; }
        public long DealsMaterialPricingGroupsId { get; set; }

        public virtual DealsDetail? Deal { get; set; }
        public virtual SapcharacteristicOption? Zr00Navigation { get; set; }
    }
}
