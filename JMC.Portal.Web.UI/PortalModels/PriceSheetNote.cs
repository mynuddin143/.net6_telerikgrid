using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PriceSheetNote
    {
        public long PriceSheetNoteId { get; set; }
        public long? RegionSapconditionGroupId { get; set; }
        public string? Notes { get; set; }
        public long? SapsoldToId { get; set; }

        public virtual SapconditionGroup? RegionSapconditionGroup { get; set; }
        public virtual SapsoldTo? SapsoldTo { get; set; }
    }
}
