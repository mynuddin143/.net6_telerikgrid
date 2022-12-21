using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Quote
    {
        public Quote()
        {
            QuoteMaterials = new HashSet<QuoteMaterial>();
            QuoteSapconditions = new HashSet<QuoteSapcondition>();
            QuoteStatusChanges = new HashSet<QuoteStatusChange>();
            SapsalesOrders = new HashSet<SapsalesOrder>();
        }

        public long QuoteId { get; set; }
        public long DivisionId { get; set; }
        public string? Name { get; set; }
        public string? Attention { get; set; }
        public long SapsoldToId { get; set; }
        public long? SapshipToId { get; set; }
        public long? ProductionPlantId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public int? CustomerCreditLimit { get; set; }
        public int? CustomerCreditExposure { get; set; }
        public bool Locked { get; set; }
        public string? Comments { get; set; }
        public DateTime? LastUpdatedFromSap { get; set; }
        public long? RegionSapconditionGroupId { get; set; }
        public long? TierSapconditionGroupId { get; set; }
        public string? Currency { get; set; }
        public string? NewCustomerName { get; set; }
        public bool? DownloadPricing { get; set; }
        public bool? DownloadStock { get; set; }
        public bool? DownloadRollings { get; set; }
        public string? IncoTerms2 { get; set; }
        public long? FreightIndicatorSapconditionGroupId { get; set; }
        public DateTime? PromiseDate { get; set; }
        public string? Ponumber { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual SapconditionGroup? FreightIndicatorSapconditionGroup { get; set; }
        public virtual Plant? ProductionPlant { get; set; }
        public virtual SapconditionGroup? RegionSapconditionGroup { get; set; }
        public virtual SapshipTo? SapshipTo { get; set; }
        public virtual SapsoldTo SapsoldTo { get; set; } = null!;
        public virtual SapconditionGroup? TierSapconditionGroup { get; set; }
        public virtual ICollection<QuoteMaterial> QuoteMaterials { get; set; }
        public virtual ICollection<QuoteSapcondition> QuoteSapconditions { get; set; }
        public virtual ICollection<QuoteStatusChange> QuoteStatusChanges { get; set; }
        public virtual ICollection<SapsalesOrder> SapsalesOrders { get; set; }
    }
}
