using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class QuoteMaterial
    {
        public QuoteMaterial()
        {
            QuoteMaterialSapconditions = new HashSet<QuoteMaterialSapcondition>();
            Rollings = new HashSet<Rolling>();
            Stocks = new HashSet<Stock>();
        }

        public long QuoteMaterialId { get; set; }
        public long QuoteId { get; set; }
        public long SapmaterialId { get; set; }
        public int? Quantity { get; set; }
        public long? SapmaterialUnitOfMeasureId { get; set; }
        public string? Comments { get; set; }
        public int? Bundling1 { get; set; }
        public int? Bundling2 { get; set; }
        public string? PrintedComments { get; set; }
        public string? StockSapcode { get; set; }
        public long? SapsalesOrderItemId { get; set; }
        public long? SaptubeStandardId { get; set; }
        public decimal? Length { get; set; }
        public int? LengthFeet { get; set; }
        public int? LengthInches { get; set; }
        public string? LengthFractionalInches { get; set; }
        public long? SapkleenkoteColorId { get; set; }
        public long? SapepoxycoatColorId { get; set; }
        public long? SapspecificationId { get; set; }
        public long? SapsalesInstructionId { get; set; }
        public decimal? WeightPerFoot { get; set; }
        public string? StockMaterialNumber { get; set; }
        public bool? PlaceOnSalesOrder { get; set; }
        public DateTime? PromiseDate { get; set; }

        public virtual Quote Quote { get; set; } = null!;
        public virtual SapcharacteristicOption? SapepoxycoatColor { get; set; }
        public virtual SapcharacteristicOption? SapkleenkoteColor { get; set; }
        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
        public virtual SapcharacteristicOption? SapmaterialUnitOfMeasure { get; set; }
        public virtual SapcharacteristicOption? SapsalesInstruction { get; set; }
        public virtual SapsalesOrderItem? SapsalesOrderItem { get; set; }
        public virtual SapcharacteristicOption? Sapspecification { get; set; }
        public virtual SapcharacteristicOption? SaptubeStandard { get; set; }
        public virtual ICollection<QuoteMaterialSapcondition> QuoteMaterialSapconditions { get; set; }
        public virtual ICollection<Rolling> Rollings { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
