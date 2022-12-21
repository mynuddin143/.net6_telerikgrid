using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_BandMaster")]
    public partial class M_BandMaster
    {
        [Key]
        [StringLength(18)]
        [Unicode(false)]
        public string BandItem { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string BandGrade { get; set; } = null!;
        [StringLength(3)]
        [Unicode(false)]
        public string BandGaugeCode { get; set; } = null!;
        [Column(TypeName = "decimal(5, 4)")]
        public decimal BandGauge { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string? BandSurface { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal BandWidth { get; set; }
        public byte IsPriceProtected { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string MinNom { get; set; } = null!;
    }
}
