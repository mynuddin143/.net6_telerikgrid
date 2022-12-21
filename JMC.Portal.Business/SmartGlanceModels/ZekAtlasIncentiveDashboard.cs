using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.SmartGlanceModel
{
    [Table("zekAtlasIncentiveDashboard")]
    public partial class ZekAtlasIncentiveDashboard
    {
        [Key]
        [Column("row_id")]
        public int RowId { get; set; }
        [StringLength(80)]
        public string Server { get; set; } = null!;
        [StringLength(80)]
        public string Plant { get; set; } = null!;
        [StringLength(80)]
        public string? PerEnd { get; set; }
        [StringLength(80)]
        public string? Updated { get; set; }
        [StringLength(1000)]
        public string? Cols { get; set; }
        [StringLength(1000)]
        public string? Strs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdated { get; set; }
    }
}
