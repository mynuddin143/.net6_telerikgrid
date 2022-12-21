using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    public partial class SalesAgent
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("SAPRepNumber")]
        [StringLength(4)]
        [Unicode(false)]
        public string? SaprepNumber { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Name { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string? AgentType { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? EmailAddress { get; set; }
    }
}
