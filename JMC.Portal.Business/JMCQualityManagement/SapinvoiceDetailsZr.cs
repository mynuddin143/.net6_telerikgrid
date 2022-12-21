using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Keyless]
    [Table("SAPInvoiceDetails_ZR")]
    public partial class SapinvoiceDetailsZr
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
        public string? SoldToStreet { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? SoldToCity { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? SoldToRegion { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? SoldToPostalCode { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? SalesAgent { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? DeliveryNumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? DeliveryLine { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? CustomerPo { get; set; }
        [Column("SONumber")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Sonumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? DocCategory { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? DocType { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? SalesOrg { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? DisChannel { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? Plant { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? PlantName { get; set; }
        [Column("ActualGIDate")]
        [StringLength(20)]
        [Unicode(false)]
        public string? ActualGidate { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? MaterialNumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? ShipTo { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? ShipToName { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string? Street { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? City { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? Region { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? PostalCode { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? BilledQty { get; set; }
        [Column("UOM")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Uom { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? UnitPrice { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string? Currency { get; set; }
    }
}
