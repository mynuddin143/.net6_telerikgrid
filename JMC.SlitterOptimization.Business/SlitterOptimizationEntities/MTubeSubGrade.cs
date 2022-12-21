using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_TubeSubGrade")]
    public partial class MTubeSubGrade
    {
        [Key]
        [Column("TubeGradeSubGradeID")]
        public int TubeGradeSubGradeId { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string TubeGrade { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string TubeSubGrade { get; set; } = null!;
    }
}
