using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapcustomerGroup
    {
        public SapcustomerGroup()
        {
            SapcustomerGroupUsers = new HashSet<SapcustomerGroupUser>();
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
            SapshipTos = new HashSet<SapshipTo>();
        }

        public long SapcustomerGroupId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;
        public long? UserId { get; set; }
        public long? RegionalManagerUserId { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual User? RegionalManagerUser { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<SapcustomerGroupUser> SapcustomerGroupUsers { get; set; }
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
        public virtual ICollection<SapshipTo> SapshipTos { get; set; }
    }
}
