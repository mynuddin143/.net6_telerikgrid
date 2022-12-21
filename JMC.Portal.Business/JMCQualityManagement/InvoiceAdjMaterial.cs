using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    public partial class InvoiceAdjMaterial
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("InvoiceAdjustmentID")]
        public int? InvoiceAdjustmentId { get; set; }
        [StringLength(50)]
        public string? DeliveryNumber { get; set; }
        [StringLength(50)]
        public string? DeliveryLine { get; set; }
        [StringLength(50)]
        public string? MaterialNumber { get; set; }
        [StringLength(50)]
        public string? MaterialDescription { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BilledQty { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BilledPrice { get; set; }
        [StringLength(10)]
        public string? Unit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BilledTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ShouldBeQty { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ShouldBePrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ShouldBeTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? LineDifference { get; set; }
    }
}
