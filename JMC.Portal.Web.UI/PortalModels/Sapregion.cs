using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Sapregion
    {
        public Sapregion()
        {
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
        }

        public long SapregionId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;

        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
    }
}
