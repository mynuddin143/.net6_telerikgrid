using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ChangeApprover")]
    public partial class ChangeApprover
    {
        [Key]
        [Column("ChangeApproverID")]
        public int ChangeApproverId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Area { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [Column("PlantID")]
        public int? PlantId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Email { get; set; }
    }
}
