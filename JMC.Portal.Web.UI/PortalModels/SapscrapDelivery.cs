using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapscrapDelivery
    {
        public SapscrapDelivery()
        {
            ScrapSales = new HashSet<ScrapSale>();
        }

        public long SapdeliveryId { get; set; }

        public virtual Sapdelivery Sapdelivery { get; set; } = null!;
        public virtual ICollection<ScrapSale> ScrapSales { get; set; }
    }
}
