﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("Rolling")]
    public partial class Rolling
    {
        [Key]
        [Column("RollingID")]
        public long RollingId { get; set; }
        [Column("QuoteMaterialID")]
        public long QuoteMaterialId { get; set; }
        [Column("PlantID")]
        public long PlantId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal PlannedQuantity { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal AllocatedQuantity { get; set; }

        [ForeignKey(nameof(PlantId))]
        [InverseProperty("Rollings")]
        public virtual Plant Plant { get; set; }
        [ForeignKey(nameof(QuoteMaterialId))]
        [InverseProperty("Rollings")]
        public virtual QuoteMaterial QuoteMaterial { get; set; }
    }
}