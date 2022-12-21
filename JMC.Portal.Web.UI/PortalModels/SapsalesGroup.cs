using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsalesGroup
    {
        public SapsalesGroup()
        {
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
            SapshipTos = new HashSet<SapshipTo>();
        }

        public long SapsalesGroupId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;
        public long? UserId { get; set; }
        public long? AlternateIsr { get; set; }
        public string? BoldChatId { get; set; }

        public virtual User? AlternateIsrNavigation { get; set; }
        public virtual Division Division { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
        public virtual ICollection<SapshipTo> SapshipTos { get; set; }
    }
}
