using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ChangeManagement")]
    public partial class ChangeManagement
    {
        [Key]
        [Column("ChangeManagementID")]
        public int ChangeManagementId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        public int? Plant { get; set; }
        [Column("ChangeTypeID")]
        public int? ChangeTypeId { get; set; }
        public int? TypeOfChange { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? DocNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CurrentRevLevel { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? RequestedBy { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? DocumentName { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Description { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? ReasonForChange { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Effectiveness { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? AssignedTo { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? EffectivenessConclusion { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CompletedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDate { get; set; }
        [Column("ChangeStatusID")]
        public int? ChangeStatusId { get; set; }
        public int? CurrentStateRiskFrequency { get; set; }
        public int? CurrentStateRiskImpact { get; set; }
        public int? CurrentRate { get; set; }
        public int? ProposedStateRiskFrequency { get; set; }
        public int? ProposedStateRiskImpact { get; set; }
        public int? ProposedRate { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? ChangeRisk { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? ActionToMinimizeRisk { get; set; }
    }
}
