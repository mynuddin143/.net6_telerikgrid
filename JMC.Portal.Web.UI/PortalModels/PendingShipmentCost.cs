using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PendingShipmentCost
    {
        public long PendingShipmentCostId { get; set; }
        public long DivisionId { get; set; }
        public long ShipmentCostTypeId { get; set; }
        public string Bol { get; set; } = null!;
        public string BalanceDueSupplemental { get; set; } = null!;
        public decimal Amount { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual ShipmentCostType ShipmentCostType { get; set; } = null!;
    }
}
