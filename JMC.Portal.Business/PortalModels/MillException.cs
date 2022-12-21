﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("MillException")]
    public partial class MillException
    {
        [Key]
        [Column("MillExceptionID")]
        public long MillExceptionId { get; set; }
        [Column("MillID")]
        public long MillId { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal? MinLength { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal? MaxLength { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal? MinSize { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal? MaxSize { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal? MinGauge { get; set; }
        [Column(TypeName = "decimal(9, 3)")]
        public decimal? MaxGauge { get; set; }
    }
}