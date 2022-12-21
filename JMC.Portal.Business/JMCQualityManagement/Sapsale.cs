using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Keyless]
    [Table("SAPSales")]
    public partial class Sapsale
    {
        [Unicode(false)]
        public string? InvoiceNumber { get; set; }
        [Unicode(false)]
        public string? SoldTo { get; set; }
        [Unicode(false)]
        public string? CustomerName { get; set; }
        [Unicode(false)]
        public string? SoldToStreet { get; set; }
        [Unicode(false)]
        public string? SoldToCity { get; set; }
        [Unicode(false)]
        public string? SoldToRegion { get; set; }
        [Unicode(false)]
        public string? SoldToPostalCode { get; set; }
        [Unicode(false)]
        public string? SalesAgent { get; set; }
        [Unicode(false)]
        public string? DeliveryNumber { get; set; }
        [Unicode(false)]
        public string? DeliveryLine { get; set; }
        [Unicode(false)]
        public string? CustomerPo { get; set; }
        [Column("SONumber")]
        [Unicode(false)]
        public string? Sonumber { get; set; }
        [Unicode(false)]
        public string? DocCategory { get; set; }
        [Unicode(false)]
        public string? DocType { get; set; }
        [Unicode(false)]
        public string? SalesOrg { get; set; }
        [Unicode(false)]
        public string? DisChannel { get; set; }
        [Unicode(false)]
        public string? Plant { get; set; }
        [Unicode(false)]
        public string? PlantName { get; set; }
        [Column("ActualGIDate")]
        [Unicode(false)]
        public string? ActualGidate { get; set; }
        [Unicode(false)]
        public string? MaterialNumber { get; set; }
        [Unicode(false)]
        public string? ShipTo { get; set; }
        [Unicode(false)]
        public string? ShipToName { get; set; }
        [Unicode(false)]
        public string? Street { get; set; }
        [Unicode(false)]
        public string? City { get; set; }
        [Unicode(false)]
        public string? Region { get; set; }
        [Unicode(false)]
        public string? PostalCode { get; set; }
    }
}
