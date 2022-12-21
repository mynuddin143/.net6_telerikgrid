using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Table("UnitOfMeasure")]
    public partial class UnitOfMeasure
    {
        public UnitOfMeasure()
        {
            SupplierComplaintMaterialUnitOfMeasures = new HashSet<SupplierComplaint>();
            SupplierComplaintUnitOfMeasures = new HashSet<SupplierComplaint>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [InverseProperty("MaterialUnitOfMeasure")]
        public virtual ICollection<SupplierComplaint> SupplierComplaintMaterialUnitOfMeasures { get; set; }
        [InverseProperty("UnitOfMeasure")]
        public virtual ICollection<SupplierComplaint> SupplierComplaintUnitOfMeasures { get; set; }
    }
}
