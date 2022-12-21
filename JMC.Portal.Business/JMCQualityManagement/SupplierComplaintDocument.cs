using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("SupplierComplaintDocument")]
    public partial class SupplierComplaintDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("SupplierComplaintID")]
        public int SupplierComplaintId { get; set; }
        [StringLength(250)]
        public string? DocumentName { get; set; }

        [ForeignKey("SupplierComplaintId")]
        [InverseProperty("SupplierComplaintDocuments")]
        public virtual SupplierComplaint SupplierComplaint { get; set; } = null!;
    }
}
