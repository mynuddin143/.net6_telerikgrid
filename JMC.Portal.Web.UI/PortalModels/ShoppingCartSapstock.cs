using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ShoppingCartSapstock
    {
        public long ShoppingCartSapstockId { get; set; }
        public long ShoppingCartId { get; set; }
        public long SapstockId { get; set; }
        public string? Status { get; set; }
        public decimal CartQuantity { get; set; }
        public string? RollDate { get; set; }
        public string? Note { get; set; }
        public string? RecordSource { get; set; }

        public virtual Sapstock Sapstock { get; set; } = null!;
        public virtual ShoppingCart ShoppingCart { get; set; } = null!;
    }
}
