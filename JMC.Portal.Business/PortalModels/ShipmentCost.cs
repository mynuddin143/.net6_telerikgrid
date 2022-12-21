﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("ShipmentCost")]
    public partial class ShipmentCost
    {
        [Key]
        [Column("ShipmentCostID")]
        public long ShipmentCostId { get; set; }
        [Column("ShipmentCostTypeID")]
        public long ShipmentCostTypeId { get; set; }
        [Column("ShipmentID")]
        public long ShipmentId { get; set; }
        [Column(TypeName = "decimal(13, 2)")]
        public decimal Amount { get; set; }

        [ForeignKey(nameof(ShipmentId))]
        [InverseProperty(nameof(Sapshipment.ShipmentCosts))]
        public virtual Sapshipment Shipment { get; set; }
        [ForeignKey(nameof(ShipmentCostTypeId))]
        [InverseProperty("ShipmentCosts")]
        public virtual ShipmentCostType ShipmentCostType { get; set; }
    }
}