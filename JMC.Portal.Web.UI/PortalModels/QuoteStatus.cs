using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class QuoteStatus
    {
        public QuoteStatus()
        {
            QuoteStatusChanges = new HashSet<QuoteStatusChange>();
        }

        public long QuoteStatusId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<QuoteStatusChange> QuoteStatusChanges { get; set; }
    }
}
