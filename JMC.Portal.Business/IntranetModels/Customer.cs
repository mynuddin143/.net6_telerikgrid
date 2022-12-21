using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Customer")]
    [Index("SapcustomerId", Name = "IX_Customer", IsUnique = true)]
    [Index("InsideSalesRepId", Name = "ix_InsideSalesRepID")]
    public partial class Customer
    {
        public Customer()
        {
            CustomerUsers = new HashSet<CustomerUser>();
            RandomLengthCustomers = new HashSet<RandomLengthCustomer>();
            ScrapCustomers = new HashSet<ScrapCustomer>();
            ShipTos = new HashSet<ShipTo>();
        }

        [Key]
        [Column("CustomerID")]
        public int CustomerID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCustomerID")]
        [StringLength(10)]
        [Unicode(false)]
        public string SAPCustomerID { get; set; } = null!;
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
        public string? FaxNumber { get; set; }
        [Column("InsideSalesRepID")]
        public int? InsideSalesRepID { get; set; }
        [Column("OutsideSalesRepID")]
        public int? OutsideSalesRepID { get; set; }
        public bool Active { get; set; }
        public bool AllowOrderCreation { get; set; }

        [ForeignKey("CityID")]
        [InverseProperty("Customers")]
        public virtual City City { get; set; } = null!;
        [ForeignKey("InsideSalesRepID")]
        [InverseProperty("Customers")]
        public virtual InsideSalesRep? InsideSalesRep { get; set; }
        [ForeignKey("OutsideSalesRepID")]
        [InverseProperty("Customers")]
        public virtual OutsideSalesRep? OutsideSalesRep { get; set; }
        [InverseProperty("PrimaryCustomer")]
        public virtual ICollection<CustomerUser> CustomerUsers { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<RandomLengthCustomer> RandomLengthCustomers { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<ScrapCustomer> ScrapCustomers { get; set; }
        [InverseProperty("Customer")]
        public virtual ICollection<ShipTo> ShipTos { get; set; }
    }
}
