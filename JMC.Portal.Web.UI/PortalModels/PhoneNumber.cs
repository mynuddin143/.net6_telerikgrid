using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class PhoneNumber
    {
        public long PhoneNumberId { get; set; }
        public string Name { get; set; } = null!;
        public string Number { get; set; } = null!;
        public long? LocationId { get; set; }

        public virtual Location? Location { get; set; }
    }
}
