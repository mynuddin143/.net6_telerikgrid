using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("City")]
    [Index("Name", "StateId", Name = "IX_City", IsUnique = true)]
    public partial class City
    {
        public City()
        {
            Customers = new HashSet<Customer>();
            Locations = new HashSet<Location>();
            SAPShipTos = new HashSet<SAPShipTo>();
            SAPDeliverys = new HashSet<SAPDelivery>();
            ShipTos = new HashSet<ShipTo>();
            Vendors = new HashSet<Vendor>();
        }

        [Key]
        [Column("CityID")]
        public int CityID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("StateID")]
        public int StateID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("StateID")]
        [InverseProperty("Cities")]
        public virtual State State { get; set; } = null!;
        [InverseProperty("City")]
        public virtual ICollection<Customer> Customers { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<Location> Locations { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<SAPShipTo> SAPShipTos { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<SAPDelivery> SAPDeliverys { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<ShipTo> ShipTos { get; set; }
        [InverseProperty("City")]
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
