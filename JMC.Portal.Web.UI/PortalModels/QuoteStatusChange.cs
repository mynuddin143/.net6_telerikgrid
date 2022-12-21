using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class QuoteStatusChange
    {
        public long QuoteStatusChangeId { get; set; }
        public long QuoteId { get; set; }
        public long QuoteStatusId { get; set; }
        public DateTime Date { get; set; }

        public virtual Quote Quote { get; set; } = null!;
        public virtual QuoteStatus QuoteStatus { get; set; } = null!;
    }
}
