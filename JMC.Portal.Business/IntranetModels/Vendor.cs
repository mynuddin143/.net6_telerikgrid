using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Vendor")]
    [Index("SAPDeliveryId", Name = "IX_Vendor", IsUnique = true)]
    public partial class Vendor
    {
        [Key]
        [Column("VendorID")]
        public int VendorID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPDeliveryID")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPDeliveryID { get; set; } = null!;
        [Column("CityID")]
        public int CityID { get; set; }
        [StringLength(150)]
        [Unicode(false)]
        public string Address { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string PostalCode { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string PhoneNumber { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        public bool Active { get; set; }

        [ForeignKey("CityID")]
        [InverseProperty("Vendors")]
        public virtual City City { get; set; } = null!;
    }
}
