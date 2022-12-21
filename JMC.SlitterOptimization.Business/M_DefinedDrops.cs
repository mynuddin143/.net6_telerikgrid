using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_DefinedDrops")]
    public partial class M_DefinedDrops
    {
        [Key]
        [Column("M_DefinedDropID")]
        public long MDefinedDropID { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string PrimaryFamily { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string SecondaryFamily { get; set; } = null!;
        public int PrimaryCuts { get; set; }
        public int SecondaryCuts { get; set; }
    }
}
