using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("AtlasMillsCurrentShiftSummary")]
    public partial class AtlasMillsCurrentShiftSummary
    {
        [Key]
        [StringLength(40)]
        public string Division { get; set; } = null!;
        [Key]
        [StringLength(40)]
        public string Plant { get; set; } = null!;
        [Column("State_Color")]
        [StringLength(40)]
        public string? StateColor { get; set; }
        [Column("pic")]
        [StringLength(40)]
        public string? Pic { get; set; }
        [Key]
        [StringLength(40)]
        public string Equipment { get; set; } = null!;
        [StringLength(40)]
        public string? Duration { get; set; }
        public double? Tons { get; set; }
        [Column("Uptime %")]
        public double? Uptime { get; set; }
        [Column("TPSH")]
        public double? Tpsh { get; set; }
        [Column("Job_Description")]
        [StringLength(80)]
        public string? JobDescription { get; set; }
        public int? Downtime { get; set; }
        [StringLength(40)]
        public string? Shift { get; set; }
        [StringLength(40)]
        public string? State { get; set; }
        [StringLength(80)]
        public string? Reason { get; set; }
    }
}
