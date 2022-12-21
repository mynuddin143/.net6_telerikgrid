using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapdeliveryType
    {
        public SapdeliveryType()
        {
            Sapshipments = new HashSet<Sapshipment>();
        }

        public long SapdeliveryTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Sapcode { get; set; } = null!;

        public virtual ICollection<Sapshipment> Sapshipments { get; set; }
    }
}
