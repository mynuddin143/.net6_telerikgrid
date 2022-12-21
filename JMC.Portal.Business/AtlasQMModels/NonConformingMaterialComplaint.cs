using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("NonConformingMaterialComplaint")]
    public partial class NonConformingMaterialComplaint
    {
        [Key]
        [Column("NonConformingMaterialComplaintID")]
        public int NonConformingMaterialComplaintID { get; set; }
        [Column("PlantID")]
        public int? PlantID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? OriginatedBy { get; set; }
        [Column("MillID")]
        public int? MillID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Department { get; set; }
        [StringLength(2000)]
        public string? TubeSize { get; set; }
        [StringLength(2000)]
        public string? Gauge { get; set; }
        [StringLength(2000)]
        public string? Length { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? SupplierComplaintNumber { get; set; }
        public bool SupplierRelated { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string? SalesOrderNumber { get; set; }
        [Column("ShiftID")]
        public int? ShiftID { get; set; }
        [Column("ProductTypeID")]
        public int? ProductTypeID { get; set; }
        [StringLength(2000)]
        public string? BatchNumber { get; set; }

        [ForeignKey("NonConformingMaterialComplaintId")]
        [InverseProperty("NonConformingMaterialComplaint")]
        public virtual Complaint NonConformingMaterialComplaintNavigation { get; set; } = null!;
    }
}
