using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ShippingCart
    {
        public ShippingCart()
        {
            ShippingCartSapsalesOrderItems = new HashSet<ShippingCartSapsalesOrderItem>();
        }

        public long ShippingCartId { get; set; }
        public long UserId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? SoldTo { get; set; }
        public string? ShipTo { get; set; }
        public long? SubTotal { get; set; }
        public string? Plant { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<ShippingCartSapsalesOrderItem> ShippingCartSapsalesOrderItems { get; set; }
    }
}
