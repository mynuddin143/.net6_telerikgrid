using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ShoppingCartWheatland
    {
        public long ShoppingCartWheatlandId { get; set; }
        public long ShoppingCartId { get; set; }
        public long SapMaterialId { get; set; }
        public string? Status { get; set; }
        public long? PlantId { get; set; }
        public decimal CartQuantity { get; set; }
        public int? Length { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? RollingDate { get; set; }
        public string? Type { get; set; }
    }
}
