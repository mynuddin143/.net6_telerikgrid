using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("InvoiceAdjPL")]
    public partial class InvoiceAdjPl
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(10)]
        public string? Name { get; set; }
    }
}
