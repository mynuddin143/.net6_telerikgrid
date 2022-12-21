﻿using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapcustomerGroupSapsalesOrganization
    {
        public long SapcustomerGroupId { get; set; }
        public long SapsalesOrganizationId { get; set; }

        public virtual SapcustomerGroup SapcustomerGroup { get; set; } = null!;
        public virtual SapsalesOrganization SapsalesOrganization { get; set; } = null!;
    }
}
