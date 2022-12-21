using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Country
    {
        public Country()
        {
            States = new HashSet<State>();
        }

        public long CountryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Abbreviation { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<State> States { get; set; }
    }
}
