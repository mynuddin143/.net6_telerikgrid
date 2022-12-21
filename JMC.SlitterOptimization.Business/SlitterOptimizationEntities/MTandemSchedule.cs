using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_TandemSchedule")]
    public partial class MTandemSchedule
    {
        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [StringLength(8)]
        [Unicode(false)]
        public string Dept { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string? Routing { get; set; }
        [Key]
        [StringLength(18)]
        [Unicode(false)]
        public string TubeItem { get; set; } = null!;
        [Column("MONum")]
        public int? Monum { get; set; }
        [Column("MOSeq")]
        public short? Moseq { get; set; }
        [Column(TypeName = "decimal(12, 3)")]
        public decimal? Feet { get; set; }
        [Column(TypeName = "decimal(12, 3)")]
        public decimal Lbs { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal Tons { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string Source { get; set; } = null!;
        [Key]
        [Column(TypeName = "smalldatetime")]
        public DateTime FirstStartDate { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string? SeqStatus { get; set; }
        [StringLength(18)]
        [Unicode(false)]
        public string? OverrideCoil { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string Family { get; set; } = null!;
    }
}
