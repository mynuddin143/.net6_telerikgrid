using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DealsPricingGroup
    {
        public long? Zr01 { get; set; }
        public long? DealId { get; set; }
        public decimal Discount { get; set; }
        public long DealsPricingGroupsId { get; set; }

        public virtual DealsDetail? Deal { get; set; }
        public virtual SapcharacteristicOption? Zr01Navigation { get; set; }
    }
}
