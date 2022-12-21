﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Table("SAPCustomerGroupUser")]
    public partial class SapcustomerGroupUser
    {
        [Key]
        [Column("SAPCustomerGroupUserID")]
        public long SapcustomerGroupUserId { get; set; }
        [Column("SAPCustomerGroupID")]
        public long SapcustomerGroupId { get; set; }
        [Column("UserID")]
        public long UserId { get; set; }

        [ForeignKey(nameof(SapcustomerGroupId))]
        [InverseProperty("SapcustomerGroupUsers")]
        public virtual SapcustomerGroup SapcustomerGroup { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("SapcustomerGroupUsersNavigation")]
        public virtual User User { get; set; }
    }
}