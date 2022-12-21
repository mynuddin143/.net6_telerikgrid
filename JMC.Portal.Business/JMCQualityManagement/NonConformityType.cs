using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("NonConformityType")]
    public partial class NonConformityType
    {
        public NonConformityType()
        {
            NonConformities = new HashSet<NonConformity>();
            SupplierComplaints = new HashSet<SupplierComplaint>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column("InvoiceAdjNatureID")]
        public int? InvoiceAdjNatureId { get; set; }

        [InverseProperty("NonConformityType")]
        public virtual ICollection<NonConformity> NonConformities { get; set; }
        [InverseProperty("NonConformityType")]
        public virtual ICollection<SupplierComplaint> SupplierComplaints { get; set; }
    }
}
