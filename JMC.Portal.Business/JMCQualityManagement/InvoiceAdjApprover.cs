using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    public partial class InvoiceAdjApprover
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int? SalesOrg { get; set; }
        [Column("PL")]
        public int? Pl { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Amount { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        public bool? AlwaysSend { get; set; }
        public bool? IsApprover { get; set; }
    }
}
