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
    [Table("SAPSoldToSAPShipTo")]
    public partial class SapsoldToSapshipTo
    {
        [Column("SAPSoldToID")]
        public long SapsoldToId { get; set; }
        [Column("SAPShipToID")]
        public long SapshipToId { get; set; }

        [ForeignKey(nameof(SapshipToId))]
        public virtual SapshipTo SapshipTo { get; set; }
        [ForeignKey(nameof(SapsoldToId))]
        public virtual SapsoldTo SapsoldTo { get; set; }
    }
}