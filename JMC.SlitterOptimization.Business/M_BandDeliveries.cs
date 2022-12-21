using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_BandDeliveries")]
    public partial class M_BandDeliveries
    {
        [Key]
        public int DeliveryNo { get; set; }
        [StringLength(18)]
        [Unicode(false)]
        public string Item { get; set; } = null!;
        [Column(TypeName = "smalldatetime")]
        public DateTime AvailableDate { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string DestWarehouse { get; set; } = null!;
        [StringLength(12)]
        [Unicode(false)]
        public string Vendor { get; set; } = null!;
        [Column(TypeName = "decimal(12, 3)")]
        public decimal OpenTons { get; set; }
    }
}
