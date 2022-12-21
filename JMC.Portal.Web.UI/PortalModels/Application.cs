using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Application
    {
        public Application()
        {
            ApplicationRoles = new HashSet<ApplicationRole>();
            UserProfiles = new HashSet<UserProfile>();
        }

        public long ApplicationId { get; set; }
        public string Name { get; set; } = null!;
        public bool? Active { get; set; }
        public string? Url { get; set; }
        public string? LongName { get; set; }

        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
