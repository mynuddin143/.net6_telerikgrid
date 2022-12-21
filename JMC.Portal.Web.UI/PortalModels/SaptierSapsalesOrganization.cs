using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SaptierSapsalesOrganization
    {
        public long SaptierId { get; set; }
        public long SapsalesOrganizationId { get; set; }

        public virtual SapsalesOrganization SapsalesOrganization { get; set; } = null!;
        public virtual Saptier Saptier { get; set; } = null!;
    }
}
