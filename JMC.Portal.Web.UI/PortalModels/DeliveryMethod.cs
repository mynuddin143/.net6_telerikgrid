using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class DeliveryMethod
    {
        public DeliveryMethod()
        {
            Sapshipments = new HashSet<Sapshipment>();
        }

        public long DeliveryMethodId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Sapshipment> Sapshipments { get; set; }
    }
}
