using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ChangeStatus")]
    public partial class ChangeStatus
    {
        [Key]
        [Column("ChangeStatusID")]
        public int ChangeStatusId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        public bool? Active { get; set; }
    }
}
