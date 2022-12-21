using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ScrapSaleSapsalesOrderItem
    {
        public long ScrapSaleId { get; set; }
        public long SapsalesOrderItemId { get; set; }

        public virtual SapsalesOrderItem SapsalesOrderItem { get; set; } = null!;
        public virtual ScrapSale ScrapSale { get; set; } = null!;
    }
}
