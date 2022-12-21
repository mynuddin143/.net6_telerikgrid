using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("CorrectivePreventiveActionDocument")]
    public partial class CorrectivePreventiveActionDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("CorrectivePreventiveActionID")]
        public int CorrectivePreventiveActionId { get; set; }
        [StringLength(255)]
        public string? DocumentName { get; set; }

        [ForeignKey("CorrectivePreventiveActionId")]
        [InverseProperty("CorrectivePreventiveActionDocuments")]
        public virtual CorrectivePreventiveAction CorrectivePreventiveAction { get; set; } = null!;
    }
}
