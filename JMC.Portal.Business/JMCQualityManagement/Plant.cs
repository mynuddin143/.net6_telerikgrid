using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Keyless]
    public partial class Plant
    {
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(57)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("DivisionID")]
        public long DivisionId { get; set; }
    }
}
