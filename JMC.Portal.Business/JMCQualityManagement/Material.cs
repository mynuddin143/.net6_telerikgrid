using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Keyless]
    public partial class Material
    {
        [Column("ID")]
        [StringLength(30)]
        [Unicode(false)]
        public string? Id { get; set; }
        [StringLength(61)]
        [Unicode(false)]
        public string? Name { get; set; }
        [StringLength(25)]
        [Unicode(false)]
        public string Number { get; set; } = null!;
    }
}
