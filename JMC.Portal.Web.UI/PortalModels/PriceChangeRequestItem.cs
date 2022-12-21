using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PriceChangeRequestItem
    {
        public long PriceChangeRequestItemId { get; set; }
        public long PriceChangeRequestId { get; set; }
        public long SapconditionId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public long? SapshipToId { get; set; }
        public long? SapmaterialPricingGroupId { get; set; }
        public long? SappricingGroupId { get; set; }
        public long? SapmaterialGroupId { get; set; }
        public decimal? Rate { get; set; }
        public bool? Approved { get; set; }
        public long? ApprovedUserId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public decimal? OldRate { get; set; }
        public DateTime? OldEffectiveDate { get; set; }
        public string? FreightIndicator { get; set; }
        public string? IncoTerms2 { get; set; }

        public virtual User? ApprovedUser { get; set; }
        public virtual PriceChangeRequest PriceChangeRequest { get; set; } = null!;
        public virtual Sapcondition Sapcondition { get; set; } = null!;
        public virtual SapcharacteristicOption? SapmaterialGroup { get; set; }
        public virtual SapcharacteristicOption? SapmaterialPricingGroup { get; set; }
        public virtual SapcharacteristicOption? SappricingGroup { get; set; }
        public virtual SapshipTo? SapshipTo { get; set; }
    }
}
