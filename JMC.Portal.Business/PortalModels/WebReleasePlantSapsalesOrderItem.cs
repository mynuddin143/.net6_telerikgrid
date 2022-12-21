﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("WebReleasePlantSAPSalesOrderItem")]
    [Microsoft.EntityFrameworkCore.Index(nameof(WebReleasePlantId), nameof(SapsalesOrderItemId), Name = "IX_WebReleasePlantSAPSalesOrderItem", IsUnique = true)]
    [Microsoft.EntityFrameworkCore.Index(nameof(SapsalesOrderItemId), Name = "missing_index_24_23_WebReleasePlantSAPSalesOrderItem")]
    public partial class WebReleasePlantSapsalesOrderItem
    {
        [Key]
        [Column("WebReleasePlantSAPSalesOrderItemID")]
        public long WebReleasePlantSapsalesOrderItemId { get; set; }
        [Column("WebReleasePlantID")]
        public long WebReleasePlantId { get; set; }
        [Column("SAPSalesOrderItemID")]
        public long SapsalesOrderItemId { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Quantity { get; set; }
        public int? Pieces { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Weight { get; set; }

        [ForeignKey(nameof(SapsalesOrderItemId))]
        [InverseProperty("WebReleasePlantSapsalesOrderItems")]
        public virtual SapsalesOrderItem SapsalesOrderItem { get; set; }
        [ForeignKey(nameof(WebReleasePlantId))]
        [InverseProperty("WebReleasePlantSapsalesOrderItems")]
        public virtual WebReleasePlant WebReleasePlant { get; set; }
    }
}