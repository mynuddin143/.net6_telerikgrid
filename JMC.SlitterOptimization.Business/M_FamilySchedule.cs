using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_FamilySchedule")]
    public partial class M_FamilySchedule
    {
        [Key]
        [Column("M_FamilyScheduleID")]
        public long MFamilyScheduleID { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string BundlingWorkCenter { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string Family { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime FinishDate { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal Quantity { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string UnitOfMeasure { get; set; } = null!;
        public bool SlitComplete { get; set; }
        public bool RollComplete { get; set; }
    }
}
