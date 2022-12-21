using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class RandomLengthSapsoldTo
    {
        public RandomLengthSapsoldTo()
        {
            Boxes = new HashSet<Box>();
            ScrapSales = new HashSet<ScrapSale>();
            Trailers = new HashSet<Trailer>();
            Trucks = new HashSet<Truck>();
        }

        public long RandomLengthSapsoldToId { get; set; }
        public long SapsoldToId { get; set; }
        public long LocationId { get; set; }
        public bool Active { get; set; }

        public virtual Location Location { get; set; } = null!;
        public virtual SapsoldTo SapsoldTo { get; set; } = null!;
        public virtual ICollection<Box> Boxes { get; set; }
        public virtual ICollection<ScrapSale> ScrapSales { get; set; }
        public virtual ICollection<Trailer> Trailers { get; set; }
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
