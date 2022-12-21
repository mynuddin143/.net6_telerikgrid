﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("UserProfile")]
    public partial class UserProfile
    {
        [Key]
        [Column("UserProfileID")]
        public long UserProfileId { get; set; }
        [Column("UserID")]
        public long UserId { get; set; }
        [Column("ApplicationID")]
        public long ApplicationId { get; set; }
        [Required]
        [StringLength(50)]
        public string SettingName { get; set; }

        [ForeignKey(nameof(ApplicationId))]
        [InverseProperty("UserProfiles")]
        public virtual Application Application { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserProfiles")]
        public virtual User User { get; set; }
    }
}