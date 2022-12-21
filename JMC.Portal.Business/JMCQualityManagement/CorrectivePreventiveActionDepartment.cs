using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("CorrectivePreventiveActionDepartment")]
    public partial class CorrectivePreventiveActionDepartment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("DivisionID")]
        public long? DivisionId { get; set; }
    }
}
