using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace JMC.Portal.Business.IntranetModel
{
    [Table("SAPEpoxycoatColor")]
    [Index("Sapcode", Name = "IX_SAPEpoxycoatColor", IsUnique = true)]
    public partial class SAPEpoxycoatColor
    {
        public SAPEpoxycoatColor()
        {
            SAPMaterials = new HashSet<SAPMaterial>();
        }

        [Key]
        [Column("SAPEpoxycoatColorID")]
        public int SAPEpoxycoatColorId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("SAPCode")]
        [StringLength(50)]
        [Unicode(false)]
        public string SAPCode { get; set; } = null!;

        [InverseProperty("SAPEpoxycoatColor")]
        public virtual ICollection<SAPMaterial> SAPMaterials { get; set; }
    }
}
