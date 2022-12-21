using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("ProductComplaintCategory")]
    public partial class ProductComplaintCategory
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? SalesTeamEmail { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string? SalesAgentType { get; set; }
    }
}
