using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ShoppingCartSaprolling
    {
        public long ShoppingCartSaprollingId { get; set; }
        public long ShoppingCartId { get; set; }
        public string? Status { get; set; }
        public long PlantId { get; set; }
        public long SapmaterialId { get; set; }
        public DateTime RollDate { get; set; }
        public decimal CartQuantity { get; set; }
        public long? SaprollingId { get; set; }
        public int? LengthFeet { get; set; }
        public int? LengthInches { get; set; }
        public int? Bundling1 { get; set; }
        public int? Bundling2 { get; set; }
        public int? BundlingRound { get; set; }
        public long SaptubeStandardId { get; set; }
        public string? Note { get; set; }
        public DateTime RequestedDate { get; set; }
        public decimal AvailableQuantity { get; set; }
        public string? RecordSource { get; set; }
        public string? SalesInstructionId { get; set; }
        public string? SpecificationId { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
        public virtual SapcharacteristicOption SaptubeStandard { get; set; } = null!;
        public virtual ShoppingCart ShoppingCart { get; set; } = null!;
    }
}
