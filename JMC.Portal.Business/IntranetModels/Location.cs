using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Location")]
    public partial class Location
    {
        public Location()
        {
            AdditionalPhoneNumbers = new HashSet<AdditionalPhoneNumber>();
            Employees = new HashSet<Employee>();
            PhoneNumbers = new HashSet<PhoneNumber>();
            RandomLengthCustomers = new HashSet<RandomLengthCustomer>();
            RandomLengthSAPSoldTos = new HashSet<RandomLengthSAPSoldTo>();
            ScrapCustomers = new HashSet<ScrapCustomer>();
            ScrapSAPSoldTos = new HashSet<ScrapSAPSoldTo>();
        }

        [Key]
        [Column("LocationID")]
        public int LocationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("CityID")]
        public int CityID { get; set; }
        [StringLength(150)]
        [Unicode(false)]
        public string Address { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string PostalCode { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string PhoneNumber { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string? TollFreePhoneNumber { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        public bool Active { get; set; }
        [Column("DivisionID")]
        public int? DivisionID { get; set; }
        [Column("ADName")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Adname { get; set; }
        [StringLength(10)]
        public string? LocationPrefix { get; set; }

        [ForeignKey("CityID")]
        [InverseProperty("Locations")]
        public virtual City City { get; set; } = null!;
        [ForeignKey("DivisionID")]
        [InverseProperty("Locations")]
        public virtual Division? Division { get; set; }
        [InverseProperty("PlantNavigation")]
        public virtual Plant Plant { get; set; } = null!;
        [InverseProperty("Location")]
        public virtual ICollection<AdditionalPhoneNumber> AdditionalPhoneNumbers { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<RandomLengthCustomer> RandomLengthCustomers { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<RandomLengthSAPSoldTo> RandomLengthSAPSoldTos { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<ScrapCustomer> ScrapCustomers { get; set; }
        [InverseProperty("Location")]
        public virtual ICollection<ScrapSAPSoldTo> ScrapSAPSoldTos { get; set; }
    }
}
