using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Tzzone
    {
        public int ZoneId { get; set; }
        public string CountryCode { get; set; } = null!;
        public string ZoneName { get; set; } = null!;

        public virtual Tzcountry CountryCodeNavigation { get; set; } = null!;
    }
}
