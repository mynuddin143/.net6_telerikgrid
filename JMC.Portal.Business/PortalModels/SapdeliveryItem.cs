// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPDeliveryItem")]
    [Microsoft.EntityFrameworkCore.Index(nameof(SapdeliveryId), nameof(Position), Name = "IX_SAPDeliveryItem", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(SapsalesOrderItemId), Name = "missing_index_26_25_SAPDeliveryItem")]
    public partial class SapdeliveryItem
    {
        [Key]
        [Column("SAPDeliveryItemID")]
        public long SapdeliveryItemId { get; set; }
        [Column("SAPDeliveryID")]
        public long SapdeliveryId { get; set; }
        public int Position { get; set; }
        [Column("SAPSalesOrderItemID")]
        public long? SapsalesOrderItemId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Weight { get; set; }
        [StringLength(3)]
        public string WeightUnit { get; set; }
        [StringLength(1)]
        public string Status { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? QuantityDelivered { get; set; }
        [StringLength(3)]
        public string SalesUnit { get; set; }

        [ForeignKey(nameof(SapdeliveryId))]
        [InverseProperty("SapdeliveryItems")]
        public virtual Sapdelivery Sapdelivery { get; set; }
        [ForeignKey(nameof(SapsalesOrderItemId))]
        [InverseProperty("SapdeliveryItems")]
        public virtual SapsalesOrderItem SapsalesOrderItem { get; set; }
    }
}