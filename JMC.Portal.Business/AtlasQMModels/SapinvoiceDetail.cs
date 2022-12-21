using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Keyless]
    [Table("SAPInvoiceDetails")]
    public partial class SAPInvoiceDetail
    {
        [StringLength(50)]
        [Unicode(false)]
        public string? InvoiceNumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? SoldTo { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? CustomerName { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Street { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? City { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Region { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? PostalCode { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? SalesAgent { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? DeliveryNumber { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? CustomerPo { get; set; }
        [Column("SONumber")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Sonumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? Plant { get; set; }
        [Column("ActualGIDate", TypeName = "datetime")]
        public DateTime? ActualGidate { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? MaterialNumber { get; set; }
    }
}
