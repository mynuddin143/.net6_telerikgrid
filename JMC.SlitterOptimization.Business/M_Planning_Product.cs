using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_Planning_Product")]
    public partial class M_Planning_Product
    {
        [Key]
        [Column("PLANT")]
        [StringLength(30)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [Column("RESOURCE_NAME")]
        [StringLength(40)]
        [Unicode(false)]
        public string ResourceName { get; set; } = null!;
        [Key]
        [Column("PLANNING_MATERIAL")]
        [StringLength(40)]
        [Unicode(false)]
        public string PlanningMaterial { get; set; } = null!;
        [Column("FAMILY")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Family { get; set; }
        [Column("DIAMETER", TypeName = "decimal(6, 3)")]
        public decimal? Diameter { get; set; }
        [Column("TUBE_SIZE1", TypeName = "decimal(6, 3)")]
        public decimal? TubeSize1 { get; set; }
        [Column("TUBE_SIZE2", TypeName = "decimal(6, 3)")]
        public decimal? TubeSize2 { get; set; }
        [Column("SHAPE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Shape { get; set; }
        [Column("GAUGE", TypeName = "decimal(6, 3)")]
        public decimal? Gauge { get; set; }
        [Column("GRADE_GROUP")]
        [StringLength(20)]
        [Unicode(false)]
        public string? GradeGroup { get; set; }
        [Column("RUN_RATE", TypeName = "decimal(6, 3)")]
        public decimal? RunRate { get; set; }
        [Column("PRIM_SLITCOIL")]
        [StringLength(40)]
        [Unicode(false)]
        public string? PrimSlitcoil { get; set; }
        [Column("LOSS", TypeName = "decimal(5, 2)")]
        public decimal? Loss { get; set; }
        [Column("PRIM_MASTERCOIL")]
        [StringLength(40)]
        [Unicode(false)]
        public string? PrimMastercoil { get; set; }
        [Column("LAST_BLOCK_ALLOWED")]
        [StringLength(1)]
        [Unicode(false)]
        public string? LastBlockAllowed { get; set; }
        [Column("CYCLETIME", TypeName = "numeric(3, 0)")]
        public decimal? Cycletime { get; set; }
    }
}
