using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("ClaimRequest")]
    public partial class ClaimRequest
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? ClaimNumber { get; set; }
        [Column("SONumber")]
        [StringLength(50)]
        public string? Sonumber { get; set; }
        [Column("PONumber")]
        [StringLength(50)]
        public string? Ponumber { get; set; }
        [StringLength(50)]
        public string? HeatNumber { get; set; }
        [StringLength(50)]
        public string? RunNumber { get; set; }
        [StringLength(50)]
        public string? ContactName { get; set; }
        [StringLength(50)]
        public string? ContactPhone { get; set; }
        [Column(TypeName = "text")]
        public string? Description { get; set; }
        [StringLength(50)]
        public string? ReportedBy { get; set; }
        [StringLength(50)]
        public string? Site { get; set; }
        public int? ClaimType { get; set; }
        public bool? SignedForException { get; set; }
        public bool? Isreturn { get; set; }
        [StringLength(50)]
        public string? BatchNumber { get; set; }
        [StringLength(255)]
        public string? ContactEmail { get; set; }
        [Column("ProductComplaintID")]
        public int? ProductComplaintId { get; set; }
        [Column("RGAFormID")]
        public int? RgaformId { get; set; }
        [StringLength(50)]
        public string? SoldToNumber { get; set; }
        [StringLength(255)]
        public string? SoldToName { get; set; }
        [StringLength(50)]
        public string? ShipToNumber { get; set; }
        [StringLength(255)]
        public string? ShipToAddress { get; set; }
        [StringLength(50)]
        public string? ProductComplaintNo { get; set; }
        [Column("RGAFormNo")]
        [StringLength(50)]
        public string? RgaformNo { get; set; }
    }
}
