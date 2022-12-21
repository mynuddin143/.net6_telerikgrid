using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("ApplicationRole")]
    public partial class ApplicationRole
    {
        [Key]
        [Column("ApplicationRoleID")]
        public int ApplicationRoleID { get; set; }
        [Column("ApplicationID")]
        public int ApplicationID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [ForeignKey("ApplicationID")]
        [InverseProperty("ApplicationRoles")]
        public virtual Application Application { get; set; } = null!;
    }
}
