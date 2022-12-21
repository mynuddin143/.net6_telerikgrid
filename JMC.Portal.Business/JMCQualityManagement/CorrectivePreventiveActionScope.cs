﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("CorrectivePreventiveActionScope")]
    public partial class CorrectivePreventiveActionScope
    {
        public CorrectivePreventiveActionScope()
        {
            CorrectivePreventiveActions = new HashSet<CorrectivePreventiveAction>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }

        [InverseProperty("CorrectivePreventiveActionScope")]
        public virtual ICollection<CorrectivePreventiveAction> CorrectivePreventiveActions { get; set; }
    }
}
