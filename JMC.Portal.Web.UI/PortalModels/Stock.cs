using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Stock
    {
        public long StockId { get; set; }
        public long QuoteMaterialId { get; set; }
        public string Name { get; set; } = null!;
        public string Grade { get; set; } = null!;
        public long PlantId { get; set; }
        public decimal TubeLength { get; set; }
        public int AvailablePieces { get; set; }
        public decimal Weight { get; set; }
        public string Uom { get; set; } = null!;
        public int? Bundling1 { get; set; }
        public int? Bundling2 { get; set; }
        public string? Sapcode { get; set; }
        public long? SapsalesOrderItemId { get; set; }
        public int? BundlingRound { get; set; }
        public bool? PlaceOnSalesOrder { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual QuoteMaterial QuoteMaterial { get; set; } = null!;
        public virtual SapsalesOrderItem? SapsalesOrderItem { get; set; }
    }
}
