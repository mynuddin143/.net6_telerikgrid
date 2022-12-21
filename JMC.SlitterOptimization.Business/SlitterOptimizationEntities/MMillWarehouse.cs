using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_MillWarehouse")]
    public partial class MMillWarehouse
    {
        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [StringLength(8)]
        [Unicode(false)]
        public string Dept { get; set; } = null!;
        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string Warehouse { get; set; } = null!;
    }
}
