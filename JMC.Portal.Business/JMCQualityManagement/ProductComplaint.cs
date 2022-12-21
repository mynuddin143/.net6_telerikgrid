using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("ProductComplaint")]
    [Index("ComplaintNum", Name = "IX_ComplaintNum", IsUnique = true)]
    public partial class ProductComplaint
    {
        public ProductComplaint()
        {
            CorrectivePreventiveActions = new HashSet<CorrectivePreventiveAction>();
            ProductComplaintDocuments = new HashSet<ProductComplaintDocument>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("PlantID")]
        public int? PlantId { get; set; }
        public string? InvoiceNum { get; set; }
        public int? ComplaintNum { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IssuedDate { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerContact { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerFax { get; set; }
        public string? CustomerTrackingNum { get; set; }
        public string? CustomerLocation { get; set; }
        public string? CustomerItemNum { get; set; }
        public string? Material { get; set; }
        [Column("DepartmentID")]
        public int? DepartmentId { get; set; }
        [Column("MaterialPlantLocationID")]
        public int? MaterialPlantLocationId { get; set; }
        public string? MaterialTubeSize { get; set; }
        [Column("NonConformityID")]
        public int? NonConformityId { get; set; }
        [Column("NonConformityTypeID")]
        public int? NonConformityTypeId { get; set; }
        [Column("MaterialBOL")]
        public string? MaterialBol { get; set; }
        public string? MaterialGauge { get; set; }
        [Column("MaterialCustomerPO")]
        public string? MaterialCustomerPo { get; set; }
        public string? MaterialLength { get; set; }
        public string? MaterialSalesOrderNum { get; set; }
        public string? MaterialHeatNum { get; set; }
        [Column("MaterialShippedLocationID")]
        public int? MaterialShippedLocationId { get; set; }
        public string? MaterialPurchaseOrderNum { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MaterialQtyRejected { get; set; }
        public string? MaterialRunNum { get; set; }
        public string? MaterialVideoJetNum { get; set; }
        public string? ComplaintDescription { get; set; }
        public bool? ComplaintMaterialReturned { get; set; }
        public string? ComplaintSupplierComplaintNum { get; set; }
        public bool? ComplaintCorrectiveActionRequired { get; set; }
        public bool? ComplaintSupplierRelated { get; set; }
        public string? ComplaintFieldedBy { get; set; }
        public string? ComplaintCarNum { get; set; }
        [Column("ComplaintRGA")]
        public string? ComplaintRga { get; set; }
        public bool? ComplaintSamplesRequested { get; set; }
        public bool? ComplaintSamplesReceived { get; set; }
        public string? ActionDescription { get; set; }
        public string? ActionApprovedBy { get; set; }
        public string? ActionReturnApprovedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActionDate { get; set; }
        public bool? ActionOtherCustomersAffected { get; set; }
        public bool? ActionNotifyCustomers { get; set; }
        [StringLength(250)]
        public string? ActionAuthorizedBy { get; set; }
        public bool? ActionAgentMaterialContained { get; set; }
        public string? DispositionUnitOfMeasure { get; set; }
        public string? DispositionScrapLbs { get; set; }
        public string? DispositionReclassifyLbs { get; set; }
        public bool? DispositionReturnToSupplier { get; set; }
        public string? DispositionUsedAsIslbs { get; set; }
        public string? DispositionReworkLbs { get; set; }
        public bool? DispositionIncentiveRelated { get; set; }
        [Column("DispositionSECDGLbs")]
        public string? DispositionSecdglbs { get; set; }
        public string? DispositionReworkHoursLbs { get; set; }
        public string? DispositionCuttingOnlyLbs { get; set; }
        public string? ReturnCondition { get; set; }
        public string? ReturnCheckedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReturnCheckedDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingTruckingCharges { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingLabourCharges { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingMiscCharges { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingCreditToCustomer { get; set; }
        public string? AccountingCreditNumber { get; set; }
        public string? AccountingDebitNumber { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingCostOfQuality { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AccountingDate { get; set; }
        public string? AccountingNotes { get; set; }
        [Column("DispositionUnitOfMeasureID")]
        public int? DispositionUnitOfMeasureId { get; set; }
        [StringLength(255)]
        public string? Attachment1 { get; set; }
        [StringLength(255)]
        public string? Attachment2 { get; set; }
        [StringLength(255)]
        public string? Attachment3 { get; set; }
        [StringLength(255)]
        public string? Attachment4 { get; set; }
        [StringLength(255)]
        public string? Attachment5 { get; set; }
        [Column("CorrectivePreventiveActionID")]
        public int? CorrectivePreventiveActionId { get; set; }
        [Column("MaterialUnitOfMeasureID")]
        public int? MaterialUnitOfMeasureId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsVisitWarranted { get; set; }
        [Column("ProductComplaintCategoryID")]
        public int? ProductComplaintCategoryId { get; set; }
        [StringLength(50)]
        public string? AssignedTo { get; set; }
        public bool? FollowUpTestingRequired { get; set; }
        [StringLength(50)]
        public string? TaskAssignedTo { get; set; }
        public int? CustomerSatisfactionScore { get; set; }
        public string? CustomerSatisfactionComment { get; set; }
        public string? ShipToCustomerLocation { get; set; }
        [StringLength(50)]
        public string? ClaimStatus { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingReturnMaterial { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingReStocking { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingProdCredit { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingTesting { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AccountingTravel { get; set; }
        [Column("SAPRepNumber")]
        public int? SaprepNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CloseDate { get; set; }
        [StringLength(255)]
        public string? LastModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastModifiedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ShippedDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MaterialValue { get; set; }
        public string? EnteredByEmail { get; set; }
        [Column(TypeName = "text")]
        public string? SampleDetail { get; set; }
        [StringLength(50)]
        public string? SampleReviewedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? SampleReviewedDate { get; set; }

        [InverseProperty("ProductComplaint")]
        public virtual ICollection<CorrectivePreventiveAction> CorrectivePreventiveActions { get; set; }
        [InverseProperty("ProductComplaint")]
        public virtual ICollection<ProductComplaintDocument> ProductComplaintDocuments { get; set; }
    }
}
