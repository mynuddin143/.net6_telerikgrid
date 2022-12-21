using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsalesOrder
    {
        public SapsalesOrder()
        {
            SapsalesOrderItems = new HashSet<SapsalesOrderItem>();
        }

        public long SapsalesOrderId { get; set; }
        public long DivisionId { get; set; }
        public DateTime? Date { get; set; }
        public string? Number { get; set; }
        public string? BapireturnMessage { get; set; }
        public long? QuoteId { get; set; }
        public string? Ponumber { get; set; }
        public long? SapsoldToId { get; set; }
        public long? PlantId { get; set; }
        public long? SapshipToId { get; set; }
        public string? DocumentType { get; set; }
        public string? DistributionChannel { get; set; }
        public string? CreditStatus { get; set; }
        public string? DeliveryBlock { get; set; }
        public string? DeliveryBlockText { get; set; }
        public DateTime? Podate { get; set; }
        public long? UserId { get; set; }
        public string? YourReference { get; set; }
        public long? DealId { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual Plant? Plant { get; set; }
        public virtual Quote? Quote { get; set; }
        public virtual SapshipTo? SapshipTo { get; set; }
        public virtual SapsoldTo? SapsoldTo { get; set; }
        public virtual User? User { get; set; }
        public virtual SapscrapOrder SapscrapOrder { get; set; } = null!;
        public virtual ICollection<SapsalesOrderItem> SapsalesOrderItems { get; set; }
    }
}
