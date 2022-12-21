using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.SlitterOptimization.Business.SlitterOptimizationEntities
{
    [Table("M_ItemMasterOld")]
    public partial class MItemMasterOld
    {
        [Key]
        [Column("M_ItemMasterOldID")]
        public long MItemMasterOldId { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string Plant { get; set; } = null!;
        [Column("CurrentBOMCoilItem")]
        [StringLength(18)]
        [Unicode(false)]
        public string CurrentBomcoilItem { get; set; } = null!;
        [Column("PreviousBOMCoilItem")]
        [StringLength(18)]
        [Unicode(false)]
        public string PreviousBomcoilItem { get; set; } = null!;
    }
}
