using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("CorrectivePreventiveActionProcess")]
    public partial class CorrectivePreventiveActionProcess
    {
        public CorrectivePreventiveActionProcess()
        {
            CorrectivePreventiveActions = new HashSet<CorrectivePreventiveAction>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(10)]
        public string? Name { get; set; }

        [InverseProperty("CorrectivePreventiveActionProcess")]
        public virtual ICollection<CorrectivePreventiveAction> CorrectivePreventiveActions { get; set; }
    }
}
