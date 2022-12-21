using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Sapcondition
    {
        public Sapcondition()
        {
            FreightandFscs = new HashSet<FreightandFsc>();
            PriceChangeRequestItems = new HashSet<PriceChangeRequestItem>();
            QuoteMaterialSapconditions = new HashSet<QuoteMaterialSapcondition>();
            QuoteSapconditions = new HashSet<QuoteSapcondition>();
        }

        public long SapconditionId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;
        public string Application { get; set; } = null!;
        public bool? Override { get; set; }
        public bool? Extra { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<FreightandFsc> FreightandFscs { get; set; }
        public virtual ICollection<PriceChangeRequestItem> PriceChangeRequestItems { get; set; }
        public virtual ICollection<QuoteMaterialSapcondition> QuoteMaterialSapconditions { get; set; }
        public virtual ICollection<QuoteSapcondition> QuoteSapconditions { get; set; }
    }
}
