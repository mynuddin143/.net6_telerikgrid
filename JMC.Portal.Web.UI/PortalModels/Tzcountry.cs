using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Tzcountry
    {
        public Tzcountry()
        {
            Tzzones = new HashSet<Tzzone>();
        }

        public string CountryCode { get; set; } = null!;
        public string? CountryName { get; set; }

        public virtual ICollection<Tzzone> Tzzones { get; set; }
    }
}
