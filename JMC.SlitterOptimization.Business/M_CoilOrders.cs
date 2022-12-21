using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_CoilOrders")]
    public partial class M_CoilOrders
    {
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [StringLength(18)]
        [Unicode(false)]
        public string CoilItem { get; set; } = null!;
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
        public string PONum { get; set; } = null!;
        [Column(TypeName = "decimal(8, 3)")]
        public decimal Tons { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string Warehouse { get; set; } = null!;
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
        public decimal OrderTons { get; set; }
        [Column("Available_LeadTimeDays")]
        public byte AvailableLeadTimeDays { get; set; }
        public int? LeadTimeDays { get; set; }
        [Column("POStatusCode")]
        [StringLength(1)]
        [Unicode(false)]
        public string? PostatusCode { get; set; }
        [Column("POStatusDescShort")]
        [StringLength(30)]
        [Unicode(false)]
        public string? PostatusDescShort { get; set; }
        [Column("POStatusDescLong")]
        [StringLength(30)]
        [Unicode(false)]
        public string? PostatusDescLong { get; set; }
        [StringLength(18)]
        [Unicode(false)]
        public string? Component { get; set; }
    }
}
