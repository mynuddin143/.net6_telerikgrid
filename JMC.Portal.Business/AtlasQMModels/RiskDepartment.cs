using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("RiskDepartment")]
    public partial class RiskDepartment
    {
        [Key]
        [Column("RiskDepartmentID")]
        public int RiskDepartmentId { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public bool? Active { get; set; }
    }
}
