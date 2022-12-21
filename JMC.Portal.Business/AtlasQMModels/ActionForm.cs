using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ActionForm")]
    public partial class ActionForm
    {
        public ActionForm()
        {
            Attachments = new HashSet<Attachment>();
        }

        [Key]
        [Column("ActionFormID")]
        public int ActionFormId { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column("PlantID")]
        public int PlantId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("DepartmentID")]
        public int? DepartmentId { get; set; }
        [Column("ActionFormTypeID")]
        public int? ActionFormTypeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [Column("CustomerID")]
        public int? CustomerId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? InitiatedBy { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerPartNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerRefNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Team { get; set; }
        [Column("CustomerRMANumber")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerRmanumber { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Description { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? RemedialAction { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? RootCauseDetermination { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? PermanentAction { get; set; }
        public bool Completed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletionDate { get; set; }
        public bool VerificationRequired { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerificationDueDate { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? VerificationEffectivenessActionsTaken { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? VerifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VerifiedDate { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string? DocumentationRevisionRequired { get; set; }
        public bool ChangeInternalAuditFrequency { get; set; }
        [Column("InternalProcessID")]
        public int? InternalProcessId { get; set; }
        [Column("ActionFormScopeID")]
        public int? ActionFormScopeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AssignedTo { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Status { get; set; }

        [InverseProperty("ActionForm")]
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
