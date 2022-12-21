using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPDelivery")]
    public partial class SAPDelivery
    {
        [Key]
        [Column("SAPDeliveryID")]
        public int SAPDeliveryID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string Number { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string Address { get; set; } = null!;
        [Column("CityID")]
        public int CityID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string PostalCode { get; set; } = null!;
        [StringLength(16)]
        [Unicode(false)]
        public string Phone { get; set; } = null!;
        [StringLength(31)]
        [Unicode(false)]
        public string Fax { get; set; } = null!;
        public bool Active { get; set; }

        [ForeignKey("CityID")]
        [InverseProperty("SAPDeliverys")]
        public virtual City City { get; set; } = null!;
    }
}
