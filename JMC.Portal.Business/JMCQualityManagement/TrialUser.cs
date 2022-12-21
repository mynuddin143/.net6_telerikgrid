using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("TrialUser")]
    public partial class TrialUser
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("TrialDepartmentID")]
        public int TrialDepartmentId { get; set; }
        [Column("PlantID")]
        public int? PlantId { get; set; }
    }
}
