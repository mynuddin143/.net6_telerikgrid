using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("RGAForm")]
    public partial class Rgaform
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("RGANumber")]
        [StringLength(50)]
        public string? Rganumber { get; set; }
        [StringLength(50)]
        public string? EnteredBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateIssued { get; set; }
        [StringLength(50)]
        public string? SalesRep { get; set; }
        [StringLength(50)]
        public string? InvoiceNumber { get; set; }
        [StringLength(50)]
        public string? ClaimNumber { get; set; }
        [StringLength(255)]
        public string? CustomerName { get; set; }
        [StringLength(50)]
        public string? CustomerNumber { get; set; }
        public string? CustomerAddress { get; set; }
        [StringLength(255)]
        public string? CustomerContact { get; set; }
        [StringLength(50)]
        public string? CustomerPhone { get; set; }
        [StringLength(255)]
        public string? CustomerEmail { get; set; }
        [Column(TypeName = "text")]
        public string? ReasonForReturn { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValueOfReturn { get; set; }
        [StringLength(100)]
        public string? Freight { get; set; }
        public string? ReturnLocaltion { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalCredit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ReStockingFee { get; set; }
        [StringLength(50)]
        public string? Approver { get; set; }
        [Column("RGAStatus")]
        public int? Rgastatus { get; set; }
        public int? ReturnLocPlant { get; set; }
        public bool? ComplaintMaterialReturned { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ApprovedDate { get; set; }
        public int? ProductComplaintNum { get; set; }
    }
}
