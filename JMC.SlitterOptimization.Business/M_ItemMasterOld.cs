using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business
{
    [Table("M_ItemMasterOld")]
    public partial class M_ItemMasterOld
    {
        [Key]
        [Column("M_ItemMasterOldID")]
        public long MItemMasterOldID { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Column("CurrentBOMCoilItem")]
        [StringLength(18)]
        [Unicode(false)]
        public string CurrentBOMCoilItem { get; set; } = null!;
        [Column("PreviousBOMCoilItem")]
        [StringLength(18)]
        [Unicode(false)]
        public string PreviousBOMCoilItem { get; set; } = null!;
    }
}
