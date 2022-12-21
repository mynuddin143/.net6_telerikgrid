using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("RectangleMaterialBundling")]
    public partial class RectangleMaterialBundling
    {
        public RectangleMaterialBundling()
        {
            MaterialBundlings = new HashSet<MaterialBundling>();
        }

        [Key]
        [Column("RectangleMaterialBundlingID")]
        public int RectangleMaterialBundlingId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TubeSize { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TubeSize2 { get; set; }
        [Column(TypeName = "decimal(17, 3)")]
        public decimal GaugeRestrictable { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TubeLengthLow { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TubeLengthHigh { get; set; }
        public int TubeBundling1 { get; set; }
        public int TubeBundling2 { get; set; }

        [InverseProperty("RectangleMaterialBundling")]
        public virtual ICollection<MaterialBundling> MaterialBundlings { get; set; }
    }
}
