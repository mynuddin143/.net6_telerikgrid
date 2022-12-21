using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("SupplierComplaint")]
    [Index("ComplaintNum", Name = "IX_ComplaintNum", IsUnique = true)]
    public partial class SupplierComplaint
    {
        public SupplierComplaint()
        {
            SupplierComplaintDocuments = new HashSet<SupplierComplaintDocument>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int ComplaintNum { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime IssuedDate { get; set; }
        [Column("SupplierID")]
        public int SupplierId { get; set; }
        [StringLength(250)]
        public string? SupplierContact { get; set; }
        [StringLength(50)]
        public string? SupplierPhone { get; set; }
        [StringLength(50)]
        public string SupplierPlant { get; set; } = null!;
        [StringLength(50)]
        public string? SupplierFax { get; set; }
        [Column("SupplierPO")]
        [StringLength(50)]
        public string? SupplierPo { get; set; }
        [StringLength(250)]
        public string Material { get; set; } = null!;
        [Column("NonConformityID")]
        public int? NonConformityId { get; set; }
        [Column("NonConformityTypeID")]
        public int? NonConformityTypeId { get; set; }
        [StringLength(50)]
        public string? MaterialGauge { get; set; }
        [Column("MaterialBOLWeight")]
        [StringLength(50)]
        public string? MaterialBolweight { get; set; }
        [StringLength(50)]
        public string? MaterialHeatNum { get; set; }
        [Column("DefectClassificationID")]
        public int? DefectClassificationId { get; set; }
        [StringLength(50)]
        public string? MaterialTagNum { get; set; }
        [StringLength(50)]
        public string? MaterialCoilNum { get; set; }
        [StringLength(50)]
        public string? MaterialMultNum { get; set; }
        [StringLength(50)]
        public string? MaterialPcsPerLift { get; set; }
        [StringLength(50)]
        public string? MaterialLifts { get; set; }
        [StringLength(50)]
        public string? MaterialTotalPcs { get; set; }
        [StringLength(50)]
        public string MaterialComplainFieldedBy { get; set; } = null!;
        [StringLength(50)]
        public string? MaterialReturnApprovedBy { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? MaterialQtyRejected { get; set; }
        [Column(TypeName = "text")]
        public string ComplaintDescription { get; set; } = null!;
        public bool? ComplaintCorrectiveActionRequired { get; set; }
        [Column("CorrectivePreventiveActionID")]
        public int? CorrectivePreventiveActionId { get; set; }
        [Column(TypeName = "text")]
        public string? FinishedProductAffected { get; set; }
        [Column(TypeName = "text")]
        public string? ActionTaken { get; set; }
        [StringLength(50)]
        public string? ActionApprovedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActionDate { get; set; }
        [Column("UnitOfMeasureID")]
        public int? UnitOfMeasureId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DispositionScrap { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DispositionReclassify { get; set; }
        public bool? DispositionReturnToSupplier { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DispositionUsedAsIs { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DispositionRework { get; set; }
        public bool? DispositionIncentiveRelated { get; set; }
        [Column("DispositionSEC", TypeName = "decimal(18, 4)")]
        public decimal? DispositionSec { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DispositionReworkHours { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? DispositionCutting { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingTrucking { get; set; }
        [StringLength(50)]
        public string? AccountingCreditNum { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AccountingDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingLabour { get; set; }
        [StringLength(50)]
        public string? AccountingDebitNum { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingMisc { get; set; }
        [Column(TypeName = "text")]
        public string? AccountingNotes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AccountingDebitIssued { get; set; }
        [Column("MaterialUnitOfMeasureID")]
        public int? MaterialUnitOfMeasureId { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("CorrectivePreventiveActionId")]
        [InverseProperty("SupplierComplaints")]
        public virtual CorrectivePreventiveAction? CorrectivePreventiveAction { get; set; }
        [ForeignKey("MaterialUnitOfMeasureId")]
        [InverseProperty("SupplierComplaintMaterialUnitOfMeasures")]
        public virtual UnitOfMeasure? MaterialUnitOfMeasure { get; set; }
        [ForeignKey("NonConformityId")]
        [InverseProperty("SupplierComplaints")]
        public virtual NonConformity? NonConformity { get; set; }
        [ForeignKey("NonConformityTypeId")]
        [InverseProperty("SupplierComplaints")]
        public virtual NonConformityType? NonConformityType { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        [InverseProperty("SupplierComplaintUnitOfMeasures")]
        public virtual UnitOfMeasure? UnitOfMeasure { get; set; }
        [InverseProperty("SupplierComplaint")]
        public virtual ICollection<SupplierComplaintDocument> SupplierComplaintDocuments { get; set; }
    }
}
