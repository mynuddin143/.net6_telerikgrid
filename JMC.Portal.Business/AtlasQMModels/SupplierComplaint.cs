using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("SupplierComplaint")]
    public partial class SupplierComplaint
    {
        [Key]
        [Column("SupplierComplaintID")]
        public int SupplierComplaintId { get; set; }
        [Column("VendorID")]
        public int? VendorId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Contact { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? PhoneNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? FaxNumber { get; set; }
        [Column("AtlasPONumber")]
        [StringLength(50)]
        [Unicode(false)]
        public string? AtlasPonumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Plant { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Material { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ReportedBy { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ReturnApprovedBy { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? FinishedProductAffected { get; set; }
        [Column(TypeName = "money")]
        public decimal? TruckingCharge { get; set; }
        [Column(TypeName = "money")]
        public decimal? DebitIssued { get; set; }

        [ForeignKey("SupplierComplaintId")]
        [InverseProperty("SupplierComplaint")]
        public virtual Complaint SupplierComplaintNavigation { get; set; } = null!;
    }
}
