using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwSupplierComplaint
    {
        [Column("SupplierComplaintID")]
        public int SupplierComplaintId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column("LocationID")]
        public int LocationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LocationName { get; set; } = null!;
        [Column("VendorID")]
        public int? VendorID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string VendorName { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? HeatNumber { get; set; }
    }
}
