using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("TrialDepartment")]
    public partial class TrialDepartment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [Column("PlantID")]
        public int PlantId { get; set; }
        public bool? Active { get; set; }
    }
}
