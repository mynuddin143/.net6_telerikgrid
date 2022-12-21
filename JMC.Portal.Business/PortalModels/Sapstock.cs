﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPStock")]
    public partial class Sapstock
    {
        public Sapstock()
        {
            ShoppingCartSapstocks = new HashSet<ShoppingCartSapstock>();
        }

        [Key]
        [Column("SAPStockID")]
        public long SapstockId { get; set; }
        [Column("SAPMaterialID")]
        public long SapmaterialId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(40)]
        public string Grade { get; set; }
        [Column("PlantID")]
        public long PlantId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TubeLength { get; set; }
        public int AvailablePieces { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal Weight { get; set; }
        [Required]
        [Column("UOM")]
        [StringLength(3)]
        public string Uom { get; set; }
        public int? Bundling1 { get; set; }
        public int? Bundling2 { get; set; }
        [Column("SAPCode")]
        [StringLength(50)]
        public string Sapcode { get; set; }
        [Column("SAPSalesOrderItemID")]
        public long? SapsalesOrderItemId { get; set; }
        public int? BundlingRound { get; set; }
        [Column("SAPSalesOrderNumber")]
        [StringLength(10)]
        public string SapsalesOrderNumber { get; set; }
        [Column("SAPSalesOrderItemNumber")]
        public int? SapsalesOrderItemNumber { get; set; }
        [Column("SAPTubeStandardID")]
        public long? SaptubeStandardId { get; set; }
        [StringLength(10)]
        public string BatchNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BatchDate { get; set; }
        [Column("SAPSpecificationID")]
        public long? SapspecificationId { get; set; }
        public bool? MaxStockLevel { get; set; }
        [Column(TypeName = "date")]
        public DateTime? RollingEndDate { get; set; }
        public int? CloseRollingPcs { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? CloseRollingWeight { get; set; }

        [ForeignKey(nameof(PlantId))]
        [InverseProperty("Sapstocks")]
        public virtual Plant Plant { get; set; }
        [ForeignKey(nameof(SapmaterialId))]
        [InverseProperty("Sapstocks")]
        public virtual Sapmaterial Sapmaterial { get; set; }
        [ForeignKey(nameof(SapsalesOrderItemId))]
        [InverseProperty("Sapstocks")]
        public virtual SapsalesOrderItem SapsalesOrderItem { get; set; }
        [ForeignKey(nameof(SapspecificationId))]
        [InverseProperty(nameof(SapcharacteristicOption.SapstockSapspecifications))]
        public virtual SapcharacteristicOption Sapspecification { get; set; }
        [ForeignKey(nameof(SaptubeStandardId))]
        [InverseProperty(nameof(SapcharacteristicOption.SapstockSaptubeStandards))]
        public virtual SapcharacteristicOption SaptubeStandard { get; set; }
        [InverseProperty(nameof(ShoppingCartSapstock.Sapstock))]
        public virtual ICollection<ShoppingCartSapstock> ShoppingCartSapstocks { get; set; }
    }
}