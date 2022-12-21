using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class ApplicationRole
    {
        public long ApplicationRoleId { get; set; }
        public long ApplicationId { get; set; }
        public string Name { get; set; } = null!;

        public virtual Application Application { get; set; } = null!;
    }
}
