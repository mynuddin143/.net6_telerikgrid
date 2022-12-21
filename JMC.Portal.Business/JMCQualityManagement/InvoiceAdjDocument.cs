using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("InvoiceAdjDocument")]
    public partial class InvoiceAdjDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("InvoiceAdjustmentID")]
        public int? InvoiceAdjustmentId { get; set; }
        [StringLength(255)]
        public string? DocumentName { get; set; }
    }
}
