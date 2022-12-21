﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Keyless]
    [Table("StockingList")]
    public partial class StockingList
    {
        [Column("SAPMaterialID")]
        public long SapmaterialId { get; set; }
        public bool? Active { get; set; }

        [ForeignKey(nameof(SapmaterialId))]
        public virtual Sapmaterial Sapmaterial { get; set; }
    }
}