using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapsalesOrganization
    {
        public SapsalesOrganization()
        {
            Cmirs = new HashSet<Cmir>();
            SapcustomerTexts = new HashSet<SapcustomerText>();
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
        }

        public long SapsalesOrganizationId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;

        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<Cmir> Cmirs { get; set; }
        public virtual ICollection<SapcustomerText> SapcustomerTexts { get; set; }
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
    }
}
