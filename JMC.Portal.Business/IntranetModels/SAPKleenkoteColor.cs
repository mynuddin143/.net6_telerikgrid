using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPKleenkoteColor")]
    [Index("Sapcode", Name = "IX_SAPKleenkoteColor", IsUnique = true)]
    public partial class SAPKleenkoteColor
    {
        public SAPKleenkoteColor()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPKleenkoteColorID")]
        public int SAPKleenkoteColorID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;

        [InverseProperty("SAPKleenkoteColor")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
