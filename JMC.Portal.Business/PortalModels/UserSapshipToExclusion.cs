﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("UserSAPShipToExclusion")]
    public partial class UserSapshipToExclusion
    {
        [Key]
        [Column("UserID")]
        public long UserId { get; set; }
        [Key]
        [Column("SAPShipToID")]
        public long SapshipToId { get; set; }

        [ForeignKey(nameof(SapshipToId))]
        [InverseProperty("UserSapshipToExclusions")]
        public virtual SapshipTo SapshipTo { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserSapshipToExclusions")]
        public virtual User User { get; set; }
    }
}