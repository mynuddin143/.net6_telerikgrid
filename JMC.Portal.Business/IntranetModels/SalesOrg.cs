using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SalesOrg")]
    public partial class SalesOrg
    {
        [Key]
        [Column("SalesOrgID")]
        public int SalesOrgId { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
    }
}
