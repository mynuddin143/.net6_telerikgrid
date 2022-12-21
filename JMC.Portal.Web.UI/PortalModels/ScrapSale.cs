using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ScrapSale
    {
        public long ScrapSaleId { get; set; }
        public long DivisionId { get; set; }
        public DateTime Date { get; set; }
        public long? ScrapSapsoldToId { get; set; }
        public long? RandomLengthSapsoldToId { get; set; }
        public long? TruckId { get; set; }
        public long? TrailerId { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? TareWeight { get; set; }
        public long PlantId { get; set; }
        public long? SapscrapDeliveryId { get; set; }
        public bool? WeightAddedToSapsalesOrder { get; set; }
        public decimal? WeightAddedToInventory { get; set; }
        public bool? Cancelled { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual Plant Plant { get; set; } = null!;
        public virtual RandomLengthSapsoldTo? RandomLengthSapsoldTo { get; set; }
        public virtual SapscrapDelivery? SapscrapDelivery { get; set; }
        public virtual ScrapSapsoldTo? ScrapSapsoldTo { get; set; }
        public virtual Trailer? Trailer { get; set; }
        public virtual Truck? Truck { get; set; }
    }
}
