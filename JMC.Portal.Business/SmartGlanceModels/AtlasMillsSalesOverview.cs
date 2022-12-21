using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("AtlasMillsSalesOverview")]
    public partial class AtlasMillsSalesOverview
    {
        [Key]
        [StringLength(80)]
        public string Plant { get; set; } = null!;
        [Key]
        [StringLength(80)]
        public string Equipment { get; set; } = null!;
        [Column("Order_Number")]
        [StringLength(80)]
        public string? OrderNumber { get; set; }
        [Column("Job_Description")]
        [StringLength(80)]
        public string? JobDescription { get; set; }
        [StringLength(80)]
        public string? State { get; set; }
        [Column("State_Color")]
        [StringLength(80)]
        public string? StateColor { get; set; }
        [StringLength(80)]
        public string? Reason { get; set; }
        [StringLength(80)]
        public string? Duration { get; set; }
        [Column("Tubes_Required")]
        public int? TubesRequired { get; set; }
        [Column("Tubes_Made")]
        public int? TubesMade { get; set; }
        public double? OrderTons { get; set; }
        public double? OrderTonsDone { get; set; }
        [Column("pic")]
        [StringLength(80)]
        public string? Pic { get; set; }
        [Column("last_update_utc", TypeName = "datetime")]
        public DateTime? LastUpdateUtc { get; set; }
    }
}
