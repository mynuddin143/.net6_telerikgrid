using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapregionSapsalesOrganization
    {
        public long SapregionId { get; set; }
        public long SapsalesOrganizationId { get; set; }

        public virtual Sapregion Sapregion { get; set; } = null!;
        public virtual SapsalesOrganization SapsalesOrganization { get; set; } = null!;
    }
}
