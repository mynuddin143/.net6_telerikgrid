using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class SapstorageLocation
    {
        public SapstorageLocation()
        {
            SapsalesDeliveries = new HashSet<SapsalesDelivery>();
        }

        public long SapstorageLocationId { get; set; }
        public string Sapcode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        public long PlantId { get; set; }

        public virtual Plant Plant { get; set; } = null!;
        public virtual ICollection<SapsalesDelivery> SapsalesDeliveries { get; set; }
    }
}
