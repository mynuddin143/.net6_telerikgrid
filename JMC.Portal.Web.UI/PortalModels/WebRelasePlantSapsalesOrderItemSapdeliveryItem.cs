using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class WebRelasePlantSapsalesOrderItemSapdeliveryItem
    {
        public long WebReleasePlantSapsalesOrderItemId { get; set; }
        public long SapdeliveryItemId { get; set; }

        public virtual SapdeliveryItem SapdeliveryItem { get; set; } = null!;
        public virtual WebReleasePlantSapsalesOrderItem WebReleasePlantSapsalesOrderItem { get; set; } = null!;
    }
}
