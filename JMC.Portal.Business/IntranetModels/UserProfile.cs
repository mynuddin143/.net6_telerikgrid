using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("UserProfile")]
    public partial class UserProfile
    {
        [Key]
        [Column("UserProfileID")]
        public int UserProfileID { get; set; }
        [Column("UserID")]
        public int UserID { get; set; }
        [Column("ApplicationID")]
        public int ApplicationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SettingName { get; set; } = null!;
        [StringLength(8000)]
        [Unicode(false)]
        public string? SettingValue { get; set; }

        [ForeignKey("UserID")]
        [InverseProperty("UserProfiles")]
        public virtual User User { get; set; } = null!;
    }
}
