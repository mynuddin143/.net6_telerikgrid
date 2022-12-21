using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ShipmentCost
    {
        public long ShipmentCostId { get; set; }
        public long ShipmentCostTypeId { get; set; }
        public long ShipmentId { get; set; }
        public decimal Amount { get; set; }

        public virtual Sapshipment Shipment { get; set; } = null!;
        public virtual ShipmentCostType ShipmentCostType { get; set; } = null!;
    }
}
