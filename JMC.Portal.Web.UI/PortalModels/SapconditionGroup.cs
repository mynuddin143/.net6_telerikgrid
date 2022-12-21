using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapconditionGroup
    {
        public SapconditionGroup()
        {
            Plants = new HashSet<Plant>();
            PriceChangeRequestRegionSapconditionGroups = new HashSet<PriceChangeRequest>();
            PriceChangeRequestTierSapconditionGroups = new HashSet<PriceChangeRequest>();
            PriceSheetNotes = new HashSet<PriceSheetNote>();
            QuoteFreightIndicatorSapconditionGroups = new HashSet<Quote>();
            QuoteRegionSapconditionGroups = new HashSet<Quote>();
            QuoteTierSapconditionGroups = new HashSet<Quote>();
            SapshipToFreightIndicatorSapconditionGroups = new HashSet<SapshipTo>();
            SapshipToFuelSurchargeSapconditionGroups = new HashSet<SapshipTo>();
            SapsoldToHomeMillSapconditionGroups = new HashSet<SapsoldTo>();
            SapsoldToRegionSapconditionGroups = new HashSet<SapsoldTo>();
            SapsoldToTierSapconditionGroups = new HashSet<SapsoldTo>();
        }

        public long SapconditionGroupId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;

        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<PriceChangeRequest> PriceChangeRequestRegionSapconditionGroups { get; set; }
        public virtual ICollection<PriceChangeRequest> PriceChangeRequestTierSapconditionGroups { get; set; }
        public virtual ICollection<PriceSheetNote> PriceSheetNotes { get; set; }
        public virtual ICollection<Quote> QuoteFreightIndicatorSapconditionGroups { get; set; }
        public virtual ICollection<Quote> QuoteRegionSapconditionGroups { get; set; }
        public virtual ICollection<Quote> QuoteTierSapconditionGroups { get; set; }
        public virtual ICollection<SapshipTo> SapshipToFreightIndicatorSapconditionGroups { get; set; }
        public virtual ICollection<SapshipTo> SapshipToFuelSurchargeSapconditionGroups { get; set; }
        public virtual ICollection<SapsoldTo> SapsoldToHomeMillSapconditionGroups { get; set; }
        public virtual ICollection<SapsoldTo> SapsoldToRegionSapconditionGroups { get; set; }
        public virtual ICollection<SapsoldTo> SapsoldToTierSapconditionGroups { get; set; }
    }
}
