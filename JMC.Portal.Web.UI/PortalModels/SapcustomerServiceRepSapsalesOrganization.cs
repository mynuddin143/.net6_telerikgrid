using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapcustomerServiceRepSapsalesOrganization
    {
        public long SapcustomerServiceRepId { get; set; }
        public long SapsalesOrganizationId { get; set; }

        public virtual SapcustomerServiceRep SapcustomerServiceRep { get; set; } = null!;
        public virtual SapsalesOrganization SapsalesOrganization { get; set; } = null!;
    }
}
