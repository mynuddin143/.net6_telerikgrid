using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Cmir
    {
        public long Cmirid { get; set; }
        public long SapsalesOrganizationId { get; set; }
        public string? DistributionChannel { get; set; }
        public long SapshipToId { get; set; }
        public long SapmaterialId { get; set; }
        public string? CustomerPartNumber { get; set; }

        public virtual Sapmaterial Sapmaterial { get; set; } = null!;
        public virtual SapsalesOrganization SapsalesOrganization { get; set; } = null!;
        public virtual SapsoldTo SapshipTo { get; set; } = null!;
    }
}
