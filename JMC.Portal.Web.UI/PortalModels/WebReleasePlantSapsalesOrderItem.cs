using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class WebReleasePlantSapsalesOrderItem
    {
        public long WebReleasePlantSapsalesOrderItemId { get; set; }
        public long WebReleasePlantId { get; set; }
        public long SapsalesOrderItemId { get; set; }
        public decimal? Quantity { get; set; }
        public int? Pieces { get; set; }
        public decimal? Weight { get; set; }

        public virtual SapsalesOrderItem SapsalesOrderItem { get; set; } = null!;
        public virtual WebReleasePlant WebReleasePlant { get; set; } = null!;
    }
}
