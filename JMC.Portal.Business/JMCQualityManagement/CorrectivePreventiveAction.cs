using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("CorrectivePreventiveAction")]
    [Index("CarNumber", Name = "IX_CarNumber", IsUnique = true)]
    public partial class CorrectivePreventiveAction
    {
        public CorrectivePreventiveAction()
        {
            CorrectivePreventiveActionDocuments = new HashSet<CorrectivePreventiveActionDocument>();
            SupplierComplaints = new HashSet<SupplierComplaint>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string CarNumber { get; set; } = null!;
        [Column("ProductComplaintID")]
        public int? ProductComplaintId { get; set; }
        [Column("CorrectivePreventiveActionScopeID")]
        public int? CorrectivePreventiveActionScopeId { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [StringLength(250)]
        public string? Customer { get; set; }
        [StringLength(50)]
        public string? CustomerPartNumber { get; set; }
        [StringLength(50)]
        public string? CustomerRefNumber { get; set; }
        [Column("CustomerRMANumber")]
        [StringLength(50)]
        public string? CustomerRmanumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateIssued { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateDue { get; set; }
        [StringLength(250)]
        public string? InitiatedBy { get; set; }
        [StringLength(250)]
        public string? TeamLead { get; set; }
        [Column("CorrectivePreventiveActionSeverityID")]
        public int? CorrectivePreventiveActionSeverityId { get; set; }
        [Column(TypeName = "text")]
        public string? ProblemDescription { get; set; }
        [Column(TypeName = "text")]
        public string? StatementOfRequirements { get; set; }
        [Column(TypeName = "text")]
        public string? ContainmentAction { get; set; }
        [Column(TypeName = "text")]
        public string? RootCause { get; set; }
        [Column(TypeName = "text")]
        public string? CorrectiveActionPlan { get; set; }
        [Column(TypeName = "text")]
        public string? PreventiveActionPlan { get; set; }
        public bool? Completed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDate { get; set; }
        public bool? LeadPersonVerify { get; set; }
        public bool? VerificationRequired { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerificationDueDate { get; set; }
        [StringLength(250)]
        public string? AssignedTo { get; set; }
        [Column(TypeName = "text")]
        public string? FollowUpVerificationActivities { get; set; }
        [StringLength(250)]
        public string? FollowUpVerifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FollowUpVerifiedDate { get; set; }
        [Column(TypeName = "text")]
        public string? VerificationActivity { get; set; }
        [StringLength(250)]
        public string? VerificationActivityVerifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerificationActivityVerifiedDate { get; set; }
        [Column(TypeName = "text")]
        public string? DocumentationRevisionRequired { get; set; }
        public bool? ChangeInInternalAuditFrequency { get; set; }
        [Column("CorrectivePreventiveActionProcessID")]
        public int? CorrectivePreventiveActionProcessId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? RiskAssessmentRequired { get; set; }
        [StringLength(250)]
        public string? RiskAssessmentPersonnel { get; set; }
        [StringLength(250)]
        public string? Team { get; set; }
        public bool? ManagementOfChangeRequired { get; set; }
        [Column("CARStatus")]
        [StringLength(10)]
        public string? Carstatus { get; set; }
        [Column("IsCARDeleted")]
        public bool? IsCardeleted { get; set; }
        [Column("ActionTypeID")]
        public int? ActionTypeId { get; set; }

        [ForeignKey("CorrectivePreventiveActionProcessId")]
        [InverseProperty("CorrectivePreventiveActions")]
        public virtual CorrectivePreventiveActionProcess? CorrectivePreventiveActionProcess { get; set; }
        [ForeignKey("CorrectivePreventiveActionScopeId")]
        [InverseProperty("CorrectivePreventiveActions")]
        public virtual CorrectivePreventiveActionScope? CorrectivePreventiveActionScope { get; set; }
        [ForeignKey("CorrectivePreventiveActionSeverityId")]
        [InverseProperty("CorrectivePreventiveActions")]
        public virtual CorrectivePreventiveActionSeverity? CorrectivePreventiveActionSeverity { get; set; }
        [ForeignKey("DepartmentId")]
        [InverseProperty("CorrectivePreventiveActions")]
        public virtual Department Department { get; set; } = null!;
        [ForeignKey("ProductComplaintId")]
        [InverseProperty("CorrectivePreventiveActions")]
        public virtual ProductComplaint? ProductComplaint { get; set; }
        [InverseProperty("CorrectivePreventiveAction")]
        public virtual ICollection<CorrectivePreventiveActionDocument> CorrectivePreventiveActionDocuments { get; set; }
        [InverseProperty("CorrectivePreventiveAction")]
        public virtual ICollection<SupplierComplaint> SupplierComplaints { get; set; }
    }
}
