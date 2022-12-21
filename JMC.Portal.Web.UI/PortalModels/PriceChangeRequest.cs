using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PriceChangeRequest
    {
        public PriceChangeRequest()
        {
            PriceChangeRequestItems = new HashSet<PriceChangeRequestItem>();
        }

        public long PriceChangeRequestId { get; set; }
        public long DivisionId { get; set; }
        public DateTime Date { get; set; }
        public long UserId { get; set; }
        public long? SapsoldToId { get; set; }
        public long? RegionSapconditionGroupId { get; set; }
        public long? TierSapconditionGroupId { get; set; }
        public string? Currency { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual SapconditionGroup? RegionSapconditionGroup { get; set; }
        public virtual SapsoldTo? SapsoldTo { get; set; }
        public virtual SapconditionGroup? TierSapconditionGroup { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<PriceChangeRequestItem> PriceChangeRequestItems { get; set; }
    }
}
