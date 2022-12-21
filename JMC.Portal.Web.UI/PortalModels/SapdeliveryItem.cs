using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapdeliveryItem
    {
        public long SapdeliveryItemId { get; set; }
        public long SapdeliveryId { get; set; }
        public int Position { get; set; }
        public long? SapsalesOrderItemId { get; set; }
        public decimal? Weight { get; set; }
        public string? WeightUnit { get; set; }
        public string? Status { get; set; }
        public decimal? QuantityDelivered { get; set; }
        public string? SalesUnit { get; set; }

        public virtual Sapdelivery Sapdelivery { get; set; } = null!;
        public virtual SapsalesOrderItem? SapsalesOrderItem { get; set; }
    }
}
