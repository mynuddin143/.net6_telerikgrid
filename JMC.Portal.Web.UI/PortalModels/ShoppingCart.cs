using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingCartSaprollings = new HashSet<ShoppingCartSaprolling>();
            ShoppingCartSapstocks = new HashSet<ShoppingCartSapstock>();
        }

        public long ShoppingCardId { get; set; }
        public long UserId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<ShoppingCartSaprolling> ShoppingCartSaprollings { get; set; }
        public virtual ICollection<ShoppingCartSapstock> ShoppingCartSapstocks { get; set; }
    }
}
