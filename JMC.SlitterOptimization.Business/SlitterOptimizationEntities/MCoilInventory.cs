using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_CoilInventory")]
    public partial class MCoilInventory
    {
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [Column("TrackID")]
        [StringLength(10)]
        [Unicode(false)]
        public string TrackId { get; set; } = null!;
        [StringLength(18)]
        [Unicode(false)]
        public string CoilItem { get; set; } = null!;
        [StringLength(12)]
        [Unicode(false)]
        public string Vendor { get; set; } = null!;
        [Column(TypeName = "decimal(8, 3)")]
        public decimal Tons { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string Warehouse { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string Domestic { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string? HeatNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OriginalSource { get; set; } = null!;
        public int CoilSequence { get; set; }
        public int AllocSequence { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? AvailableDate { get; set; }
        [Column("ParentBandTrackID")]
        [StringLength(8)]
        [Unicode(false)]
        public string? ParentBandTrackId { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string? SlitOrder { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? CreateDate { get; set; }
    }
}
