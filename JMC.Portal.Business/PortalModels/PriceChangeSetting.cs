﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("PriceChangeSetting")]
    public partial class PriceChangeSetting
    {
        [Key]
        [Column("PriceChangeSettingID")]
        public long PriceChangeSettingId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? DecimalValue { get; set; }
        public int? IntegerValue { get; set; }
        [StringLength(50)]
        public string StringValue { get; set; }
        public bool? BooleanValue { get; set; }
    }
}