using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("InvoiceAdjustment")]
    public partial class InvoiceAdjustment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? InvoiceAdjNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateIssued { get; set; }
        [StringLength(50)]
        public string? EnteredBy { get; set; }
        [Column("SalesOrgID")]
        public int? SalesOrgId { get; set; }
        [Column("PL")]
        public int? Pl { get; set; }
        [StringLength(50)]
        public string? InvoiceNumber { get; set; }
        [Column("AdjRequestTypeID")]
        public int? AdjRequestTypeId { get; set; }
        [StringLength(50)]
        public string? Quote { get; set; }
        [StringLength(50)]
        public string? CustomerNumber { get; set; }
        [Column("SONumber")]
        [StringLength(50)]
        public string? Sonumber { get; set; }
        [Column("CustomerPO")]
        [StringLength(50)]
        public string? CustomerPo { get; set; }
        [Column("CSRName")]
        [StringLength(50)]
        public string? Csrname { get; set; }
        [StringLength(50)]
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        [Column("AdjNatureID")]
        public int? AdjNatureId { get; set; }
        [Column("RGANumber")]
        public int? Rganumber { get; set; }
        public int? ClaimNumber { get; set; }
        public int? ReturnLoc { get; set; }
        [StringLength(50)]
        public string? RequestedBy { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Freight { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ReStockingFee { get; set; }
        [Column("CD", TypeName = "decimal(18, 2)")]
        public decimal? Cd { get; set; }
        [Column(TypeName = "text")]
        public string? Reason { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SubTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }
        [Column("ApproverID")]
        public int? ApproverId { get; set; }
        [StringLength(50)]
        public string? ApproverName { get; set; }
        public int? Status { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ApprovedDate { get; set; }
    }
}
