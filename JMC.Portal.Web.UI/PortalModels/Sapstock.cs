using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Sapstock
    {
        public Sapstock()
        {
            ShoppingCartSapstocks = new HashSet<ShoppingCartSapstock>();
        }

        public long SapstockId { get; set; }
        public long SapmaterialId { get; set; }
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
        public string? SapsalesOrderNumber { get; set; }
        public int? SapsalesOrderItemNumber { get; set; }
        public long? SaptubeStandardId { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime? BatchDate { get; set; }
        public long? SapspecificationId { get; set; }
        public bool? MaxStockLevel { get; set; }
        public DateTime? RollingEndDate { get; set; }
        public int? CloseRollingPcs { get; set; }
        public decimal? CloseRollingWeight { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
        public virtual SapsalesOrderItem? SapsalesOrderItem { get; set; }
        public virtual SapcharacteristicOption? Sapspecification { get; set; }
        public virtual SapcharacteristicOption? SaptubeStandard { get; set; }
        public virtual ICollection<ShoppingCartSapstock> ShoppingCartSapstocks { get; set; }
    }
}
