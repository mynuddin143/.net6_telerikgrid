using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapcustomerServiceRep
    {
        public SapcustomerServiceRep()
        {
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
        }

        public long SapcustomerServiceRepId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;
        public long? UserId { get; set; }

        public virtual Division Division { get; set; } = null!;
        public virtual User? User { get; set; }
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
    }
}
