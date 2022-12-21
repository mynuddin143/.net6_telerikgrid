using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class QuoteSapcondition
    {
        public long QuoteSapconditionId { get; set; }
        public long QuoteId { get; set; }
        public long SapconditionId { get; set; }
        public decimal? Rate { get; set; }
        public string RateUnit { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int PricePer { get; set; }
        public string PricePerUnit { get; set; } = null!;

        public virtual Quote Quote { get; set; } = null!;
        public virtual Sapcondition Sapcondition { get; set; } = null!;
    }
}
