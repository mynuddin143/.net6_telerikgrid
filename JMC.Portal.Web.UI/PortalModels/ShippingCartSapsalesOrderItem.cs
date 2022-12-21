using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ShippingCartSapsalesOrderItem
    {
        public long ShippingCartSapsalesOrderItemId { get; set; }
        public long ShippingCartId { get; set; }
        public long SapsalesOrderItemId { get; set; }
        public decimal CartQuantity { get; set; }

        public virtual SapsalesOrderItem SapsalesOrderItem { get; set; } = null!;
        public virtual ShippingCart ShippingCart { get; set; } = null!;
    }
}
