using System;
using System.Collections.Generic;

namespace JMC.Portal.Web.UI.PortalModels
{
    public partial class Location
    {
        public Location()
        {
            Employees = new HashSet<Employee>();
            PhoneNumbers = new HashSet<PhoneNumber>();
            PlantComputers = new HashSet<PlantComputer>();
            RandomLengthSapsoldTos = new HashSet<RandomLengthSapsoldTo>();
            ScrapSapsoldTos = new HashSet<ScrapSapsoldTo>();
        }

        public long LocationId { get; set; }
        public string Name { get; set; } = null!;
        public long CityId { get; set; }
        public string Address { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? TollFreePhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public bool? Active { get; set; }
        public long DivisionId { get; set; }
        public string? Adname { get; set; }

        public virtual City City { get; set; } = null!;
        public virtual Division Division { get; set; } = null!;
        public virtual Plant Plant { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<PlantComputer> PlantComputers { get; set; }
        public virtual ICollection<RandomLengthSapsoldTo> RandomLengthSapsoldTos { get; set; }
        public virtual ICollection<ScrapSapsoldTo> ScrapSapsoldTos { get; set; }
    }
}
