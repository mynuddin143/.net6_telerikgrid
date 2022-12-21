using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.JMCQualityManagement
{
    [Keyless]
    public partial class Employee
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("DivisionID")]
        public long DivisionId { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string? Domain { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Username { get; set; }
    }
}
