using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_SlitOrders")]
    public partial class MSlitOrder
    {
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Column("MONum")]
        public int Monum { get; set; }
        [Key]
        [StringLength(18)]
        [Unicode(false)]
        public string BandItem { get; set; } = null!;
        [Key]
        [StringLength(18)]
        [Unicode(false)]
        public string CoilItem { get; set; } = null!;
        public byte Cuts { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal OrderTons { get; set; }
        [Column("M_OrderNo")]
        public int MOrderNo { get; set; }
        [Key]
        [Column("M_Order")]
        [StringLength(10)]
        [Unicode(false)]
        public string MOrder { get; set; } = null!;
        public bool Processed { get; set; }
        public int? AllocSequence { get; set; }
        public int? BandPattern { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? BandSlitter { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? Workcenter { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string? ReceivingLocation { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? DueDate { get; set; }
        [Column("MOStatus")]
        [StringLength(3)]
        [Unicode(false)]
        public string? Mostatus { get; set; }
        public int? TargetBands { get; set; }
        public byte HasTransfer { get; set; }
    }
}
