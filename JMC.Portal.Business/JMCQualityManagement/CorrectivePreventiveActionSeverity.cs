using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("CorrectivePreventiveActionSeverity")]
    public partial class CorrectivePreventiveActionSeverity
    {
        public CorrectivePreventiveActionSeverity()
        {
            CorrectivePreventiveActions = new HashSet<CorrectivePreventiveAction>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }

        [InverseProperty("CorrectivePreventiveActionSeverity")]
        public virtual ICollection<CorrectivePreventiveAction> CorrectivePreventiveActions { get; set; }
    }
}
