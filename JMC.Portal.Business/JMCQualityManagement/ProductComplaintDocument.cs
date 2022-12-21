using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("ProductComplaintDocument")]
    public partial class ProductComplaintDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("ProductComplaintID")]
        public int ProductComplaintId { get; set; }
        [StringLength(255)]
        public string? DocumentName { get; set; }

        [ForeignKey("ProductComplaintId")]
        [InverseProperty("ProductComplaintDocuments")]
        public virtual ProductComplaint ProductComplaint { get; set; } = null!;
    }
}
