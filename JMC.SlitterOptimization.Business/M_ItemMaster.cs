using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_ItemMaster")]
    public partial class M_ItemMaster
    {
        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [StringLength(8)]
        [Unicode(false)]
        public string Routing { get; set; } = null!;
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
        public string TubeSurface { get; set; } = null!;
        [Column(TypeName = "decimal(5, 3)")]
        public decimal? TubeStripWidth { get; set; }
        [Key]
        [Column("BOMCoilItem")]
        [StringLength(18)]
        [Unicode(false)]
        public string BOMCoilItem { get; set; } = null!;
        [Column(TypeName = "decimal(9, 4)")]
        public decimal? TubeWeightPerFoot { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string? MinCoilGaugeCode { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string? MaxCoilGaugeCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? TubeDesc { get; set; }
        [StringLength(18)]
        [Unicode(false)]
        public string? TubeItemGroup { get; set; }
    }
}
