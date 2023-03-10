// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPSalesDelivery")]
    public partial class SapsalesDelivery
    {
        [Key]
        [Column("SAPDeliveryID")]
        public long SapdeliveryId { get; set; }
        [Column("SAPStorageLocationID")]
        public long? SapstorageLocationId { get; set; }
        [StringLength(3)]
        public string WarehouseNumber { get; set; }
        [Column("SAPShipToID")]
        public long? SapshipToId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? FreightChargedToCustomer { get; set; }
        [StringLength(5)]
        public string CustomerFreightCurrency { get; set; }
        [Column("USDFreightChargedToCustomer", TypeName = "decimal(18, 4)")]
        public decimal? UsdfreightChargedToCustomer { get; set; }
        [Column("CustomerCADExchangeRateUsed", TypeName = "decimal(18, 4)")]
        public decimal? CustomerCadexchangeRateUsed { get; set; }
        [StringLength(50)]
        public string TrailorNumber { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Length { get; set; }
        [StringLength(1)]
        public string EqualizedFreight { get; set; }
        [StringLength(600)]
        public string InvoiceNumbers { get; set; }

        [ForeignKey(nameof(SapdeliveryId))]
        [InverseProperty("SapsalesDelivery")]
        public virtual Sapdelivery Sapdelivery { get; set; }
        [ForeignKey(nameof(SapshipToId))]
        [InverseProperty("SapsalesDeliveries")]
        public virtual SapshipTo SapshipTo { get; set; }
        [ForeignKey(nameof(SapstorageLocationId))]
        [InverseProperty("SapsalesDeliveries")]
        public virtual SapstorageLocation SapstorageLocation { get; set; }
    }
}