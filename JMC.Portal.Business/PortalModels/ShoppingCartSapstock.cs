﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("ShoppingCartSAPStock")]
    public partial class ShoppingCartSapstock
    {
        [Key]
        [Column("ShoppingCartSAPStockID")]
        public long ShoppingCartSapstockId { get; set; }
        [Column("ShoppingCartID")]
        public long ShoppingCartId { get; set; }
        [Column("SAPStockID")]
        public long SapstockId { get; set; }
        [StringLength(30)]
        public string Status { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal CartQuantity { get; set; }
        [StringLength(10)]
        public string RollDate { get; set; }
        [StringLength(250)]
        public string Note { get; set; }
        [StringLength(1)]
        public string RecordSource { get; set; }

        [ForeignKey(nameof(SapstockId))]
        [InverseProperty("ShoppingCartSapstocks")]
        public virtual Sapstock Sapstock { get; set; }
        [ForeignKey(nameof(ShoppingCartId))]
        [InverseProperty("ShoppingCartSapstocks")]
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}