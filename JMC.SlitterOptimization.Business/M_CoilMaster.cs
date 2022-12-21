using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_CoilMaster")]
    public partial class M_CoilMaster
    {
        [Key]
        [StringLength(18)]
        [Unicode(false)]
        public string CoilItem { get; set; } = null!;
        [StringLength(8)]
        [Unicode(false)]
        public string? CoilGrade { get; set; }
        [Key]
        [StringLength(30)]
        [Unicode(false)]
        public string TubeGrade { get; set; } = null!;
        [Key]
        [StringLength(3)]
        [Unicode(false)]
        public string CoilGaugeCode { get; set; } = null!;
        [Column(TypeName = "decimal(5, 4)")]
        public decimal CoilGauge { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string? CoilSurface { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal CoilWidth { get; set; }
        [Key]
        [StringLength(7)]
        [Unicode(false)]
        public string TubeFamily { get; set; } = null!;
        [StringLength(12)]
        [Unicode(false)]
        public string? Vendor { get; set; }
    }
}
