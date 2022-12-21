using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("RiskType")]
    public partial class RiskType
    {
        [Key]
        [Column("RiskTypeID")]
        public int RiskTypeId { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public bool? Active { get; set; }
    }
}
