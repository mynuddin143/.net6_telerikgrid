using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    public partial class VwCustomerComplaint
    {
        [Column("CustomerComplaintID")]
        public int? CustomerComplaintId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Number { get; set; }
        [Column("LocationID")]
        public int? LocationId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? LocationName { get; set; }
        [Column("CustomerID")]
        public int? CustomerId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? CustomerName { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? TubeSize { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? Gauge { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? Length { get; set; }
        [StringLength(2000)]
        public string? HeatNumber { get; set; }
        [Column("ReasonCodeID")]
        public int? ReasonCodeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? ReasonCodeName { get; set; }
        [StringLength(2000)]
        [Unicode(false)]
        public string? Description { get; set; }
        [StringLength(5000)]
        [Unicode(false)]
        public string? BillOfLading { get; set; }
        [Column("SAPCustomerID")]
        [StringLength(10)]
        [Unicode(false)]
        public string? SapcustomerId { get; set; }
        [Column("SAPCustomerName")]
        [StringLength(50)]
        [Unicode(false)]
        public string? SapcustomerName { get; set; }
        [Column("StatusID")]
        public int StatusId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Status { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string? Source { get; set; }
    }
}
