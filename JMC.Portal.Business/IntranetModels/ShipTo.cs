using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("ShipTo")]
    [Index("CustomerId", "SAPShipToId", Name = "IX_ShipTo", IsUnique = true)]
    public partial class ShipTo
    {
        [Key]
        [Column("ShipToID")]
        public int ShipToID { get; set; }
        [Column("CustomerID")]
        public int CustomerID { get; set; }
        [Column("SAPShipToID")]
        [StringLength(10)]
        [Unicode(false)]
        public string SAPShipToID { get; set; } = null!;
        [StringLength(150)]
        [Unicode(false)]
        public string ShipToName { get; set; } = null!;
        [StringLength(150)]
        [Unicode(false)]
        public string Address { get; set; } = null!;
        [Column("CityID")]
        public int CityID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string PostalCode { get; set; } = null!;
        [StringLength(25)]
        [Unicode(false)]
        public string? PhoneNumber { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        public bool IsDefault { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CityID")]
        [InverseProperty("ShipTos")]
        public virtual City City { get; set; } = null!;
        [ForeignKey("CustomerID")]
        [InverseProperty("ShipTos")]
        public virtual Customer Customer { get; set; } = null!;
    }
}
