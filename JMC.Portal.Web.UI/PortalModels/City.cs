using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class City
    {
        public City()
        {
            Locations = new HashSet<Location>();
            SapshipTos = new HashSet<SapshipTo>();
            Sapvendors = new HashSet<Sapvendor>();
        }

        public long CityId { get; set; }
        public string Name { get; set; } = null!;
        public long StateId { get; set; }
        public bool? Active { get; set; }
        public int? TimeZoneId { get; set; }

        public virtual State State { get; set; } = null!;
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<SapshipTo> SapshipTos { get; set; }
        public virtual ICollection<Sapvendor> Sapvendors { get; set; }
    }
}
