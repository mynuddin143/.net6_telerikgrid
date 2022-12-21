using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsalesOrderItem
    {
        public SapsalesOrderItem()
        {
            QuoteMaterials = new HashSet<QuoteMaterial>();
            SapdeliveryItems = new HashSet<SapdeliveryItem>();
            Sapstocks = new HashSet<Sapstock>();
            ShippingCartSapsalesOrderItems = new HashSet<ShippingCartSapsalesOrderItem>();
            Stocks = new HashSet<Stock>();
            WebReleasePlantSapsalesOrderItems = new HashSet<WebReleasePlantSapsalesOrderItem>();
        }

        public long SapsalesOrderItemId { get; set; }
        public long SapsalesOrderId { get; set; }
        public long SapmaterialId { get; set; }
        public int Position { get; set; }
        public decimal Price { get; set; }
        public string? CustomerMaterialNumber { get; set; }
        public long? SapshipToId { get; set; }
        public string? MaterialShortDescription { get; set; }
        public long? PlantId { get; set; }
        public string? RequirementsType { get; set; }
        public DateTime? ReadyDate { get; set; }
        public DateTime? MaterialStagingDate { get; set; }
        public decimal? OpenQuantity { get; set; }
        public string? BaseUnit { get; set; }
        public DateTime? ScheduleLineDate { get; set; }
        public decimal? ScheduleOrderQuantity { get; set; }
        public decimal? ConfirmedQuantity { get; set; }
        public string? SalesUnit { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? OrderQuantity { get; set; }
        public string? ItemCategory { get; set; }
        public string? DeliveryStatus { get; set; }
        public bool? Backlog { get; set; }
        public int? PiecesPerBundle { get; set; }
        public int? ReadyPieces { get; set; }
        public decimal? ReadyWeight { get; set; }
        public int? NotReadyPieces { get; set; }
        public decimal? NotReadyWeight { get; set; }
        public int? OpenPieces { get; set; }
        public int? DisplayReadyPieces { get; set; }
        public decimal? DisplayReadyWeight { get; set; }
        public int? DisplayNotReadyPieces { get; set; }
        public decimal? DisplayNotReadyWeight { get; set; }
        public int? DisplayReleasedPieces { get; set; }
        public decimal? DisplayReleasedWeight { get; set; }
        public int? DisplayDeliveredPieces { get; set; }
        public decimal? DisplayDeliveredWeight { get; set; }
        public DateTime? RollDate { get; set; }
        public string? RejectionCode { get; set; }
        public string? RecordSource { get; set; }
        public string? PolineNumber { get; set; }
        public bool? DealIndicator { get; set; }

        public virtual Plant? Plant { get; set; }
        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
        public virtual SapsalesOrder SapsalesOrder { get; set; } = null!;
        public virtual SapshipTo? SapshipTo { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterials { get; set; }
        public virtual ICollection<SapdeliveryItem> SapdeliveryItems { get; set; }
        public virtual ICollection<Sapstock> Sapstocks { get; set; }
        public virtual ICollection<ShippingCartSapsalesOrderItem> ShippingCartSapsalesOrderItems { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<WebReleasePlantSapsalesOrderItem> WebReleasePlantSapsalesOrderItems { get; set; }
    }
}
