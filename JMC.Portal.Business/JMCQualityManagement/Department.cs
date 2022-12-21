using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            CorrectivePreventiveActions = new HashSet<CorrectivePreventiveAction>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("DivisionID")]
        public long? DivisionId { get; set; }
        [Column("PlantID")]
        public long? PlantId { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<CorrectivePreventiveAction> CorrectivePreventiveActions { get; set; }
    }
}
