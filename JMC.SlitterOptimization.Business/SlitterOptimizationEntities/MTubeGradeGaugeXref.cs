﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_TubeGradeGaugeXRef")]
    public partial class MTubeGradeGaugeXref
    {
        [Key]
        [Column("TubeGradeGaugeXRefID")]
        public int TubeGradeGaugeXrefId { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string TubeGrade { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string GaugeCode { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string BandGrade { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string BandMinNom { get; set; } = null!;
        [Column(TypeName = "decimal(18, 4)")]
        public decimal BandMinGauge { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal BandMaxGauge { get; set; }
    }
}
