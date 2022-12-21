using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_BandWarehouse")]
    public partial class M_BandWarehouse
    {
        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Key]
        [StringLength(4)]
        [Unicode(false)]
        public string Warehouse { get; set; } = null!;
        public int OrderLbsPerInch { get; set; }
    }
}
