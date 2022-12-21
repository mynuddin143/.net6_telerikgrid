﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    public partial class DefaultTubeStandard
    {
        [Key]
        [Column("DefaultID")]
        public long DefaultId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Size { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal? Size2 { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal? Diameter { get; set; }
        [Column("PlantID")]
        public long PlantId { get; set; }
        [Required]
        [StringLength(50)]
        public string GradeValue { get; set; }
    }
}