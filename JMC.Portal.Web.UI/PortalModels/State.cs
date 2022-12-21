using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
        }

        public long StateId { get; set; }
        public string Name { get; set; } = null!;
        public long CountryId { get; set; }
        public string? Abbreviation { get; set; }
        public bool? Active { get; set; }

        public virtual Country Country { get; set; } = null!;
        public virtual ICollection<City> Cities { get; set; }
    }
}
