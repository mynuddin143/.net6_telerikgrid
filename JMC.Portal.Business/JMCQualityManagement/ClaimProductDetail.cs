using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("ClaimProductDetail")]
    public partial class ClaimProductDetail
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("ClaimRequestID")]
        public int ClaimRequestId { get; set; }
        [StringLength(50)]
        public string? DeliveryNumber { get; set; }
        public double? Quantity { get; set; }
        [StringLength(10)]
        public string? Unit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }
        [StringLength(20)]
        public string? MaterialNumber { get; set; }
        [StringLength(255)]
        public string? MaterialDescription { get; set; }
        [StringLength(50)]
        public string? HeatNumber { get; set; }
        [StringLength(50)]
        public string? RunNumber { get; set; }
        [Column("SONumber")]
        [StringLength(50)]
        public string? Sonumber { get; set; }
        [Column("PONumber")]
        [StringLength(50)]
        public string? Ponumber { get; set; }
        [StringLength(50)]
        public string? ShipToNumber { get; set; }
        [StringLength(255)]
        public string? ShipToAddress { get; set; }
    }
}
