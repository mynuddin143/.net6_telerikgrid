using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Keyless]
    [Table("QDRForms")]
    public partial class Qdrform
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("QDRNumber")]
        [StringLength(50)]
        public string? Qdrnumber { get; set; }
        [StringLength(50)]
        public string? Vendor { get; set; }
        [Column("PlantID")]
        public int? PlantId { get; set; }
        public int? Year { get; set; }
        [Column("PONumber")]
        [StringLength(50)]
        public string? Ponumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateRejected { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateSlit { get; set; }
        [StringLength(50)]
        public string? BarCode { get; set; }
        [StringLength(50)]
        public string? OrgTag { get; set; }
        [StringLength(50)]
        public string? HoldTag { get; set; }
        [StringLength(50)]
        public string? ProductType { get; set; }
        [Column("QDRType")]
        [StringLength(50)]
        public string? Qdrtype { get; set; }
        [StringLength(255)]
        public string? ProdDesc { get; set; }
        [StringLength(50)]
        public string? TubeSize { get; set; }
        [StringLength(50)]
        public string? Gauge { get; set; }
        [StringLength(50)]
        public string? CoilWidth { get; set; }
        [StringLength(50)]
        public string? MultWidth { get; set; }
        [StringLength(100)]
        public string? CoilNumber { get; set; }
        [StringLength(50)]
        public string? HeatNumber { get; set; }
        [StringLength(50)]
        public string? MillWeight { get; set; }
        [StringLength(50)]
        public string? ActualWeight { get; set; }
        [StringLength(50)]
        public string? WeightDiff { get; set; }
        [StringLength(50)]
        public string? ScrapWeight { get; set; }
        public string? CoilRejectreason { get; set; }
        public string? ProblemDesc { get; set; }
        [StringLength(100)]
        public string? SlitterForeman { get; set; }
        [StringLength(50)]
        public string? ClaimStatus { get; set; }
        [StringLength(50)]
        public string? ClaimNumber { get; set; }
        [StringLength(255)]
        public string? ContactInfo { get; set; }
        [StringLength(50)]
        public string? ContactApproved { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        public string? ShippingNote { get; set; }
        public string? VendorComment { get; set; }
        [Column("ApprovedWTC")]
        [StringLength(10)]
        public string? ApprovedWtc { get; set; }
        [Column("QAResolution")]
        public string? Qaresolution { get; set; }
        public string? InvResolution { get; set; }
        public string? AccResolution { get; set; }
        [StringLength(50)]
        public string? Carrier { get; set; }
        [StringLength(100)]
        public string? CarrierContact { get; set; }
        [StringLength(20)]
        public string? CarrierPhone { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReqShipDate { get; set; }
        public string? ConsigneeAddress { get; set; }
        [Column("QDRStatus")]
        [StringLength(10)]
        public string? Qdrstatus { get; set; }
    }
}
