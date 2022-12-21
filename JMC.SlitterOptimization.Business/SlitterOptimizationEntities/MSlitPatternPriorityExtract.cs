using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_Slit_Pattern_Priority_Extract")]
    public partial class MSlitPatternPriorityExtract
    {
        [Key]
        [Column("PRIM_SLIT_COIL")]
        [StringLength(30)]
        [Unicode(false)]
        public string PrimSlitCoil { get; set; } = null!;
        [Key]
        [Column("PLANT")]
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [Column("PRIORITY")]
        public short Priority { get; set; }
        [Column("MASTER_COIL")]
        [StringLength(30)]
        [Unicode(false)]
        public string? MasterCoil { get; set; }
        [Column("NUMBER_OF_CUTS")]
        [StringLength(50)]
        [Unicode(false)]
        public string? NumberOfCuts { get; set; }
        [Column("DROP_FAMILY")]
        [StringLength(10)]
        [Unicode(false)]
        public string? DropFamily { get; set; }
        [Column("DROP_FAMILY_GAUGE", TypeName = "decimal(4, 3)")]
        public decimal? DropFamilyGauge { get; set; }
        [Column("CUTS_ON_DROP")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CutsOnDrop { get; set; }
        [Column("SLAB_IND")]
        [StringLength(1)]
        [Unicode(false)]
        public string? SlabInd { get; set; }
        [Column("SLAB_GROUP")]
        [StringLength(3)]
        [Unicode(false)]
        public string? SlabGroup { get; set; }
        [Column("SLAB_GROUP_DESC")]
        [StringLength(30)]
        [Unicode(false)]
        public string? SlabGroupDesc { get; set; }
        [Column("OPTIMIZER_FAMILY")]
        [StringLength(10)]
        [Unicode(false)]
        public string? OptimizerFamily { get; set; }
    }
}
