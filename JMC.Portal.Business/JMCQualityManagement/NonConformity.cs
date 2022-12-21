using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("NonConformity")]
    public partial class NonConformity
    {
        public NonConformity()
        {
            SupplierComplaints = new HashSet<SupplierComplaint>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("NonConformityTypeID")]
        public int NonConformityTypeId { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;

        [ForeignKey("NonConformityTypeId")]
        [InverseProperty("NonConformities")]
        public virtual NonConformityType NonConformityType { get; set; } = null!;
        [InverseProperty("NonConformity")]
        public virtual ICollection<SupplierComplaint> SupplierComplaints { get; set; }
    }
}
