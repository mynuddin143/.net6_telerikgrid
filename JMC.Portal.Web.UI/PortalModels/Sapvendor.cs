using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Sapvendor
    {
        public Sapvendor()
        {
            Boxes = new HashSet<Box>();
            Sapdeliveries = new HashSet<Sapdelivery>();
            Sapshipments = new HashSet<Sapshipment>();
            Trailers = new HashSet<Trailer>();
            Trucks = new HashSet<Truck>();
        }

        public long SapvendorId { get; set; }
        public long DivisionId { get; set; }
        public string Number { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public long CityId { get; set; }
        public string PostalCode { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Fax { get; set; } = null!;
        public bool? Active { get; set; }
        public bool Rail { get; set; }
        public bool Express { get; set; }
        public bool Intermodal { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<Box> Boxes { get; set; }
        public virtual ICollection<Sapdelivery> Sapdeliveries { get; set; }
        public virtual ICollection<Sapshipment> Sapshipments { get; set; }
        public virtual ICollection<Trailer> Trailers { get; set; }
        public virtual ICollection<Truck> Trucks { get; set; }
    }
}
