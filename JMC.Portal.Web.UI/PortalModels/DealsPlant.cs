using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DealsPlant
    {
        public long Id { get; set; }
        public long? PlantId { get; set; }
        public long? DealId { get; set; }

        public virtual DealsDetail? Deal { get; set; }
    }
}
