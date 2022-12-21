using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Keyless]
    [Table("UserApplicationRole")]
    public partial class UserApplicationRole
    {
        [Column("UserID")]
        public int UserID { get; set; }
        [Column("ApplicationRoleID")]
        public int ApplicationRoleID { get; set; }

        [ForeignKey("ApplicationRoleID")]
        public virtual ApplicationRole ApplicationRole { get; set; } = null!;
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
    }
}
