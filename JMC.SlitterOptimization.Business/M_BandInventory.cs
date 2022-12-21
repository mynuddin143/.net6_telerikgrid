using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_BandInventory")]
    public partial class M_BandInventory
    {
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [Column("TrackID")]
        [StringLength(10)]
        [Unicode(false)]
        public string TrackID { get; set; } = null!;
        [StringLength(18)]
        [Unicode(false)]
        public string BandItem { get; set; } = null!;
        [StringLength(12)]
        [Unicode(false)]
        public string? Vendor { get; set; }
        [Column(TypeName = "decimal(8, 3)")]
        public decimal Tons { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string Warehouse { get; set; } = null!;
        [StringLength(30)]
        [Unicode(false)]
        public string? Domestic { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? HeatNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OriginalSource { get; set; } = null!;
        public int BandSequence { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? AvailableDate { get; set; }
        public int AllocSequence { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string SlitOrder { get; set; } = null!;
        [Column("PONum")]
        public int? Ponum { get; set; }
        public bool ReservedBand { get; set; }
        public byte? OnConsignment { get; set; }
        public byte InStock { get; set; }
        public byte OnOrder { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? CurRequestDate { get; set; }
        [Column("Available_LeadTimeDays")]
        public byte AvailableLeadTimeDays { get; set; }
        [Column("Warehouse_LeadTimeDays")]
        public byte WarehouseLeadTimeDays { get; set; }
        [Column("Slitter_LeadTimeDays")]
        public byte SlitterLeadTimeDays { get; set; }
        public byte LeadTimeDays { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? CreateDate { get; set; }
    }
}
