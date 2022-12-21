using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class WebRelease
    {
        public WebRelease()
        {
            WebReleasePlants = new HashSet<WebReleasePlant>();
        }

        public long WebReleaseId { get; set; }
        public DateTime DateTime { get; set; }
        public long UserId { get; set; }
        public string? CustReleaseNumber { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<WebReleasePlant> WebReleasePlants { get; set; }
    }
}
