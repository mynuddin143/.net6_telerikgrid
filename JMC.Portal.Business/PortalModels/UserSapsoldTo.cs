// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.PortalModels
{
    [Keyless]
    [Table("UserSAPSoldTo")]
    [Microsoft.EntityFrameworkCore.Index(nameof(UserId), nameof(SapsoldToId), Name = "IX_UserSAPSoldTo", IsUnique = true)]
    public partial class UserSapsoldTo
    {
        [Column("UserID")]
        public long UserId { get; set; }
        [Column("SAPSoldToID")]
        public long SapsoldToId { get; set; }

        [ForeignKey(nameof(SapsoldToId))]
        public virtual SapsoldTo SapsoldTo { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}