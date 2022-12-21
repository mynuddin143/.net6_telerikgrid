using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ShipmentCostType
    {
        public ShipmentCostType()
        {
            PendingShipmentCosts = new HashSet<PendingShipmentCost>();
            ShipmentCosts = new HashSet<ShipmentCost>();
        }

        public long ShipmentCostTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public bool Ips { get; set; }
        public bool Sap { get; set; }

        public virtual ICollection<PendingShipmentCost> PendingShipmentCosts { get; set; }
        public virtual ICollection<ShipmentCost> ShipmentCosts { get; set; }
    }
}
