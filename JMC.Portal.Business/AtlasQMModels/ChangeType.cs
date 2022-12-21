using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business
{
    [Table("ChangeType")]
    public partial class ChangeType
    {
        [Key]
        [Column("ChangeTypeID")]
        public int ChangeTypeId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? Name { get; set; }
        public bool? Active { get; set; }
    }
}
