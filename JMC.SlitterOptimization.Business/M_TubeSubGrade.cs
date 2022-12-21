using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_TubeSubGrade")]
    public partial class M_TubeSubGrade
    {
        [Key]
        [Column("TubeGradeSubGradeID")]
        public int TubeGradeSubGradeID { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string TubeGrade { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string TubeSubGrade { get; set; } = null!;
    }
}
