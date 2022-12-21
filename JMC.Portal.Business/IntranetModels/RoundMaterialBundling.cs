using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("RoundMaterialBundling")]
    public partial class RoundMaterialBundling
    {
        public RoundMaterialBundling()
        {
            MaterialBundlings = new HashSet<MaterialBundling>();
        }

        [Key]
        [Column("RoundMaterialBundlingID")]
        public int RoundMaterialBundlingId { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal TubeDiameter { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal GaugeRestrictable { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TubeLengthLow { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TubeLengthHigh { get; set; }
        public int TubeBundling { get; set; }

        [InverseProperty("RoundMaterialBundling")]
        public virtual ICollection<MaterialBundling> MaterialBundlings { get; set; }
    }
}
