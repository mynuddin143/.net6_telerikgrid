using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_APO_Rolling_Schedule_Import")]
    public partial class MApoRollingScheduleImport
    {
        [Key]
        [Column("M_APO_Rolling_Schedule_ImportID")]
        public long MApoRollingScheduleImportId { get; set; }
        [Column("PLANT")]
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Column("BUNDLING_W")]
        [StringLength(10)]
        [Unicode(false)]
        public string BundlingW { get; set; } = null!;
        [Column("MATERIAL_NUMBER")]
        [StringLength(18)]
        [Unicode(false)]
        public string MaterialNumber { get; set; } = null!;
        [Column("PLANNED_QUANTITY", TypeName = "decimal(13, 3)")]
        public decimal? PlannedQuantity { get; set; }
        [Column("FINISH_DAT", TypeName = "smalldatetime")]
        public DateTime FinishDat { get; set; }
        [Column("SLIT_COIL")]
        [StringLength(18)]
        [Unicode(false)]
        public string? SlitCoil { get; set; }
        [Column("RECEIVED_QUANTITY", TypeName = "decimal(13, 3)")]
        public decimal? ReceivedQuantity { get; set; }
        [Column("SOURCE")]
        [StringLength(3)]
        [Unicode(false)]
        public string? Source { get; set; }
        [Column("FAMILY")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Family { get; set; }
        [Column("FAMILY_ZPP01")]
        [StringLength(10)]
        [Unicode(false)]
        public string? FamilyZpp01 { get; set; }
        [Column("BLOCK_NUMBER")]
        [StringLength(12)]
        [Unicode(false)]
        public string? BlockNumber { get; set; }
        [Column("B_FIXED")]
        [StringLength(1)]
        [Unicode(false)]
        public string? BFixed { get; set; }
        [Column("GAUGE", TypeName = "decimal(4, 3)")]
        public decimal? Gauge { get; set; }
        [Column("GRADE_GROUP")]
        [StringLength(30)]
        [Unicode(false)]
        public string? GradeGroup { get; set; }
        [Column("Comb_Free_Cap", TypeName = "decimal(30, 3)")]
        public decimal? CombFreeCap { get; set; }
        [Column("Stock_Ord", TypeName = "decimal(15, 3)")]
        public decimal? StockOrd { get; set; }
        [Column("Stock_Req", TypeName = "decimal(15, 3)")]
        public decimal? StockReq { get; set; }
        [Column("Blk_Hrs", TypeName = "decimal(30, 2)")]
        public decimal? BlkHrs { get; set; }
        [Column("Stk_Level")]
        public int? StkLevel { get; set; }
        [Column("Plan_Hrs", TypeName = "decimal(30, 2)")]
        public decimal? PlanHrs { get; set; }
        [Column("Book_Hrs", TypeName = "decimal(30, 2)")]
        public decimal? BookHrs { get; set; }
        [Column("StkOrd_Hrs", TypeName = "decimal(30, 2)")]
        public decimal? StkOrdHrs { get; set; }
    }
}
