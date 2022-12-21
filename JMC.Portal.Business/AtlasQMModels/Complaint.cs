using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("Complaint")]
    public partial class Complaint
    {
        public Complaint()
        {
            Attachments = new HashSet<Attachment>();
        }

        [Key]
        [Column("ComplaintID")]
        public int ComplaintID { get; set; }
        [Column("UserID")]
        public int UserID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column("LocationID")]
        public int LocationID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [StringLength(2000)]
        public string? HeatNumber { get; set; }
        [Column("ReasonCodeID")]
        public int? ReasonCodeID { get; set; }
        [Column("ClassificationID")]
        public int? ClassificationID { get; set; }
        [Column("NonConformanceTypeID")]
        public int? NonConformanceTypeID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? QuantityRejected { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Description { get; set; }
        public bool CorrectiveActionRequired { get; set; }
        [Column("CARNumber")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Carnumber { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? ActionTaken { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ActionApprovedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActionDate { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Scrap { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? UsedAsIs { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? CuttingOnly { get; set; }
        [Column("OffcutDG", TypeName = "decimal(18, 4)")]
        public decimal? OffcutDg { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Rework { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? ReworkHours { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Reclassify { get; set; }
        public bool ReturnToSupplier { get; set; }
        public bool Incentive { get; set; }
        [Column(TypeName = "money")]
        public decimal? LabourCharge { get; set; }
        [Column(TypeName = "money")]
        public decimal? MiscCharge { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? DebitNumber { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? CreditNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ChargeDate { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? ChargeNotes { get; set; }
        public bool? OtherCustomersAffected { get; set; }
        public bool? NotifyCustomer { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ApprovedBy { get; set; }
        public bool? Incomplete { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? BatchNumber { get; set; }

        [ForeignKey("ClassificationID")]
        [InverseProperty("Complaints")]
        public virtual Classification? Classification { get; set; }
        [ForeignKey("NonConformanceTypeID")]
        [InverseProperty("Complaints")]
        public virtual NonConformanceType? NonConformanceType { get; set; }
        [ForeignKey("ReasonCodeID")]
        [InverseProperty("Complaints")]
        public virtual ReasonCode? ReasonCode { get; set; }
        [InverseProperty("CustomerComplaintNavigation")]
        public virtual CustomerComplaint CustomerComplaint { get; set; } = null!;
        [InverseProperty("CustomerServiceComplaintNavigation")]
        public virtual CustomerServiceComplaint CustomerServiceComplaint { get; set; } = null!;
        [InverseProperty("NonConformingMaterialComplaintNavigation")]
        public virtual NonConformingMaterialComplaint NonConformingMaterialComplaint { get; set; } = null!;
        [InverseProperty("SupplierComplaintNavigation")]
        public virtual SupplierComplaint SupplierComplaint { get; set; } = null!;
        [InverseProperty("Complaint")]
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
