using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("RiskStatus")]
    public partial class RiskStatus
    {
        [Key]
        [Column("RiskStatusID")]
        public int RiskStatusId { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public bool? Active { get; set; }
    }
}
