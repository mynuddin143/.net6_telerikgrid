using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class FreightandFsc
    {
        public long FreightId { get; set; }
        public long SapshiptoId { get; set; }
        public long SapconditionId { get; set; }
        public decimal Rate { get; set; }
        public string Currency { get; set; } = null!;
        public long Per { get; set; }
        public string Unit { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public bool RegionalFreight { get; set; }
        public long? SapsoldtoId { get; set; }

        public virtual Sapcondition Sapcondition { get; set; } = null!;
        public virtual SapshipTo Sapshipto { get; set; } = null!;
    }
}
