﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("WTCPipeSize")]
    public partial class WtcpipeSize
    {
        [Key]
        [Column("RoundID")]
        public int RoundId { get; set; }
        [StringLength(50)]
        public string Size { get; set; }
        [Required]
        [StringLength(10)]
        public string ProductSize { get; set; }
    }
}