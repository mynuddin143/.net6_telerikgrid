using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("RiskAssessment")]
    public partial class RiskAssessment
    {
        [Key]
        [Column("RiskAssessmentID")]
        public int RiskAssessmentId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column("RiskDepartmentID")]
        public int? RiskDepartmentId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        public int? Plant { get; set; }
        [Column("ChangeStatusID")]
        public int? ChangeStatusId { get; set; }
        [Column("RiskTypeID")]
        public int? RiskTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? InitiatedBy { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? EventDetails { get; set; }
        public int? CurrentStateRiskFrequency { get; set; }
        public int? CurrentStateRiskImpact { get; set; }
        public int? CurrentRate { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? ContingencyPlan { get; set; }
        public int? ProposedStateRiskFrequency { get; set; }
        public int? ProposedStateRiskImpact { get; set; }
        public int? ProposedRate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AssignedTo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CompletedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDate { get; set; }
        public bool? ExtCommunication { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? EmailPersonnel { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? EmailSubject { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EmailDueDate { get; set; }
    }
}
