using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_TubeMaster")]
    public partial class MTubeMaster
    {
        [Key]
        [StringLength(18)]
        [Unicode(false)]
        public string TubeItem { get; set; } = null!;
        [Column(TypeName = "decimal(5, 3)")]
        public decimal? TubePerimeter { get; set; }
        [StringLength(7)]
        [Unicode(false)]
        public string? TubeFamily { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string? TubeGaugeCode { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? TubeGrade { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string? TubeSurface { get; set; }
        [Column(TypeName = "decimal(9, 4)")]
        public decimal? TubeWeightPerFoot { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? TubeDesc { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string? CoilSurface { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string? CoilGrade { get; set; }
        [Column(TypeName = "decimal(5, 4)")]
        public decimal? CoilGauge { get; set; }
    }
}
