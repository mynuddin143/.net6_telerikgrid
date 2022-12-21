using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("BusinessArea")]
    public partial class BusinessArea
    {
        [Key]
        [Column("BusinessAreaID")]
        public int BusinessAreaId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
