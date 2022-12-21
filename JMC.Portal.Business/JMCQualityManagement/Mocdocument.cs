using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("MOCDocument")]
    public partial class Mocdocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("MOCID")]
        public int Mocid { get; set; }
        [StringLength(255)]
        public string? DocumentName { get; set; }

        [ForeignKey("Mocid")]
        [InverseProperty("Mocdocuments")]
        public virtual ManagementOfChange Moc { get; set; } = null!;
    }
}
