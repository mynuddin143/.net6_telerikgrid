using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Saptier
    {
        public Saptier()
        {
            SapshipToSapsalesOrganizations = new HashSet<SapshipToSapsalesOrganization>();
        }

        public long SaptierId { get; set; }
        public long DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;

        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<SapshipToSapsalesOrganization> SapshipToSapsalesOrganizations { get; set; }
    }
}
