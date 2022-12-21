using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsalesGroupSapsalesOrganization
    {
        public long SapsalesGroupId { get; set; }
        public long SapsalesOrganizationId { get; set; }

        public virtual SapsalesGroup SapsalesGroup { get; set; } = null!;
        public virtual SapsalesOrganization SapsalesOrganization { get; set; } = null!;
    }
}
