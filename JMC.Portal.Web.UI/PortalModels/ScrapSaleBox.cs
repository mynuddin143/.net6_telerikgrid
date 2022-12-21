using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ScrapSaleBox
    {
        public long ScrapSaleId { get; set; }
        public long BoxId { get; set; }

        public virtual Box Box { get; set; } = null!;
        public virtual ScrapSale ScrapSale { get; set; } = null!;
    }
}
