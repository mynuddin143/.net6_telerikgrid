using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapscrapOrder
    {
        public long SapsalesOrderId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public bool Consignment { get; set; }

        public virtual SapsalesOrder SapsalesOrder { get; set; } = null!;
    }
}
