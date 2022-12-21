using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_SteelOrders")]
    public partial class MSteelOrder
    {
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [StringLength(18)]
        [Unicode(false)]
        public string BandItem { get; set; } = null!;
        [StringLength(12)]
        [Unicode(false)]
        public string Vendor { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string Domestic { get; set; } = null!;
        [Column(TypeName = "smalldatetime")]
        public DateTime? AvailableDate { get; set; }
        [Key]
        [Column("PONum")]
        [StringLength(20)]
        [Unicode(false)]
        public string Ponum { get; set; } = null!;
        [Column(TypeName = "decimal(8, 3)")]
        public decimal Tons { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string? Warehouse { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string? ShipVia { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? OrigReqDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? OrigPromiseDate { get; set; }
        [Key]
        [Column(TypeName = "smalldatetime")]
        public DateTime CurReqDate { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? CurPromiseDate { get; set; }
        [Column(TypeName = "decimal(8, 3)")]
        public decimal? OrderTons { get; set; }
        [Column("Available_LeadTimeDays")]
        public byte? AvailableLeadTimeDays { get; set; }
        [Column("Warehouse_LeadTimeDays")]
        public byte? WarehouseLeadTimeDays { get; set; }
        [Column("Slitter_LeadTimeDays")]
        public byte? SlitterLeadTimeDays { get; set; }
        public byte? LeadTimeDays { get; set; }
        [Column("POStatus")]
        public byte? Postatus { get; set; }
        [Column("POStatusDesc")]
        [StringLength(5)]
        [Unicode(false)]
        public string? PostatusDesc { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string? VendorOrderNo { get; set; }
    }
}
