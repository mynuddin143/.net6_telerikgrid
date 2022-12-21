using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Carrier")]
    public partial class Carrier
    {
        [Key]
        [Column("CarrierID")]
        public int CarrierId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCarrierID")]
        [StringLength(25)]
        [Unicode(false)]
        public string SapcarrierId { get; set; } = null!;
    }
}
