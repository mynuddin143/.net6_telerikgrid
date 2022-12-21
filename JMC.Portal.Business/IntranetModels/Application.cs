using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("Application")]
    public partial class Application
    {
        public Application()
        {
            ApplicationRoles = new HashSet<ApplicationRole>();
        }

        [Key]
        [Column("ApplicationID")]
        public int ApplicationId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
        [Column("URL")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Url { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? LongName { get; set; }

        [InverseProperty("Application")]
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
    }
}
